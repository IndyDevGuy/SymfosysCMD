using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SymfosysCMD
{
    public class CloseableTab: TabItem
    {
        public int consoleIndex;
        public string profileIndex;
        private MainWindow mainWindow;
        public CloseableTab(MainWindow mainWindow,string profileIndex, int consoleIndex)
        {
            this.mainWindow = mainWindow;
            this.profileIndex = profileIndex;
            this.consoleIndex = consoleIndex;
            //create a instance of the user control
            CloseableHeader closeableHeader = new CloseableHeader();
            //assign the usercontrol to the tab header
            this.Header = closeableHeader;
            // Attach to the CloseableHeader events
            // (Mouse Enter/Leave, Button Click, and Label resize)
            closeableHeader.button_close.MouseEnter += new MouseEventHandler(button_close_MouseEnter);
            closeableHeader.button_close.MouseLeave += new MouseEventHandler(button_close_MouseLeave);
            closeableHeader.button_close.Click += new RoutedEventHandler(button_close_Click);
            closeableHeader.label_TabTitle.SizeChanged += new SizeChangedEventHandler(label_TabTitle_SizeChanged);
        }

        // Button MouseEnter - When the mouse is over the button - change color to Red
        void button_close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Red;
        }
        // Button MouseLeave - When mouse is no longer over button - change color back to black
        void button_close_MouseLeave(object sender, MouseEventArgs e)
        {
            this.changeTheme();
        }
        // Button Close Click - Remove the Tab - (or raise
        // an event indicating a "CloseTab" event has occurred)
        void button_close_Click(object sender, RoutedEventArgs e)
        {
            Profile profile;
            bool foundProfile = this.mainWindow.profiles.TryGetValue(this.profileIndex, out profile);
            if(foundProfile)
                profile.removeSymfosysConsole(this.consoleIndex);
            ((TabControl)this.Parent).Items.Remove(this);
        }
        // Label SizeChanged - When the Size of the Label changes
        // (due to setting the Title) set position of button properly
        void label_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Margin = new Thickness(5, 0, 0, 0);
        }

        public void changeTheme()
        {
            ((CloseableHeader)this.Header).button_close.Foreground = this.mainWindow.IsDark ? Brushes.White : Brushes.Black;
        }
        public string Title
        {
            set
            {
                ((CloseableHeader)this.Header).label_TabTitle.Content = value;
            }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.IsSelected)
            {
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
            }
        }
    }
}
