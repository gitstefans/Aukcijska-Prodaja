using MainWindowVM;
using Microsoft.SqlServer.Server;
using ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace auapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Product pr = new Product();
        MainWindowViewModel mainViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mainViewModel;
           
            bidBtn.Visibility = Visibility.Hidden;
           
            startauctionbtn.Visibility = Visibility.Hidden;
            newBtn.Visibility = Visibility.Hidden;
            dlBtn.Visibility = Visibility.Hidden;
            buttonLogout.Visibility = Visibility.Hidden;
            

        }
        
    private void btnNew(object sender, RoutedEventArgs e)
    {
        
        NewEditWindow neweditwin = new NewEditWindow();
       
        neweditwin.DataContext = neweditwin;
        neweditwin.ShowDialog();
    }

    private void btnLgn(object sender, RoutedEventArgs e)
    {
        Login lgn = new Login();
        this.Visibility = Visibility.Hidden;
        lgn.Show();
        
    }

    private void btnReg(object sender, RoutedEventArgs e)
    {
        Register reg = new Register();
        this.Visibility = Visibility.Hidden;
        reg.Show();
    }
    private System.Windows.Forms.Timer timer1;
    private int counter = 120;

    private void restartTimer()
    {
        counter = 120;
        timer1.Start();
    }
    private void auctionbtn(object sender, RoutedEventArgs e)
    {

        timer1 = new System.Windows.Forms.Timer();
        timer1.Tick += Timer1_Tick;
        timer1.Interval = 1000;
        timer1.Start();
        startauctionbtn.Visibility = Visibility.Hidden;


    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
        counter--;
        if (counter == 0)
        {
            timer1.Stop();

            
            pr.ProductID = mainViewModel.CurrentProduct.ProductID;
            pr.ProductName = prName.Text;
            pr.ProductPrice = Convert.ToDecimal(PriceTextBox.Text);
            pr.LastBid = Convert.ToDecimal(lastBidBox.Text);
            pr.LastBidder = lastBidderBox.Text;
            pr.SaveProduct();


            MessageBox.Show("The auction has ended! For product " + pr.ProductName + " Last bid was " + lastBidBox.Text + ". Last bidder is " + lastBidderBox.Text);
        }
        timerlbl.Content = counter.ToString();
    }
    static decimal i = 1;
    private void bidBtn_Click(object sender, RoutedEventArgs e)
    {

        restartTimer();
        lastBidBox.Clear();
        lastBidBox.Text = (decimal.Parse(PriceTextBox.Text) + i).ToString();
        i++;
        lastBidderBox.Text = userLabel.Content.ToString();

    }

    private void btnLogout(object sender, RoutedEventArgs e)
    {
        Login w = new Login();
        MainWindow mw = new MainWindow();
        mw.Hide();
        this.Hide();
        w.ShowDialog();
    }
}
}
