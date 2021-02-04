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
        internal static TabControl UX_Control { get; set; }

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

        public static void Add(TabItem tabItem, bool selectTab = true) => AddTab(tabItem, selectTab);

        private static void AddTab(TabItem tab, bool selectTab = true)
        {
            if (tab.Content == null)
                return;
            TabItem[] tabs = UX_Control.Items.Cast<TabItem>().ToArray();
            bool exists = false;
            for (int i = 0; i < tabs.Length; i++)
            {
                Type currentType = tabs[i].Content.GetType();
                Type tabType = tab.Content.GetType();
                if (currentType == tabType)
                {
                    exists = true;
                    break;
                }
            }
            if(exists)
                UX_Control.SelectedItem = tab;
            else
            {
                UX_Control.Items.Add(tab);
                UX_Control.SelectedItem = tab;
            }
        }

        internal static void Select(TabItem tabItem)
        {
            UX_Control.SelectedItem = tabItem;
        }
    }
}
