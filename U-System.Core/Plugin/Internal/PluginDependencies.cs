using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Plugin.Internal
{
    public class PluginDependencies
    {
        public RuntimeTarget runtimeTarget { get; set; }
        public Dictionary<string,Dictionary<string,object>> targets { get; set; }
    }

    public class RuntimeTarget
    {
        public string name { get; set; }
        public string signature { get; set; }
    }
    public class Targets
    {
        
    }
}
