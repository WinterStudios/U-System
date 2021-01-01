using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using U_System;
using U_System.API;

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
            API.Paths.StorageFolder = Paths.Storage.AppDataLocation;
            API.Plugins.PluginSystem.InitializeComponent();

            //API.Paths.Settings.CreateDirectory();
            MainWindow window = new MainWindow();
            //window.Loaded += (sender, arg) => LoadAPI();
            window.Show();
            API.Plugins.PluginSystem.EnablePlugins();
            





        }

        private void LoadAPI()
        {
            API.Plugins.PluginSystem.InitializeComponent();
            API.Navigation.MenuBar.Add("File>D", null);
        }
    }
}
