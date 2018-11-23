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
    public partial class Panier : Window
    {
        ListViewItem itemDeBase = new ListViewItem();
        Sql sql = new Sql();
        public Panier()
        {
            InitializeComponent();
            namePanierLabel.Content = "Voici la liste de vos achat Monsieur " + CurrentUser.UserName;

            foreach (var item in Data.listpanier)
            {
                itemDeBase = new ListViewItem();
                /*itemDeBase.Background = Brushes.Green;
                itemDeBase.Foreground = Brushes.White;*/
                itemDeBase.FontSize = 25;
                itemDeBase.Content = item;
                listPanierListView.Items.Add(itemDeBase);
                listPanierListView.Items.Refresh();
            }

        }

        private void panierButtonSuppr_Click(object sender, RoutedEventArgs e)
        {
            foreach (ListViewItem item in listPanierListView.SelectedItems)
            {
                //Console.WriteLine(Data.listpanier.Count);
                String selection = item.Content.ToString();
                Data.listpanier.Remove(selection);
                //Console.WriteLine(Data.listpanier.Count);
                listPanierListView.Items.Remove(selection);
                //listPanierListView.Items.Refresh();
            }
        }

        public Boolean VerifierPanier(String _string)
        {
            foreach (ListViewItem item in listPanierListView.Items)
            {
                if (_string == item.Content.ToString())
                {
                    return false;
                }
            }
            return true;
        }

        private void BackPanierButton_Click(object sender, RoutedEventArgs e)
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

        private void panierButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (ListViewItem item in listPanierListView.SelectedItems)
            {
                String _titreOBJ = item.Content.ToString();


                int _id = RechercheIdProduit(_titreOBJ);
                sql.OpenConnexion();

                String query = " insert into achat values ("+ CurrentUser.IdUser +", " + _id + ")";
                SqlCommand cmd = new SqlCommand(query, sql.con);
                //cmd.Parameters.AddWithValue("@id_u", CurrentUser.IdUser);
                //cmd.Parameters.AddWithValue("@id", _id);
                //cmd.ExecuteNonQuery();
                MessageBox.Show("youpis");


                //AcheterProd(_id_prod);

            }
            sql.ClosConnexion();
        }

        public bool AcheterProd(int id_prod)
        {
            sql.OpenConnexion();
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

        public int RechercheIdProduit(string textrecherche)
        {
            int _id = 0;
            sql.OpenConnexion();
            string commandesql = "SELECT TOP(1) id_prod FROM produit WHERE";
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

                _id = dataReader.GetInt32(0);
                Console.WriteLine(_id);
            }
            else
            {
                MessageBox.Show("pas trouvé ! ! !");
            }



            //String truc = dataReader.GetString(0);

            //Console.WriteLine(truc.ToString());

            //sql.ClosConnexion();
            return _id;
        }
    }
}
