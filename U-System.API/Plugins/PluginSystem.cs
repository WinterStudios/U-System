using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public class PluginSystem
    {
        public static List<Plugin> Plugins { get; set; }

        public static void InitializeComponent()
        {
            Plugins = new List<Plugin>();
            string path = Paths.PluginsDirectory + "U-System.TestLibary.dll";
            GetPlugin(path);
        }
        public static void AddPlugin(string file)
        {
            Plugin plugin = new Plugin();
            



        }

        private static void GetPlugin(string file)
        {
            AssemblyName assemblyInfo = AssemblyName.GetAssemblyName(file);
            AssemblyLoadContext temp = new AssemblyLoadContext(assemblyInfo.Name, true);
            Assembly assembly = temp.LoadFromAssemblyPath(file);
            
            Type IPlugin = assembly.GetTypes().First(x => x.GetInterfaces().Contains(typeof(IPlugin)));
            IPlugin PluginInfo = (IPlugin)Activator.CreateInstance(IPlugin);

            Plugin plugin = new Plugin();
            plugin.FileLocation = assembly.Location.Replace("\\","/");
            plugin.Name = PluginInfo.Name;
            plugin.Description = PluginInfo.Description;
            plugin.Version = PluginInfo.Version;
            plugin.Module = PluginInfo.Modules;


            Plugins.Add(plugin);
            EnablePlugin(plugin);
            Save();
            //plugin.FileLocation = "../WinterStudios/U-System.TestLibary.dll";
        }

        public static void EnablePlugin(Plugin plugin)
        {
            for (int i = 0; i < plugin.Module.Length; i++)
            {
                Module module = plugin.Module[i];
                if(module.PluginType == PluginType.Tab)
                {
                    Navigation.MenuBar.Add(module.Path, null);
                }
            }
        }
        public static void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "plugin.json",JsonSerializer.Serialize(Plugins, options));
        }
    }
}
