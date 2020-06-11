using AdonisUI.Controls;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace SymfosysCMD
{
    public class SymfosysConsole
    {
        public MainWindow mainWindow;
        public CloseableTab tab;
        public RichTextBox console;
        public Profile profile;
        public string command;
        public int consoleIndex;

        public SymfosysConsole(MainWindow mainWindow, Profile profile, int consoleIndex, string command)
        {
            this.mainWindow = mainWindow;
            this.profile = profile;
            this.consoleIndex = consoleIndex;
            this.command = command;
            this.tab = new CloseableTab(this.mainWindow, profile.getName(), this.consoleIndex);
            this.tab.Title = "Symfosys Console Command";
            this.mainWindow.consoleTabControl.Items.Add(this.tab);
            this.tab.Focus();
        }

        public void writeConsole(string text)
        {
            this.console.AppendText(text);
        }

        public void clearConsole()
        {
            this.console.SelectAll();
            this.console.Selection.Text = "";
        }

        public void displayErrorBox(string errorText, string errorTitle)
        {
            MessageBox.Show(errorText, errorTitle);
        }

        public void callCommandWithErrorBox(string command, string message)
        {

        }

        public void callCommand(string command, string consoleMessage, Boolean clearConsole = true)
        {
            if (clearConsole)
                this.clearConsole();
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"" + this.profile.getDocumentRoot();
            p.StartInfo.Arguments = "/c " + command;
            p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            p.ErrorDataReceived += new DataReceivedEventHandler(SortOutputHandler);

            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        private void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (this.console.Dispatcher.CheckAccess())
            {
                this.console.Dispatcher.Invoke(new DataReceivedEventHandler(SortOutputHandler), new[] { sendingProcess, outLine });
            }
            else
            {
                this.console.AppendText(outLine.Data + Environment.NewLine ?? string.Empty);
                this.console.ScrollToEnd();
            }
        }
    }
}
