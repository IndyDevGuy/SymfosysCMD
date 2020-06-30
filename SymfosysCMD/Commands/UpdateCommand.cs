using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class UpdateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ApplicationDataContext _viewModel;
        MainWindow mainWindow;

        public UpdateCommand(ApplicationDataContext viewModel)
        {
            this._viewModel = viewModel;
            this.mainWindow = this._viewModel.mainWindow;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.mainWindow.updateManager.CheckForUpdate();
        }
    }
}
