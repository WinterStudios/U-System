using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.UX
{
    public class ThemeSystem
    {
        public static List<Theme> Themes { get; set; }
        public static void Initialize()
        {
            Themes.AddRange(LoadDefaultThemes());
        }

        public static Theme[] LoadDefaultThemes()
        {
            Theme[] themes = new Theme[]
            {
                new Theme()
                {
                    Name = "Light",
                    ThemeMode = ThemeMode.Light,
                    ThemeColorPallete = new ThemeColorPallete()
                    {

                    }
                },
                new Theme()
                {

                }
            };



            return null;
        }
    }
}
