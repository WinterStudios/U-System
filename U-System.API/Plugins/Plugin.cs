using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using U_System.API.GitHub;

namespace U_System.API.Plugins
{
    public class Plugin : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public bool Enable { get; set; }

        public bool AutomaticUpdates { get; set; }
        public bool IsInstalled { get => isInstalled; set { isInstalled = value; NotifyPropertyChanged(); } }

        public Repository GitHub_Repository { get; set; }

        public SemVersion Version { get; set; }


        internal Module[] Modules { get; set; }
        internal MenuItem[] MenuItems { get; set; }         
        internal AssemblyLoadContext Assembly { get; set; }


        private bool isInstalled;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
