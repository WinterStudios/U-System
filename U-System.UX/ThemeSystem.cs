using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace U_System.UX
{
    public class ThemeSystem
    {
        public static List<Theme> Themes { get; set; }
        public static Theme CurrentTheme { get =>_currentTheme; set { ApplyTheme(value); _currentTheme = value; } }
        private static Theme _currentTheme;

        public static ResourceDictionary ThemeResourceDictionary { get => _themeResourceDictionary; set => _themeResourceDictionary = value; }
        private static ResourceDictionary _themeResourceDictionary;

        public static void Initialize()
        {
            Themes = new List<Theme>();
            Themes.AddRange(LoadDefaultThemes());
            ThemeResourceDictionary = new ResourceDictionary();
            ThemeResourceDictionary.Source = new Uri("/U-System.UX;component/ThemeResource.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(ThemeResourceDictionary);
            ApplyTheme(Themes[0]);
        }

        public static Theme[] LoadDefaultThemes()
        {
            Theme[] themes = new Theme[2]
            {
                new Theme()
                {
                    Name = "Light",
                    ThemeMode = ThemeMode.Light,
                    ThemeColorPallete = new ThemeColorPallete()
                    {
                        PrimaryColor = "#000000",
                        PrimaryVariantColor = "#212121",

                        BackgroundColor = "#FAFAFA",
                        SurfaceColor = "#EAEAEA"
                    }
                },
                new Theme()
                {
                    Name = "Dark",
                    ThemeMode = ThemeMode.Dark,
                    ThemeColorPallete = new ThemeColorPallete()
                    {
                        PrimaryColor = "#FFFFFF",
                        PrimaryVariantColor = "#DEDEDE",

                        BackgroundColor = "#050505",
                        SurfaceColor = "#212121"
                        
                    }
                }
            };



            return themes;
        }
        public static Theme ApplyTheme() => ApplyTheme(CurrentTheme);
        public static Theme ApplyTheme(Theme theme) 
        {
            ThemeResourceDictionary["Theme.Primary"] = (SolidColorBrush)new BrushConverter().ConvertFrom(theme.ThemeColorPallete.PrimaryColor);
            ThemeResourceDictionary["Theme.PrimaryVariant"] = (SolidColorBrush)new BrushConverter().ConvertFrom(theme.ThemeColorPallete.PrimaryVariantColor);

            ThemeResourceDictionary["Theme.Background"] = (SolidColorBrush)new BrushConverter().ConvertFrom(theme.ThemeColorPallete.BackgroundColor);
            ThemeResourceDictionary["Theme.Surface"] = (SolidColorBrush)new BrushConverter().ConvertFrom(theme.ThemeColorPallete.SurfaceColor);
            return theme;
        }
    }
}
