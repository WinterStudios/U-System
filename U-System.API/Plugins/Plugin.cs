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


        public PluginRelease CurrentRelease { get => currentRelease; set { currentRelease = value; NotifyPropertyChanged(); } } 

        public bool AllowPreview { get => allowPreview; set { allowPreview = value; NotifyPropertyChanged(); } }
        public bool AutomaticUpdates { get; set; }
        public bool IsEnable { get => isEnable; set { isEnable = value; NotifyPropertyChanged(); } }
        public bool IsInstalled { get => isInstall; set { isInstall = value; NotifyPropertyChanged(); } }
        public bool IsDoingStuff { get => isDoingStuff; set { isDoingStuff = value; NotifyPropertyChanged(); } }
        public bool UpdateAvalable { get => updateAvalable; set { updateAvalable = value; NotifyPropertyChanged(); } }
        

        internal Repository GitHubRepository { get; set; }
        internal Release GitHubRelease { get; set; }

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
        private bool allowPreview;
        private bool updateAvalable;

        private PluginRelease currentRelease;

        private int releaseActiveID;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public static class Extensions
    {
        public static void Remove (this TabItem[] tabs, TabItem item)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i] == item)
                    tabs[i] = null;
            }
        }
    }
}
