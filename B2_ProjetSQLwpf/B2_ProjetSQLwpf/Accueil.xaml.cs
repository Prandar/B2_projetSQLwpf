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
using System.Data;

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
            labelBonjour.Content = "Bonjour " + CurrentUser.UserName;
            RechercheProduit("patate moulinex truc");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void accueilBoutonAfficher_Click(object sender, RoutedEventArgs e)
        {
            string sql = RechercheProduit(accueilTextboxBarDeRecherche.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sql, Sql.connectionString);
            DataTable dt = new DataTable("utilisateur, produit");
            sda.Fill(dt);
            accueilDataGrid.ItemsSource = dt.DefaultView;


        }

        public string RechercheProduit (string textrecherche)
        {
            string commandesql = "SELECT nom_prod as Nom, nom_u as Vendeur, description_prod as Description, libelle_cat as Catégorie, etat_prod  as Etat, prix_prod as Prix " +
                                 "FROM utilisateur u, produit p, categorie c WHERE u.id_u = p.id_u and c.id_cat = p.id_cat and";
            string[] motsderecherche = textrecherche.Split(' ');
            if (String.IsNullOrEmpty(motsderecherche[0]))
            {
                MessageBox.Show("erreur string de recherche");
            }
            else if (motsderecherche.Length >= 1)
            {
                int index = 1;
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
            }
            

            SqlDataReader dataRead = Sql.DataReader(commandesql);
            if (dataRead.Read())
            {
                MessageBox.Show("trouvé frere ! ! !");
            }
            else
            {
                MessageBox.Show("pas trouver gros");
            }
            return commandesql;
        }
    }
}
