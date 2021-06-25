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

namespace U_System.Core.UX.Preferences
{
    /// <summary>
    /// Interaction logic for UX_ThemeManager.xaml
    /// </summary>
    public partial class UX_ThemeManager : UserControl
    {
        public UX_ThemeManager()
        {
            InitializeComponent();
            UC_ComboBox_Theme.ItemsSource = U_System.UX.ThemeSystem.Themes;
            UC_ComboBox_Theme.SelectedItem = U_System.UX.ThemeSystem.CurrentTheme;
            UC_ComboBox_Theme.SelectionChanged += UC_ComboBox_Theme_SelectionChanged;
        }

        private void UC_ComboBox_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            U_System.UX.ThemeSystem.ApplyTheme((U_System.UX.Theme)UC_ComboBox_Theme.SelectedItem);
        }
    }
}
