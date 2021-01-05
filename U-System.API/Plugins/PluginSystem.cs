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
using System.Windows;
using System.Windows.Controls;


using U_System.API.GitHub;
using U_System.API.GitHub.Extensions;



namespace U_System.API.Plugins
{
    public class PluginSystem
    {
        public static List<Plugin> Plugins { get; set; }
       

        public static void InitializeComponent()
        {
            Plugins = new List<Plugin>();
            //Load();
            //EnablePlugins();
            //LoadPlugins();
            //string path = Paths.PLUGINS.PLUGIN_DIRECTORY + "U-System.TestLibary.dll";
            //GetPlugin(path);
        }

        public static async void AddPlugin(Repository repository, bool install, UserControl control)
        {
            Plugin plugin = new Plugin();
            plugin.ID = Plugins.Count;
            plugin.GitHubRepositoryID = repository.ID;
            plugin.GitHubRepositoryURL = repository.URL;
            plugin.GitHubRepository = repository;
            plugin.Name = repository.Name;
            plugin.Description = repository.Description;
            plugin.IsDoingStuff = true;
            plugin.CurrentRelease = new PluginRelease();
            Plugins.Add(plugin);

            ListBox list = (ListBox)control.FindName("UC_ListBox_Plugins");
            list.Items.Refresh();
            Release[] releases = await GitHub.GitHub.GetReleasesAsync(repository);

            plugin.GitHubRepository.Releases = releases;

            plugin.CurrentRelease = GetRelease(plugin);
            


            //plugin.ReleaseActive = plugin.PluginReleases[0];
            plugin.IsDoingStuff = false;

            Save();
           
        }

        public static PluginRelease GetRelease(Plugin plugin)
        {
            bool previewVersion = false;
            Release[] releases = plugin.GitHubRepository.Releases;
            if (releases.Length < 1 || releases == null)
                return null;

            if (plugin.AllowPreview)
            {
                Release release = releases.FirstOrDefault(x => x.PreRelease);
                plugin.CurrentRelease.ReleaseTag = release.Tag;
                plugin.CurrentRelease.ReleaseID = release.ID;

                PluginRelease pluginRelease = new PluginRelease();
                pluginRelease.ReleaseID = release.ID;
                pluginRelease.ReleaseTag = release.Tag;
                pluginRelease.PreRelease = true;

                plugin.GitHubRelease = release;

                return pluginRelease;
            }
            else
            {
                Release release = releases.FirstOrDefault(x => !x.PreRelease);
                plugin.CurrentRelease.ReleaseTag = release.Tag;
                plugin.CurrentRelease.ReleaseID = release.ID;

                PluginRelease pluginRelease = new PluginRelease();
                pluginRelease.ReleaseID = release.ID;
                pluginRelease.ReleaseTag = release.Tag;
                pluginRelease.PreRelease = false;

                plugin.GitHubRelease = release;

                return pluginRelease;
            }

            

        }

        public static async void Install(Plugin plugin)
        {
            plugin.IsDoingStuff = true;

            await Task.Run(() => Thread.Sleep(1000));

            Release release = plugin.GitHubRelease;

            Asset pluginAsset = release.Assets.First(x => x.Name == "Release.zip");
            Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
            string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", plugin.Name, release.Tag);
            FileSystem.SaveStreamToFile(stream, pluginPath);


            ZipArchive zip = ZipFile.OpenRead(pluginPath);
            string name = zip.Entries.First(x => x.Name.EndsWith(".dll")).Name;
            plugin.CurrentRelease.ReleaseZipFile = pluginPath.Replace("\\", "/");
            plugin.CurrentRelease.AssetID = pluginAsset.ID;
            zip.ExtractToDirectory(Paths.PLUGINS.PLUGIN_DIRECTORY, true);
            
            string[] files = new string[zip.Entries.Count];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Paths.PLUGINS.PLUGIN_DIRECTORY.Replace("\\", "/") + zip.Entries[i].FullName;
            }

            plugin.CurrentRelease.PluginFilesLocation = files;
            plugin.CurrentRelease.IsInstalled = true;

            plugin.IsDoingStuff = false;
            Save();
        }

        public async static void Enable(Plugin plugin)
        {
            AssemblyLoadContext temp = new AssemblyLoadContext(plugin.Name, true);
            temp.Unloading += (alc) =>
            {
                GC.Collect();
            };
            Assembly assembly = temp.LoadFromAssemblyPath(plugin.CurrentRelease.PluginFilesLocation.First(x => x.EndsWith(".dll")));
            plugin.Assembly = temp;

            Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

            plugin.Modules = PluginInfo.Modules;
            plugin.Tabs = new TabItem[plugin.Modules.Length];
            List<MenuItem> menus = new List<MenuItem>();
            for (int i = 0; i < plugin.Modules.Length; i++)
            {
                Module module = plugin.Modules[i];

                int index = i;
                if (module.PluginType == PluginType.Tab)
                {
                    MenuItem item = Navigation.MenuBar.Add(module.Path);
                    item.Click += (sender, arg) =>
                    {
                        if (plugin.Tabs[index] == null)
                        {
                            plugin.Tabs[index] = new TabItem();
                            plugin.Tabs[index].Header = module.Name;
                            Type type = assembly.GetType(module.Type);
                            object content = Activator.CreateInstance(type);
                            plugin.Tabs[index].Content = content;
                            plugin.Tabs[index].DataContext = new object[3] { "_PLUGIN" , plugin.ID, index };
                            Navigation.TabSystem.Add(plugin.Tabs[index]);
                        }
                        else
                        {
                            Navigation.TabSystem.Select(plugin.Tabs[index]);
                        }
                        
                    };
                    menus.Add(item);
                }
            }
            plugin.MenuItems = menus.ToArray();
            plugin.CurrentRelease.IsEnable = true;
            plugin.IsDoingStuff = false;
            Save();
        }
        public static async void EnablePlugins()
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (Plugins[i].CurrentRelease.IsEnable)
                    Enable(Plugins[i]);
            }

        }
        public async static void Disable(Plugin plugin)
        {
            Navigation.MenuBar.Remove(plugin.MenuItems);

            plugin.Assembly.Unload();

            plugin.CurrentRelease.IsEnable = false;

            Save();
        }

        public static void RemoveTabFromPlugin(int pluginIndex, int moduloIndex) => Plugins[pluginIndex].Tabs[moduloIndex] = null;


        public static async void Update(Plugin plugin)
        {
            
        }
        public static async void Update()
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (Plugins[i].GitHubRepository == null)
                    Plugins[i].GitHubRepository = await GitHub.GitHub.GetRepositoryAsync(Plugins[i].GitHubRepositoryID);
                if (Plugins[i].GitHubRelease == null)
                {
                    Release[] releases = await GitHub.GitHub.GetReleasesAsync(Plugins[i].GitHubRepository);
                    Plugins[i].GitHubRelease = releases.First(x => x.ID == Plugins[i].CurrentRelease.ReleaseID);
                }
                /// --> +e preciso continuar para quando inicia-se o programa, verficar se existe alguma nova atualizaçao,
                /// e se hover fazer o download automatico, instalar e ativar se tiver atualizaçaos automaticas, e ativo.
                /// 

            }
        }

        

        //public static void AddPlugin(string file)
        //{
        //    Plugin plugin = new Plugin();
        //}

        //private static void GetPlugin(string file)
        //{
        //    AssemblyName assemblyInfo = AssemblyName.GetAssemblyName(file);
        //    AssemblyLoadContext temp = new AssemblyLoadContext(assemblyInfo.Name, true);
        //    temp.Unloading += (alc) =>
        //    {
        //        GC.Collect();
        //    };
        //    Assembly assembly = temp.LoadFromAssemblyPath(file);

        //    Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
        //    IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

        //    Plugin plugin = new Plugin();
        //    //plugin.FileLocation = assembly.Location.Replace("\\","/");
        //    //plugin.Name = PluginInfo.Name;
        //    plugin.Description = PluginInfo.Description;
        //    plugin.Version = PluginInfo.Version;
        //    plugin.Modules = PluginInfo.Modules;


        //    Plugins.Add(plugin);
        //    EnablePlugins();
        //    //Save();
        //    //plugin.FileLocation = "../WinterStudios/U-System.TestLibary.dll";
        //}


        //public static async void AddPlugin(Repository output, bool forceInstall)
        //{
        //    Plugin m_plugin = new Plugin();
        //    m_plugin.ID = Plugins.Count;
        //    m_plugin.GitHub_Repository = output;
        //    m_plugin.ActiveRelease = -1;
        //    m_plugin.IsDoingStuff = true;
        //    Plugins.Add(m_plugin);

        //    if (forceInstall)
        //        InstallFresh(m_plugin);
        //    else
        //    {
        //        await UpdatePlugin(m_plugin);
        //        m_plugin.ActiveRelease = 0;
        //    }
        //    Save();

        //    //DownloadPlugin(output, PluginState.Stable);
        //}

        //public static void PluginChangeRelease(Plugin plugin, int NewIndex)
        //{
        //    plugin.ActiveRelease = NewIndex;
        //    //DisablePlugin(plugin);
        //    Save(plugin);
        //}



        //public async static void InstallFresh(Plugin plugin)
        //{
        //    await Task.Run(() => Thread.Sleep(2000));

        //    plugin.GitHub_Repository.Releases = await GitHub.GitHub.GetReleasesAsync(plugin.GitHub_Repository);

        //    Release lastReleaseStable = plugin.GitHub_Repository.Releases.First(x => x.PreRelease == false);
        //    plugin.ActiveRelease = GetIndexOfRelease(plugin.GitHub_Repository.Releases, lastReleaseStable);
        //    Asset pluginAsset = lastReleaseStable.Assets.First(x => x.Name == "Release.zip");
        //    Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
        //    string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", plugin.GitHub_Repository.Name, lastReleaseStable.Tag);
        //    FileSystem.SaveStreamToFile(stream, pluginPath);


        //    ZipArchive zip = ZipFile.OpenRead(pluginPath);
        //    string name = zip.Entries.First(x => x.Name.EndsWith(".dll")).Name;
        //    lastReleaseStable.LocalZipFile = pluginPath.Replace("\\", "/");
        //    zip.ExtractToDirectory(Paths.PLUGINS.PLUGIN_DIRECTORY, true);
        //    lastReleaseStable.IsInstall = true;
        //    string[] files = new string[zip.Entries.Count];
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        files[i] = Paths.PLUGINS.PLUGIN_DIRECTORY.Replace("\\", "/") + zip.Entries[i].FullName;
        //    }
        //    lastReleaseStable.filesLocations = files;
        //    lastReleaseStable.IsInstall = true;
        //    plugin.IsInstalled = true;
        //    Save();
        //    EnablePlugin(plugin);

        //    plugin.IsDoingStuff = false;
        //    Save();
        //}
        //public async static void InstallRelease(Plugin plugin)
        //{
        //    plugin.IsDoingStuff = true;
        //    Release release = plugin.GitHub_Repository.Releases[plugin.ActiveRelease];
        //    Asset pluginAsset = release.Assets.First(x => x.Name == "Release.zip");
        //    Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
        //    string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", plugin.GitHub_Repository.Name, release.Tag);
        //    FileSystem.SaveStreamToFile(stream, pluginPath);


        //    ZipArchive zip = ZipFile.OpenRead(pluginPath);
        //    string name = zip.Entries.First(x => x.Name.EndsWith(".dll")).Name;
        //    release.LocalZipFile = pluginPath.Replace("\\", "/");
        //    zip.ExtractToDirectory(Paths.PLUGINS.PLUGIN_DIRECTORY, true);
        //    release.IsInstall = true;
        //    string[] files = new string[zip.Entries.Count];
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        files[i] = Paths.PLUGINS.PLUGIN_DIRECTORY.Replace("\\", "/") + zip.Entries[i].FullName;
        //    }
        //    release.filesLocations = files;
        //    release.IsInstall = true;
        //    plugin.IsInstalled = true;
        //    Save();
        //    plugin.IsDoingStuff = false;
        //}


        //public static async Task<string> DownloadPlugin(Repository repository, PluginState state = PluginState.Stable)
        //{
        //    if(state == PluginState.Stable)
        //    {
        //        Release lastReleaseStable = repository.Releases.First(x => x.PreRelease == false);
        //        Asset pluginAsset = lastReleaseStable.Assets.First(x => x.Name == "Release.zip");
        //        Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
        //        string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", repository.Name, lastReleaseStable.Tag);
        //        FileSystem.SaveStreamToFile(stream, pluginPath);
        //        return pluginPath;
        //    }
        //    if(state == PluginState.Preview)
        //    {
        //        Release lastReleaseStable = repository.Releases.First(x => x.PreRelease == true);
        //        Asset pluginAsset = lastReleaseStable.Assets.First(x => x.Name == "Release.zip");
        //        Stream stream = await GitHub.GitHub.GetReleaseAssetAsync(pluginAsset.URL);
        //        string pluginPath = Paths.STORAGE_DOWNLOADS + string.Format("{0}-{1}.zip", repository.Name, lastReleaseStable.Tag);
        //        FileSystem.SaveStreamToFile(stream, pluginPath);
        //        return pluginPath;
        //    }
        //    return null;
        //}

        //public static async Task UpdatePlugin(Plugin plugin)
        //{
        //    plugin.IsDoingStuff = true;

        //    plugin.GitHub_Repository.Releases = await GitHub.GitHub.GetReleasesAsync(plugin.GitHub_Repository);

        //    plugin.IsDoingStuff = false;

        //}

        //public static async void EnablePlugin(Plugin plugin)
        //{
        //    //CheckPlugin(plugin);
        //    if (!plugin.IsDoingStuff)
        //        plugin.IsDoingStuff = true;

        //    Release release = plugin.GitHub_Repository.Releases[plugin.ActiveRelease];

        //    AssemblyLoadContext temp = new AssemblyLoadContext(plugin.Name, true);
        //    temp.Unloading += (alc) =>
        //    {
        //      GC.Collect();
        //    };
        //    Assembly assembly = temp.LoadFromAssemblyPath(plugin.activeRelease.filesLocations.First(x => x.EndsWith(".dll")));
        //    plugin.Assembly = temp;

        //    Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
        //    IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

        //    plugin.Modules = PluginInfo.Modules;

        //    List<MenuItem> menus = new List<MenuItem>();
        //    for (int i = 0; i < plugin.Modules.Length; i++)
        //    {
        //        Module module = plugin.Modules[i];
        //        if(module.PluginType == PluginType.Tab)
        //        {
        //            menus.Add(Navigation.MenuBar.Add(module.Path, null));
        //        }
        //    }
        //    plugin.MenuItems = menus.ToArray();
        //    plugin.IsEnable = true;
        //    plugin.IsDoingStuff = false;
        //}
        //public static async void EnablePlugins()
        //{
        //    for (int i = 0; i < Plugins.Count; i++)
        //    {
        //        if(Plugins[i].IsEnable)
        //            EnablePlugin(Plugins[i]);
        //    }
        //}

        //public static async void DisablePlugin(Plugin plugin)
        //{
        //    if (!plugin.IsDoingStuff)
        //        plugin.IsDoingStuff = true;

        //    plugin.IsEnable = false;

        //    Navigation.MenuBar.MainMenu.Items.Remove(plugin.MenuItems.First());

        //    //Release release = plugin.GitHub_Repository.Releases[plugin.ActiveRelease];
        //    //
        //    //
        //    //plugin.Assembly.Unload();
        //}


        public static void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            File.WriteAllText(Paths.SETTINGS.PLUGINS_SETTINGS, JsonSerializer.Serialize(Plugins, options));
        }
        private static void Save(Plugin plugin)
        {
            Plugins[plugin.ID] = plugin;
            Save();
        }
        public static void Load()
        {
            if (!File.Exists(Paths.SETTINGS.PLUGINS_SETTINGS))
                return;

            string json = File.ReadAllText(Paths.SETTINGS.PLUGINS_SETTINGS);
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            Plugins = JsonSerializer.Deserialize<Plugin[]>(json, options).ToList();
            Update();
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

        internal static int GetIndexOfRelease(Release[] releases, Release release)
        {
            if (releases == null || release == null)
                return -1;
            for (int i = 0; i < releases.Length; i++)
            {
                if (releases[i].ID == release.ID)
                    return i;
            }
            return -1;
        }
    }
}
