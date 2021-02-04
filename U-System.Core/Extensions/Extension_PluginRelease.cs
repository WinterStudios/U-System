using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Extensions
{
    public static class Extension_PluginRelease
    {
        public static Plugin.Internal.PluginRelease ToPluginRelease(this External.GitHub.Internal.Release release)
        {
            Plugin.Internal.PluginRelease pluginRelease = new Plugin.Internal.PluginRelease();

            pluginRelease.ID = release.ID;
            pluginRelease.Name = release.Tag;

            return pluginRelease;
        }
        public static Plugin.Internal.PluginRelease[] ToPluginRelease(this External.GitHub.Internal.Release[] release)
        {
            Plugin.Internal.PluginRelease[] pluginReleases = new Plugin.Internal.PluginRelease[release.Length];
            for (int i = 0; i < release.Length; i++)
            {
                pluginReleases[i] = new Plugin.Internal.PluginRelease();

                pluginReleases[i].ID = release[i].ID;
                pluginReleases[i].Name = release[i].Tag;
            }
            

            return pluginReleases;
        }
    }
}
