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

using U_System.API;
using U_System.API.Navigation;
using U_System.API.Plugins;


namespace U_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MenuBar.MainMenu = this.W_MainMenuBar;


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((MenuItem)sender).Tag.ToString();
            if(tag == "PLUGIN_MANAGER")
            {
                TabItem tab = new TabItem();
                Pages.Preferences.PluginManager manager = new Pages.Preferences.PluginManager();

                tab.Content = manager;
                tab.Header = "Plugin Manager";
                W_TabControl.Items.Add(tab);
                W_TabControl.SelectedItem = tab;
            }
        }
    }
}
