﻿using System;
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
            //RechercheProduit("patate moulinex truc");
            //AfficherToutProduits();
            //AjouterProduit("Moulin à café", 15, "C:\\test", "Le moulin a café de mon coloc", 1, 1);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void accueilBoutonAfficher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlProduit = RechercheProduit(accueilTextboxBarDeRecherche.Text);
                SqlDataAdapter sda = new SqlDataAdapter(sqlProduit, sql.connectionString);
                DataTable dt = new DataTable("utilisateur, produit");
                sda.Fill(dt);
                accueilDataGrid.ItemsSource = dt.DefaultView;
            }
            catch
            {
                MessageBox.Show("Error.");
            }
            
        }

        public SqlDataReader AfficherToutProduits()
        {
            try
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
            catch
            {
                throw new Exception();
            }

            
        }

        public string RechercheProduit(string textrecherche)
        {
            try
            {
                string commandesql = "SELECT nom_prod as Nom, nom_u, description_prod, libelle_cat, etat_prod, prix_prod FROM utilisateur u, produit p, categorie c WHERE u.id_u = p.id_u and c.id_cat = p.id_cat and";
                SqlCommand cmd = new SqlCommand(commandesql, sql.con);
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

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
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
            }
            catch
            {
                throw new Exception();
            } 
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduit addProduit = new AddProduit();
            addProduit.Show();
        }
    }
}
