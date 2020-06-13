using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymfosysCMD.Controls
{
    /// <summary>
    /// Interaction logic for ProjectInformationControl.xaml
    /// </summary>
    public partial class ProjectInformationControl : UserControl
    {
        public static readonly DependencyProperty SymfonyVersionProperty = DependencyProperty.Register("SymfonyVersion", typeof(string), typeof(ProjectInformationControl), new PropertyMetadata(null));
        public string SymfonyVersion
        {
            get { return (string)this.GetValue(SymfonyVersionProperty); }
            set { this.SetValue(SymfonyVersionProperty, value); }
        }
        public ProjectInformationControl()
        {
            InitializeComponent();
        }
    }
}
