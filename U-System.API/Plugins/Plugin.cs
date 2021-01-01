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
        public string Name { get => GitHub_Repository.Name; }
        public string Description { get; set; }
        public string[] Files { get; set; }
        
        public bool AutomaticUpdates { get; set; }
        public bool IsEnable { get => isEnable; set { isEnable = value; NotifyPropertyChanged(); } }
        public bool IsInstalled { get => isInstall; set { isInstall = value; NotifyPropertyChanged(); } }
        public bool IsDoingStuff { get => isDoingStuff; set { isDoingStuff = value; NotifyPropertyChanged(); } }

        public PluginInfoActive ActivePlugin { get; set; }

        public Repository GitHub_Repository { get => repository; set { repository = value; NotifyPropertyChanged(); } }
        public int ActiveRelease { get => PluginSystem.GetIndexOfRelease(GitHub_Repository.Releases, activeRelease);
            set {
                if (GitHub_Repository.Releases != null && value > -1)
                {
                    activeRelease = GitHub_Repository.Releases[value];
                    IsInstalled = GitHub_Repository.Releases[ActiveRelease].IsInstall;
                }
                else 
                    activeRelease = null; 
                NotifyPropertyChanged(); 
            } } 
        public SemVersion Version { get; set; }



        internal Module[] Modules { get; set; }
        internal MenuItem[] MenuItems { get; set; }         
        internal AssemblyLoadContext Assembly { get; set; }

        private Repository repository;
        internal Release? activeRelease;

        private bool isInstall;
        private bool isDoingStuff;
        private bool isEnable;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
