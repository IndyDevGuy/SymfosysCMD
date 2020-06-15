using SymfosysCMD.Console;
using SymfosysCMD.Framework;
using SymfosysCMD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymfosysCMD.Controls
{
    /// <summary>
    /// Interaction logic for MainMenuControl.xaml
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        MainWindow mainWindow;
        public MainMenuControl()
        {
            this.mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        public void CreateConsole(object sender, RoutedEventArgs e)
        {
            if (this.mainWindow.applicationContext.activeProject)
            {
                MenuItem menu = (MenuItem)sender;
                string tmp = (string)menu.Tag;
                string[] tmpz = tmp.Split('|');
                string tabName = tmpz[0];
                string tabIndex = tmpz[1];
                string command = tmpz[2];
            
                if (!this.mainWindow.consoleManagers.ContainsKey(this.mainWindow.activeProfile.name))
                {
                    ConsoleManager consoleManager = new ConsoleManager();
                    this.mainWindow.consoleManagers.Add(this.mainWindow.activeProfileName, consoleManager);
                }
                ConsoleManager consoleManagerRef = this.mainWindow.consoleManagers[this.mainWindow.activeProfileName];
                SymfosysConsole symfosysConsole = new SymfosysConsole(this.mainWindow, tabName, tabIndex, command);
                consoleManagerRef.AddConsole(tabIndex, symfosysConsole);
                symfosysConsole.callCommand();
                //we need to add a datatab to the profile so that it will auto open on program start if it has not been closed by user
                DataTab dtab = new DataTab();
                dtab.tabName = tabName;
                dtab.tabIndex = tabIndex;
                dtab.command = command;
                this.mainWindow.activeProfile.addTab(dtab);
                this.mainWindow.settingsManager.updateSettingsProfile(this.mainWindow.activeProfile, this.mainWindow.activeProfileName);

            }
        }
    }
}
