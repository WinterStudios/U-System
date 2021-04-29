using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.External.Plugin
{
    public class PluginInfo
    {
        public string PluginStorageData { get => CheckDirectory(_pluginStorageData); set => _pluginStorageData = value; }
        private string _pluginStorageData;


        private static string CheckDirectory(string directory, bool create = true)
        {
            if (!System.IO.Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);
            return directory;
        }
        public PluginInfo()
        {

        }
    }
}
