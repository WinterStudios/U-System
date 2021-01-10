using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Plugin.Internal
{
    public class Plugin
    {
        public int ID { get; set; }
        public string Name { get => name; set { name = value; PluginUX.Name = value; } }

        public PluginUX PluginUX { get; set; }





        private int id;
        private string name;

    }
}
