using System;
using System.Collections.Generic;
using System.Text;

namespace U_System.API.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// Name of the Plugin
        /// </summary>
        string Name { get; }
        string Description { get; }
        SemVersion Version { get; }
        Module[] Modules { get;}
    }
}
