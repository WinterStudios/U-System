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
        public static string ThemeFile { get => Directory.GetCurrentDirectory() + "/theme.json"; }
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
            if (!File.Exists(ThemeFile))
                LoadDefault();
            else
            {
                try
                {
                    JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
                    string json = File.ReadAllText(ThemeFile);
                    Themes = JsonSerializer.Deserialize<ThemeColores[]>(json, serializerOptions);
                    ApplyTheme(Themes[Settings.SettingsSystem.Setting.Theme]);

                    //Settings.SettingsSystem.Setting.Theme = 0;
                    //Settings.SettingsSystem.Save();
                }
                catch { }
            }
        }
        internal static void LoadDefault()
        {
            if (Themes == null)
            {
                Themes = new ThemeColores[] { 
                    new ThemeColores() { Name = "Dark", 
                        Colores = new Colores() { 
                            APP_MAIN_COLOR = "#FF323232",
                            APP_NAVBAR_BTN_FOREGROUND = "#FFDCDCDC",
                            APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH = "#FF3C3C3C",
                            APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED = "#FFFF0000",
                            APP_TABCONTROL_BACKGROUND = "#FF282828",
                            APP_TABCONTROL_ITEM_BACKGROUND = "#FF282828"

                        } } };
                Save();
                ApplyDefaultTheme();
            }
        }
        internal static void Save()
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(Themes, serializerOptions);
            File.WriteAllText(ThemeFile, json);
        }
        internal static THEME ApplyTheme(ThemeColores themeColores)
        {
            THEME theme = new THEME();
            theme.APP_MAIN_COLOR = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_MAIN_COLOR);
            theme.APP_NAVBAR_BTN_FOREGROUND = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_NAVBAR_BTN_FOREGROUND);
            theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH);
            theme.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_NAVBAR_BTN_FOREGROUND_HIGHLIGH_RED);
            theme.APP_TABCONTROL_BACKGROUND = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_TABCONTROL_BACKGROUND);
            theme.APP_TABCONTROL_ITEM_BACKGROUND = (SolidColorBrush)new BrushConverter().ConvertFrom(themeColores.Colores.APP_TABCONTROL_ITEM_BACKGROUND);
            _THEME = theme;
            return _THEME;
        }
        internal static void ApplyDefaultTheme() => ApplyTheme(Themes[0]);

    }
}
