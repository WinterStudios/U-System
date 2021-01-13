using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using U_System.Core.Extensions;
using U_System.External.GitHub.Internal;

namespace U_System.Core.Plugin.Internal
{
    public class Plugin
    {
        public int ID { get => id; set { id = value; PluginUX.PluginID = value; } }
        public string Name { get => name; set { name = value; PluginUX.Name = value; } }
        public string Description { get => description; set { description = value; PluginUX.Description = value; } }

        public PluginUX PluginUX { get; set; }


        internal Repository GitHubRepository { get; set; }
        public int GitHubRepositoryID { get; set; }
        public string GitHubRepositoryURL { get; set; }

        public int CurrentReleaseID { get => currentPluginReleaseID; set { currentPluginReleaseID = value; PluginUX.ReleaseIndex = value; } } 
        public PluginRelease CurrentPluginRelease { get; set; }
        public PluginRelease[] PluginReleases { get => pluginReleases; set { pluginReleases = value; PluginUX.Releases = value; } }
        internal Release[] Releases { get => releases; set { releases = value; GitHubRepository.Releases = value; } }


        private int id;
        private string name;
        private string description;
        private Release[] releases;
        private int currentPluginReleaseID;
        private PluginRelease[] pluginReleases;
    }
}
