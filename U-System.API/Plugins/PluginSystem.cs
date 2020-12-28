﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using U_System.API.GitHub;

namespace U_System.API.Plugins
{
    public class PluginSystem
    {
        public static List<Plugin> Plugins { get; set; }

        public static void InitializeComponent()
        {
            Plugins = new List<Plugin>();
            Load();
            //LoadPlugins();
            //string path = Paths.PLUGINS.PLUGIN_DIRECTORY + "U-System.TestLibary.dll";
            //GetPlugin(path);
        }
        public static void AddPlugin(string file)
        {
            Plugin plugin = new Plugin();
        }

        private static void GetPlugin(string file)
        {
            AssemblyName assemblyInfo = AssemblyName.GetAssemblyName(file);
            AssemblyLoadContext temp = new AssemblyLoadContext(assemblyInfo.Name, true);
            temp.Unloading += (alc) =>
            {
                GC.Collect();
            };
            Assembly assembly = temp.LoadFromAssemblyPath(file);
            
            Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

            Plugin plugin = new Plugin();
            plugin.FileLocation = assembly.Location.Replace("\\","/");
            plugin.Name = PluginInfo.Name;
            plugin.Description = PluginInfo.Description;
            plugin.Version = PluginInfo.Version;
            plugin.Modules = PluginInfo.Modules;


            Plugins.Add(plugin);
            EnablePlugin(plugin);
            //Save();
            //plugin.FileLocation = "../WinterStudios/U-System.TestLibary.dll";
        }


        public static void AddPlugin(Repository output)
        {
            Plugin m_plugin = new Plugin();
            m_plugin.ID = Plugins.Count;
            m_plugin.GitHub_Repository = output;
            Plugins.Add(m_plugin);

            //Save();
            
            //DownloadPlugin(output, PluginState.Stable);
        }

        public static async Task<string> DownloadPlugin(Repository repository, PluginState state = PluginState.Stable)
        {
            if(state == PluginState.Stable)
            {
                Release lastReleaseStable = repository.Releases.First(x => x.PreRelease == false);
                Asset pluginAsset = lastReleaseStable.Assets.First(x => x.Name == "Release.zip");
                Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
                string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", repository.Name, lastReleaseStable.Tag);
                FileSystem.SaveStreamToFile(stream, pluginPath);
                return pluginPath;
            }
            if(state == PluginState.Preview)
            {
                Release lastReleaseStable = repository.Releases.First(x => x.PreRelease == true);
                Asset pluginAsset = lastReleaseStable.Assets.First(x => x.Name == "Release.zip");
                Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
                string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", repository.Name, lastReleaseStable.Tag);
                FileSystem.SaveStreamToFile(stream, pluginPath);
                return pluginPath;
            }
            return null;
        }

        public static void InstallPlugin(Plugin plugin)
        {
            CheckPlugin(plugin);
        }

        public static async void CheckPlugin(Plugin plugin)
        {
            if(!plugin.IsInstalled)
            {
                plugin.GitHub_Repository.Releases = await GitHub.GitHub.GetReleasesAsync(plugin.GitHub_Repository);
                string location = await DownloadPlugin(plugin.GitHub_Repository, PluginState.Stable);

                ZipArchive zip = ZipFile.OpenRead(location);
                string name = zip.Entries.First(x => x.Name.EndsWith(".dll")).Name;
                zip.ExtractToDirectory(Paths.PLUGINS.PLUGIN_DIRECTORY, true);
                plugin.IsInstalled = true;
                plugin.FileLocation = Paths.PLUGINS.PLUGIN_DIRECTORY + name;

                Save(plugin);
            }
        }
        public static void EnablePlugin(Plugin plugin)
        {
            CheckPlugin(plugin);

            AssemblyLoadContext temp = new AssemblyLoadContext(plugin.Name, true);
            temp.Unloading += (alc) =>
            {
                GC.Collect();
            };
            Assembly assembly = temp.LoadFromAssemblyPath(plugin.FileLocation);


            //List<MenuItem> menus = new List<MenuItem>();
            //for (int i = 0; i < plugin.Modules.Length; i++)
            //{
            //    Module module = plugin.Modules[i];
            //    if(module.PluginType == PluginType.Tab)
            //    {
            //        menus.Add(Navigation.MenuBar.Add(module.Path, null));
            //    }
            //}
            //plugin.MenuItems = menus.ToArray();
        }
        private static void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            File.WriteAllText(Paths.SETTINGS.PLUGINS_SETTINGS, JsonSerializer.Serialize(Plugins, options));
        }
        private static void Save(Plugin plugin)
        {
            Plugins[plugin.ID] = plugin;
            Save();
        }
        private static void Load()
        {
            if (!File.Exists(Paths.SETTINGS.PLUGINS_SETTINGS))
                return;

            string json = File.ReadAllText(Paths.SETTINGS.PLUGINS_SETTINGS);
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            Plugins = JsonSerializer.Deserialize<Plugin[]>(json, options).ToList();

        }
        //public static void LoadPluginsInfo()
        //{
        //    if (!File.Exists(Paths.PluginsSettings))
        //        return;
        //    string json = File.ReadAllText(Paths.PluginsSettings);
        //    JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
        //    Plugins = JsonSerializer.Deserialize<Plugin[]>(json, options).ToList();
        //}
        //private static void LoadPlugins()
        //{
        //    for (int i = 0; i < Plugins.Count; i++)
        //    {
        //        if(Plugins[i].)
        //    }
        //}
    }
}
