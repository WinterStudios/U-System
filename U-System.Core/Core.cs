using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using U_System;
using U_System.Core.Extensions;
using U_System.Core.Plugin;
using U_System.Core.Settings;
using U_System.Core.Theme;
using U_System.Core.UX;

namespace U_System.Core
{
    public class Core
    {
        private static Application Application { get; set; }
        public static void Inicialize(Application application)
        {
            Application = application;
            SettingsSystem.Inicialize();
            ThemeSystem.Inicialize();

            MainWindow window = new MainWindow();
            window.Resources.MergedDictionaries.Add(Theme.ThemeSystem.ThemeResourceDictionary);
            Plugin.PluginManager.Inicialize();
            window.Show();
            

        }
    }
}
