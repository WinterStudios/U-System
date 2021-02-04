using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace U_System.Core.Styles
{
    public partial class MainWindowStyle
    {
        private void titleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Window window = (Window)((Border)sender).TemplatedParent;

                if (window.WindowState == WindowState.Maximized)
                {
                    
                    Point mousePos = e.GetPosition(window);
                    if(mousePos.Y == 0) { }
                    window.WindowState = WindowState.Normal;
                    window.Top = mousePos.Y - 8;
                }
                window.DragMove();
            }
        }
    }
}
