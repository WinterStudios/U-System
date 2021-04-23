using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace U_System.Core.Theme
{
    public enum ThemeColor
    {
        Light = 0,
        Dark = 1
    }
    public static class Colores
    {

        public static SolidColorBrush _WINDOW_NAVBAR_ACTIVE { get => (SolidColorBrush)System.Windows.SystemParameters.WindowGlassBrush; }
        public static SolidColorBrush _WINDOW_NAVBAR_DEACTIVE { get => Brushes.White; }

        public static SolidColorBrush APP_WINDOWBAR_MAIN { get => new SolidColorBrush(); }
    }
}
