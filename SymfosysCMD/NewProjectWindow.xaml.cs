using AdonisUI.Controls;
using System;
using System.Windows;
using System.Windows.Interop;

using SymfosysCMD.Validation;
using SymfosysCMD.DataContext;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SymfosysCMD
{
    /// <summary>
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : AdonisWindow
    {
        private MainWindow mainWindow;
        public NewProjectWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            this.DataContext = new NewProjectDataContext(this); ;
            this.SourceInitialized += Window1_SourceInitialized;
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

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            projectName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            bool projectDirectoryError = false;
            if (String.IsNullOrEmpty(projectDirectory.Text))
            {
                projectDirectoryError = true;
                var messageBox = new MessageBoxModel
                {
                    Text = "Please select a folder for or where your project is located.",
                    Caption = "Project Directory Error",
                    Icon = AdonisUI.Controls.MessageBoxImage.Error,
                    Buttons = MessageBoxButtons.OkCancel("Ok","Close"),
                };
                this.saveButton.IsEnabled = false;
                AdonisUI.Controls.MessageBox.Show(messageBox);
            }
            if (Validator.IsValid(this) && !projectDirectoryError)
            {
                this.Close();
            }
        }


    }

}
