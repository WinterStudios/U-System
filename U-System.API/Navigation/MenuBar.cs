using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace U_System.API.Navigation
{
    public class MenuBar
    {
        public static Menu MainMenu { get; set; }

        //public static void I


        public static void AddPluginAction(Plugins.Module module)
        {
            Type typePlugin = Type.GetType(module.Type);
        }
        public static MenuItem Add(string path)
        {
            string[] hierarchy = path.Split('>');
            int level = 0;
            MenuItem[] item = MainMenu.Items.Cast<MenuItem>().ToArray();
            MenuItem parent = null;
            for (int i = 0; i <= item.Length; i++)
            {
                System.Diagnostics.Trace.WriteLine(i.ToString());
                if (level >= hierarchy.Length)
                    break;

                if (item.Length == 0)
                {
                    if (level == 0)
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        MainMenu.Items.Add(menuItem);
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
                if(i == (item.Length - 1))
                {
                    /// --> Create the parent item on main menu
                    if (level == 0)
                    {
                        MenuItem menuItem = Create(hierarchy[level]);
                        MainMenu.Items.Add(menuItem);
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
        public static MenuItem Create(string header)
        {
            MenuItem item = new MenuItem();
            item.Header = header;

            return item;
        }

        //public static void Remove(string header)
        //{
        //    MenuItem removedItem = null;
        //    foreach (MenuItem item in MainMenu.Items)
        //    {
        //        if (item.Name == header)
        //        {
        //            removedItem = item;
        //            break;
        //        }
        //    }

        //    if (removedItem != null)
        //    {
        //        MainMenu.Items.Remove(removedItem);
        //    }

        
        public static void Remove(string path)
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
        private static void Remove(MenuItem item)
        {

        }

       
    }
}
