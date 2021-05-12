using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace U_System.UX
{
    public class NotificationBar
    {
        public static StatusBar StatusBar { get; private set; }
        public static ProgressBar Progress { get; private set; }
        public static void Inicialize()
        {
            
        }

        public class ProgressInformation
        {
            /// <summary>
            /// Set Value on progress bar
            /// </summary>
            /// <remarks>Value must be between 0 and 1</remarks>
            /// <param name="percentage">Percentage of process</param>
            public static void SetValue(double percentage)
            {
                if (percentage > 1 && percentage < 0)
                    Progress.Value = 0;
            }
            public static void Exit() { }
        }
    }
}
