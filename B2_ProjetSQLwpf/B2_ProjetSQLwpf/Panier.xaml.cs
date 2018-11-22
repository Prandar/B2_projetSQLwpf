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
    }
}
