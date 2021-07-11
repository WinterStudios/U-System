using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Plugin.Internal
{
    public class PluginRelease
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public string ReleaseZipFile { get; set; }
        public int AssetID { get; set; }
        public string[] PluginFilesLocation { get; set; }
        public bool IsInstalled { get; set; }

    }
}
