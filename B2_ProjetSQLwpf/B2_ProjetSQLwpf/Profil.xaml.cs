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
    /// Logique d'interaction pour Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {
        Sql sql = new Sql();
        public Profil()
        {
            InitializeComponent();
            ProfilLabel.Content = "Profil de " + CurrentUser.UserName;
            string sqlProd = RechercheProduit("");
            SqlDataAdapter sda = new SqlDataAdapter(sqlProd, sql.connectionString);
            DataTable dt = new DataTable("utilisateur, produit");
            sda.Fill(dt);
            ProfilDataGrid.ItemsSource = dt.DefaultView;
        }

        public string RechercheProduit(string textrecherche)
        {
            sql.OpenConnexion();
            string commandesql = "SELECT nom_prod as Nom, nom_u as Vendeur, description_prod as Description, libelle_cat as Catégorie, etat_prod as Etat, prix_prod as Prix " +
                                 "FROM utilisateur u, produit p, categorie c WHERE u.id_u = p.id_u and c.id_cat = p.id_cat and u.id_u = " + CurrentUser.IdUser + " and ";
            try
            {
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
            }
            catch (Exception)
            {

                MessageBox.Show("pas trouver gros");
            }            
            return commandesql;
        }

        private void BackProfilButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DeleteProfilButton_Click(object sender, RoutedEventArgs e)
        {
            sql.OpenConnexion();
            try
            {
                DataRowView gd = (DataRowView)ProfilDataGrid.SelectedItem;
                if (gd != null)
                {
                    string nom = gd["Nom"].ToString();
                    string query = "DELETE FROM Produit WHERE id_u = " + CurrentUser.IdUser + " and nom_prod = '" + nom + "'";
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
    }
}
