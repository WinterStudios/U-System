using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.External.Plugin
{
    public class Module : IModule
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string TabIconLocations { get; set; }
        public string Shortcut { get; set; }
        public string Icon { get; set; }
        public PluginTypeBehavior PluginTypeBehavior { get; set; }
        
    }
}
