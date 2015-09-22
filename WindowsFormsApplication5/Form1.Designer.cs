namespace MultidiskBackup
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 1D);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.estimatedCompletionCurrentDestinationLabel = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timeElapsedLabel = new System.Windows.Forms.Label();
            this.copiedNBytesSoFar = new System.Windows.Forms.Label();
            this.estimatedTimeLeftLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.currentDestinationSizeLabel = new System.Windows.Forms.Label();
            this.currentDestinationLabel = new System.Windows.Forms.Label();
            this.destinationsUsedFractionLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.destinationsUsedPercentageLabel = new System.Windows.Forms.Label();
            this.overallProgressPercentageLabel = new System.Windows.Forms.Label();
            this.copiedNFilesOutOfTotal = new System.Windows.Forms.Label();
            this.queuedFileNameLabel = new System.Windows.Forms.Label();
            this.currentFileNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.overallProgressBar = new System.Windows.Forms.ProgressBar();
            this.destinationsUsedProgressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.capacityWarningLabel = new System.Windows.Forms.Label();
            this.sufficientCapacityLabel = new System.Windows.Forms.Label();
            this.copiedNFilesOutOfTotalProgressBar = new MultidiskBackup.NoVisualStyleProgressBar();
            this.currentFileProgressBar = new MultidiskBackup.NoVisualStyleProgressBar();
            this.purpleInitialUsedProgressBar = new MultidiskBackup.NoVisualStyleProgressBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlToolStripMenuItem,
            this.logToolStripMenuItem,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // beginToolStripMenuItem
            // 
            this.beginToolStripMenuItem.Name = "beginToolStripMenuItem";
            this.beginToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.beginToolStripMenuItem.Text = "Begin";
            this.beginToolStripMenuItem.Click += new System.EventHandler(this.button5_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.button6_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLogsToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.logToolStripMenuItem.Text = "Logs";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // saveLogsToolStripMenuItem
            // 
            this.saveLogsToolStripMenuItem.Name = "saveLogsToolStripMenuItem";
            this.saveLogsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveLogsToolStripMenuItem.Text = "Save Logs";
            this.saveLogsToolStripMenuItem.Click += new System.EventHandler(this.saveLogsToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.fileToolStripMenuItem.Text = "Help";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(421, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem
            // 
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem.Name = "whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem";
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem.Size = new System.Drawing.Size(421, 22);
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem.Text = "Why do I see less free space than what Windows Explorer tells me?";
            this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem.Click += new System.EventHandler(this.whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 624);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(933, 594);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox3);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(925, 568);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setup";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(578, 210);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(130, 17);
            this.checkBox3.TabIndex = 26;
            this.checkBox3.Text = "List files that will not fit";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(228, 210);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(137, 17);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "Keep original timestamp";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(101, 210);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Unattended mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button6.Location = new System.Drawing.Point(533, 402);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(229, 95);
            this.button6.TabIndex = 23;
            this.button6.Text = "Cancel";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button5.BackColor = System.Drawing.Color.Chartreuse;
            this.button5.Location = new System.Drawing.Point(101, 402);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(241, 95);
            this.button5.TabIndex = 22;
            this.button5.Text = "Begin";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button4.Location = new System.Drawing.Point(546, 310);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(185, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "Remove Destination";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.Location = new System.Drawing.Point(146, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Remove Source";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(565, 248);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Add Destination Directory";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(146, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Add Source Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(432, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Destinations";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Sources";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(435, 35);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(359, 160);
            this.listBox2.TabIndex = 15;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(37, 35);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(356, 160);
            this.listBox1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.estimatedCompletionCurrentDestinationLabel);
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Controls.Add(this.timeElapsedLabel);
            this.tabPage1.Controls.Add(this.copiedNBytesSoFar);
            this.tabPage1.Controls.Add(this.estimatedTimeLeftLabel);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.currentDestinationSizeLabel);
            this.tabPage1.Controls.Add(this.currentDestinationLabel);
            this.tabPage1.Controls.Add(this.destinationsUsedFractionLabel);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.destinationsUsedPercentageLabel);
            this.tabPage1.Controls.Add(this.overallProgressPercentageLabel);
            this.tabPage1.Controls.Add(this.copiedNFilesOutOfTotal);
            this.tabPage1.Controls.Add(this.queuedFileNameLabel);
            this.tabPage1.Controls.Add(this.currentFileNameLabel);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.overallProgressBar);
            this.tabPage1.Controls.Add(this.destinationsUsedProgressBar);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.capacityWarningLabel);
            this.tabPage1.Controls.Add(this.sufficientCapacityLabel);
            this.tabPage1.Controls.Add(this.copiedNFilesOutOfTotalProgressBar);
            this.tabPage1.Controls.Add(this.currentFileProgressBar);
            this.tabPage1.Controls.Add(this.purpleInitialUsedProgressBar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(925, 568);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Status";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // estimatedCompletionCurrentDestinationLabel
            // 
            this.estimatedCompletionCurrentDestinationLabel.AutoSize = true;
            this.estimatedCompletionCurrentDestinationLabel.Location = new System.Drawing.Point(704, 163);
            this.estimatedCompletionCurrentDestinationLabel.Name = "estimatedCompletionCurrentDestinationLabel";
            this.estimatedCompletionCurrentDestinationLabel.Size = new System.Drawing.Size(0, 13);
            this.estimatedCompletionCurrentDestinationLabel.TabIndex = 30;
            // 
            // chart1
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 94F;
            chartArea1.Position.Width = 55.9119F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 3F;
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 85.18275F;
            legend1.Position.Width = 35.08811F;
            legend1.Position.X = 61.9119F;
            legend1.Position.Y = 3F;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(8, 338);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar100;
            series1.Label = "To be determined";
            series1.Legend = "Legend1";
            series1.Name = "To be determined";
            series1.Points.Add(dataPoint1);
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(909, 198);
            this.chart1.TabIndex = 28;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // timeElapsedLabel
            // 
            this.timeElapsedLabel.AutoSize = true;
            this.timeElapsedLabel.Location = new System.Drawing.Point(704, 87);
            this.timeElapsedLabel.Name = "timeElapsedLabel";
            this.timeElapsedLabel.Size = new System.Drawing.Size(0, 13);
            this.timeElapsedLabel.TabIndex = 27;
            // 
            // copiedNBytesSoFar
            // 
            this.copiedNBytesSoFar.AutoSize = true;
            this.copiedNBytesSoFar.Location = new System.Drawing.Point(17, 60);
            this.copiedNBytesSoFar.Name = "copiedNBytesSoFar";
            this.copiedNBytesSoFar.Size = new System.Drawing.Size(0, 13);
            this.copiedNBytesSoFar.TabIndex = 26;
            // 
            // estimatedTimeLeftLabel
            // 
            this.estimatedTimeLeftLabel.AutoSize = true;
            this.estimatedTimeLeftLabel.Location = new System.Drawing.Point(704, 60);
            this.estimatedTimeLeftLabel.Name = "estimatedTimeLeftLabel";
            this.estimatedTimeLeftLabel.Size = new System.Drawing.Size(0, 13);
            this.estimatedTimeLeftLabel.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Overall progress: ";
            // 
            // currentDestinationSizeLabel
            // 
            this.currentDestinationSizeLabel.AutoSize = true;
            this.currentDestinationSizeLabel.Location = new System.Drawing.Point(10, 163);
            this.currentDestinationSizeLabel.Name = "currentDestinationSizeLabel";
            this.currentDestinationSizeLabel.Size = new System.Drawing.Size(0, 13);
            this.currentDestinationSizeLabel.TabIndex = 23;
            // 
            // currentDestinationLabel
            // 
            this.currentDestinationLabel.AutoSize = true;
            this.currentDestinationLabel.Location = new System.Drawing.Point(10, 138);
            this.currentDestinationLabel.Name = "currentDestinationLabel";
            this.currentDestinationLabel.Size = new System.Drawing.Size(0, 13);
            this.currentDestinationLabel.TabIndex = 22;
            // 
            // destinationsUsedFractionLabel
            // 
            this.destinationsUsedFractionLabel.AutoSize = true;
            this.destinationsUsedFractionLabel.Location = new System.Drawing.Point(9, 90);
            this.destinationsUsedFractionLabel.Name = "destinationsUsedFractionLabel";
            this.destinationsUsedFractionLabel.Size = new System.Drawing.Size(94, 13);
            this.destinationsUsedFractionLabel.TabIndex = 21;
            this.destinationsUsedFractionLabel.Text = "Destinations used:";
            this.destinationsUsedFractionLabel.Click += new System.EventHandler(this.destinationsUsedFractionLabel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Current file:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // destinationsUsedPercentageLabel
            // 
            this.destinationsUsedPercentageLabel.AutoSize = true;
            this.destinationsUsedPercentageLabel.Location = new System.Drawing.Point(704, 139);
            this.destinationsUsedPercentageLabel.Name = "destinationsUsedPercentageLabel";
            this.destinationsUsedPercentageLabel.Size = new System.Drawing.Size(0, 13);
            this.destinationsUsedPercentageLabel.TabIndex = 18;
            // 
            // overallProgressPercentageLabel
            // 
            this.overallProgressPercentageLabel.AutoSize = true;
            this.overallProgressPercentageLabel.Location = new System.Drawing.Point(122, 18);
            this.overallProgressPercentageLabel.Name = "overallProgressPercentageLabel";
            this.overallProgressPercentageLabel.Size = new System.Drawing.Size(0, 13);
            this.overallProgressPercentageLabel.TabIndex = 17;
            // 
            // copiedNFilesOutOfTotal
            // 
            this.copiedNFilesOutOfTotal.AutoSize = true;
            this.copiedNFilesOutOfTotal.Location = new System.Drawing.Point(8, 288);
            this.copiedNFilesOutOfTotal.Name = "copiedNFilesOutOfTotal";
            this.copiedNFilesOutOfTotal.Size = new System.Drawing.Size(0, 13);
            this.copiedNFilesOutOfTotal.TabIndex = 16;
            // 
            // queuedFileNameLabel
            // 
            this.queuedFileNameLabel.AutoSize = true;
            this.queuedFileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queuedFileNameLabel.Location = new System.Drawing.Point(62, 259);
            this.queuedFileNameLabel.Name = "queuedFileNameLabel";
            this.queuedFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.queuedFileNameLabel.TabIndex = 15;
            // 
            // currentFileNameLabel
            // 
            this.currentFileNameLabel.AutoSize = true;
            this.currentFileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentFileNameLabel.Location = new System.Drawing.Point(62, 246);
            this.currentFileNameLabel.Name = "currentFileNameLabel";
            this.currentFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.currentFileNameLabel.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Queued:";
            // 
            // overallProgressBar
            // 
            this.overallProgressBar.Location = new System.Drawing.Point(12, 34);
            this.overallProgressBar.Name = "overallProgressBar";
            this.overallProgressBar.Size = new System.Drawing.Size(905, 23);
            this.overallProgressBar.TabIndex = 12;
            // 
            // destinationsUsedProgressBar
            // 
            this.destinationsUsedProgressBar.Location = new System.Drawing.Point(11, 113);
            this.destinationsUsedProgressBar.Name = "destinationsUsedProgressBar";
            this.destinationsUsedProgressBar.Size = new System.Drawing.Size(906, 23);
            this.destinationsUsedProgressBar.TabIndex = 11;
            this.destinationsUsedProgressBar.Click += new System.EventHandler(this.destinationsUsedProgressBar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Copying: ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // capacityWarningLabel
            // 
            this.capacityWarningLabel.AutoSize = true;
            this.capacityWarningLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.capacityWarningLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capacityWarningLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.capacityWarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capacityWarningLabel.Location = new System.Drawing.Point(20, 539);
            this.capacityWarningLabel.Name = "capacityWarningLabel";
            this.capacityWarningLabel.Size = new System.Drawing.Size(102, 26);
            this.capacityWarningLabel.TabIndex = 8;
            this.capacityWarningLabel.Text = "WARNING";
            this.capacityWarningLabel.Visible = false;
            // 
            // sufficientCapacityLabel
            // 
            this.sufficientCapacityLabel.AutoSize = true;
            this.sufficientCapacityLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sufficientCapacityLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sufficientCapacityLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sufficientCapacityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sufficientCapacityLabel.Location = new System.Drawing.Point(20, 539);
            this.sufficientCapacityLabel.Name = "sufficientCapacityLabel";
            this.sufficientCapacityLabel.Size = new System.Drawing.Size(358, 26);
            this.sufficientCapacityLabel.TabIndex = 7;
            this.sufficientCapacityLabel.Text = "Sufficient destination capacity to complete";
            this.sufficientCapacityLabel.Visible = false;
            // 
            // copiedNFilesOutOfTotalProgressBar
            // 
            this.copiedNFilesOutOfTotalProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.copiedNFilesOutOfTotalProgressBar.Location = new System.Drawing.Point(12, 306);
            this.copiedNFilesOutOfTotalProgressBar.Name = "copiedNFilesOutOfTotalProgressBar";
            this.copiedNFilesOutOfTotalProgressBar.Opacity = 100;
            this.copiedNFilesOutOfTotalProgressBar.Size = new System.Drawing.Size(905, 23);
            this.copiedNFilesOutOfTotalProgressBar.TabIndex = 19;
            // 
            // currentFileProgressBar
            // 
            this.currentFileProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.currentFileProgressBar.Location = new System.Drawing.Point(12, 207);
            this.currentFileProgressBar.Name = "currentFileProgressBar";
            this.currentFileProgressBar.Opacity = 100;
            this.currentFileProgressBar.Size = new System.Drawing.Size(905, 23);
            this.currentFileProgressBar.TabIndex = 9;
            // 
            // purpleInitialUsedProgressBar
            // 
            this.purpleInitialUsedProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.purpleInitialUsedProgressBar.Location = new System.Drawing.Point(11, 103);
            this.purpleInitialUsedProgressBar.Name = "purpleInitialUsedProgressBar";
            this.purpleInitialUsedProgressBar.Opacity = 100;
            this.purpleInitialUsedProgressBar.Size = new System.Drawing.Size(906, 10);
            this.purpleInitialUsedProgressBar.TabIndex = 29;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.statusTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(925, 568);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Program Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // statusTextBox
            // 
            this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTextBox.Location = new System.Drawing.Point(0, 0);
            this.statusTextBox.Multiline = true;
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.statusTextBox.Size = new System.Drawing.Size(925, 568);
            this.statusTextBox.TabIndex = 7;
            this.statusTextBox.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.logTextBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(925, 568);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Logs";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(925, 568);
            this.logTextBox.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.simulate_and_copy);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.update_simulation_progress_bar);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 646);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Multidisk Backup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label capacityWarningLabel;
        private System.Windows.Forms.Label sufficientCapacityLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar overallProgressBar;
        private System.Windows.Forms.ProgressBar destinationsUsedProgressBar;
        private System.Windows.Forms.Label queuedFileNameLabel;
        private System.Windows.Forms.Label currentFileNameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label copiedNFilesOutOfTotal;
        private System.Windows.Forms.Label overallProgressPercentageLabel;
        private System.Windows.Forms.Label destinationsUsedPercentageLabel;
        private System.Windows.Forms.Label destinationsUsedFractionLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label currentDestinationLabel;
        private System.Windows.Forms.Label currentDestinationSizeLabel;
        private System.Windows.Forms.Label copiedNBytesSoFar;
        private System.Windows.Forms.Label estimatedTimeLeftLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label timeElapsedLabel;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private NoVisualStyleProgressBar purpleInitialUsedProgressBar;
        private System.Windows.Forms.Label estimatedCompletionCurrentDestinationLabel;
        private System.Windows.Forms.CheckBox checkBox3;
        private NoVisualStyleProgressBar currentFileProgressBar;
        private NoVisualStyleProgressBar copiedNFilesOutOfTotalProgressBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem whyDoISeeLessFreeSpaceThanWhatWindowsExplorerTellsMeToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

