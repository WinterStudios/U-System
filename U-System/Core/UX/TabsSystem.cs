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
            UX_Control.Items.Remove(tab);
        }

        internal static void Add(TabItem tabItem)
        {
            throw new NotImplementedException();
        }

        internal static void Select(TabItem tabItem)
        {
            throw new NotImplementedException();
        }
    }
}
