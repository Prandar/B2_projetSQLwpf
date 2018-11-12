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
            //RechercheProduit("patate moulinex truc");
            //AfficherToutProduits();
            //AjouterProduit("Moulin à café", 15, "C:\\test", "Le moulin a café de mon coloc", 1, 1);
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

//public bool AjouterProduit(string nom_prod, int prix_prod, string photo_prod, string description_prod, int id_u, int id_cat)
        //{
        //    //soi en sql pure ou avec une procedure stocker

        //    string commandesql = "INSERT INTO produit (nom_prod, prix_prod, etat_prod, photo_prod, description_prod, id_u, id_cat) " +
        //        "VALUES('" + nom_prod + "', " + prix_prod + ", 'En Vente', '" + photo_prod + "', '" + description_prod + "', " + id_u + ", " + id_cat + ") ";

        //    SqlCommand com = new SqlCommand("AjouterProduit", Sql.con);
        //    //com.CommandType = CommandType.StoredProcedure;
        //    //com.Parameters.AddWithValue("@nom_prod", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@prix_prod", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@etat_prod", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@photo_prod", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@description_prod", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@id_u", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //com.Parameters.AddWithValue("@id_cat", xoxoxoxoxo).Value = xoxoxoxoxo.Text;
        //    //Console.WriteLine("@nom_prod");

        //    com.ExecuteNonQuery();
        //    try
        //    {
        //        MessageBox.Show("Vous êtes bien inscrit :)");
        //        return true;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Une erreur c'est produite");
        //        return false;
        //    }
        //}

        public SqlDataReader AfficherToutProduits()        {

            string commandesql = "SELECT * FROM produit WHERE etat_prod = 'En Vente'";

            SqlDataReader dtreader = Sql.DataReader(commandesql);
            if (dtreader.Read())
            {
                MessageBox.Show("gagner");
                //MessageBox.Show(dtreader.GetString(1));
            }
            else
            {
                MessageBox.Show("perdu");
            }            return dtreader;
        }

        public string RechercheProduit(string textrecherche)
        {
            string commandesql = "SELECT nom_prod as Nom, nom_u, description_prod, libelle_cat, etat_prod, prix_prod FROM utilisateur u, produit p, categorie c WHERE u.id_u = p.id_u and c.id_cat = p.id_cat and";
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

            SqlDataReader dataReader = Sql.DataReader(commandesql);
            if (dataReader.Read())            {
                MessageBox.Show("trouvé frere ! ! !");
            }
            else
            {
                MessageBox.Show("pas trouver gros");
            }
            return commandesql;
        }

        public bool EnvoyerMessage()
        {
            string commandesql = "INSERT into messager(contenue_m, id_u, id_u_destinataire) values('Je pense que 17€ est un prix acceptable', 6, 1)";
            return true;
        }

        public string AfficherMessage(int id_u_expe, int id_u_dest)
        {
            string commandesql = "Select contenue_m, nom_u, prenom_u From messager M, utilisateur U where M.id_u = " +id_u_expe + " AND U.id_u = " + id_u_dest + " OR M.id_u = " + id_u_expe + " AND U.id_u = " + id_u_dest ;

            SqlDataReader dataReader = Sql.DataReader(commandesql);
            if (dataReader.Read())
            {
                MessageBox.Show("trouvé GG!");
            }
            else
            {
                MessageBox.Show("pas trouvé /ff?");
            }
            return commandesql;
        }

        public bool AcheterProd(int id_prod)
        {
            string commandesql = "UPDATE produit SET etat_prod = 'En Négociation' Where id_prod =" + id_prod;

            SqlDataReader dataReader = Sql.DataReader(commandesql);
            if (dataReader.Read())
            {
                MessageBox.Show("Vous avez acheté, Attendez la validation du vendeur");
                return true;
            }
            else
            {
                MessageBox.Show("erreur ... deja acheté par quelqu'un d'autre ?");
                return false;
            }
        }

        public bool ValiderAchatProd(int id_prod)
        {
            string commandesql = "UPDATE produit SET etat_prod = 'Vendu' Where id_prod =" + id_prod;

            SqlDataReader dataReader = Sql.DataReader(commandesql);
            if (dataReader.Read())
            {
                MessageBox.Show("Vous avez acheté");
                return true;
            }
            else
            {
                MessageBox.Show("erreur !?!");
                return false;
            }
        }

        public bool SupprimerProd(int AdminStatus, int id_prod)
        {
            if (AdminStatus < 1)
            {
                MessageBox.Show("Vous n'avez pas le droit de supprimer ce produit");
                return false;
            }
            else
            {
                string commandesql = "DELETE FROM produit WHERE id_prod = 2; " + id_prod;

                //SqlDataReader dataReader = Sql.DataReader(commandesql);
                //if (dataReader.Read())
                //{
                //    MessageBox.Show("Vous avez suppr la vente");
                //    return true;
                //}
                //else
                //{
                //    MessageBox.Show("erreur ?_?");
                //    return false;
                //}

                return true;
            }
        }
    }
}
