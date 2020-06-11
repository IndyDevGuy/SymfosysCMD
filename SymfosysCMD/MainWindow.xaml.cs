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

        public MainWindow()
        {
            this.profiles = new Dictionary<string, Profile>();
            this.settingsManager = new SettingsManager();
            string themeType = this.settingsManager.getSettingsTheme();
            if (themeType == "dark")
                this.IsDark = true;
            else
                this.IsDark = false;
            
            AdonisUI.SpaceExtension.SetSpaceResourceOwnerFallback(this);
            this.commandConsole = new CommandConsole();
            InitializeComponent();
            this.reloadProfiles();
            DataContext = new ApplicationDataContext();
            php_version.Text = "PHP Version: " + this.commandConsole.phpVersion;
            if(this.activeProfile != null)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = "Project Information";
                this.consoleTabControl.Items.Add(tabItem);
            }
            else
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = "Startup";
                StartupUserControl startupUserControl = new StartupUserControl();
                tabItem.Content = startupUserControl;
                this.consoleTabControl.Items.Add(tabItem);
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

        private class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                //generates the text shown in the combo box
                return Name;
            }
        }

        public void reloadProfiles()
        {
            this.profiles = this.settingsManager.getSettingsProfiles();
            this.activeProfileName = this.settingsManager.getSettingsActiveProfile();
            if(this.activeProfileName != null)
            {
                this.activeProfile = this.settingsManager.getSettingsProfile(this.activeProfileName);
                Item item = new Item(this.activeProfile.getName(), this.activeProfile.getName());
                selectedProjectComboBox.Items.Add(item);
                selectedProjectComboBox.SelectedItem = item;
            }
            if(this.profiles != null)
            {
                foreach (KeyValuePair<string, Profile> profile in this.profiles)
                {
                    if (profile.Value.getName() != this.activeProfileName)
                    {
                        selectedProjectComboBox.Items.Add(new Item(profile.Value.getName(), profile.Value.getName()));
                    }
                }
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
            e.CanExecute = true;
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
