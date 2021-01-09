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

namespace Shopping_App
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private string userid;
        public Dashboard()
        {
            InitializeComponent();
        }

        public Dashboard(string userid)
        {
            InitializeComponent();
            lblUser.Content = "Welcome " + userid;
            this.userid = userid;
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.Show();
        }

        
        private void BAllProducts_Click(object sender, RoutedEventArgs e)
        {
            AllProducts ap = new AllProducts();
            ap.ShowDialog();
        }

        private void BOrderProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BAllOrders_Click(object sender, RoutedEventArgs e)
        {
            AllOrders ao = new AllOrders();
            ao.ShowDialog();
        }

        private void BAllCustomers_Click(object sender, RoutedEventArgs e)
        {
            AllCustomers ac = new AllCustomers();
            ac.ShowDialog();
        }

        private void BchangePwd_Click(object sender, RoutedEventArgs e)
        {
            ChangePwd cp = new ChangePwd(userid);
            cp.ShowDialog();
        }

        private void BLogout_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BAllProductwise_Click(object sender, RoutedEventArgs e)
        {
            InventoryDetails details = new InventoryDetails();
            details.ShowDialog();
        }
    }
}
