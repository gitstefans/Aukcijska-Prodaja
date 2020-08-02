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
using ModelClass;

namespace auapp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            User user = new User();
            user.UserName = UserName.Text;
            user.Password = Password.Text;
            user.Insert();
            MessageBox.Show("You have been registred. Go to login page.");
            
        }

        private void LoginPage(object sender, RoutedEventArgs e)
        {
            Login w = new Login();
            w.ShowDialog();
        }
    }
}
