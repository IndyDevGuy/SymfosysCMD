using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class NewProjectCommand : ICommand
    {
        ApplicationDataContext _viewModel;
        public MainWindow mainWindow;
        public event EventHandler CanExecuteChanged;

        public NewProjectCommand(ApplicationDataContext viewModel)
        {
            this._viewModel = viewModel;
            this.mainWindow = this._viewModel.mainWindow;
        }
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, new EventArgs());
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
