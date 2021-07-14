using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace U_System.External.UI
{
    public class TabSystem
    {
        public static TabControl TabController { get; private set; }

        public static object SetTabController(object ux_Control)
        {
            if(ux_Control == null || ux_Control.GetType() != typeof(TabControl))
                throw new Exception();  
            else
            {
                TabController = (TabControl)ux_Control;
            }
            return ux_Control;
        }



        //public static void Add(TabItem tabItem, bool selectTab = true) => AddTab(tabItem, selectTab);

        private static void AddTab(TabItem tab, bool selectTab = true)
        {
            if (tab.Content == null)
                return;
            TabItem[] tabs = TabController.Items.Cast<TabItem>().ToArray();
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
            if (exists)
                TabController.SelectedItem = tab;
            else
            {
                TabController.Items.Add(tab);
                TabController.SelectedItem = tab;
            }
        }
        public static TabItem Add(object content, string? header = "Tab", bool selectTab = true, object? icon = null)
        {
            bool exists = CheckContentExist(content);
            if(exists)
            { }
            else
            {
                TabItem tab = new TabItem();
                tab.Content = content;
                tab.Header = header;
                Add(tab, selectTab);
            }
            return null;
        }

        internal static TabItem Add(TabItem tab, bool select)
        {
            TabController.Items.Add(tab);

            if (select)
                TabController.SelectedItem = tab;
            return tab;
        }

        private static bool CheckContentExist(object content)
        {
            bool exits = false;
            for (int i = 0; i < TabController.Items.Count; i++)
            {
                TabItem tab = (TabItem)TabController.Items[i];
                if(tab.Content.GetType() == content.GetType())
                {
                    exits = true;
                    break;
                }
            }

            return exits;
        }

        internal static void Select(TabItem tabItem)
        {
            TabController.SelectedItem = tabItem;
        }
    }
}
