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

using U_System.API.Plugins;

namespace U_System.Pages.Preferences
{
    /// <summary>
    /// Interaction logic for PluginManager.xaml
    /// </summary>
    public partial class PluginManager : UserControl
    {

        public PluginManager()
        {
            InitializeComponent();

            UC_ListBox_Plugins.ItemsSource = PluginSystem.Plugins;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _tag = (string)((Button)sender).Tag;

            if (_tag == "ADD_PLUGIN")
            {
                PluginManager_Add _w_p_add = new PluginManager_Add();
                if (_w_p_add.ShowDialog() == true)
                {
                    bool install = false;
                    PluginSystem.AddPlugin(_w_p_add._output, install, this); ;
                    UC_ListBox_Plugins.Items.Refresh();
                }
            }
        }
        private void PluginsListBoxItem_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Plugin plugin = (Plugin)button.DataContext;

            string _tag = button.Tag.ToString();
            if(_tag == "PLUGIN_INSTALL")
            {
                PluginSystem.Install(plugin);

            }
            if(_tag == "PLUGIN_ACTIVATE")
            {
                //PluginSystem.EnablePlugin(plugin);
            }
            if(_tag == "PLUGIN_UPDATE")
            {
                //PluginSystem.UpdatePlugin(plugin);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            Plugin plugin = (Plugin)box.DataContext;
            plugin.ReleaseActiveID = box.SelectedIndex;
            plugin.ReleaseActive = plugin.PluginReleases[plugin.ReleaseActiveID];
            PluginSystem.Save();
            //PluginSystem.PluginChangeVersion(plugin, box.SelectedIndex);

        }
    }
}
