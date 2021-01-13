using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.External.Plugin
{
    public interface IModule
    {
        string Name { get; }
        /// <summary>
        /// 
        /// </summary> 
        string Type { get; }
        string Path { get; }
        PluginTypeBehavior PluginTypeBehavior { get; }
    }
}
