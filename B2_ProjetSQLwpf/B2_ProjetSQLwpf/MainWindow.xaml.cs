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
        Sql sql = new Sql();
        public MainWindow()
        {
            InitializeComponent();


        }

        private void mainwindowButtonInscription_Click(object sender, RoutedEventArgs e)
        {
            Inscription f = new Inscription();
            f.Show();
        }

        private void mainwindowButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            sql.OpenConnexion();
            SqlCommand cmd = new SqlCommand("SELECT * FROM utilisateur WHERE mail_u = '" + mainwindowTextboxLogin.Text + "' AND mdp_u ='" + mainwindowTextboxMdp.Password + "'", sql.con);
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    CurrentUser.IdUser = dataReader.GetInt32(0);
                    CurrentUser.UserName = mainwindowTextboxLogin.Text;
                    CurrentUser.UserAdmin = dataReader.GetBoolean(11);
                    mainwindowTextboxLogin.Clear();
                    mainwindowTextboxMdp.Clear();
                    Accueil accueil = new Accueil();
                    accueil.Show();
                    //Console.WriteLine(CurrentUser.IdUser);
                }
                else
                {
                    MessageBox.Show("Mail ou mot de passe incorrect");
                }
            }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
             sql.ClosConnexion();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
