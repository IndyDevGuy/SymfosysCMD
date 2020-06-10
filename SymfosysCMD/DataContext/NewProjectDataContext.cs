using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymfosysCMD.DataContext
{
    public class NewProjectDataContext
    {
        NewProjectWindow newProjectWindow;
        public NewProjectDataContext(NewProjectWindow newProjectWindow)
        {
            this.newProjectWindow = newProjectWindow;
        }
        public string ProjectName { get; set; }
        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set { 
                folderName = value; 
                this.newProjectWindow.projectDirectory.Text = value;
                if(Validation.Validator.IsValid(this.newProjectWindow))
                {
                    //enable the save button
                    this.newProjectWindow.saveButton.IsEnabled = true;
                }
            }
        }
    }
}
