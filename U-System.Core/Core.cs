﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using U_System;
using U_System.Core.Extensions;
using U_System.Core.Plugin;
using U_System.Core.Settings;
using U_System.Core.UX;
using U_System.Debug;
using U_System.UX;

namespace U_System.Core
{
    public class Core
    {
        private static Application Application { get; set; }
        public static void Inicialize(Application application)
        {
            Debug.Log.LogMessage("", typeof(Core));
            Application = application;
            SettingsSystem.Initialize();
            //Theme.ThemeSystem.Inicialize();
            U_System.UX.ThemeSystem.Initialize();

            Debug.Log.LogMessage("Starting creating the main Window", typeof(Core));
            MainWindow window = new MainWindow();
            Debug.Log.LogMessage("Window is ready", typeof(Core));
            window.Resources.MergedDictionaries.Add(U_System.UX.ThemeSystem.ThemeResourceDictionary);
            Plugin.PluginManager.Inicialize();
            Debug.Log.LogMessage("Show Main Window", typeof(Core));
            window.Show();
            
        }
    }
}
