using SymfosysCMD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SymfosysCMD.Console
{
    public class ConsoleManager
    {
        public Dictionary<string, SymfosysConsole> consoles;

        public MainWindow mainWindow;
        public ConsoleManager()
        {
            this.consoles = new Dictionary<string, SymfosysConsole>();
            this.mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public void AddConsole(string tabIndex, SymfosysConsole console)
        {
            if (!this.consoles.ContainsKey(tabIndex))
            {
                this.consoles.Add(tabIndex, console);
                console.initConsole();
            }
            else
            {
                foreach(TabItem item in this.mainWindow.consoleTabControl.Items)
                {
                    if(item.Name == tabIndex)
                    {
                        item.Focus();
                    }
                }
            }
        }

        public void RemoveConsole(string command)
        {
            if (this.consoles.ContainsKey(command))
                this.consoles.Remove(command);
        }
    }
}
