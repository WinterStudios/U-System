using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using U_System.Core.UX;

namespace U_System.Core.Styles
{
    public partial class TabControlStyles
    {
        private void tab_Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                TabItem tab = (TabItem)button.TemplatedParent;
                TabsSystem.Remove(tab);
                //API.Navigation.TabSystem.Remove(tab);

                //if (tab.DataContext != null)
                //{
                //    object[] data = (object[])tab.DataContext;
                //    if(data[0] != null && (string)data[0] == "_PLUGIN")
                //    {
                //        API.Plugins.PluginSystem.RemoveTabFromPlugin((int)data[1], (int)data[2]);
                //    }


                //}
            }
        }
    }
}
