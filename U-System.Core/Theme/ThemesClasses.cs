using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Theme
{
    public enum ThemeMode { Light = 0, Dark = 1}

    public class ThemeColorPallete
    {
        public string Primary { get; set; }
        public string PrimaryVariant { get; set; }

        public string Secondary { get; set; }
        public string SecondaryVariant { get; set; }

        public string Background { get; set; }
        public string Surface { get; set; }
        public string Error { get; set; }
    }
    public class ThemeColor
    {
        public ThemeMode ThemeMode { get; set; }
        public ThemeColorPallete ColorPallete { get; set; }
        
    }
    public class WindowColor
    {
        
    }
}
