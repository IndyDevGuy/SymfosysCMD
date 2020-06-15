using SymfosysCMD.Framework;
using SymfosysCMD.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using static SymfosysCMD.Framework.ProcessAsyncHelper;

namespace SymfosysCMD.Console
{
    public class CommandConsole
    {
        public string phpVersion;
        public string phpPath;
        public string symfonyVersion;
        public string projectDirectory;
        public MainWindow mainWindow;

        public CommandConsole()
        {
            this.initPHPVersion();
            this.mainWindow = (MainWindow) Application.Current.MainWindow;
        }

        public void initSymfony()
        {
            if(Directory.Exists(this.mainWindow.applicationContext.RootDirectory))
            {
                this.mainWindow.applicationContext.SymfonyInstalled = true;
                this.setSymfonyVersion();

            }
        }

        public async void setSymfonyVersion()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"" + this.mainWindow.applicationContext.RootDirectory;
            p.StartInfo.Arguments = "/c php bin/console -V --version";
            Result result = await ProcessAsyncHelper.RunAsync(p.StartInfo);
            string texter = result.StdOut;
            if (texter != "Could not open input file: bin/console")
            {
                if (this.mainWindow.activeProfile != null)
                {
                    this.mainWindow.activeProfile.setSymfonyVersion(texter);
                    this.mainWindow.settingsManager.updateSettingsProfile(this.mainWindow.activeProfile, this.mainWindow.activeProfile.getName());
                    this.mainWindow.applicationContext.SymfonyVersion = this.mainWindow.activeProfile.symfonyVersion;
                    this.mainWindow.applicationContext.SymfonyVersionFound = true;
                }
            }
            else
            {
                this.mainWindow.applicationContext.SymfonyInstalled = false;
                this.mainWindow.applicationContext.SymfonyVersionFound = false;
                this.mainWindow.applicationContext.SymfonyVersion = "Symfony is not installed!";
            }
        }

        public void initPHPVersion()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c php --version";
            p.Start();
            var reader = p.StandardOutput;
            string tempVersion = "";
            while (!reader.EndOfStream)
            {
                tempVersion = reader.ReadLine();
                break;
            }
            p.WaitForExit();
            String[] spearator = { "PHP "," (cli) " };
            Int32 count = 2;

            // using the method 
            String[] strlist = tempVersion.Split(spearator, count,StringSplitOptions.RemoveEmptyEntries);

            foreach (String s in strlist)
            {
                this.phpVersion = s;
                break;
            }
            
        }

        public void callCommand(string command, string consoleMessage, Boolean clearConsole = true)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"" + this.projectDirectory;
            p.StartInfo.Arguments = "/c " + command;
        }
    }
}
