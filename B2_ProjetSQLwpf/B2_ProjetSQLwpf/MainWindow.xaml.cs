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

namespace B2_ProjetSQLwpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainwindowTextboxMdp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mainwindowButtonInscription_Click(object sender, RoutedEventArgs e)
        {
            Inscription f = new Inscription();
            f.Show();
        }
    }
}
