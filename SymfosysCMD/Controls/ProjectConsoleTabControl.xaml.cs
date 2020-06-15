using SymfosysCMD.Console;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SymfosysCMD.Controls
{
    /// <summary>
    /// Interaction logic for ProjectConsoleTabControl.xaml
    /// </summary>
    public partial class ProjectConsoleTabControl : UserControl, INotifyPropertyChanged
    {
        private bool _callingCommand;

        public SymfosysConsole symfosysConsole;
        public bool CallingCommand { get { return this._callingCommand; } set { this._callingCommand = value; OnPropertyChanged("CallingCommand"); } }
        public ProjectConsoleTabControl()
        {
            this.CallingCommand = false;
            DataContext = this;
            InitializeComponent();
        }

        public void CallCommand(object sender, RoutedEventArgs e)
        {
            if(!this.CallingCommand)
                this.symfosysConsole.callCommand();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
