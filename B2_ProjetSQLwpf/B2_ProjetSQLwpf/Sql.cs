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
    public class Sql
    {
        //public string connectionString = "Data Source=192.168.137.128;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
        public string connectionString = "Data Source=192.168.159.140;Initial Catalog=exchange;User ID=sa;Password=abcd4ABCD";
        public SqlConnection con;

        public void OpenConnexion()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        public void ClosConnexion()
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                MessageBox.Show("Une erreur grave vient de ce passer");
            }   
        }
    }
}
