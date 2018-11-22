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
        Sql sql = new Sql();
        public Accueil()
        {
            InitializeComponent();
            labelBonjour.Content = "Bonjour " + CurrentUser.UserName;
            string sqlProd = RechercheProduit("");
            SqlDataAdapter sda = new SqlDataAdapter(sqlProd, sql.connectionString);
            DataTable dt = new DataTable("utilisateur, produit");
            sda.Fill(dt);
            accueilDataGrid.ItemsSource = dt.DefaultView;
            //RechercheProduit("patate moulinex truc");
            //AfficherToutProduits();
            //AjouterProduit("Moulin à café", 15, "C:\\test", "Le moulin a café de mon coloc", 1, 1);
            //Data.listpanier.Add("truc");
            //Console.WriteLine( Data.listpanier.Count);
            //EnleverProdList("truc");
            //Console.WriteLine(Data.listpanier.Count);

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void accueilBoutonAfficher_Click(object sender, RoutedEventArgs e)
        {
            string sqlProduit = RechercheProduit(accueilTextboxBarDeRecherche.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sqlProduit, sql.connectionString);
            DataTable dt = new DataTable("utilisateur, produit");
            sda.Fill(dt);
            accueilDataGrid.ItemsSource = dt.DefaultView; 
        }

        private void buyProduitAccueilButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView gd = (DataRowView)accueilDataGrid.SelectedItem;
            if (gd != null)
            {
                string nom = gd["Nom"].ToString();
                Data.listpanier.Add(nom);
            }
            Panier panier = new Panier();
            panier.Show();
        }

        public SqlDataReader AfficherToutProduits()
        {
            string commandesql = "SELECT * FROM produit WHERE etat_prod = 'En Vente'";
            SqlCommand cmd = new SqlCommand(commandesql, sql.con);
            using (SqlDataReader dtreader = cmd.ExecuteReader())
            {
                if (dtreader.Read())
                {
                    MessageBox.Show("gagner");
                    //MessageBox.Show(dtreader.GetString(1));
                }
                else
                {
                    MessageBox.Show("perdu");
                }
                return dtreader;
            }
        }

        public string RechercheProduit(string textrecherche)
        {
            sql.OpenConnexion();
            string commandesql = "SELECT id_prod, nom_prod as Nom, nom_u as Vendeur, description_prod as Description, libelle_cat as Catégorie, etat_prod as Etat, prix_prod as Prix " +
                                 "FROM utilisateur u, produit p, categorie c WHERE u.id_u = p.id_u and c.id_cat = p.id_cat and ";
            string[] motsderecherche = textrecherche.Split(' ');
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

            SqlCommand cmd = new SqlCommand(commandesql, sql.con);
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                MessageBox.Show("trouvé frere ! ! !");
            }
            else
            {
                MessageBox.Show("pas trouver gros");
            }
            return commandesql;
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
                return true;
            }
        }

        public void EnleverProdList(string nomduprod)
        {
            Data.listpanier.Remove(nomduprod);
        }

        /*public bool EnvoyerMessage()
         {
             string commandesql = "INSERT into messager(contenue_m, id_u, id_u_destinataire) values('Je pense que 17€ est un prix acceptable', 6, 1)";
             return true;
         }*/

        public string AfficherMessage(int id_u_expe, int id_u_dest)
        {
            string commandesql = "Select contenue_m, nom_u, prenom_u From messager M, utilisateur U where M.id_u = " +id_u_expe + " AND U.id_u = " + id_u_dest + " OR M.id_u = " + id_u_expe + " AND U.id_u = " + id_u_dest ;
            SqlCommand cmd = new SqlCommand(commandesql);
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
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
        }

        public bool AcheterProd(int id_prod)
        {
            string commandesql = "UPDATE produit SET etat_prod = 'En Négociation' Where id_prod =" + id_prod;
            SqlCommand cmd = new SqlCommand(commandesql);
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduit addProduit = new AddProduit();
            addProduit.Show();
        }
    }
}
