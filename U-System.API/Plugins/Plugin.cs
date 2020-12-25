using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using U_System.API.GitHub;

namespace U_System.API.Plugins
{
    public class Plugin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public bool Enable { get; set; }
        public SemVersion Version { get; set; }
        public Module[] Modules { get; set; }
        internal MenuItem[] MenuItems { get; set; } 
        public Repository GitHub_Repository { get; set; }

    }
}
