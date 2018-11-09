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
using System.Data.SqlClient;

namespace B2_ProjetSQLwpf
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
            RechercheProduit("patate moulinex truc");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        public SqlDataReader RechercheProduit (string textrecherche)
        {
            //string connectionString = "Data Source=192.168.137.128;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
            string connectionString = "Data Source=192.168.159.140;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
            SqlConnection connectionSQL = new SqlConnection(connectionString);

            try
            {
                connectionSQL.Open();
            }
            catch
            {
                MessageBox.Show("Erreur de connection a la BDD");
            }
            SqlCommand cmd = connectionSQL.CreateCommand();

            string[] motsderecherche = textrecherche.Split(' ');
            if (String.IsNullOrEmpty(motsderecherche[0]))
            {
                MessageBox.Show("erreur string de recherche");
            }
            else if (motsderecherche.Length >= 1)
            {
                int index = 1;
                string commandesql = "SELECT * FROM produit WHERE";
                foreach (string mot in motsderecherche)
                {
                    if (index < motsderecherche.Length)
                    {
                        commandesql += " nom_prod LIKE '%" + mot + "%' OR";
                        index++;
                    }
                    else
                    {
                        commandesql += " nom_prod LIKE '%" + mot + "%'";
                    }
                }
                cmd.CommandText = commandesql;
            }

            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                MessageBox.Show("trouvé frere ! ! !");
            }
            else
            {
                MessageBox.Show("pas trouver gros");
            }

            return dataReader;
        }
    }
}
