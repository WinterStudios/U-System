using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using U_System.API.Plugins;

namespace U_System.API.Navigation
{
    public static class TabSystem
    {
        public static TabControl TabControl { get; set; }
        public static bool Add(TabItem tab)
        {
            if (TabControl == null)
                return false;

            if(!TabControl.Items.Contains(tab))
            TabControl.Items.Add(tab);
            TabControl.SelectedItem = tab;
            return true;
        }

        public static void Remove(TabItem tab)
        {
            TabControl.Items.Remove(tab);
        }

        internal static void Select(TabItem tabItem)
        {
            TabControl.SelectedItem = tabItem;
        }
    }
}
