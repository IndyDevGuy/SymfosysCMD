using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class ProjectPreferencesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public MainWindow mainWindow;
        ApplicationDataContext _viewModel;

        public ProjectPreferencesCommand(ApplicationDataContext viewModel)
        {
            this._viewModel = viewModel;
            this.mainWindow = this._viewModel.mainWindow;
        }
        public bool CanExecute(object parameter)
        {
            if(!string.IsNullOrEmpty(this.mainWindow.activeProfileName))
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            this.mainWindow.windowManager.initProjectPreferencesDialog();
        }
    }
}
