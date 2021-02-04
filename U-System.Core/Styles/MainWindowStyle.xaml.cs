using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace U_System.Core.Styles
{
    public partial class MainWindowStyle
    {
        
        private void titleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void window_border_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Window window = (Window)((Grid)sender).TemplatedParent;
            WindowChrome chrome = WindowChrome.GetWindowChrome(window);
            if(window.WindowState == WindowState.Maximized)
            {
                //chrome.ResizeBorderThickness = new Thickness(8);
                //WindowChrome.SetWindowChrome(window, chrome);
            }
        }
    }
}