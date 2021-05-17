using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            public SolidColorBrush APP_NAVBAR_MENU_BACKGROUND { get => (SolidColorBrush)ThemeResourceDictionary["APP.NAVBAR.MENU.BACKGROUND"]; set => ThemeResourceDictionary["APP.NAVBAR.MENU.BACKGROUND"] = value; }
            public SolidColorBrush APP_TABCONTROL_BACKGROUND { get => (SolidColorBrush)ThemeResourceDictionary["APP.TABCONTROL.BACKGROUND"]; set => ThemeResourceDictionary["APP.TABCONTROL.BACKGROUND"] = value; }
            public SolidColorBrush APP_TABCONTROL_ITEM_BACKGROUND { get => (SolidColorBrush)ThemeResourceDictionary["APP.TABCONTROL.ITEM.BACKGROUND"]; set => ThemeResourceDictionary["APP.TABCONTROL.ITEM.BACKGROUND"] = value; }
        }

        public static ThemeColores[] Themes { get; set; }
        public static void Inicialize()
        {
            ThemeResourceDictionary = new ResourceDictionary();
            ThemeResourceDictionary.Source = new Uri("/U-System.Core;component/Styles/Colores.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(ThemeResourceDictionary);
            //_THEME = DarkTheme();
            LoadTheme();
        }

        private static THEME DarkTheme()
        {
            THEME _theme = new THEME();

            //_theme.APP_MAIN_COLOR = new SolidColorBrush(Color.FromRgb(32, 32, 32));
            //_theme.APP_NAVBAR_BTN_FOREGROUND = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            //_theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH = new SolidColorBrush(Color.FromRgb(60, 60, 60));
            //_theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            //_theme.APP_TABCONTROL_BACKGROUND = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            //_theme.APP_TABCONTROL_ITEM_BACKGROUND = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            return _theme;
        }

        internal static void LoadTheme()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            Themes = new ThemeColores[] { new ThemeColores() { Name = "Dark", Colores = new Colores() { APP_MAIN_COLOR = "#FF323232" } } };
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/theme.json");
            Themes = JsonSerializer.Deserialize<ThemeColores[]>(json, options);
            ApplyTheme(Themes[0]);
            
            //if(File.Exists(Directory.GetCurrentDirectory()))


        }
        internal static THEME ApplyTheme(ThemeColores themeColores)
        {
            THEME theme = new THEME();
            theme.APP_MAIN_COLOR = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_MAIN_COLOR);

            _THEME = theme;
            return _THEME;
        }
    }
}
