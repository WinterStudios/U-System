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
                    PluginSystem.AddPlugin(_w_p_add._output);
                    UC_ListBox_Plugins.Items.Refresh();
                    if (API.Properties.Settings.AutomaticInstallPlugins)
                    {

                    }
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
                plugin.IsDoingStuff = true;
                PluginSystem.InstallPlugin(plugin);

            }
            if(_tag == "PLUGIN_ENABEL")
            {
                plugin.IsDoingStuff = !plugin.IsDoingStuff;
            }
        }


    }
}
