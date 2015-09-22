#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
using MultidiskBackup.Properties;

#endregion

namespace MultidiskBackup
{
    public partial class Form1 : Form
    {
        public Copier Copier;
        public List<Destination> Destinations = new List<Destination>();
        public List<BackupFile> FilesToBackup = new List<BackupFile>();
        public List<BackupFile> IntermediateFilesToBackup = new List<BackupFile>();
        public Logger Logger = new Logger();
        public long NumberOfDestinationsUsed = 0;
        public int RetryCount = 0;
        public long TotalAvailableSpaceOnDestination = 0;
        public long TotalScheduledSizeOfFilesToBackUp = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void setup()
        {
            Invoke(new Action(() =>
                                  {
                                      Logger.AppendLine("Resetting variables");
                                      FilesToBackup = new List<BackupFile>();
                                      foreach (var dest in Destinations)
                                      {
                                          dest.Used = false;
                                          dest.RecalculateInitialUsedSpace();
                                          dest.ScheduledSizeOfFilesToBackup = 0;
                                          dest.FilesCopiedHere = 0;
                                      }
                                      TotalScheduledSizeOfFilesToBackUp = 0;
                                      TotalAvailableSpaceOnDestination = 0;
                                      NumberOfDestinationsUsed = 0;
                                      capacityWarningLabel.Visible = false;
                                      sufficientCapacityLabel.Visible = false;
                                      statusTextBox.Text = string.Empty;
                                      while (!chart1.Series[0].Name.Equals("To be determined"))
                                          chart1.Series.RemoveAt(0);
                                  }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.LogChanged += Logger_LogChanged;
            Logger.AppendLine("Application start");
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Logger.AppendLine("{0} version {1}", fvi.ProductName, fvi.ProductVersion);
            Logger.AppendLine("{0}", fvi.LegalCopyright);
            if (File.Exists("savedoperation.dat"))
            {
                Logger.AppendLine("Found saved backup operation");
                try
                {
                    if (MessageBox.Show(
                        Resources.Can_load_files,
                        Resources.Saved_operation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Logger.AppendLine("User wants to load saved backup");
                        var bf = new BinaryFormatter();
                        using (
                            Stream s = new FileStream("savedoperation.dat", FileMode.Open, FileAccess.Read,
                                                      FileShare.Read))
                        {
                            IntermediateFilesToBackup = (List<BackupFile>) bf.Deserialize(s);
                            listBox1.Items.Clear();
                            listBox1.Items.Add(string.Format("<{0} skipped files from last backup>",
                                                             IntermediateFilesToBackup.Count));
                            Destinations.Clear();
                            listBox2.Items.Clear();
                        }
                        File.Delete("savedoperation.dat");
                    }
                    else
                    {
                        Logger.AppendLine("User does not want to load saved backup");
                        File.Delete("savedoperation.dat");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(
                        string.Format(
                            "Skipped files from the last backup have been saved, but the application is unable to load them. The error is {0}",
                            exc.Message),
                        Resources.Error);
                    Logger.AppendLine("Error when attempting to load file");
                }
                finally
                {
                    Destinations.Clear();
                    listBox2.Items.Clear();
                }
            }
        }

        private void Logger_LogChanged(object sender, LogChangedEventArgs e)
        {
            Invoke(new Action(() =>
                                  {
                                      logTextBox.AppendText(e.Msg);
                                      logTextBox.AppendText(Environment.NewLine);
                                  }));
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(folderBrowserDialog1.SelectedPath);
            }
            Logger.AppendLine("User added {0} to sources", folderBrowserDialog1.SelectedPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var dest = new Destination(folderBrowserDialog1.SelectedPath);
                //var dest = new Destination(folderBrowserDialog1.SelectedPath,
                //long.Parse(Interaction.InputBox("Free space")), long.Parse(Interaction.InputBox("Total space")));
                listBox2.Items.Add(string.Format("{0} ({1})", folderBrowserDialog1.SelectedPath,
                                                 HelperFunctions.UserFriendlySize(dest.SafeFreeSpace)));
                Destinations.Add(dest);
                Logger.AppendLine("User added {0} to destinations", folderBrowserDialog1.SelectedPath);
            }
        }

        private void RecalculateDestinationFreeSpaceInUI()
        {
            Logger.AppendLine("Recalculating free space");
            listBox2.Items.Clear();
            foreach (var dest in Destinations)
            {
                listBox2.Items.Add(string.Format("{0} ({1})", folderBrowserDialog1.SelectedPath,
                                                 HelperFunctions.UserFriendlySize(dest.SafeFreeSpace)));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            Logger.AppendLine("Removed {0} from sources", listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0) return;
            Destinations.RemoveAt(listBox2.SelectedIndex);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            Logger.AppendLine("Removed {0} from destinations", listBox2.SelectedIndex);
        }

        private void simulate_and_copy(object o, DoWorkEventArgs v)
        {
            var bw = o as BackgroundWorker;
            Action cancelFunction = () =>
                                        {
                                            if (!v.Cancel)
                                            {
                                                Logger.AppendLine("Operation was canceled");
                                                MessageBox.Show(Resources.Cancelled);
                                                var skippedFiles =
                                                    FilesToBackup.FindAll(s => s.ProblemFile || !s.HasBeenCopied);
                                                Logger.AppendLine(
                                                    "Some files could not be backed up because the operation was cancelled: ");
                                                if (skippedFiles.Count != 0)
                                                {
                                                    var result =
                                                        MessageBox.Show(
                                                            string.Format(
                                                                "{0} of {1} files were backed up before operation was cancelled. Press [Yes] to resume",
                                                                FilesToBackup.FindAll(s => s.HasBeenCopied).Count,
                                                                FilesToBackup.Count), Resources.Resume_backup,
                                                            MessageBoxButtons.YesNo);
                                                    Logger.AppendLine(
                                                        "{0} of {1} files were backed up before operation was cancelled",
                                                        FilesToBackup.FindAll(s => s.HasBeenCopied).Count,
                                                        FilesToBackup.Count);
                                                    var sb = new StringBuilder();
                                                    for (var index = 0; index < skippedFiles.Count; index++)
                                                    {
                                                        var x = skippedFiles[index];
                                                        sb.AppendLine(string.Format("({0}) {1}", index + 1,
                                                                                    HelperFunctions.ShortenFileNamePath(
                                                                                        x.Source)));
                                                    }
                                                    Logger.AppendBigLine(sb.ToString());
                                                    if (result == DialogResult.Yes)
                                                    {
                                                        IntermediateFilesToBackup = skippedFiles;
                                                        IntermediateFilesToBackup.ForEach(s =>
                                                                                              {
                                                                                                  s.ProblemFile = false;
                                                                                                  s.HasBeenCopied =
                                                                                                      false;
                                                                                              });
                                                        Invoke(new Action(() =>
                                                                              {
                                                                                  listBox1.Items.Clear();
                                                                                  listBox1.Items.Add(
                                                                                      string.Format(
                                                                                          "<{0} skipped files from last backup>",
                                                                                          IntermediateFilesToBackup.
                                                                                              Count));
                                                                                  RecalculateDestinationFreeSpaceInUI();
                                                                              }));
                                                        Logger.AppendLine("Files have been preloaded into sources");
                                                        MessageBox.Show(
                                                            string.Format(
                                                                "The files which could not be backed up have been preloaded into the Sources box."));
                                                    }
                                                }
                                                setup();
                                            }
                                            v.Cancel = true;
                                        };
            if (bw == null) return;
            Invoke(new Action(() => tabControl1.SelectedTab = tabPage3));
            bw.ReportProgress(10);
            // Enumerate all files
            StatusWriteLine("Enumerating files...");
            StatusBarWrite("Enumerating files (0 found)");
            Logger.AppendLine("Enumerating files");
            foreach (string item in listBox1.Items)
            {
                if (item.Contains(" skipped files from last backup>"))
                {
                    FilesToBackup.AddRange(IntermediateFilesToBackup);
                    Logger.AppendLine("Loading skipped files");
                    IntermediateFilesToBackup.Clear();
                }
                else if (File.Exists(item))
                    FilesToBackup.Add(new BackupFile(item));
                else if (Directory.Exists(item))
                    FilesToBackup.AddRange(HelperFunctions.GetAllFilesRecursively(item,
                                                                                  a =>
                                                                                  StatusBarWrite("Enumerating files (" +
                                                                                                 (FilesToBackup.Count +
                                                                                                  a) + " found)"),
                                                                                  () =>
                                                                                      {
                                                                                          if (bw.CancellationPending)
                                                                                          {
                                                                                              cancelFunction();
                                                                                              return true;
                                                                                          }
                                                                                          return false;
                                                                                      }));
                else
                    StatusWriteLine(string.Format("ERROR: {0} is not a directory nor file!", item));
                if (bw.CancellationPending)
                {
                    cancelFunction();
                    return;
                }
            }

            // Find largest file
            StatusWriteLine("Finding largest file...");
            StatusBarWrite("Finding largest file...");
            Logger.AppendLine("Finding largest file...");
            var largestFile = string.Empty;
            long largestFileSize = 0;
            for (var index = 0; index < FilesToBackup.Count; index++)
            {
                var file = FilesToBackup[index];
                if (file.Size > largestFileSize)
                {
                    largestFileSize = file.Size;
                    largestFile = file.Source;
                }
                bw.ReportProgress((int) (15 + (double) index/FilesToBackup.Count*(40 - 15)));
                if (bw.CancellationPending)
                {
                    cancelFunction();
                    return;
                }
            }
            StatusWriteLine("Largest file: " + HelperFunctions.ShortenFileNamePath(largestFile));
            StatusWriteLine("Size: " + HelperFunctions.UserFriendlySize(largestFileSize));
            Logger.AppendLine("Largest file: {0}", HelperFunctions.ShortenFileNamePath(largestFile));
            Logger.AppendLine("Size: {0}", HelperFunctions.UserFriendlySize(largestFileSize));


            bw.ReportProgress(40);

            // Simulate
            Logger.AppendLine("Beginning simulation");
            var simulatedDestinations = new Tuple<Destination, long>[Destinations.Count];
                //the destination and the space used
            TotalAvailableSpaceOnDestination = 0;
            for (var i = 0; i < Destinations.Count; i++)
            {
                simulatedDestinations[i] = new Tuple<Destination, long>(Destinations[i], 0);
                TotalAvailableSpaceOnDestination += Destinations[i].SafeFreeSpace;
                if (bw.CancellationPending)
                {
                    cancelFunction();
                    return;
                }
                Logger.AppendLine("Populating safe free space");
            }
            StatusBarWrite("Simulating backup...");
            long totalSize = 0;
            long sizeOfProblemFiles = 0;
            var problemFiles = new List<BackupFile>();
            for (var index = 0; index < FilesToBackup.Count; index++)
            {
                var file = FilesToBackup[index];
                bw.ReportProgress((int) (40 + (double) index/FilesToBackup.Count*60));
                // Find smallest destination with enough free space to accomodate this file
                var smallestFreeSpace = long.MaxValue;
                var smallestDestinationIndex = -1;
                var sizeOfThisFile = file.Size;
                totalSize += sizeOfThisFile;
                for (var i = 0; i < Destinations.Count; i++)
                {
                    var dest = simulatedDestinations[i].Item1;
                    var freeSpace = dest.SafeFreeSpace - simulatedDestinations[i].Item2;
                    if (freeSpace <= sizeOfThisFile) continue;
                    if (freeSpace < smallestFreeSpace)
                    {
                        smallestFreeSpace = freeSpace;
                        smallestDestinationIndex = i;
                    }
                }
                if (smallestDestinationIndex >= 0)
                {
                    var singleBasePath = Path.GetDirectoryName(file.BasePath) ?? string.Empty;
                    var relativePath =
                        file.Source.Replace(singleBasePath, string.Empty).Trim(Path.DirectorySeparatorChar);
                    file.Destination = simulatedDestinations[smallestDestinationIndex].Item1;
                    file.DestinationPath = Path.Combine(simulatedDestinations[smallestDestinationIndex].Item1.Path,
                                                        relativePath);
                    // Update free space
                    simulatedDestinations[smallestDestinationIndex] =
                        new Tuple<Destination, long>(simulatedDestinations[smallestDestinationIndex].Item1,
                                                     simulatedDestinations[smallestDestinationIndex].Item2 +
                                                     sizeOfThisFile);
                    file.ProblemFile = false;
                    TotalScheduledSizeOfFilesToBackUp += sizeOfThisFile;
                }
                else
                {
                    file.ProblemFile = true;
                    sizeOfProblemFiles += sizeOfThisFile;
                    problemFiles.Add(file);
                }
                if (!bw.CancellationPending) continue;
                cancelFunction();
                return;
            }
            Logger.AppendLine("Sorting files by destination");
            FilesToBackup.Sort(
                (p1, p2) => Destinations.IndexOf(p1.Destination) - Destinations.IndexOf(p2.Destination)
                );
            StatusWriteLine("Simulation complete");
            StatusBarWrite("Simulation complete");
            Logger.AppendLine("Simulation complete");
            DialogResult dialogResult;
            if (problemFiles.Count > 0)
            {
                if (checkBox3.Checked)
                {
                    StatusWriteLine("Files that will not fit: ");
                    Logger.AppendLine("Files that will not fit: ");
                    var sb = new StringBuilder();
                    for (var index = 0; index < problemFiles.Count; index++)
                    {
                        var file = problemFiles[index];
                        StatusWriteLine(HelperFunctions.ShortenFileNamePath(file.Source) + ": " +
                                        HelperFunctions.UserFriendlySize(file.Size));
                        sb.AppendLine(string.Format("[{0}] {1} ({2})", index + 1,
                                                    HelperFunctions.ShortenFileNamePath(file.Source),
                                                    HelperFunctions.UserFriendlySize(file.Size)));
                        if (bw.CancellationPending)
                        {
                            cancelFunction();
                            return;
                        }
                    }
                    Logger.AppendBigLine(sb.ToString());
                }

                StatusWriteLine(string.Format("You need {0} more space",
                                              HelperFunctions.UserFriendlySize(sizeOfProblemFiles)));
                dialogResult = (DialogResult) Invoke(new Func<DialogResult>(() =>
                                                                                {
                                                                                    sufficientCapacityLabel.Visible =
                                                                                        false;
                                                                                    capacityWarningLabel.Text =
                                                                                        string.Format(
                                                                                            "Additional {0} needed",
                                                                                            HelperFunctions.
                                                                                                UserFriendlySize(
                                                                                                    sizeOfProblemFiles));
                                                                                    capacityWarningLabel.Visible = true;
                                                                                    if (!checkBox1.Checked)
                                                                                    {
                                                                                        return
                                                                                            MessageBox.Show(
                                                                                                string.Format(
                                                                                                    "Simulation complete. Not all files will be able to be backed up. \nYou need {0}  more space. Continue anyway?",
                                                                                                    HelperFunctions.
                                                                                                        UserFriendlySize
                                                                                                        (sizeOfProblemFiles)),
                                                                                                Resources.
                                                                                                    Continue_to_back_up,
                                                                                                MessageBoxButtons.YesNo);
                                                                                    }
                                                                                    return DialogResult.Yes;
                                                                                }));
                Logger.AppendLine("Not all files will be able to be backed up");
                Logger.AppendLine("Additional {0} needed", HelperFunctions.UserFriendlySize(sizeOfProblemFiles));
            }
            else
            {
                dialogResult = (DialogResult) Invoke(new Func<DialogResult>(() =>
                                                                                {
                                                                                    sufficientCapacityLabel.Visible =
                                                                                        true;
                                                                                    if (!checkBox1.Checked)
                                                                                    {
                                                                                        return
                                                                                            MessageBox.Show(
                                                                                                Resources.
                                                                                                    All_files_will_be_able_to_be_backed_up,
                                                                                                Resources.
                                                                                                    Continue_to_back_up,
                                                                                                MessageBoxButtons.YesNo);
                                                                                    }
                                                                                    return DialogResult.Yes;
                                                                                }));
                Logger.AppendLine("All files will be able to be backed up");
            }
            bw.ReportProgress(100);
            if (dialogResult != DialogResult.Yes) return;
            Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
            Logger.AppendLine("Total available space: {0}",
                              HelperFunctions.UserFriendlySize(TotalAvailableSpaceOnDestination));
            Logger.AppendLine("Total size of files able to be backed up: {0}",
                              HelperFunctions.UserFriendlySize(TotalScheduledSizeOfFilesToBackUp));
            StatusWriteLine("You currently have " + HelperFunctions.UserFriendlySize(TotalAvailableSpaceOnDestination) +
                            " of space");
            StatusWriteLine("The total size of files able to be backed up is " +
                            HelperFunctions.UserFriendlySize(TotalScheduledSizeOfFilesToBackUp));
            if (problemFiles.Count > 0)
            {
                StatusWriteLine("The total size of files selected to be backed up is " +
                                HelperFunctions.UserFriendlySize(totalSize));
                Logger.AppendLine("The total size of files selected to be backed up is {0}",
                                  HelperFunctions.UserFriendlySize(totalSize));
            }


            StatusBarWrite(string.Empty);
            if (bw.CancellationPending)
            {
                cancelFunction();
                return;
            }
            Logger.AppendLine("Starting stopwatches");
            Notify("Starting backup");
            var sw = new Stopwatch();
            var sw2 = new Stopwatch();
            Logger.AppendLine("Setting counter variables to 0");
            long cBytesCopiedSoFar = 0;
            var cFilesCopiedSoFar = 0;
            Logger.AppendLine("Counting files to be copied to each destination");
            foreach (var d in Destinations)
            {
                d.ScheduledSizeOfFilesToBackup = FilesToBackup.Sum(b =>
                                                                   Equals(b.Destination,
                                                                          d)
                                                                       ? b.Size
                                                                       : 0);
                Logger.AppendLine("Destination {0} will have {1} of files", d.Path,
                                  HelperFunctions.UserFriendlySize(d.ScheduledSizeOfFilesToBackup));
            }
            var updateAction = new Action<int, long, int>((index, bytesCopiedSoFar, filesCopiedSoFar) =>
                                                              {
                                                                  if (TotalScheduledSizeOfFilesToBackUp != 0)
                                                                  {
                                                                      if (bytesCopiedSoFar >=
                                                                          TotalScheduledSizeOfFilesToBackUp)
                                                                      {
                                                                          overallProgressBar.Value = 100;
                                                                          overallProgressPercentageLabel.Text =
                                                                              string.Format("Progress {0:0.00}%", 100);
                                                                      }
                                                                      else
                                                                      {
                                                                          overallProgressBar.Value =
                                                                              (int)
                                                                              ((double) bytesCopiedSoFar/
                                                                               TotalScheduledSizeOfFilesToBackUp*100);
                                                                          overallProgressPercentageLabel.Text =
                                                                              string.Format("Progress {0:0.00}%",
                                                                                            ((double) bytesCopiedSoFar/
                                                                                             TotalScheduledSizeOfFilesToBackUp*
                                                                                             100));
                                                                      }
                                                                  }
                                                                  if (FilesToBackup.Count != 0)
                                                                  {
                                                                      copiedNFilesOutOfTotalProgressBar.Value =
                                                                          (int)
                                                                          ((double) filesCopiedSoFar/FilesToBackup.Count*
                                                                           100);
                                                                  }
                                                                  if (Destinations.Count != 0)
                                                                  {
                                                                      if (index < FilesToBackup.Count)
                                                                      {
                                                                          destinationsUsedProgressBar.Value =
                                                                              (int)
                                                                              (((double)
                                                                                FilesToBackup[index].Destination.
                                                                                    UsedSpace/
                                                                                FilesToBackup[index].Destination.
                                                                                    TotalSpace)*100.0);
                                                                          destinationsUsedPercentageLabel.Text =
                                                                              string.Format("Used: {0:0.00}%",
                                                                                            ((double)
                                                                                             FilesToBackup[index].
                                                                                                 Destination.UsedSpace/
                                                                                             FilesToBackup[index].
                                                                                                 Destination.TotalSpace)*
                                                                                            100.0);
                                                                          purpleInitialUsedProgressBar.Value = (int)
                                                                                                               (((double
                                                                                                                 )
                                                                                                                 FilesToBackup
                                                                                                                     [
                                                                                                                         index
                                                                                                                     ].
                                                                                                                     Destination
                                                                                                                     .
                                                                                                                     InitialUsedSpace/
                                                                                                                 FilesToBackup
                                                                                                                     [
                                                                                                                         index
                                                                                                                     ].
                                                                                                                     Destination
                                                                                                                     .
                                                                                                                     TotalSpace)*
                                                                                                                100);
                                                                          if (sw2.Elapsed.Ticks > 0 &&
                                                                              FilesToBackup[index].Destination.UsedSpace -
                                                                              FilesToBackup[index].Destination.
                                                                                  InitialUsedSpace > 0)
                                                                          {
                                                                              var deltaUsed =
                                                                                  FilesToBackup[index].Destination.
                                                                                      UsedSpace -
                                                                                  FilesToBackup[index].Destination.
                                                                                      InitialUsedSpace;
                                                                              var destEta =
                                                                                  HelperFunctions.GetEstimatedTime(
                                                                                      sw2.Elapsed.Ticks, deltaUsed,
                                                                                      FilesToBackup[index].Destination.
                                                                                          ScheduledSizeOfFilesToBackup);
                                                                              if (destEta.TotalMilliseconds < 0)
                                                                                  destEta = TimeSpan.FromSeconds(0);
                                                                              estimatedCompletionCurrentDestinationLabel
                                                                                  .Text =
                                                                                  string.Format(
                                                                                      "Estimated completion: {0:D2}h:{1:D2}m:{2:D2}s",
                                                                                      destEta.Hours,
                                                                                      destEta.Minutes, destEta.Seconds);
                                                                          }
                                                                      }


                                                                      if (FilesToBackup.Count > 0)
                                                                      {
                                                                          var filesNotCopiedYet = FilesToBackup.Count;
                                                                          foreach (var t1 in Destinations)
                                                                          {
                                                                              var proportion =
                                                                                  (double) t1.FilesCopiedHere/
                                                                                  FilesToBackup.Count*100;
                                                                              var s = new Series(t1.Path)
                                                                                          {
                                                                                              ChartArea = "ChartArea1",
                                                                                              ChartType =
                                                                                                  SeriesChartType.
                                                                                                  StackedBar100,
                                                                                              ToolTip = t1.Path,
                                                                                              Legend = "Legend1",
                                                                                              Label =
                                                                                                  (proportion < 5)
                                                                                                      ? ""
                                                                                                      : string.Format(
                                                                                                          "{0:0.00}%",
                                                                                                          proportion)
                                                                                          };
                                                                              filesNotCopiedYet -= t1.FilesCopiedHere;
                                                                              if (s.Points.Count > 0)
                                                                                  s.Points[0] = new DataPoint(2,
                                                                                                              proportion);
                                                                              else
                                                                                  s.Points.AddXY(2, proportion);


                                                                              var t2 = t1;
                                                                              chart1.Invoke(new Action(() =>
                                                                                                           {
                                                                                                               var j =
                                                                                                                   chart1
                                                                                                                       .
                                                                                                                       Series
                                                                                                                       .
                                                                                                                       IndexOf
                                                                                                                       (t2
                                                                                                                            .
                                                                                                                            Path);
                                                                                                               var k =
                                                                                                                   chart1
                                                                                                                       .
                                                                                                                       Series
                                                                                                                       .
                                                                                                                       IndexOf
                                                                                                                       ("To be determined");
                                                                                                               if (j < 0)
                                                                                                               {
                                                                                                                   chart1
                                                                                                                       .
                                                                                                                       Series
                                                                                                                       .
                                                                                                                       Insert
                                                                                                                       (k,
                                                                                                                        s);
                                                                                                               }
                                                                                                               else
                                                                                                                   chart1
                                                                                                                       .
                                                                                                                       Series
                                                                                                                       [
                                                                                                                           j
                                                                                                                       ]
                                                                                                                       =
                                                                                                                       s;
                                                                                                           }));
                                                                          }
                                                                          var proportion1 = (double) filesNotCopiedYet/
                                                                                            FilesToBackup.Count*100;
                                                                          var t = new Series("To be determined")
                                                                                      {
                                                                                          ChartArea = "ChartArea1",
                                                                                          ChartType =
                                                                                              SeriesChartType.
                                                                                              StackedBar100,
                                                                                          ToolTip = "To be determined",
                                                                                          Legend = "Legend1",
                                                                                          Label =
                                                                                              (proportion1 < 5)
                                                                                                  ? ""
                                                                                                  : string.Format(
                                                                                                      "{0:0.00}%",
                                                                                                      proportion1)
                                                                                      };
                                                                          if (t.Points.Count > 0)
                                                                              t.Points[0] = new DataPoint(2, proportion1);
                                                                          else
                                                                              t.Points.AddXY(2, proportion1);
                                                                          chart1.Invoke(new Action(() =>
                                                                                                       {
                                                                                                           var j =
                                                                                                               chart1.
                                                                                                                   Series
                                                                                                                   .
                                                                                                                   IndexOf
                                                                                                                   ("To be determined");
                                                                                                           if (j < 0)
                                                                                                               chart1.
                                                                                                                   Series
                                                                                                                   .Add(
                                                                                                                       t);
                                                                                                           else
                                                                                                               chart1.
                                                                                                                   Series
                                                                                                                   [j] =
                                                                                                                   t;
                                                                                                           chart1.Update
                                                                                                               ();
                                                                                                           chart1.
                                                                                                               Refresh();
                                                                                                       }));
                                                                      }
                                                                  }
                                                                  copiedNBytesSoFar.Text =
                                                                      string.Format("{0} of {1} copied",
                                                                                    HelperFunctions.
                                                                                        UserFriendlySize(
                                                                                            bytesCopiedSoFar),
                                                                                    HelperFunctions.
                                                                                        UserFriendlySize(
                                                                                            TotalScheduledSizeOfFilesToBackUp));

                                                                  destinationsUsedFractionLabel.Text =
                                                                      string.Format(
                                                                          "Destinations used: {0} of {1} destinations",
                                                                          Destinations.FindAll(
                                                                              s => s.Used).Count, Destinations.Count);
                                                                  copiedNFilesOutOfTotal.Text =
                                                                      string.Format("Copied {0} of {1} files",
                                                                                    filesCopiedSoFar,
                                                                                    FilesToBackup.Count);
                                                                  if (index < FilesToBackup.Count)
                                                                      Logger.AppendLine("Copied {0} -> {1} ({2})",
                                                                                        HelperFunctions.
                                                                                            ShortenFileNamePath(
                                                                                                FilesToBackup[index].
                                                                                                    Source),
                                                                                        HelperFunctions.
                                                                                            ShortenFileNamePath(
                                                                                                FilesToBackup[index].
                                                                                                    DestinationPath),
                                                                                        HelperFunctions.UserFriendlySize
                                                                                            (FilesToBackup[index].Size));

                                                                  currentFileNameLabel.Text =
                                                                      HelperFunctions.ShortenFileNamePath((index) >=
                                                                                                          FilesToBackup.
                                                                                                              Count
                                                                                                              ? string.
                                                                                                                    Empty
                                                                                                              : string.
                                                                                                                    Format
                                                                                                                    ("{0} ({1})",
                                                                                                                     FilesToBackup
                                                                                                                         [
                                                                                                                             index
                                                                                                                         ]
                                                                                                                         .
                                                                                                                         Source,
                                                                                                                     HelperFunctions
                                                                                                                         .
                                                                                                                         UserFriendlySize
                                                                                                                         (FilesToBackup
                                                                                                                              [
                                                                                                                                  index
                                                                                                                              ]
                                                                                                                              .
                                                                                                                              Size)));
                                                                  queuedFileNameLabel.Text = (index + 1) >=
                                                                                             FilesToBackup.Count
                                                                                                 ? string.Empty
                                                                                                 : (
                                                                                                       FilesToBackup[
                                                                                                           index + 1].
                                                                                                           ProblemFile
                                                                                                           ? string.
                                                                                                                 Format(
                                                                                                                     "{0} ({1}) (skip)",
                                                                                                                     HelperFunctions
                                                                                                                         .
                                                                                                                         ShortenFileNamePath
                                                                                                                         (
                                                                                                                             FilesToBackup
                                                                                                                                 [
                                                                                                                                     index +
                                                                                                                                     1
                                                                                                                                 ]
                                                                                                                                 .
                                                                                                                                 Source),
                                                                                                                     HelperFunctions
                                                                                                                         .
                                                                                                                         UserFriendlySize
                                                                                                                         (FilesToBackup
                                                                                                                              [
                                                                                                                                  index +
                                                                                                                                  1
                                                                                                                              ]
                                                                                                                              .
                                                                                                                              Size))
                                                                                                           : string.
                                                                                                                 Format(
                                                                                                                     "{0} ({1})",
                                                                                                                     HelperFunctions
                                                                                                                         .
                                                                                                                         ShortenFileNamePath
                                                                                                                         (
                                                                                                                             FilesToBackup
                                                                                                                                 [
                                                                                                                                     index +
                                                                                                                                     1
                                                                                                                                 ]
                                                                                                                                 .
                                                                                                                                 Source),
                                                                                                                     HelperFunctions
                                                                                                                         .
                                                                                                                         UserFriendlySize
                                                                                                                         (FilesToBackup
                                                                                                                              [
                                                                                                                                  index +
                                                                                                                                  1
                                                                                                                              ]
                                                                                                                              .
                                                                                                                              Size))
                                                                                                   );
                                                                  currentDestinationLabel.Text =
                                                                      string.Format("Destination: {0}",
                                                                                    (index) >=
                                                                                    FilesToBackup.Count
                                                                                        ? string.Empty
                                                                                        : FilesToBackup[index
                                                                                              ].Destination.
                                                                                              Path);
                                                                  currentDestinationSizeLabel.Text =
                                                                      string.Format("Free space: {0}",
                                                                                    (index) >=
                                                                                    FilesToBackup.Count
                                                                                        ? string.Empty
                                                                                        : HelperFunctions
                                                                                              .
                                                                                              UserFriendlySize
                                                                                              (FilesToBackup
                                                                                                   [index
                                                                                                   ].
                                                                                                   Destination
                                                                                                   .
                                                                                                   FreeSpace));
                                                                  if (sw.Elapsed.Ticks <= 0 || bytesCopiedSoFar == 0)
                                                                      return;
                                                                  var eta =
                                                                      HelperFunctions.GetEstimatedTime(
                                                                          sw.Elapsed.Ticks, bytesCopiedSoFar,
                                                                          TotalScheduledSizeOfFilesToBackUp);
                                                                  if (eta.TotalMilliseconds < 0)
                                                                      eta = TimeSpan.FromSeconds(0);

                                                                  estimatedTimeLeftLabel.Text =
                                                                      string.Format(
                                                                          "Estimated completion: {0:D2}h:{1:D2}m:{2:D2}s",
                                                                          eta.Hours,
                                                                          eta.Minutes, eta.Seconds);
                                                                  timeElapsedLabel.Text =
                                                                      string.Format(
                                                                          "Time elapsed: {0:D2}h:{1:D2}m:{2:D2}s",
                                                                          sw.Elapsed.Hours,
                                                                          sw.Elapsed.Minutes, sw.Elapsed.Seconds);
                                                              });

            sw.Start();
            sw2.Start();
            for (var index = 0; index < FilesToBackup.Count; index++)
            {
                if (bw.CancellationPending)
                {
                    cancelFunction();
                    return;
                }
                if (FilesToBackup[index].ProblemFile) continue;
                Invoke(updateAction, index, cBytesCopiedSoFar, cFilesCopiedSoFar);
                var file = FilesToBackup[index];
                if (!file.Destination.Used) sw2.Restart();
                file.Destination.Used = true;

                Copier = new Copier(file);
                Copier.ProgressChanged += DownloadProgressChanged;
                Copier.Cancelled +=
                    () =>
                    Invoke(updateAction, FilesToBackup.Count, FilesToBackup.Sum(s => s.HasBeenCopied ? s.Size : 0),
                           FilesToBackup.Sum(s => s.HasBeenCopied ? 1 : 0)); //Update UI after cancellation
                Copier.Error += x =>
                                    {
                                        StatusWriteLine(string.Format("Error copying {0}",
                                                                      HelperFunctions.ShortenFileNamePath(file.Source)));
                                        Logger.AppendLine("Error copying {0}",
                                                          HelperFunctions.ShortenFileNamePath(file.Source));
                                    };
                Copier.Copy().RunSynchronously();
                if (file.HasBeenCopied)
                {
                    if (checkBox2.Checked)
                    {
                        Copier.CopyTimestamps();
                        Logger.AppendLine("Timestamps set");
                    }
                    cFilesCopiedSoFar++;
                    cBytesCopiedSoFar += file.Size;
                    file.Destination.FilesCopiedHere++;
                }
            }
            sw.Stop();

            Invoke(updateAction, FilesToBackup.Count, cBytesCopiedSoFar, cFilesCopiedSoFar);
            problemFiles = FilesToBackup.FindAll(s => s.ProblemFile || !s.HasBeenCopied);
            if (problemFiles.Count != 0)
            {
                var result = MessageBox.Show(Resources.Some_files_could_not_be_backed_up, Resources.Try_again,
                                             MessageBoxButtons.YesNo);
                Logger.AppendLine(Resources.Some_files_could_not_be_backed_up_short);
                var sb = new StringBuilder();
                for (var index = 0; index < problemFiles.Count; index++)
                {
                    var x = problemFiles[index];
                    sb.AppendLine(string.Format("({0}) {1}", index + 1, HelperFunctions.ShortenFileNamePath(x.Source)));
                }
                Logger.AppendBigLine(sb.ToString());
                if (result == DialogResult.Yes)
                {
                    IntermediateFilesToBackup = problemFiles;
                    IntermediateFilesToBackup.ForEach(s => s.ProblemFile = false);
                    Invoke(new Action(() =>
                                          {
                                              listBox1.Items.Clear();
                                              listBox1.Items.Add(string.Format("<{0} skipped files from last backup>",
                                                                               IntermediateFilesToBackup.Count));
                                              RecalculateDestinationFreeSpaceInUI();
                                          }));

                    MessageBox.Show(
                        string.Format(
                            "The files which could not be backed up have been preloaded into the Sources box. Please add at least {0} of free space",
                            HelperFunctions.UserFriendlySize(problemFiles.Sum(s => s.Size))));
                    Invoke(new Action(() => tabControl1.SelectedTab = tabPage2));
                    Logger.AppendLine("Prompted user to add at least {0} of free space",
                                      HelperFunctions.UserFriendlySize(problemFiles.Sum(s => s.Size)));
                }
            }
            else
            {
                Notify(Resources.Backup_Complete);
                MessageBox.Show(Resources.Backup_Complete);
                Logger.AppendLine(Resources.Backup_Complete);
            }
        }

        private void update_simulation_progress_bar(object o, ProgressChangedEventArgs v)
        {
            toolStripProgressBar1.Value = v.ProgressPercentage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setup();
            Logger.AppendLine("Creating new thread");
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            else
                backgroundWorker1.RunWorkerAsync();
        }

        private void DownloadProgressChanged(int value)
        {
            Invoke(new Action(() => currentFileProgressBar.Value = value));
        }

        private void StatusBarWrite(object obj)
        {
            Invoke(new Action(() => toolStripStatusLabel1.Text = obj.ToString()));
        }

        private void StatusWriteLine(object obj)
        {
            Invoke(new Action(() => statusTextBox.AppendText(obj.ToString())));
            Invoke(new Action(() => statusTextBox.AppendText(Environment.NewLine)));
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void destinationsUsedFractionLabel_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void destinationsUsedProgressBar_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            if (Copier != null)
                Copier.Cancel();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void Notify(string format, params object[] args)
        {
            Invoke(
                new Action(
                    () =>
                    notifyIcon1.ShowBalloonTip(3000, "Multidisk Backup", string.Format(format, args), ToolTipIcon.Info)));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IntermediateFilesToBackup.Count != 0)
            {
                var bf = new BinaryFormatter();
                try
                {
                    Stream fileStream = File.Create("savedoperation.dat");
                    bf.Serialize(fileStream, IntermediateFilesToBackup);
                    fileStream.Close();
                    MessageBox.Show(
                        Resources.OnClosing_Notification);
                    Logger.AppendLine("Skipped files have been saved");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(
                        string.Format(
                            "There are still files from a previous operation that have not been backed up yet. An attempt to save these files was made but was unsuccessful. The error is: {0}",
                            exc.Message));
                }
                Logger.AppendLine("Application end");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem_Click(object sender,
                                                                                                 EventArgs e)
        {
            MessageBox.Show(Resources.Help_1);
        }

        private void saveLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var noerror = true;
            noerror &= Logger.Save();
            noerror = Destinations.Aggregate(noerror, (current, d) => current & Logger.SaveTo(d.Path));
            MessageBox.Show(noerror ? Resources.Log_files_saved : Resources.Error_when_saving_log_files);
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
        }
    }

    public static class HelperFunctions
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
                                                     out ulong lpFreeBytesAvailable,
                                                     out ulong lpTotalNumberOfBytes,
                                                     out ulong lpTotalNumberOfFreeBytes);

        public static bool DriveFreeBytes(string folderName, out ulong freespace)
        {
            freespace = 0;
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            if (!folderName.EndsWith("\\"))
            {
                folderName += '\\';
            }

            ulong free = 0, dummy1 = 0, dummy2 = 0;

            if (GetDiskFreeSpaceEx(folderName, out free, out dummy1, out dummy2))
            {
                freespace = free;
                return true;
            }
            return false;
        }

        public static bool DriveTotalBytes(string folderName, out ulong totalspace)
        {
            totalspace = 0;
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            if (!folderName.EndsWith("\\"))
            {
                folderName += '\\';
            }

            ulong total = 0, dummy1 = 0, dummy2 = 0;

            if (GetDiskFreeSpaceEx(folderName, out dummy1, out total, out dummy2))
            {
                totalspace = total;
                return true;
            }
            return false;
        }

        private static void DirSearch(string sDir, List<string> files, Action<int> callback, Func<bool> cancelFunction)
        {
            foreach (var d in Directory.GetDirectories(sDir))
            {
                try
                {
                    files.AddRange(Directory.GetFiles(d));
                }
                catch (Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                    continue;
                }
                if (cancelFunction != null && cancelFunction())
                {
                    return;
                }
                DirSearch(d, files, callback, cancelFunction);
                if (callback != null)
                    callback(files.Count);
            }
        }

        public static List<BackupFile> GetAllFilesRecursively(string path, Action<int> callback = null,
                                                              Func<bool> cancelFunction = null)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path));
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            DirSearch(path, files, callback, cancelFunction);
            var rFiles = files.Select(i => new BackupFile(i) {BasePath = path}).ToList();
            return rFiles;
        }

        public static string ShortenFileNamePath(string original)
        {
            return original.Length < 160 ? original : Path.GetFileName(original);
        }

        public static string ShortenDirNamePath(string original)
        {
            if (original.Length < 50) return original;
            return Path.GetPathRoot(original) + "..." +
                   Path.GetDirectoryName(original[original.Length - 1] == Path.PathSeparator
                                             ? original
                                             : original + Path.PathSeparator);
        }

        public static string ShortenFileAndDirPath(string original)
        {
            return Path.Combine(ShortenDirNamePath(Path.GetDirectoryName(original)),
                                ShortenFileNamePath(Path.GetFileName(original)));
        }

        public static string UserFriendlySize(long value)
        {
            string[] sizeSuffixes =
                {"bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"};
            if (value < 0)
            {
                return "-" + UserFriendlySize(-value);
            }
            if (value == 0)
            {
                return "0.0 bytes";
            }

            var mag = (int) Math.Log(value, 1024);
            var adjustedSize = (decimal) value/(1L << (mag*10));

            return string.Format("{0:n1} {1}", adjustedSize, sizeSuffixes[mag]);
        }

        public static TimeSpan GetEstimatedTime(long ticks, long used, long total)
        {
            return TimeSpan.FromTicks(
                (long) (ticks*(double) (total - used)/used));
        }
    }

    [Serializable]
    public class BackupFile
    {
        private string _basePath;
        private string _destinationPath;
        [NonSerialized] private bool _hasBeenCopied;
        private string _source;

        public BackupFile(string src)
        {
            Source = src;
        }

        public BackupFile()
        {
        }


        public string BasePath
        {
            get { return _basePath; }
            set { _basePath = value; }
        }

        public long Size
        {
            get
            {
                long length = 0;
                try
                {
                    length = (new FileInfo(Source)).Length;
                }
                catch (Exception)
                {
                    ProblemFile = true;
                }
                return length;
            }
        }

        [XmlIgnore]
        public string DestinationPath
        {
            get { return _destinationPath; }
            set { _destinationPath = value; }
        }


        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        [XmlIgnore]
        public bool HasBeenCopied
        {
            get { return _hasBeenCopied && !ProblemFile; }
            set { _hasBeenCopied = value; }
        }


        public Destination Destination { get; set; }

        [XmlIgnore]
        public bool ProblemFile { get; set; }

        public override string ToString()
        {
            return Source + ": " + Size;
        }
    }

    public class Copier
    {
        #region Delegates

        public delegate void CopierCancelledEventHandler();

        public delegate void CopierCompleteEventHandler();

        public delegate void CopierErrorEventHandler(Errors e);

        public delegate void CopierProgressEventHandler(int value);

        #endregion

        #region Errors enum

        public enum Errors
        {
            Integrity,
            Other
        }

        #endregion

        private readonly Object _cancelObj;
        private bool _cancel;
        private bool _hasBeenAnError;
        private bool _hasBeenCanceled;

        public Copier(BackupFile file)
        {
            BackupFile = file;
            _cancelObj = new Object();
            _hasBeenCanceled = false;
            _hasBeenAnError = false;
        }

        public BackupFile BackupFile { get; set; }

        public event CopierProgressEventHandler ProgressChanged;
        public event CopierCompleteEventHandler Completed;
        public event CopierErrorEventHandler Error;
        public event CopierCancelledEventHandler Cancelled;

        protected virtual void OnChanged(int value)
        {
            if (ProgressChanged != null)
                ProgressChanged(value);
        }

        protected virtual void OnComplete()
        {
            if (Completed != null)
                Completed();
        }

        protected virtual void OnCancelled()
        {
            if (Cancelled != null && !_hasBeenCanceled)
                Cancelled();
            _hasBeenCanceled = true;
        }

        protected virtual void OnError(Errors e)
        {
            if (Error != null && !_hasBeenAnError)
                Error(e);
            _hasBeenAnError = true;
        }

        public void CopyTimestamps()
        {
            var destination = new FileInfo(BackupFile.DestinationPath);
            var origin = new FileInfo(BackupFile.Source);
            if (destination.IsReadOnly)
            {
                destination.IsReadOnly = false;
                destination.CreationTime = origin.CreationTime;
                destination.LastWriteTime = origin.LastWriteTime;
                destination.LastAccessTime = origin.LastAccessTime;
                destination.IsReadOnly = true;
            }
            else
            {
                destination.CreationTime = origin.CreationTime;
                destination.LastWriteTime = origin.LastWriteTime;
                destination.LastAccessTime = origin.LastAccessTime;
            }
        }

        public Task Copy()
        {
            return new Task(() =>
                                {
                                    if (BackupFile.DestinationPath != null &&
                                        Path.GetDirectoryName(BackupFile.DestinationPath) != null)
                                    {
// ReSharper disable AssignNullToNotNullAttribute
                                        Directory.CreateDirectory(Path.GetDirectoryName(BackupFile.DestinationPath));
// ReSharper restore AssignNullToNotNullAttribute
                                        _copy();
                                    }
                                });
        }

        private bool checkHash()
        {
            try
            {
                var srchash = new byte[0];
                var desthash = new byte[0];
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(BackupFile.Source))
                    {
                        srchash = md5.ComputeHash(stream);
                    }
                    using (var stream = File.OpenRead(BackupFile.DestinationPath))
                    {
                        desthash = md5.ComputeHash(stream);
                    }
                    IStructuralEquatable se1 = srchash;
                    return se1.Equals(desthash, StructuralComparisons.StructuralEqualityComparer);
                }
            }
            catch
            {
                return false;
            }
        }

        public void Cancel()
        {
            lock (_cancelObj)
            {
                _cancel = true;
            }
        }

        private void _copy()
        {
            var buffer = new byte[1024*1024];
            try
            {
                if (File.Exists(BackupFile.DestinationPath))
                    File.Delete(BackupFile.DestinationPath);
                using (var source = new FileStream(BackupFile.Source, FileMode.Open, FileAccess.Read))
                {
                    var fileLength = source.Length;
                    using (var dest = new FileStream(BackupFile.DestinationPath, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        var currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            var persentage = totalBytes*100.0/fileLength;

                            dest.Write(buffer, 0, currentBlockSize);

                            OnChanged((int) persentage);
                            lock (_cancelObj)
                            {
                                if (_cancel)
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                OnError(Errors.Other);
            }

            if (_hasBeenCanceled || _hasBeenAnError)
            {
                try
                {
                    File.Delete(BackupFile.DestinationPath);
                }
                catch
                {
                    Console.WriteLine(Resources.Failed_to_delete_file);
                }
                return;
            }
            if (!checkHash())
            {
                OnError(Errors.Integrity);
                return;
            }
            BackupFile.HasBeenCopied = true;
            OnChanged(100);
            OnComplete();
        }
    }

    [Serializable]
    public class Destination
    {
        private readonly bool _simulation;
        private long _initialUsedSpace;
        private long _simulatedFreeSpace;
        private long _simulatedTotalSpace;

        public Destination(string path)
        {
            Path = path;
            FilesCopiedHere = 0;
            RecalculateInitialUsedSpace();
        }

        public Destination(string path, long freespace, long totalspace)
        {
            Path = path;
            _simulation = true;
            FilesCopiedHere = 0;
            _simulatedFreeSpace = freespace;
            _simulatedTotalSpace = totalspace;
            RecalculateInitialUsedSpace();
        }

        public int FilesCopiedHere { get; set; }

        public string Path { get; set; }
        public bool Used { get; set; }

        public long FreeSpace
        {
            get
            {
                if (_simulation)
                {
                    return _simulatedFreeSpace;
                }
                ulong num;
                HelperFunctions.DriveFreeBytes(Path, out num);
                return (long) num;
            }
            set
            {
                if (_simulation)
                    _simulatedFreeSpace = value;
            }
        }

        public long SafeFreeSpace
        {
            get
            {
                var sf = (long) (FreeSpace*.92);
                if (sf < 500000000) // 500 MB minimum size
                    return 0;
                return sf;
            }
        }

        public long TotalSpace
        {
            get
            {
                if (_simulation)
                {
                    return _simulatedTotalSpace;
                }
                ulong num;
                HelperFunctions.DriveTotalBytes(Path, out num);
                return (long) num;
            }
            set
            {
                if (_simulation)
                    _simulatedTotalSpace = value;
            }
        }

        public long UsedSpace
        {
            get { return TotalSpace - FreeSpace; }
        }

        public long InitialUsedSpace
        {
            get { return _initialUsedSpace; }
        }

        public long ScheduledSizeOfFilesToBackup { get; set; }

        protected bool Equals(Destination other)
        {
            return string.Equals(Path, other.Path);
        }

        public override int GetHashCode()
        {
            return (Path != null ? Path.GetHashCode() : 0);
        }

        public void RecalculateInitialUsedSpace()
        {
            _initialUsedSpace = UsedSpace;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Destination) obj);
        }
    }
}