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


using U_System.Core;
using U_System.Core.UX;


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
            //MenuBar.MainMenu = this.W_MainMenuBar;
            TabsSystem.UX_Control = this.W_TabControl;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((MenuItem)sender).Tag.ToString();
            if(tag == "PLUGIN_MANAGER")
            {
                TabItem tab = new TabItem();
                UX.Preferences.UX_Plugin pluginTab = new UX.Preferences.UX_Plugin();

                tab.Content = pluginTab;
                tab.Header = "Plugin Manager";
                W_TabControl.Items.Add(tab);
                W_TabControl.SelectedItem = tab;
            }
        }
    }
}
