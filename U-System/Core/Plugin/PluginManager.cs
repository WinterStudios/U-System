using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using U_System.Core.Plugin.Internal;


namespace U_System.Core.Plugin
{
    public static class PluginManager
    {
        public static List<Internal.Plugin> Plugins { get; set; }
        public static List<Internal.PluginUX> PluginUXs { get; set; }



        public static void Inicialize()
        {
            Plugins = new List<Internal.Plugin>();
            PluginUXs = new List<Internal.PluginUX>();
        }

        internal async static void Add()
        {
            Internal.Plugin plugin = new Internal.Plugin();
            PluginUX pluginUX = new PluginUX();
            plugin.PluginUX = pluginUX;
            PluginUXs.Add(pluginUX);
            Plugins.Add(plugin);

            await Task.Run(() => Thread.Sleep(1000));

            plugin.ID = Plugins.Count;
            plugin.Name = "Teste Plugin";

            

            

            //plugin.ReleaseActive = plugin.PluginReleases[0];

        }
    }
}
