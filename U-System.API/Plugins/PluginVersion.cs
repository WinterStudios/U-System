using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using U_System.API;

namespace U_System.API.Plugins
{
    public class PluginRelease
    {
        public string ReleaseTag { get; set; }
        public string ReleaseID { get; set; }
        public string ZipFile { get; set; }
        public string[] Files { get; set; }
        public string Version { get; set; }

        public override string ToString()
        {
            return ReleaseTag;
        }
    }
}
