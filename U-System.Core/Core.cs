using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U_System;

namespace U_System.Core
{
    public class Core
    {
        private static Application Application { get; set; }
        public static void Inicialize(Application application)
        {
            Application = application;
            MainWindow window = new MainWindow();
            window.Show();

            Plugin.PluginManager.Inicialize();
        }
    }
}
