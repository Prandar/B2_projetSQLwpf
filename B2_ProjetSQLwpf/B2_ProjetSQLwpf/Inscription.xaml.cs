﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        Sql sql = new Sql();
        public Inscription()
        {
            InitializeComponent();
        }

        private void InscriptionTextBoxMail_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void InscriptionButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sql.OpenConnexion();
                
                SqlCommand com = new SqlCommand("Inscription", sql.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@nom", InscriptionTextBoxNom).Value = InscriptionTextBoxNom.Text;
                com.Parameters.AddWithValue("@prenom", InscriptionTextBoxPrenom).Value = InscriptionTextBoxPrenom.Text;
                com.Parameters.AddWithValue("@ad1", InscriptionTextBoxAdresse1).Value = InscriptionTextBoxAdresse1.Text;
                com.Parameters.AddWithValue("@ad2", InscriptionTextBoxAdresse2).Value = InscriptionTextBoxAdresse2.Text;
                com.Parameters.AddWithValue("@cp", InscriptionTextBoxCp).Value = InscriptionTextBoxCp.Text;
                com.Parameters.AddWithValue("@ville", InscriptionTextBoxVille).Value = InscriptionTextBoxVille.Text;
                com.Parameters.AddWithValue("@tel", InscriptionTextBoxTel).Value = InscriptionTextBoxTel.Text;
                com.Parameters.AddWithValue("@mdp", InscriptionTextBoxMdp).Value = InscriptionTextBoxMdp.Password;
                com.Parameters.AddWithValue("@mail", InscriptionTextBoxMail).Value = InscriptionTextBoxMail.Text;
                com.Parameters.AddWithValue("@solde", InscriptionTextBoxSolde).Value = InscriptionTextBoxSolde.Text;
                Console.WriteLine("@nom");

                com.ExecuteNonQuery();
                try
                {
                    MessageBox.Show("Vous êtes bien inscrit :)");
                }
                catch
                {
                    MessageBox.Show("Une erreur c'est produite");
                }
            }
            catch
            {
                MessageBox.Show("Une erreur inattendu c'est produite");
            }
            
        }
    }
}
