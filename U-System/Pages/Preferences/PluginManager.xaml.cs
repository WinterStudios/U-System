﻿using System;
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
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _tag = (string)((Button)sender).Tag;

            if(_tag == "ADD_PLUGIN")
            {
                PluginManager_Add _w_p_add = new PluginManager_Add();
                if(_w_p_add.ShowDialog() == true) 
                {
                     
                }
            }

        }
    }
}
