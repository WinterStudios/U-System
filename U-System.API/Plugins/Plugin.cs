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
        public int GitHubRepositoryID { get; set; }
        public string GitHubRepositoryURL { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }

        public int ReleaseActiveID { get => releaseActiveID; set { releaseActiveID = value; NotifyPropertyChanged(); } }
        public PluginRelease ReleaseActive { get { if (ReleaseActiveID > -1) return PluginReleases[ReleaseActiveID]; else return null; } set { if (ReleaseActiveID > -1 && PluginReleases != null) { PluginReleases[ReleaseActiveID] = value; NotifyPropertyChanged(); } } }
        public PluginRelease[] PluginReleases { get => pluginReleases; set { pluginReleases = value; NotifyPropertyChanged(); } }

        public bool AutomaticUpdates { get; set; }
        public bool IsEnable { get => isEnable; set { isEnable = value; NotifyPropertyChanged(); } }
        public bool IsInstalled { get => isInstall; set { isInstall = value; NotifyPropertyChanged(); } }
        public bool IsDoingStuff { get => isDoingStuff; set { isDoingStuff = value; NotifyPropertyChanged(); } }

        

        internal Repository GitHubRepository { get; set; }

        public SemVersion Version { get; set; }



        internal Module[] Modules { get; set; }
        internal MenuItem[] MenuItems { get; set; }         
        internal TabItem[] Tabs { get; set; }
        internal AssemblyLoadContext Assembly { get; set; }


        private Repository repository;
        internal Release? activeRelease;

        private bool isInstall;
        private bool isDoingStuff;
        private bool isEnable;


        private PluginRelease[] pluginReleases;
        private int releaseActiveID;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
