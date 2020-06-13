using AdonisUI;
using AdonisUI.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using SymfosysCMD.Controls;
using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using SymfosysCMD.Settings;
using System.Windows.Data;

namespace SymfosysCMD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow 
    {
        //windows
        public NewProjectWindow newProjectWindow;
        public ProjectPreferences projectPreferencesWindow;
        public ApplicationDataContext applicationContext;

        public bool IsDark
        {
            get => (bool)GetValue(IsDarkProperty);
            set => SetValue(IsDarkProperty, value);
        }
        public static readonly DependencyProperty IsDarkProperty = DependencyProperty.Register("IsDark", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnIsDarkChanged));

        private static void OnIsDarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainWindow)d).ChangeTheme((bool)e.OldValue);
        }

        public CommandConsole commandConsole;
        public SettingsManager settingsManager;

        public Dictionary<string, Profile> profiles;

        public string activeProfileName;
        public Profile activeProfile;

        public TabItem projectInformationTab;
        public TabItem startupTab;
        public StartupUserControl startupUserControl;
        public ProjectInformationControl projectInformationControl;

        public Dictionary<Profile, ComboBoxItem> projectComboBoxItems;

        public MainWindow()
        {
            this.projectComboBoxItems = new Dictionary<Profile, ComboBoxItem>();
            this.profiles = new Dictionary<string, Profile>();
            this.settingsManager = new SettingsManager();
            this.applicationContext = new ApplicationDataContext();
            this.applicationContext.activeProject = false;
            string themeType = this.settingsManager.getSettingsTheme();
            if (themeType == "dark")
                this.IsDark = true;
            else
                this.IsDark = false;
            
            AdonisUI.SpaceExtension.SetSpaceResourceOwnerFallback(this);
            this.commandConsole = new CommandConsole();
            InitializeComponent();
            DataContext = this.applicationContext;
            this.reloadProfiles();
            //init tabs
            this.projectInformationTab = new TabItem();
            this.projectInformationTab.Header = "Project Inforamtion";
            this.projectInformationControl = new ProjectInformationControl();
            Binding myBinding = new Binding("SymfonyVersion");
            myBinding.Source = this.applicationContext;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            this.projectInformationControl.SetBinding(ProjectInformationControl.SymfonyVersionProperty, myBinding);
            this.projectInformationTab.Content = this.projectInformationControl;
            this.startupTab = new TabItem();
            this.startupTab.Header = "Startup";
            this.startupUserControl = new StartupUserControl();
            this.startupTab.Content = this.startupUserControl;
            this.swapStartupTabs();

            php_version.Text = "PHP Version: " + this.commandConsole.phpVersion;
            
            //Event handlers
            selectedProjectComboBox.SelectionChanged += new SelectionChangedEventHandler(selectedProjectChanged);
        }

        public void swapStartupTabs()
        {
            if (this.activeProfile != null)
            {
                this.syncProfileWithDataContext();
                if (this.consoleTabControl.Items.Contains(this.startupTab))
                    this.consoleTabControl.Items.Remove(this.startupTab);
                if (this.consoleTabControl.Items.Contains(this.projectInformationTab))
                    this.consoleTabControl.Items.Remove(this.projectInformationTab);
                this.consoleTabControl.Items.Add(this.projectInformationTab);
                this.projectInformationTab.Focus();
            }
            else
            {
                this.syncProfileWithDataContext(true);
                if (this.consoleTabControl.Items.Contains(this.projectInformationTab))
                    this.consoleTabControl.Items.Remove(this.projectInformationTab);
                if (this.consoleTabControl.Items.Contains(this.startupTab))
                    this.consoleTabControl.Items.Remove(this.startupTab);
                this.consoleTabControl.Items.Add(this.startupTab);
                this.startupTab.Focus();
            }
        }

        private void ChangeTheme(bool oldValue)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, oldValue ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
            string themeType = "light";
            if (!oldValue)
                themeType = "dark";
            this.settingsManager.setSettingsTheme(themeType);
            string activeProfileName = this.settingsManager.getSettingsActiveProfile();
            if (activeProfileName != null)
            {
                //Profile activeProfile = this.settingsManager.getSettingsProfile(activeProfileName);
                //foreach (KeyValuePair<int, SymfosysConsole> item in activeProfile.getSymfosysConsoles())
                //{
                //    item.Value.tab.changeTheme();
                //}
            }
        }


        private void newProjectForm(object sender)
        {
            this.newProjectWindow = new NewProjectWindow(this);
            this.newProjectWindow.Owner = this;
            this.newProjectWindow.ShowDialog();
        }

        private void projectPreferencesForm(object sender)
        {
            this.projectPreferencesWindow = new ProjectPreferences(this);
            this.projectPreferencesWindow.Owner = this;
            this.projectPreferencesWindow.ShowDialog();
        }

        private void selectedProjectChanged(object sender, SelectionChangedEventArgs e)
        {
            Profile selectedProfile = (Profile)selectedProjectComboBox.SelectedItem;
            if (selectedProfile != null)
            {
                string newProfileName = selectedProfile.ToString();
                if (newProfileName == "None")
                {
                    this.settingsManager.setSettingsActiveProfile("");
                    this.applicationContext.activeProject = false;
                    this.applicationContext.SymfonyInstalled = false;
                    this.applicationContext.SymfonyVersionFound = false;
                    this.applicationContext.SymfonyVersion = "No Project Selected";
                    this.activeProfile = null;
                }
                else
                {
                    //get the new selected project
                    this.settingsManager.setSettingsActiveProfile(newProfileName);
                    this.applicationContext.activeProject = true;
                    this.applicationContext.SymfonyInstalled = false;
                    this.applicationContext.SymfonyVersionFound = false;
                    this.activeProfileName = newProfileName;
                    this.activeProfile = selectedProfile;
                }
            }
            this.swapStartupTabs();
        }

        public void syncProfileWithDataContext(bool empty = false)
        {
            if (!empty)
            {
                this.applicationContext.ProjectName = this.activeProfileName;
                this.applicationContext.RootDirectory = this.activeProfile.documentRoot;
                this.commandConsole.initSymfony();
            }
            else
            {
                this.applicationContext.ProjectName = "";
                this.applicationContext.RootDirectory = "";
                this.applicationContext.SymfonyInstalled = false;
            }
        }

        public void reloadProfiles()
        {
            this.profiles = this.settingsManager.getSettingsProfiles();
            
            this.activeProfileName = this.settingsManager.getSettingsActiveProfile();
            if(!string.IsNullOrEmpty(this.activeProfileName))
            {
                this.activeProfile = this.settingsManager.getSettingsProfile(this.activeProfileName);
                this.applicationContext.activeProject = true;
                this.syncProfileWithDataContext();
                this.commandConsole.initSymfony();
                
            }
            if(this.profiles != null)
            {
                IList<Profile> profileList = new List<Profile>();
                Profile tmpProfile = new Profile();
                tmpProfile.setName("None");
                profileList.Add(tmpProfile);
                foreach (KeyValuePair<string, Profile> profile in this.profiles)
                {
                        tmpProfile = this.settingsManager.getSettingsProfile(profile.Value.getName());
                        profileList.Add(tmpProfile);
                }
                this.applicationContext.Profiles = new CollectionView(profileList);
                this.applicationContext.Profile = this.activeProfileName;
                selectedProjectComboBox.ItemsSource = this.applicationContext.Profiles;
                selectedProjectComboBox.Text = this.activeProfileName;
            }
            else
            {
                this.profiles = new Dictionary<string, Profile>();
            }
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as CloseableTab;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }


        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            var tabItemTarget = e.Source as CloseableTab;

            var tabItemSource = e.Data.GetData(typeof(CloseableTab)) as CloseableTab;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabControl = tabItemTarget.Parent as TabControl;
                int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);
            }
        }

        //Commands

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.newProjectForm(sender);
        }

        private void PreferencesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.applicationContext.activeProject)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void PreferencesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.projectPreferencesForm(sender);
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
