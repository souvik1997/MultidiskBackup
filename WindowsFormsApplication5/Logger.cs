#region

using System;
using System.IO;

#endregion

namespace MultidiskBackup
{
    public delegate void LogChangedEventHandler(object sender, LogChangedEventArgs e);

    public class Logger
    {
        private readonly object _syncObj;

        public Logger(bool outputToConsole = false)
        {
            Filename = string.Format("backup-{0:MM-dd-yy_H-mm-ss}.log", DateTime.Now);
            ConsoleOutput = outputToConsole;
            _syncObj = new object();
        }

        public bool ConsoleOutput { get; set; }

        public string Text { get; set; }
        public string Filename { get; set; }

        public string[] Lines
        {
            get { return Text.Split(Environment.NewLine.ToCharArray()); }
        }

        public event LogChangedEventHandler LogChanged;

        protected virtual void OnLogChanged(LogChangedEventArgs e)
        {
            if (LogChanged != null)
                LogChanged(this, e);
        }

        private void _appendLine(string line)
        {
            lock (_syncObj)
            {
                var msg = string.Format("[{0:MM/dd/yy H:mm:ss zzz}] {1}", DateTime.Now, line);
                Text += msg + Environment.NewLine;
                OnLogChanged((new LogChangedEventArgs(msg)));
                if (ConsoleOutput)
                    Console.WriteLine(msg);
            }
        }

        public void AppendBigLine(string line, params object[] args)
        {
            AppendLine(Environment.NewLine + line, args);
        }

        public void AppendLine(string format, params object[] args)
        {
            _appendLine(string.Format(format, args));
        }

        public bool Save()
        {
            lock (_syncObj)
            {
                try
                {
                    File.AppendAllText(Filename, Text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool SaveTo(string dir)
        {
            lock (_syncObj)
            {
                try
                {
                    File.AppendAllText(Path.Combine(dir, Filename), Text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }

    public class LogChangedEventArgs : EventArgs
    {
        public LogChangedEventArgs(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; internal set; }
    }
}