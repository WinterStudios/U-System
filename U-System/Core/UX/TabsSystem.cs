using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace U_System.Core.UX
{
    public class TabsSystem
    {
        public static TabControl UX_Control { get; set; }

        public static void Remove(TabItem tab)
        {
            object[] tabInfo = (object[])tab.DataContext;
            if(tabInfo != null && tabInfo.Length > 0)
            {
                if((string)tabInfo[0] == "_PLUGIN")
                {
                    Plugin.PluginManager.Plugins[(int)tabInfo[1]].Tabs[(int)tabInfo[2]] = null;
                }
            }
            UX_Control.Items.Remove(tab);
        }

        internal static void Add(TabItem tabItem)
        {
            UX_Control.Items.Add(tabItem);
            UX_Control.SelectedItem = tabItem;
        }

        internal static void Select(TabItem tabItem)
        {
            UX_Control.SelectedItem = tabItem;
        }
    }
}
