using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using U_System.Core.Plugin;
using U_System.Core.Plugin.Internal;

namespace U_System.Core.UX.Preferences
{
    /// <summary>
    /// Interaction logic for UX_Plugin.xaml
    /// </summary>
    public partial class UX_Plugin : UserControl
    {
        public UX_Plugin()
        {
            InitializeComponent();
            UC_ListBox_Plugins.ItemsSource = PluginManager.PluginUXs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _tag = (string)((Button)sender).Tag;

            if (_tag == "PLUGIN_ADD")
            {
                UX_Plugin_Add _w_p_add = new UX_Plugin_Add();
                if (_w_p_add.ShowDialog() == true)
                {
                    bool install = false;
                    PluginManager.Add(_w_p_add._output);
                    //PluginSystem.AddPlugin(_w_p_add._output, install, this); ;
                    UC_ListBox_Plugins.Items.Refresh();
                }
            }
        }

        private void PluginsListBoxItem_Button_Click(object sender, RoutedEventArgs e)
        {
            Button _btn = (Button)sender;

            string _tag = (string)_btn.Tag;

            if(_tag == "PLUGIN_INSTALL") 
            {
                int _pluginID = ((PluginUX)_btn.DataContext).PluginID;
                PluginManager.Install(_pluginID);
            }
            if (_tag == "PLUGIN_UPDATE")
            {
                int _pluginID = ((PluginUX)_btn.DataContext).PluginID;
                PluginManager.Update(_pluginID);
            }
            if (_tag == "PLUGIN_REMOVE")
            {
                int _pluginID = ((PluginUX)_btn.DataContext).PluginID;
                PluginManager.Remove(_pluginID);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PluginUX pluginUX = (PluginUX)((ComboBox)sender).DataContext;
            if (pluginUX.Plugin.CurrentPluginRelease != null && pluginUX.Plugin.CurrentPluginRelease.IsInstalled)
                PluginManager.Uninstall(pluginUX.PluginID);
            pluginUX.CurrentPluginRelease = pluginUX.Releases[pluginUX.ReleaseIndex];
            pluginUX.Plugin.CurrentPluginRelease = pluginUX.CurrentPluginRelease;
            pluginUX.Plugin.CurrentReleaseID = pluginUX.ReleaseIndex;
        }
    }
}
