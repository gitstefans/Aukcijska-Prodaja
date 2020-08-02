using ModelClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace auapp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
           

        }

        private void lgnClick(object sender, RoutedEventArgs e)
        {
            User u = new User();
            u.UserName = lgnUser.Text.Trim();
            u.Password = lgnPassword.Text.Trim();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                string query = "Select * from Users Where UserName = '" + u.UserName + "' and Password = '" + u.Password + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
               
                if (dtbl.Rows.Count == 1)
                {

                    bool isAdmin = (Convert.ToBoolean(dtbl.Rows[0]["IsAdmin"].ToString()));
                    MainWindow mw = new MainWindow();

                    this.Hide();
                   
                    mw.btnRegister.Visibility = Visibility.Hidden;
                    mw.btnLogin.Visibility = Visibility.Hidden;

                    mw.userLabel.Content = u.UserName;
                    mw.bidBtn.Visibility = Visibility.Visible;
                    mw.startauctionbtn.Visibility = Visibility.Visible;
                    mw.buttonLogout.Visibility = Visibility.Visible;
                    if (isAdmin == true) //1= admin, 0= user;
                    {
                        mw.dlBtn.Visibility = Visibility.Visible;
                        mw.newBtn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        mw.dlBtn.Visibility = Visibility.Hidden;
                        mw.newBtn.Visibility = Visibility.Hidden;
                        mw.btnLogin.Visibility = Visibility.Hidden;
                    }
                    mw.Show();
                    


                }
                else
                {

                    MessageBox.Show("Check your username or password");
                    Login w = new Login();
                    w.Show();
                }

            }
        }
    }

}
