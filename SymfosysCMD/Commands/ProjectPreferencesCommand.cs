using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class ProjectPreferencesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged {
            add
            {
                Debug.WriteLine("add");
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }

        }
        public MainWindow mainWindow;
        ApplicationDataContext _viewModel;

        public ProjectPreferencesCommand(ApplicationDataContext viewModel)
        {
            this._viewModel = viewModel;
            this.mainWindow = this._viewModel.mainWindow;
        }
        public void RaiseCanExecuteChanged()
        {
            //if (this.CanExecuteChanged != null)
              //  this.CanExecuteChanged(this, new EventArgs());
        }
        public bool CanExecute(object parameter)
        {
            if(this.mainWindow.activeProfileName != null)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            this.mainWindow.windowManager.initProjectPreferencesDialog();
        }
    }
}
