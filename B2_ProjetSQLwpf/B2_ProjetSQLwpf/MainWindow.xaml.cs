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
            Sql.OpenConnexion();

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
            //SqlCommand cmd = new SqlCommand("SELECT * FROM utilisateur WHERE mail_u = '" + mainwindowTextboxLogin.Text + "' AND mdp_u ='" + mainwindowTextboxMdp.Text + "'", connectionSQL);
            SqlDataReader dataReader = Sql.DataReader("SELECT * FROM utilisateur WHERE mail_u = '" + mainwindowTextboxLogin.Text + "' AND mdp_u ='" + mainwindowTextboxMdp.Text + "'");

            if (dataReader.Read())
            {
                CurrentUser.UserName = mainwindowTextboxLogin.Text;
                Accueil accueil = new Accueil();
                accueil.Show();
            }
            else
            {
                MessageBox.Show("Mail ou mot de passe incorrect");
            }

            /*while (dataReader.Read())
            {
                Console.WriteLine(dataReader["mail_u"]);
            }*/


            /*DataTable dt = new DataTable();
            sda.Fill(dt);
            //Console.WriteLine(dt.Rows[0][0].ToString());
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("La connection a réussi");
            }
            else
            {
                MessageBox.Show("La connection a échouer");
            }*/
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Sql.ClosConnexion();
    }
    }
}
