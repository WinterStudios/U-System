using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U_System.API;
using U_System.API.Plugins;


namespace U_System.TestLibary
{
    public class PluginTest : IPlugin
    {
        public string Name => "Plugin Test";
        public string Description => "Test assembly";
        public SemVersion Version => new SemVersion();
        public Module[] Modules => new Module[] {
            new Module() { 
                Name = "A1", 
                Type = typeof(PluginSomething).FullName, 
                Path = "Ferramentas>Encomendas Escolares", 
                PluginType = PluginType.Tab 
            },
            new Module() {
                Name = "A2",
                Type = typeof(PluginSomething).FullName,
                Path = "Ferramentas>Gerador de Codigos de Bar",
                PluginType = PluginType.Tab
            }
        };
    }
}
