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
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

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
            sql.OpenConnexion();
            InitializeComponent();
            try
        {
                string query = "SELECT mail_u FROM utilisateur WHERE id_u = " + CurrentUser.IdUser;
                SqlCommand cmd = new SqlCommand(query, sql.con);                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    from.Text = reader.GetString(reader.GetOrdinal("mail_u"));
                    foreach (var item in Data.listeMail)
                    {
                        to.Text = item;
                    }
                    //from.IsReadOnly = true;
                    to.IsReadOnly = true;
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

        private void messageButton_Click(object sender, RoutedEventArgs e)
        {
            string mailRe = from.Text;
            string[] mailRecherche = mailRe.Split('@');
            string item = mailRecherche[1];
            //Console.WriteLine(item);
            if (item == "gmail.com")
            {
                MailMessage mail = new MailMessage(from.Text, to.Text, subjet.Text, messageTextBox.Text);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new NetworkCredential(user.Text, mdp.Password);
                client.EnableSsl = true;
                //client.UseDefaultCredentials = true;
                client.Send(mail);
                MessageBox.Show("mail sent", "success", MessageBoxButton.OK);
            }
            else if(item == "live.fr")
                {
                MailMessage mail = new MailMessage(from.Text, to.Text, subjet.Text, messageTextBox.Text);
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.Credentials = new NetworkCredential(user.Text, mdp.Password);
                client.EnableSsl = true;
                client.Send(mail);
                MessageBox.Show("mail sent", "success", MessageBoxButton.OK);
                }
                else
                {
                MessageBox.Show("defefe");
                }
        
            
        }   
    }
}
