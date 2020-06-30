using AdonisUI;
using AdonisUI.Controls;
using SymfosysCMD.Windows;
using SymfosysCMD.Windows.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SymfosysCMD.Framework
{
    public class ThemeManager
    {
        public MainWindow mainWindow;

        public ThemeManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void ChangeTheme(bool oldValue)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, oldValue ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
            string themeType = "Light";
            if (!oldValue)
                themeType = "Dark";
            this.mainWindow.settingsManager.setSettingsTheme(themeType);
            this.ChangeTaskbar();
        }

        private void ChangeTaskbar()
        {
            this.ChangeWindowTaskBarColor(this.mainWindow);
            if (this.mainWindow.windowManager.mandatoryUpdateWindow != null && this.mainWindow.windowManager.mandatoryUpdateWindow is MandatoryUpdateWindow)
            {
                this.ChangeWindowTaskBarColor(this.mainWindow.windowManager.mandatoryUpdateWindow);
                if (this.mainWindow.windowManager.mandatoryUpdateWindow.downloadWindow != null && this.mainWindow.windowManager.mandatoryUpdateWindow.downloadWindow is DownloadWindow)
                {
                    this.ChangeWindowTaskBarColor(this.mainWindow.windowManager.mandatoryUpdateWindow.downloadWindow);
                }
            }
            if (this.mainWindow.windowManager.optionalUpdateWindow != null && this.mainWindow.windowManager.optionalUpdateWindow is OptionalUpdateWindow)
            {
                this.ChangeWindowTaskBarColor(this.mainWindow.windowManager.optionalUpdateWindow);
                if (this.mainWindow.windowManager.optionalUpdateWindow.downloadWindow != null && this.mainWindow.windowManager.optionalUpdateWindow.downloadWindow is DownloadWindow)
                {
                    this.ChangeWindowTaskBarColor(this.mainWindow.windowManager.optionalUpdateWindow.downloadWindow);
                }
            }
        }

        public void ChangeWindowTaskBarColor(AdonisWindow window)
        {
            if (this.mainWindow.IsDark)
            {
                window.TitleBarBackground = (Brush)this.mainWindow.FindResource("BlackGlossBrush");
                window.WindowButtonHighlightBrush = (Brush)this.mainWindow.FindResource(AdonisUI.Brushes.AccentHighlightBrush);
            }
            else
            {
                window.TitleBarBackground = (Brush)this.mainWindow.FindResource("WhiteGlossBrush");
                window.WindowButtonHighlightBrush = (Brush)this.mainWindow.FindResource(AdonisUI.Brushes.Layer0BackgroundBrush);
            }
        }
    }
}
