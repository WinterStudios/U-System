using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace U_System.Core.UX
{
    public class MenuSystem
    {
        public static Menu MainNavigation { get; set; }
        public static MenuItem Add(string path)
        {
            string[] hierarchy = path.Split('>');
            int level = 0;
            MenuItem[] item = MainNavigation.Items.Cast<MenuItem>().ToArray();
            MenuItem parent = null;
            for (int i = 0; i <= item.Length; i++)
            {
                if (level >= hierarchy.Length)
                    break;

                if (item.Length == 0)
                {
                    if (level == 0)
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        MainNavigation.Items.Add(menuItem);
                        parent = menuItem;
                    }
                    else
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        parent.Items.Add(menuItem);
                        parent = menuItem;
                    }
                    i = -1;
                    level++;
                }
                if ((string)item[i].Header == hierarchy[level])
                {
                    parent = item[i];
                    item = item[i].Items.Cast<MenuItem>().ToArray();
                    i = -1;
                    level++;
                }
                /// -> cria os item quando não os encontra no parent actual
                if (i == (item.Length - 1))
                {
                    /// --> Create the parent item on main menu
                    if (level == 0)
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        MainNavigation.Items.Add(menuItem);
                        parent = menuItem;
                    }

                    /// --> Cria os item 
                    else
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        parent.Items.Add(menuItem);
                        parent = menuItem;
                    }
                    i = -1;
                    level++;
                }
            }

            return parent;
        }
        private static MenuItem Create(string header)
        {
            MenuItem item = new MenuItem();
            item.Header = header;

            return item;
        }

        /// <summary>
        /// Not Finish
        /// </summary>
        /// <param name="path"></param>
        /// 
        [Obsolete("Dont use this one")]
        internal static void Remove(string path)
        {
            string[] header = path.Split('>');
        }
        public static void Remove(MenuItem[] items)
        {
            if (items == null || items.Length < 1)
                return;

            for (int i = 0; i < items.Length; i++)
            {
                MenuItem parent = (MenuItem)items[i].Parent;
                parent.Items.Remove(items[i]);
            }
        }
    }
}
