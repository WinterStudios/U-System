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
            if(sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                TabItem tab = (TabItem)button.TemplatedParent;
                API.Navigation.TabSystem.Remove(tab);

                if (tab.DataContext != null)
                {
                    object[] data = (object[])tab.DataContext;
                    if(data[0] != null && (string)data[0] == "_PLUGIN")
                    {
                        API.Plugins.PluginSystem.RemoveTabFromPlugin((int)data[1], (int)data[2]);
                    }


                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
