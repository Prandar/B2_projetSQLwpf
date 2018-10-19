using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

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

        private void mainwindowButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=192.168.236.129;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
            SqlConnection connectionSQL = new SqlConnection(connectionString);
            
            try
            {
                connectionSQL.Open();
            }
            catch
            {
                MessageBox.Show("Erreur de connection a ala BDD");
            }

            string query = "SELECT * FROM utilisateur WHERE mail = @mail AND mdp = @mdp";
            SqlCommand commandSQL = new SqlCommand(query, connectionSQL);
            commandSQL.CommandType = CommandType.Text;

            commandSQL.ExecuteNonQuery();

            MessageBox.Show(commandSQL.ExecuteNonQuery().ToString());
        }
    }
}
