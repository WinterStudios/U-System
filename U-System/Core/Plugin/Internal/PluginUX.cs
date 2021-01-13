using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core.Plugin.Internal
{
    public class PluginUX : INotifyPropertyChanged
    {
        public int PluginID { get => id; set => id = value; }
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }
        public string Description { get => description; set { description = value; NotifyPropertyChanged(); } }
        public int ReleaseIndex { get => releaseIndex; set { releaseIndex = value; NotifyPropertyChanged(); } }
        public PluginRelease[] Releases { get => pluginReleases; set { pluginReleases = value; NotifyPropertyChanged(); } }
        public PluginRelease CurrentPluginRelease { get => currentPluginRelease; set { currentPluginRelease = value; NotifyPropertyChanged(); } }
        public bool IsWorking { get => isWorking; set { isWorking = value; NotifyPropertyChanged(); } }


        private int id;
        private string name;
        private string description;
        private int releaseIndex;
        private bool isWorking;
        private PluginRelease[] pluginReleases;
        private PluginRelease currentPluginRelease;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Working { get; internal set; }
    }
}
