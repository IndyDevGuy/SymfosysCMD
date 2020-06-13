using AdonisUI.Controls;
using SymfosysCMD.DataContext;
using SymfosysCMD.Validation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SymfosysCMD.Controls
{
    /// <summary>
    /// Interaction logic for projectFrom.xaml
    /// </summary>
    public partial class ProjectFormControl : UserControl
    {
        public MainWindow mainWindow;
        public ProjectFormDataContext projectFormDataContext;
        public Profile profile;
        public ProjectFormControl()
        {
            this.projectFormDataContext = new ProjectFormDataContext(this);
            InitializeComponent();
            this.mainWindow = (MainWindow) Application.Current.MainWindow;
            this.profile = this.mainWindow.activeProfile;
            this.DataContext = this.projectFormDataContext;
        }

        public void initProjectData()
        {
            this.projectFormDataContext.ProjectName = this.profile.getName();
            this.projectFormDataContext.FolderName = this.profile.getDocumentRoot();
            this.projectFormDataContext.hasDirectory = true;
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
                    Buttons = MessageBoxButtons.OkCancel("Ok", "Close"),
                };
                this.projectFormDataContext.hasDirectory = false;
                AdonisUI.Controls.MessageBox.Show(messageBox);
            }
            if (Validator.IsValid(this) && !projectDirectoryError)
            {
                if (this.projectFormDataContext.CrudType == "add")
                {
                    this.profile = new Profile();
                    this.profile.setName(projectName.Text);
                    this.profile.setDocumentRoot(projectDirectory.Text);

                    this.mainWindow.settingsManager.setSettingsActiveProfile(this.profile.getName());
                    this.mainWindow.settingsManager.addSettingsProfile(this.profile);
                    this.mainWindow.newProjectWindow.DialogResult = true;
                    this.mainWindow.reloadProfiles();
                    this.mainWindow.newProjectWindow.Close();
                }
                else
                {
                    this.profile = this.mainWindow.activeProfile;
                    string originalProfileName = this.profile.getName();
                    this.profile.setName(projectName.Text);
                    this.profile.setDocumentRoot(projectDirectory.Text);
                    this.mainWindow.settingsManager.setSettingsActiveProfile(this.profile.getName());
                    this.mainWindow.settingsManager.updateSettingsProfile(this.profile, originalProfileName);
                    this.mainWindow.projectPreferencesWindow.DialogResult = true;
                    this.mainWindow.reloadProfiles();
                    this.mainWindow.projectPreferencesWindow.Close();
                }
            }
        }
    }
}
