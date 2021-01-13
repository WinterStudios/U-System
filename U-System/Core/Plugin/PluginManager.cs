using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using U_System.Core.Extensions;
using U_System.Core.Plugin.Internal;

using U_System.External;
using U_System.External.Plugin;
using U_System.External.GitHub;
using U_System.External.GitHub.Internal;
using System.Windows.Controls;

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

        internal async static void Add(Repository repository)
        {
            Internal.Plugin plugin = new Internal.Plugin();
            PluginUX pluginUX = new PluginUX();
            plugin.PluginUX = pluginUX;
            PluginUXs.Add(pluginUX);
            Plugins.Add(plugin);

            await Task.Run(() => Thread.Sleep(1000));

            plugin.ID = Plugins.Count-1;
            plugin.Name = repository.Name;
            plugin.Description = repository.Description;

            plugin.GitHubRepository = repository;
            plugin.GitHubRepositoryID = repository.ID;
            plugin.GitHubRepositoryURL = repository.URL;

            await GetReleases(plugin.ID);

            plugin.CurrentReleaseID = GetStableReleaseID(plugin.GitHubRepository.Releases);
            plugin.PluginReleases = plugin.Releases.ToPluginRelease();
            plugin.CurrentPluginRelease = plugin.PluginReleases[plugin.CurrentReleaseID];
            //plugin.ReleaseActive = plugin.PluginReleases[0];
        }



        internal static async Task GetReleases(int pluginID)
        {
            if (pluginID >= Plugins.Count)
                return;
            if (Plugins[pluginID].GitHubRepository == null)
                Plugins[pluginID].GitHubRepository = await GitHubClient.GetRepositoryAsync(Plugins[pluginID].GitHubRepositoryID);

            Plugins[pluginID].Releases = await GitHubClient.GetReleasesAsync(Plugins[pluginID].GitHubRepository);
        
        }
        internal static async void Install(int pluginID)
        {
            Internal.Plugin plugin = Plugins[pluginID];
            plugin.Working = true;

            Release release = plugin.GitHubRepository.Releases[plugin.CurrentReleaseID];

            Asset pluginAsset = release.Assets.First(x => x.Name == "Release.zip");
            if (pluginAsset == null)
                return;

            Stream stream = await GitHubClient.GetReleaseAssetAsync(pluginAsset.DownloadURL);
            string pluginPath = Storage.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", plugin.Name, release.Tag);
            FileSystem.SaveStreamToFile(stream, pluginPath);


            ZipArchive zip = ZipFile.OpenRead(pluginPath);
            string name = zip.Entries.First(x => x.Name.EndsWith(".dll")).Name;
            plugin.CurrentPluginRelease.ReleaseZipFile = pluginPath.Replace("\\", "/");
            plugin.CurrentPluginRelease.AssetID = pluginAsset.ID;
            zip.ExtractToDirectory(Storage.PLUGINS.PLUGIN_DIRECTORY, true);

            string[] files = new string[zip.Entries.Count];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Storage.PLUGINS.PLUGIN_DIRECTORY.Replace("\\", "/") + zip.Entries[i].FullName;
            }

            plugin.CurrentPluginRelease.PluginFilesLocation = files;
            plugin.CurrentPluginRelease.IsInstalled = true;
            plugin.CurrentPluginRelease.Enable = true;
            Save();
            Enable();

            
        }
        private async static void Enable(int pluginID)
        {
            Internal.Plugin plugin = Plugins[pluginID];
            AssemblyLoadContext temp = new AssemblyLoadContext(plugin.Name, true);
            temp.Unloading += (alc) =>
            {
                GC.Collect();
            };
            Assembly assembly = temp.LoadFromAssemblyPath(plugin.CurrentPluginRelease.PluginFilesLocation.First(x => x.EndsWith(".dll")));
            plugin.Assembly = temp;

            Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

            plugin.Modules = PluginInfo.Modules;
            plugin.Tabs = new TabItem[plugin.Modules.Length];
            List<MenuItem> menus = new List<MenuItem>();
            for (int i = 0; i < plugin.Modules.Length; i++)
            {
                External.Plugin.Module module = plugin.Modules[i];

                int index = i;
                if (module.PluginTypeBehavior == PluginTypeBehavior.Tab)
                {
                    MenuItem item = UX.MenuSystem.Add(module.Path);
                    item.Click += (sender, arg) =>
                    {
                        if (plugin.Tabs[index] == null)
                        {
                            plugin.Tabs[index] = new TabItem();
                            plugin.Tabs[index].Header = module.Name;
                            Type type = assembly.GetType(module.Type);
                            object content = Activator.CreateInstance(type);
                            plugin.Tabs[index].Content = content;
                            plugin.Tabs[index].DataContext = new object[3] { "_PLUGIN", plugin.ID, index };
                            UX.TabsSystem.Add(plugin.Tabs[index]);
                        }
                        else
                        {
                            UX.TabsSystem.Select(plugin.Tabs[index]);
                        }

                    };
                    menus.Add(item);
                }
            }
            plugin.MenuItems = menus.ToArray();
            plugin.CurrentPluginRelease.Enable = true;
            plugin.Working = false;
            Save();
        }


        private static int GetStableReleaseID(Release[] releases)
        {
            int id = -1;

            for (int i = 0; i < releases.Length; i++)
            {
                if (!releases[i].PreRelease)
                {
                    id = i;
                    break;
                }
            }

            return id;
        }
        private static void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            File.WriteAllText(Storage.SETTINGS.PLUGINS_SETTINGS, JsonSerializer.Serialize(Plugins, options));
        }
    }
}
