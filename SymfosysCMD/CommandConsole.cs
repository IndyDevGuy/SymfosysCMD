using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD
{
    public class CommandConsole
    {
        public string phpVersion;
        public string phpPath;
        public string symfonyVersion;
        public string projectDirectory;

        public CommandConsole()
        {
            this.initPHPVersion();
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
