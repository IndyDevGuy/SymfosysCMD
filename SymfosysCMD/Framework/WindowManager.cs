using AdonisUI.Controls;
using AutoUpdaterDotNET;
using SymfosysCMD.Windows;
using SymfosysCMD.Windows.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SymfosysCMD.Framework
{
    public class WindowManager
    {
        public NewProjectWindow newProjectWindow;
        public ProjectPreferences projectPreferencesWindow;
        public MandatoryUpdateWindow mandatoryUpdateWindow;
        public OptionalUpdateWindow optionalUpdateWindow;
        public DownloadWindow downloadWindow;
        public MainWindow mainWindow;

        public WindowManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void initNewProjectDialog()
        {
            this.newProjectWindow = new NewProjectWindow(this.mainWindow);
            this.initDialog(this.newProjectWindow, this.mainWindow);
        }

        public void initProjectPreferencesDialog()
        {
            this.projectPreferencesWindow = new ProjectPreferences(this.mainWindow);
            this.initDialog(this.projectPreferencesWindow, this.mainWindow);
        }

        public void initMandatoryUpdateWindow(UpdateInfoEventArgs args)
        {
            this.mandatoryUpdateWindow = new MandatoryUpdateWindow(this.mainWindow, args);
            this.initDialog(this.mandatoryUpdateWindow, this.mainWindow);
        }

        public void initOptionalUpdateWindow(UpdateInfoEventArgs args)
        {
            this.optionalUpdateWindow = new OptionalUpdateWindow(this.mainWindow, args);
            initDialog(this.optionalUpdateWindow, this.mainWindow);
        }

        public void initDownloadWindow(UpdateInfoEventArgs args, AdonisWindow owner)
        {
            this.downloadWindow = new DownloadWindow(args);
            this.downloadWindow.DataContext = this.mainWindow;
            this.downloadWindow.Owner = owner;
            this.mainWindow.themeManager.ChangeWindowTaskBarColor(this.downloadWindow);
            this.downloadWindow.initDownload();
            bool result = (bool)this.downloadWindow.ShowDialog();
            if (result)
            {
                Application.Current.Shutdown();
            }
        }

        private void initDialog(AdonisWindow adonisWindow, AdonisWindow owner)
        {
            adonisWindow.Owner = owner;
            this.mainWindow.themeManager.ChangeWindowTaskBarColor(adonisWindow);
            adonisWindow.ShowDialog();
        }
    }
}
