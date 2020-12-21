using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public class Plugin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public SemVersion Version { get; set; }
        public Module[] Module { get; set; }

    }
}
