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
using System.Net.Mail;

namespace B2_ProjetSQLwpf
{
    /// <summary>
    /// Logique d'interaction pour Messagerie.xaml
    /// </summary>
    public partial class Messagerie : Window
    {
        Sql sql = new Sql();
        public Messagerie()
        {
            InitializeComponent();
        }

        private void messageButton_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            if (message == "")
            {
                MessageBox.Show("veuillez écrire un message.");
            }
            else
            {

            }
        }

        public string AfficherMessage(int id_u_expe, int id_u_dest)
        {
            string commandesql = "Select contenue_m, nom_u, prenom_u From messager M, utilisateur U where M.id_u = " + id_u_expe + " AND U.id_u = " + id_u_dest + " OR M.id_u = " + id_u_expe + " AND U.id_u = " + id_u_dest;
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

        public bool EnvoyerMessage()
         {
             string commandesql = "INSERT into messager(contenue_m, id_u, id_u_destinataire) values('Je pense que 17€ est un prix acceptable', 6, 1)";
             return true;
         }

        

    }
}
