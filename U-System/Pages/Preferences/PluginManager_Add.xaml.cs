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
using System.Windows.Shapes;

using U_System.API.GitHub;
using U_System.API.Plugins;

namespace U_System.Pages.Preferences
{
    /// <summary>
    /// Interaction logic for PluginManager_Add.xaml
    /// </summary>
    public partial class PluginManager_Add : Window
    {
        internal Repository _output { get; private set; }

        public PluginManager_Add()
        {
            InitializeComponent();
            GetRepositories("WinterStudios");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private async void GetRepositories(string user)
        {
            Repository[] _repositories = await GitHub.GetRepositoriesAsync(user);
            W_ListBox_Repositories.ItemsSource = _repositories;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) => AddPlugin();

        private void Button_Click(object sender, RoutedEventArgs e) => AddPlugin();

        private void AddPlugin()
        {
            _output = (Repository)W_ListBox_Repositories.SelectedItem;
            DialogResult = true;
            Close();
        }
    }
}
