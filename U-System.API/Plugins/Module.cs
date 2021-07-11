using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public class Module : IModule
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public PluginType PluginType { get; set; }
    }
}
