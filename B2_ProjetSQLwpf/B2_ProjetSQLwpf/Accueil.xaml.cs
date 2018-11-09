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
            labelBonjour.Content = "Bonjour " + CurrentUser.UserName;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void accueilBoutonAfficher_Click(object sender, RoutedEventArgs e)
        {
            Sql.DataReader("SELECT nom_prod, nom_u, description_prod, libelle_cat, etat_prod, prix_prod from produit p, categorie c where p.id_cat = c.id_cat ");
            accueilCanvasDatagrid.items
        }
    }
}
