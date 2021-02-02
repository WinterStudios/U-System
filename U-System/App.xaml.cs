using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using U_System;

namespace U_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            //API.Paths.StorageFolder = Paths.Storage.AppDataLocation; 

            Settings.Load();
            MainWindow window = new MainWindow();
            window.Show();

            Core.Plugin.PluginManager.Inicialize();

            //API.Plugins.PluginSystem.InitializeComponent();
            //API.Paths.Settings.CreateDirectory();

            //window.Loaded += (sender, arg) => LoadAPI();

            //API.Plugins.PluginSystem.EnablePlugins();
            //API.Plugins.PluginSystem.Load();
            //API.Plugins.PluginSystem.EnablePlugins();





        }
    }
}
