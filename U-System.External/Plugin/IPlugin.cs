using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using U_System.External;

namespace U_System.External.Plugin
{
    /// <summary>
    /// Interface that gives the program a entry point and inicialize the plugin by givin information
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Descript what the plugin does
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Plugin Version
        /// </summary>
        SemVersion Version { get; }
        /// <summary>
        /// Componets of the plugin
        /// </summary>
        Module[] Modules { get; }
        /// <summary>
        /// Show the welcome page on plugin Load
        /// </summary>
        bool ShowWelcomePage { get; }
        /// <summary>
        /// Welcome Page
        /// </summary>
        /// <remarks>ShowWelcomePage has to be true to display the welcome page</remarks>
        Module WelcomePage { get; }

        /// <summary>
        /// Inicialize the plugin at load plugin
        /// </summary>
        void Start();

        /// <summary>
        /// Its Call in async way before Initialization()
        /// </summary>
        void Awake();

        /// <summary>
        /// Location define by Plugin Engine were can be store data
        /// </summary>
        string DataStorage { get; set; }
        PluginInfo PluginInformation { get; set; }
        string[] PluginDependicy { get; }
    }
}
