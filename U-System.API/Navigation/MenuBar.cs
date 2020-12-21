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


        public static void AddPluginAction(Plugins.Module module)
        {
            Type typePlugin = Type.GetType(module.Type);
        }
        public static void Add(string path, Action action)
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

                if ((item[i].Items.Count > 0) && ((string)item[i].Header == hierarchy[level]))
                {
                    parent = item[i];
                    item = item[i].Items.Cast<MenuItem>().ToArray();
                    i = -1;
                    level++;
                    
                }
                else
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
            }
        }
        public static MenuItem Create(string header)
        {
            MenuItem item = new MenuItem();
            item.Header = header;
            //item.Click += eventHandler;

            return item;
        }
    }
}
