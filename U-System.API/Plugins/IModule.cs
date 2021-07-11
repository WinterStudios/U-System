using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public interface IModule
    {
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        string Type { get; }
        string Path { get; }
        PluginType PluginType { get; }
    }
}
