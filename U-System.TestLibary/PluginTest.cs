using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using U_System.External;
using U_System.External.Plugin;


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
                PluginTypeBehavior = PluginTypeBehavior.Tab 
            },
            new Module() {
                Name = "A2",
                Type = typeof(PluginSomething).FullName,
                Path = "Ferramentas>Gerador de Codigos de Bar",
                PluginTypeBehavior = PluginTypeBehavior.Tab
            }
        };
        public bool ShowWelcomePage => true;
        public Module WelcomePage => new Module() {
            Name = "Welcome",
            Type = typeof(WelcomePage).FullName,
            Path = "Help>Welcome Test",
            PluginTypeBehavior = PluginTypeBehavior.Tab
        };

        public string DataStorage { get; set; }
        public PluginInfo PluginInformation { get; set; }
        public string[] PluginDependicy { get; set; }

        public void initialization()
        {
            
        }
    }
}
