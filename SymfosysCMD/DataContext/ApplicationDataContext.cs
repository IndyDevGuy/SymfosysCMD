using SymfosysCMD.Framework;

namespace SymfosysCMD.DataContext
{
    class ApplicationDataContext : ViewModel
    {

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        private bool _isDeveloperMode;

        public bool IsDeveloperMode
        {
            get => _isDeveloperMode;
            set
            {
                SetProperty(ref _isDeveloperMode, value);
            }
        }


    }
}
