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
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        private string name;



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
