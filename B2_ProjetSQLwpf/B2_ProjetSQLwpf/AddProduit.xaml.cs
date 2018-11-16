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
    public partial class AddProduit : Window
    {
        Sql sql = new Sql();
        public AddProduit()
        {
            InitializeComponent();
        }

        private void addProduitAjouterBouton_Click(object sender, RoutedEventArgs e)
        {
            int Prix = int.Parse(addProduitPrixTextBox.Text);
            SqlCommand com = new SqlCommand("AddProduit", sql.con);
            //com.commandtype = commandtype.storedprocedure;
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@nom", addProduitNameTextBox.Text);
            com.Parameters.AddWithValue("@prix", Prix);
            com.Parameters.AddWithValue("@etat", addProduitEtatTextBox.Text);
            com.Parameters.AddWithValue("@photo", addProduitPhotoTextBox.Text);
            com.Parameters.AddWithValue("@description", addProduitDescriptionTextBox.Text);
            com.Parameters.AddWithValue("@id_u", CurrentUser.IdUser);
            com.Parameters.AddWithValue("@id_cat", addproduitCategorieComboBox.SelectedValue);
            Console.WriteLine(CurrentUser.UserName);

            com.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Le produit à bien était ajouté");
            }
            catch
            {
                MessageBox.Show("une erreur c'est produite");
            }

        }

        private void addproduitCategorieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            sql.OpenConnexion();
            SqlCommand cmd = new SqlCommand("SELECT id_cat, libelle_cat FROM categorie", sql.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            addproduitCategorieComboBox.DataContext = dt.DefaultView;
            addproduitCategorieComboBox.DisplayMemberPath = "libelle_cat";
            addproduitCategorieComboBox.SelectedValuePath = "id_cat";
            
        }
    }
}
