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
    }
}
