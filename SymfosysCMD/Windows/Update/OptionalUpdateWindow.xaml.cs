using AdonisUI.Controls;
using AutoUpdaterDotNET;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace SymfosysCMD.Windows.Update
{
    /// <summary>
    /// Interaction logic for OptionalUpdateWindow.xaml
    /// </summary>
    public partial class OptionalUpdateWindow : AdonisWindow
    {
        private MainWindow mainWindow;
        public UpdateInfoEventArgs updateInfo;
        public DownloadWindow downloadWindow;
        public OptionalUpdateWindow(MainWindow mainWindow, UpdateInfoEventArgs updateInfoEventArgs)
        {
            this.updateInfo = updateInfoEventArgs;
            this.mainWindow = mainWindow;
            InitializeComponent();
            this.SourceInitialized += Window1_SourceInitialized;
            string updateVersionString = "SymfosysCMD v"+this.updateInfo.CurrentVersion+" is ready to be installed";
            string currentVersionString = "The current installed version is v"+this.updateInfo.InstalledVersion.ToString();
            updateControlInformation.CurrentVersionText.Text = currentVersionString;
            updateControlInformation.UpdateVersionText.Text = updateVersionString;
            HttpClient client = new HttpClient();
            string text = client.GetStringAsync(this.updateInfo.ChangelogURL).Result;
            MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text));
            updateControlInformation.UpdateChangelog.Selection.Load(stream, DataFormats.Rtf);
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            this.mainWindow.Focus();
            this.Close();
        }

        public void RunUpdate(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mainWindow.windowManager.initDownloadWindow(this.updateInfo, this);
            }
            catch (Exception exception)
            {
                var messageBox = new MessageBoxModel
                {
                    Text = exception.Message,
                    Caption = exception.GetType().ToString(),
                    Icon = AdonisUI.Controls.MessageBoxImage.Error,
                    Buttons = new[]
                    {
                        MessageBoxButtons.Ok("Okay"),
                    },
                    IsSoundEnabled = false,
                };
                AdonisUI.Controls.MessageBox.Show(messageBox);
                this.mainWindow.Focus();
                this.Close();
            }
        }

        private void Window1_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }

    }
}
