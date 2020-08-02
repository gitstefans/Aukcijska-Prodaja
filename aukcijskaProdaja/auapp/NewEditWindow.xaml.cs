using MainWindowVM;
using ModelClass;
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

namespace auapp
{
    /// <summary>
    /// Interaction logic for NewEditWindow.xaml
    /// </summary>
    public partial class NewEditWindow : Window
    {
       
       
        public NewEditWindow()
        {
            InitializeComponent();
            NewEditWindowVM newvm = new NewEditWindowVM();

            this.DataContext = newvm;


        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            
            Product pr = new Product();
            pr.ProductName = PNtextBox.Text;
            pr.ProductPrice = Convert.ToDecimal(PPtextBox.Text);
            pr.SaveProduct();
           
            MessageBox.Show("Product has been saved");
            

            DialogResult = true;
            


        }
    }
}
