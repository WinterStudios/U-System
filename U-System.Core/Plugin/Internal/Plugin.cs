using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using U_System.Core.Extensions;
using U_System.External.GitHub.Internal;
using U_System.External.Plugin;

namespace U_System.Core.Plugin.Internal
{
    public class Plugin
    {
        public int ID { get => id; set { id = value; if (PluginUX == null) PluginUX = new PluginUX(); PluginUX.PluginID = value;} }
        public string Name { get => name; set { name = value; PluginUX.Name = value; } }
        public string Description { get => description; set { description = value; PluginUX.Description = value; } }
        internal PluginUX PluginUX { get => pluginUX; set { pluginUX = value; pluginUX.Plugin = this; } }
        public int GitHubRepositoryID { get; set; }
        public string GitHubRepositoryURL { get; set; }
        public int CurrentReleaseID { get => currentPluginReleaseID; set { currentPluginReleaseID = value; PluginUX.ReleaseIndex = value; } } 
        public PluginRelease CurrentPluginRelease { get => currentPluginRelease; set { currentPluginRelease = value; PluginUX.CurrentPluginRelease = value; } }
        public bool CheckUpdates { get => checkUpdates; set { checkUpdates = value; } }
        public bool AutomaticUpdate { get;  set; }
        public bool AllowPreview { get; set; }
        public string[] Runtimes { get; set; }



        internal PluginRelease[] PluginReleases { get => pluginReleases; set { pluginReleases = value; PluginUX.Releases = value; } }
        internal Repository GitHubRepository { get; set; }
        internal Release[] Releases { get => releases; set { releases = value; GitHubRepository.Releases = value; } }
        internal AssemblyLoadContext Assembly { get => assembly; set => assembly = value; }
        internal Module[] Modules { get => modules; set => modules = value; }
        internal TabItem[] Tabs { get => tabItems; set => tabItems = value; }
        internal MenuItem[] MenuItems { get => menuItems; set => menuItems = value; }
        internal bool Working { get => working; set { working = value; PluginUX.IsWorking = value; } }

        private int id;
        private string name;
        private string description;
        private bool working;
        private Release[] releases;
        private PluginUX pluginUX;
        private int currentPluginReleaseID;
        private PluginRelease currentPluginRelease;
        private PluginRelease[] pluginReleases;
        private AssemblyLoadContext assembly;
        private Module[] modules;
        private TabItem[] tabItems;
        private MenuItem[] menuItems;
        private bool automaticUpdate;
        private bool checkUpdates;
        private int trie;

        internal void TryLoadRuntime()
        {

            foreach (var item in Runtimes)
            {
                try
                {
                    Assembly.LoadFromAssemblyPath(Storage.PLUGINS.PLUGIN_DIRECTORY + item);
                }
                catch { }
            }
        }
    }
}
