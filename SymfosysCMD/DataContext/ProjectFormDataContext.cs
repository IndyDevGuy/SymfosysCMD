using SymfosysCMD.Validation;
using SymfosysCMD.Controls;
using System.ComponentModel;

namespace SymfosysCMD.DataContext
{
    public class ProjectFormDataContext : INotifyPropertyChanged
    {
        public ProjectFormControl projectForm;
        public ProjectFormDataContext(ProjectFormControl projectFormControl)
        {
            this.projectForm = projectFormControl;
        }
        public string ProjectName { get; set; }
        public string CrudType { get; set; }
        private bool _hasDirectory;
        public bool hasDirectory
        {
            get { return _hasDirectory; }
            set
            {
                _hasDirectory = value;
                OnPropertyChanged("hasDirectory");
            }
        }
        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                this.projectForm.projectDirectory.Text = value;
                this.hasDirectory = true;
                bool valid = Validator.IsValid(this.projectForm);
            }
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
