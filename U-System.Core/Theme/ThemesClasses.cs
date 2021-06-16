using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Theme
{
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
        public ThemeColorPallete ColorPalleteLight { get; set; }
        public ThemeColorPallete ColorPalleteDark { get; set; }

    }
    public class WindowColor
    {
        
    }
}
