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
            Load();
        }

        internal async static void Add(Repository repository)
        {
            Internal.Plugin plugin = new Internal.Plugin();
            PluginUX pluginUX = new PluginUX();
            plugin.PluginUX = pluginUX;
            PluginUXs.Add(pluginUX);
            Plugins.Add(plugin);
            plugin.Working = true;

            //await Task.Run(() => Thread.Sleep(1000));

            plugin.ID = Plugins.Count-1;
            plugin.Name = repository.Name;
            plugin.Description = repository.Description;
            plugin.AutomaticUpdate = true;

            plugin.GitHubRepository = repository;
            plugin.GitHubRepositoryID = repository.ID;
            plugin.GitHubRepositoryURL = repository.URL;

            await GetReleases(plugin.ID, false);


            plugin.Working = false;
            //plugin.ReleaseActive = plugin.PluginReleases[0];
        }



        internal static async Task GetReleases(int pluginID, bool load)
        {
            if (pluginID >= Plugins.Count)
                return;
            if (Plugins[pluginID].GitHubRepository == null)
            {
                Plugins[pluginID].GitHubRepository = await GitHubClient.GetRepositoryAsync(Plugins[pluginID].GitHubRepositoryID);
            }
            Plugins[pluginID].Releases = await GitHubClient.GetReleasesAsync(Plugins[pluginID].GitHubRepository);
            Plugins[pluginID].PluginReleases = Plugins[pluginID].Releases.ToPluginRelease();
            if (!load)
            {
                Plugins[pluginID].CurrentReleaseID = GetStableReleaseID(Plugins[pluginID].GitHubRepository.Releases);
                Plugins[pluginID].CurrentPluginRelease = Plugins[pluginID].PluginReleases[Plugins[pluginID].CurrentReleaseID];
            }
            

        }
        internal static async void Install(int pluginID)
        {
            Internal.Plugin plugin = Plugins[pluginID];
            plugin.Working = true;

            Release release = plugin.GitHubRepository.Releases[plugin.CurrentReleaseID];

            Asset pluginAsset = release.Assets.First(x => x.Name == "Release.zip");
            if (pluginAsset == null)
                return;

            Stream stream = await GitHubClient.GetReleaseAssetAsync(pluginAsset.URL);
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
            Enable(pluginID);

            
        }
        private async static void Enable(int pluginID)
        {
            Internal.Plugin plugin = Plugins[pluginID];
            AssemblyLoadContext temp = new AssemblyLoadContext(plugin.Name, true);
            temp.Unloading += (alc) =>
            {
                GC.Collect();
            };
            
            Assembly assembly = temp.LoadFromAssemblyPath(plugin.CurrentPluginRelease.PluginFilesLocation.First(x => x.EndsWith(".dll") && x.Contains(plugin.Name)));

            Internal.PluginDependencies pluginDependencies = JsonSerializer.Deserialize<Internal.PluginDependencies>(File.ReadAllText(plugin.CurrentPluginRelease.PluginFilesLocation.FirstOrDefault(x => x.EndsWith(".json"))));
            
            plugin.Assembly = temp;
            //var ass = AppDomain.CurrentDomain.GetAssemblies();
            //var app = AssemblyLoadContext.All;
            temp.Resolving += (sender, args) =>
            {
                try
                {
                  
                    string d = string.Format("{0}{1}.dll", Storage.PLUGINS.PLUGIN_DIRECTORY, args.Name);

                    //AssemblyLoadContext.Default.LoadFromAssemblyPath(d);
                    
                    System.Diagnostics.Debug.WriteLine(string.Format("[U-SYSTEM].[PluginManager] Load: {0}", args.Name));
                    //Assembly.ReflectionOnlyLoadFrom(d);
                    AssemblyName name = new AssemblyName(args.Name);
                    return temp.LoadFromAssemblyName(name);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("[U-SYSTEM] [PluginManager] [ERROR] -> {0} {1}",ex.Message,args.Name));
                    try
                    {
                        string d = string.Format("{0}{1}.dll", Storage.PLUGINS.PLUGIN_DIRECTORY, args.Name);
                        Assembly.ReflectionOnlyLoad(args.FullName);
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("[U-SYSTEM] [PluginManager] [ERROR] -> {0} {1}", ex2.Message, args.Name));

                    }
                    return null;
                }

            };
            Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);
            try
            {
                for (int i = 0; i < PluginInfo.PluginDependicy.Length; i++)
                {
                    string d = string.Format("{0}{1}.dll", Storage.PLUGINS.PLUGIN_DIRECTORY, PluginInfo.PluginDependicy[i]);
                    temp.LoadFromAssemblyPath(d);
                }
            }
            catch { }
            
            plugin.Modules = PluginInfo.Modules;
            plugin.Tabs = new TabItem[plugin.Modules.Length];
            List<MenuItem> menus = new List<MenuItem>();
            for (int i = 0; i < plugin.Modules.Length; i++)
            {
                External.Plugin.Module module = plugin.Modules[i];

                int index = i;
                if (module.PluginTypeBehavior == PluginTypeBehavior.Tab)
                {
                    MenuItem item = UX.MenuSystem.Add(module.Path, module.Shortcut, module.Icon);
                    item.Click += (sender, arg) =>
                    {
                        if (plugin.Tabs[index] == null)
                        {
                            plugin.Tabs[index] = new TabItem();
                            plugin.Tabs[index].Header = module.Name;
                            Type type = assembly.GetType(module.Type);
                            object content = Activator.CreateInstance(type);
                            plugin.Tabs[index].Content = content;
                            plugin.Tabs[index].DataContext = new object[3] { "_PLUGIN", plugin.ID, index};
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
            if(PluginInfo.ShowWelcomePage && PluginInfo.WelcomePage != null)
            {
                TabItem item = new TabItem();
                item.Header = PluginInfo.WelcomePage.Name;
                Type type = assembly.GetType(PluginInfo.WelcomePage.Type);
                object content = Activator.CreateInstance(type);
                item.Content = content;
                UX.TabsSystem.Add(item);
            }
            PluginInfo.PluginInformation = new PluginInfo()
            {
                PluginStorageData = string.Format("{0}Data\\{1}\\", AppContext.BaseDirectory, PluginInfo.GetType().Namespace)
            };

            PluginInfo.initialization();

            plugin.Working = false;
            Save();
        }
        /// <summary>
        /// Disable the Plugin and uninstall the plugin from app Plugins folder
        /// </summary>
        /// <param name="pluginID">Index or ID of the plugin on the list of Plugins</param>
        public async static void Uninstall(int pluginID)
        {
            Plugins[pluginID].Working = true;
            UX.MenuSystem.Remove(Plugins[pluginID].MenuItems);
            Plugins[pluginID].Assembly.Unload();
            for (int i = 0; i < Plugins[pluginID].CurrentPluginRelease.PluginFilesLocation.Length; i++)
            {
                File.Delete(Plugins[pluginID].CurrentPluginRelease.PluginFilesLocation[i]);
            }
            Plugins[pluginID].CurrentPluginRelease.IsInstalled = false;
            Plugins[pluginID].Working = false;

            Save();
        }

        public async static void Remove(int pluginID) 
        { 

        }

        internal async static Task Update(int pluginID)
        {

            if (Plugins[pluginID].GitHubRepository == null)
                Plugins[pluginID].GitHubRepository = await GitHubClient.GetRepositoryAsync(Plugins[pluginID].GitHubRepositoryID);

            Release[] _releases = await GitHubClient.GetReleasesAsync(Plugins[pluginID].GitHubRepository);

            Release _lastStableRelease = _releases.Where(x => x.PreRelease == false).OrderByDescending(x => x.PublishedDate).FirstOrDefault();
            Release _lastPreviewRelease = _releases.Where(x => x.PreRelease == true).OrderByDescending(x => x.PublishedDate).FirstOrDefault();

            if (!Plugins[pluginID].AllowPreview) {
                if (Plugins[pluginID].CurrentPluginRelease.ID != _lastStableRelease.ID)
                {
                    if (Plugins[pluginID].AutomaticUpdate)
                    {
                        Plugins[pluginID].CurrentReleaseID = GetStableReleaseID(_releases, _lastStableRelease);
                        Plugins[pluginID].CurrentPluginRelease = Plugins[pluginID].PluginReleases[Plugins[pluginID].CurrentReleaseID];
                        Install(pluginID);
                    }
                }
            }
            else
                if(Plugins[pluginID].CurrentPluginRelease.ID != _lastPreviewRelease.ID) 
                { }

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
        private static int GetStableReleaseID(Release[] releases, Release release)
        {
            int id = -1;

            for (int i = 0; i < releases.Length; i++)
            {
                if (releases[i].ID == release.ID)
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
        private async static void Load()
        {
            if(File.Exists(Storage.SETTINGS.PLUGINS_SETTINGS))
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                string json = File.ReadAllText(Storage.SETTINGS.PLUGINS_SETTINGS);
                Plugins = JsonSerializer.Deserialize<Internal.Plugin[]>(json, options).ToList();
                for (int i = 0; i < Plugins.Count; i++)
                {
                    PluginUXs.Add(Plugins[i].PluginUX);
                    await GetReleases(Plugins[i].ID, true);
                    await Update(Plugins[i].ID);
                    if (Plugins[i].CurrentPluginRelease.Enable)
                        Enable(Plugins[i].ID);
                }
            }
        }
    }
}
