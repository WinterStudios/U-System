using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Release
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("tag_name")]
        public string Tag { get; set; }
        public bool PreRelease { get; set; }
        public Asset[] Assets { get; set; }


        #region Outside of GitHub

        #endregion
    }

}
namespace U_System.API.GitHub.Extensions
{
    internal static class ReleaseExtensios
    {
        internal async static Task<Release[]> GetReleases(this Repository repository)
        {
            return await GitHub.GetReleasesAsync(repository);
        }
        internal static string[] GetTags(this Release[] releases)
        {
            string[] tags = new string[releases.Length];
            for (int i = 0; i < releases.Length; i++)
            {
                tags[i] = releases[i].Tag;
            }
            return tags;
        }
        internal static Plugins.PluginRelease[] GetReleases(this Release[] releases)
        {
            Plugins.PluginRelease[] pluginReleases = new Plugins.PluginRelease[releases.Length];
            for (int i = 0; i < releases.Length; i++)
            {
                pluginReleases[i] = new Plugins.PluginRelease();
                pluginReleases[i].PreRelease = releases[i].PreRelease;
                pluginReleases[i].ReleaseID = releases[i].ID;
                pluginReleases[i].ReleaseTag = releases[i].Tag;
                //pluginReleases[i].AssetID = releases[i].Assets.FirstOrDefault(x => x.Name.EndsWith(".zip")).ID;
            }
            return pluginReleases;
        }

        internal static Plugins.PluginRelease ToPluginRelease(this Release release)
        {
            Plugins.PluginRelease pluginRelease = new Plugins.PluginRelease();
            pluginRelease.PreRelease = release.PreRelease;
            pluginRelease.ReleaseID = release.ID;
            pluginRelease.ReleaseTag = release.Tag;

            return pluginRelease;
        }
    }
    
}
