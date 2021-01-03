using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.Plugins
{
    public class PluginRelease : INotifyPropertyChanged
    {
        public string ReleaseTag { get => releaseTag; set { releaseTag = value; NotifyPropertyChanged(); } }
        public int ReleaseID { get => releaseID; set { releaseID = value; NotifyPropertyChanged(); } }
        public bool PreRelease { get => preRelease; set { preRelease = value; NotifyPropertyChanged(); } }
        public bool IsInstalled { get => isInstalled; set { isInstalled = value; NotifyPropertyChanged(); } }
        public bool IsEnable { get => isEnable; set { isEnable = value; NotifyPropertyChanged(); } }


        public string ReleaseZipFile { get; set; }
        public string [] PluginFilesLocation { get; set; }
        



        private bool isInstalled;
        private string releaseTag;
        private int releaseID;
        private bool preRelease;
        private bool isEnable;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
