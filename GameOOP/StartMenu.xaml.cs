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
using System.Windows.Shapes;

namespace GameOOP
{
    /// <summary>
    /// Логика взаимодействия для StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void StartBattle_Click(object sender, RoutedEventArgs e)
        {
            PumpBatlle battle = new PumpBatlle();
            battle.Show();
            this.Close();
        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
