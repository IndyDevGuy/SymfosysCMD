using SymfosysCMD.Framework;
using System.Collections.Generic;
using System.Windows.Data;

namespace SymfosysCMD.DataContext
{
    public class ApplicationDataContext : ViewModel
    {
        public ApplicationDataContext()
        {
            IList<Profile> list = new List<Profile>();
            _profiles = new CollectionView(list);
        }
        private CollectionView _profiles;
        private string _profile;

        public CollectionView Profiles
        {
            get { return _profiles; }
            set
            {
                _profiles = value;
                SetProperty(ref _profiles, value);
            }
        }

        public string Profile
        {
            get { return _profile; }
            set
            {
                if (_profile == value) return;
                SetProperty(ref _profile, value);
            }
        }

        private string _projectName;

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                SetProperty(ref _projectName, value);
            }
        }

        private string _symfonyVersion;

        public string SymfonyVersion
        {
            get
            {
                return _symfonyVersion;
            }
            set
            {
                SetProperty(ref _symfonyVersion, value);
            }
        }

        private string _rootDirectory;

        public string RootDirectory
        {
            get
            {
                return _rootDirectory;
            }
            set
            {
                SetProperty(ref _rootDirectory, value);
            }
        }
        private bool _symfonyVersionFound;
        public bool SymfonyVersionFound
        {
            get
            {
                return _symfonyVersionFound;
            }
            set
            {
                SetProperty(ref _symfonyVersionFound, value);
            }
        }
        private bool _symfonyInstalled;

        public bool SymfonyInstalled
        {
            get
            {
                return _symfonyInstalled;
            }
            set
            {
                SetProperty(ref _symfonyInstalled, value);
            }
        }

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

        private bool _activeProject;

        public bool activeProject
        {
            get => _activeProject;
            set
            {
                SetProperty(ref _activeProject, value);
            }
        }


    }
}
