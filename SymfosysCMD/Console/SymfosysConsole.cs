using SymfosysCMD.Controls;
using SymfosysCMD.Framework;
using SymfosysCMD.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static SymfosysCMD.Framework.ProcessAsyncHelper;

namespace SymfosysCMD.Console
{
    public class SymfosysConsole
    {
        public MainWindow mainWindow;
        public CloseableTab tab;
        public RichTextBox console;
        public ProjectConsoleTabControl projectConsoleTabControl;
        public Profile profile;
        public string command;
        public string tabName;
        public string tabIndex;

        public SymfosysConsole(MainWindow mainWindow, string tabName, string tabIndex, string command)
        {
            this.mainWindow = mainWindow;
            this.profile = this.mainWindow.activeProfile;
            this.command = command;
            this.tabName = tabName;
            this.tabIndex = tabIndex;
            string[] tmps = command.Split('|');
        }

        public void initConsole()
        {
            this.tab = new CloseableTab(this.mainWindow, profile.getName(),this.tabName,this.tabIndex);
            this.tab.Style = (Style)this.mainWindow.FindResource("CloseableTab");
            this.tab.Title = this.tabName;
            this.tab.Name = this.tabIndex;
            
            this.projectConsoleTabControl = new ProjectConsoleTabControl();
            this.projectConsoleTabControl.symfosysConsole = this;
            this.console = this.projectConsoleTabControl.SymfonyConsole;
            this.console.Document.Blocks.Clear();
            this.tab.Content = this.projectConsoleTabControl;
            
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
            //MessageBox.Show(errorText, errorTitle);
        }

        public async void callCommand(Boolean clearConsole = true)
        {
            this.projectConsoleTabControl.CallingCommand = true;
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
            p.StartInfo.Arguments = "/c php bin/console "  + command;
            Result result = await ProcessAsyncHelper.RunAsync(p.StartInfo);
            string texter = result.StdOut;
            this.console.AppendText(texter);
            this.projectConsoleTabControl.CallingCommand = false;

        }
    }
}
