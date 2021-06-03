
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using U_System;
using U_System.Core;

//using Microsoft.Data.SqlClient;

namespace U_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Debug.Log.LogMessage("Start Application", this.GetType());
            Core.Core.Inicialize(this);
            Debug.Log.LogMessage("Core Load", this.GetType());
        }
    }
}
