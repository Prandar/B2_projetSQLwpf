using System;
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
    static class Sql
    {
        public static string connectionString = "Data Source=192.168.137.128;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
        //public static string connectionString = "Data Source=192.168.159.140;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
        public static SqlConnection con;

        public static void OpenConnexion()
        {
            con = new SqlConnection(connectionString);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("La connection à la base de données a échoué");
            }
        }

        public static void ClosConnexion()
        {
            con.Close();
        }

        public static SqlDataReader DataReader(string Query)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

    

    }
}
