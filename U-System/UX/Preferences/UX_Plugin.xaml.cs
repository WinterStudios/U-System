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

namespace U_System.UX.Preferences
{
    /// <summary>
    /// Interaction logic for UX_Plugin.xaml
    /// </summary>
    public partial class UX_Plugin : UserControl
    {
        public UX_Plugin()
        {
            InitializeComponent();
            UC_ListBox_Plugins.ItemsSource = Core.Plugin.PluginManager.PluginUXs;
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
                    Core.Plugin.PluginManager.Add();
                    //PluginSystem.AddPlugin(_w_p_add._output, install, this); ;
                    UC_ListBox_Plugins.Items.Refresh();
                }
            }
        }

        private void PluginsListBoxItem_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
