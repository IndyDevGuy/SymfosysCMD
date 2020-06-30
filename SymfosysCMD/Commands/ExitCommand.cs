using SymfosysCMD.DataContext;
using SymfosysCMD.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace SymfosysCMD.Commands
{
    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public MainWindow mainWindow;
        ApplicationDataContext _viewModel;
        public ExitCommand(ApplicationDataContext viewModel)
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
            Application.Current.Shutdown();
        }
    }
}
