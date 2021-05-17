using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace U_System.Core.Theme
{
    public class ThemeColores
    {
        public string Name { get; set; }
        public Colores Colores { get; set; }
    }
    [Serializable]
    public struct Colores
    {
        public string APP_MAIN_COLOR { get; set; }
    }
}
