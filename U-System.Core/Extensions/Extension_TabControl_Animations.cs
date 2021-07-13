using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace U_System.Core.Extensions
{
    class Extension_TabControl_Animations : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TabControl tabControl = null;
            if(targetType.GetType() == typeof(TabControl))
            {
                int i = U_System.Core.UX.TabsSystem.PreviewsTabSelect;
            }
            
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
