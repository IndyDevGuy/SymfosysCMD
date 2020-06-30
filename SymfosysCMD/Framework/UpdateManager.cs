using AdonisUI.Controls;
using AutoUpdaterDotNET;
using Newtonsoft.Json;
using SymfosysCMD.Windows;

namespace SymfosysCMD.Framework
{
    public class UpdateManager
    {
        public MainWindow mainWindow;

        public UpdateManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            //AutoUpdater
            //AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SymfosysCMD";
            AutoUpdater.Synchronous = false;
            AutoUpdater.Mandatory = true;
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        }

        public void CheckForUpdate()
        {
            AutoUpdater.Start("https://indydevguy.com/downloads/SymfosysCMD/updates/SymfosysCMD.json");
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    if (args.Mandatory.Value)
                        this.mainWindow.windowManager.initMandatoryUpdateWindow(args);
                    else
                        this.mainWindow.windowManager.initOptionalUpdateWindow(args);

                }
                else
                {
                    var messageBox = new MessageBoxModel
                    {
                        Text = "There is no update available please try again later.",
                        Caption = "No update available",
                        Icon = AdonisUI.Controls.MessageBoxImage.Information,
                        Buttons = new[]
                        {
                            MessageBoxButtons.Ok("Okay"),
                        },
                        IsSoundEnabled = false,
                    };
                    AdonisUI.Controls.MessageBox.Show(messageBox);
                }
            }
            else
            {
                var messageBox = new MessageBoxModel
                {
                    Text = "There is a problem reaching update server. Please check your internet connection and try again.",
                    Caption = "Update check failed",
                    Icon = AdonisUI.Controls.MessageBoxImage.Information,
                    Buttons = new[]
                    {
                        MessageBoxButtons.Ok("Okay"),
                    },
                    IsSoundEnabled = false,
                };
                AdonisUI.Controls.MessageBox.Show(messageBox);
            }
        }

        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.version,
                ChangelogURL = json.changelog,
                DownloadURL = json.url,
                Mandatory = new Mandatory
                {
                    Value = json.mandatory.value,
                    UpdateMode = json.mandatory.mode,
                    MinimumVersion = json.mandatory.minVersion
                }
            };
        }
    }
}
