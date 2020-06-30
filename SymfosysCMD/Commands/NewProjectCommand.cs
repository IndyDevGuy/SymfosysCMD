using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class NewProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ApplicationDataContext _viewModel;
        public MainWindow mainWindow;
        public NewProjectCommand(ApplicationDataContext viewModel)
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
            this.mainWindow.windowManager.initNewProjectDialog();
        }
    }
}
