using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public class PluginInfoActive
    {
        public string Version { get; set; }
        public int ReleaseID { get; set; }
        public string[] FilesLocations { get; set; }
    }
}
