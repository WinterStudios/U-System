using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace U_System.Core.Theme
{
    public class ThemeSystem
    {
        public static ResourceDictionary ThemeResourceDictionary { get; set; }
        public enum ThemeColor
        {
            Light,
            Dark
        }

        public static ThemeColor _ThemeColor { get; set; } = ThemeSystem.ThemeColor.Dark;
        public static THEME _THEME { get; set; }
        public struct THEME
        {
            public SolidColorBrush APP_MAIN_COLOR { get => (SolidColorBrush)ThemeResourceDictionary["APP.MAINCOLOR"]; set => ThemeResourceDictionary["APP.MAINCOLOR"] = value; }
            public SolidColorBrush APP_NAVBAR_BTN_FOREGROUND { get => (SolidColorBrush)ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND"]; set => ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND"] = value; }
            public SolidColorBrush APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH { get => (SolidColorBrush)ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND.HIGHLIGH"]; set => ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND.HIGHLIGH"] = value; }
            public SolidColorBrush APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED { get => (SolidColorBrush)ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND.HIGHLIGH.RED"]; set => ThemeResourceDictionary["APP.NAVBAR.BTN.FOREGROUND.HIGHLIGH.RED"] = value; }
            public SolidColorBrush APP_NAVBAR_MENU_BACGROUND { get => (SolidColorBrush)ThemeResourceDictionary["APP.NAVBAR.MENU.BACKGROUND"]; set => ThemeResourceDictionary["APP.NAVBAR.MENU.BACKGROUND"] = value; }
        }       
        public static void Inicialize()
        {
            ThemeResourceDictionary = new ResourceDictionary();
            ThemeResourceDictionary.Source = new Uri("/U-System.Core;component/Styles/Colores.xaml", UriKind.Relative);
            _THEME = DarkTheme();
        }

        private static THEME DarkTheme()
        {
            THEME _theme = new THEME();

            _theme.APP_MAIN_COLOR = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            _theme.APP_NAVBAR_BTN_FOREGROUND = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            _theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH = new SolidColorBrush(Color.FromRgb(60, 60, 60));
            _theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return _theme;
        }
    }
}
