using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using U_System.External;

namespace U_System.External.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        SemVersion Version { get; }
        Module[] Modules { get; }
    }
}
