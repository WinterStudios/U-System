using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace U_System.Styles
{
    public partial class TabControlStyles
    {
        private void tab_Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TabItem item)
            {
                var tabControl = (TabControl)item.Parent;
                tabControl.Items.Remove(item);
                Console.WriteLine("re");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
