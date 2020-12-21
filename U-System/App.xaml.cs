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

            

            MainWindow window = new MainWindow();
            window.Loaded += (sender, arg) => LoadAPI();
            window.Show();
            

            

            //API.Plugins.PluginSystem.InitializeComponent();
            


        }

        private void LoadAPI()
        {
            API.Navigation.MenuBar.Add("File>D", null);
        }
    }
}
