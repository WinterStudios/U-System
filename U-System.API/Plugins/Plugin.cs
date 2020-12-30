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
        public string[] Files { get; set; }
        public bool Enable { get; set; }

        public bool AutomaticUpdates { get; set; }
        public bool IsInstalled { get => isInstalled; set { isInstalled = value; NotifyPropertyChanged(); } }
        public bool IsDoingStuff { get => isDoingStuff; set { isDoingStuff = value; NotifyPropertyChanged(); } }
        public Repository GitHub_Repository { get => repository; set { repository = value; NotifyPropertyChanged(); } }
        public int ActiveRelease { get => PluginSystem.GetIndexOfRelease(GitHub_Repository.Releases, activeRelease); set { activeRelease = GitHub_Repository.Releases[value]; } }
        public SemVersion Version { get; set; }



        internal Module[] Modules { get; set; }
        internal MenuItem[] MenuItems { get; set; }         
        internal AssemblyLoadContext Assembly { get; set; }

        private Repository repository;
        private Release activeRelease;
        private bool isInstalled;
        private bool isDoingStuff;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
