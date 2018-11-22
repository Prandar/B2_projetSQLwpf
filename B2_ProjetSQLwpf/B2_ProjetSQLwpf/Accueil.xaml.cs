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
            try
            {
                if (CurrentUser.UserAdmin == true)
                {
                    DeleteAccueilButton.Visibility = Visibility.Visible;
                }
                else
                {
                    DeleteAccueilButton.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception)
            {

                throw;
            }
            accueilDataGrid.Items.Refresh();
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
                foreach (String item in Data.listpanier)
                {
                    Console.WriteLine(item);
                }
                string nom = gd["Nom"].ToString();
                Data.listpanier.Add(nom);
            }     
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
                //MessageBox.Show("trouvé frere ! ! !");
            }
            else
            {
                MessageBox.Show("pas trouvé ! ! !");
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
        /*public bool AcheterProd(int id_prod)
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
        }*/

        public bool AnnulerAchatProd(int id_prod)
        {
            string commandesql = "UPDATE produit SET etat_prod = 'En Vente' Where id_prod =" + id_prod;
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

        private void accueilButtonPaniers_Click(object sender, RoutedEventArgs e)
        {
            Panier panier = new Panier();
            panier.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentUser.IdUser = 0;
                CurrentUser.UserName = null;
                this.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ProfilAccueilButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil();
            profil.Show();
        }

        private void DeleteAccueilButton_Click(object sender, RoutedEventArgs e)
        {
            sql.OpenConnexion();
            try
            {
                DataRowView gd = (DataRowView)accueilDataGrid.SelectedItem;
                if (gd != null)
                {
                    string nom = gd["Nom"].ToString();                    
                    string description = gd["Description"].ToString();
                    string query = "DELETE FROM Produit WHERE nom_prod = '" + nom + "' and description_prod = '" + description +"'";
                    SqlCommand cmd = new SqlCommand(query, sql.con);
                    SqlDataReader reader = cmd.ExecuteReader();                    
                    MessageBox.Show("Le produit a bien été suppimé");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sql.ClosConnexion();
            }
        }

        private void accueilDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //accueilDataGrid.Items.Refresh();
        }
    }
}
