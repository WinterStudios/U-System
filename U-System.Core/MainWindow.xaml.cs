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

using U_System.Core.UX;


namespace U_System.Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SolidColorBrush _WINDOW_GLASS_BRUSH { get; set; }


        public MainWindow()
        {
            this.TaskbarItemInfo = new System.Windows.Shell.TaskbarItemInfo();
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
            this.Deactivated += MainWindow_Deactivated;
            this.Activated += MainWindow_Activated;

            InitializeComponent();
            //W_Grid_TopNav.MouseDown += W_Grid_TopNav_MouseDown;
            W_TextBlock_APP_VERSION.Text = SettingsSystem.APP_VERSION;
            //MenuBar.MainMenu = this.W_MainMenuBar;
            TabsSystem.UX_Control = this.W_TabControl;
            MenuSystem.MainNavigation = this.W_MainMenuBar;
        }

        private void W_Grid_TopNav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Window window = this;
                Thickness s = SystemParameters.WindowNonClientFrameThickness;
                if (window.WindowState == WindowState.Maximized && e.ClickCount == 2)
                {
                    
                    Point mousePos = e.GetPosition(window);
                    if (mousePos.Y == 0) { }
                    window.WindowState = WindowState.Normal;
                    //window.Top = mousePos.Y - 8;
                }
                window.DragMove();
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            W_Grid_TopNav.Background = Theme.Colores._WINDOW_NAVBAR_ACTIVE;
        }

        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            W_Grid_TopNav.Background = Theme.Colores._WINDOW_NAVBAR_DEACTIVE;
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
