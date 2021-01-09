using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Shopping_App
{
    /// <summary>
    /// Interaction logic for CustomerHome.xaml
    /// </summary>
    public partial class CustomerHome : Window
    {
        public CustomerHome()
        {
            InitializeComponent();                        
        }
        private string userid;
        public CustomerHome(string userid)
        {
            InitializeComponent();
            lblUser.Content = "Welcome " + userid;
            this.userid = userid;
            
            list.ItemsSource = GetProducts();
        }

        private List<Product> GetProducts()
        {
            List<Product> plist = new List<Product>();
            foreach (DataRow dr in DbConfig.AllRecords("products").Rows)
            {
                plist.Add(new Product
                {
                    ProdID = (int)dr["prodid"],
                    ProductName = (string)dr["pname"],
                    Category = (string)dr["pcat"],
                    Description = (string)dr["descr"],
                    Price = (int)dr["price"],
                    Discount = (int)dr["discount"],
                    AvailableQty = (int)dr["aqty"]
                });
            }
            return plist;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = list.SelectedItem as Product;
            if (item != null)
            {
                AddtoCart ap = new AddtoCart(item.ProdID,userid);
                ap.Owner = this;
                ap.ShowDialog();
            }        
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bcart_Copy2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void Bcart_Copy1_Click(object sender, RoutedEventArgs e)
        {
            ChangePwd cp = new ChangePwd(userid);
            cp.ShowDialog();
        }

        private void Bcart_Click(object sender, RoutedEventArgs e)
        {
            ViewCart vc = new ViewCart(userid);
            vc.Owner = this;
            vc.ShowDialog();
            list.ItemsSource = GetProducts();
        }

        private void Bcart_Copy_Click(object sender, RoutedEventArgs e)
        {
            OrderHistory oh = new OrderHistory(userid);
            oh.ShowDialog();
        }
    }
}
