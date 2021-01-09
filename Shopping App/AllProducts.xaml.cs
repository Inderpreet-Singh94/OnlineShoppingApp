using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Shopping_App
{
    /// <summary>
    /// Interaction logic for AllProducts.xaml
    /// </summary>
    public partial class AllProducts : Window
    {
        public AllProducts()
        {
            InitializeComponent();
            list.ItemsSource = loaddata();
        }

        private ObservableCollection<Product> loaddata()
        {
            DataTable table = DbConfig.AllRecords("products");
            ObservableCollection<Product> plist = new ObservableCollection<Product>();
            foreach(DataRow dr in table.Rows)
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
                EditProduct ap = new EditProduct(item.ProdID);
                ap.Owner = this;
                var res = ap.ShowDialog();                
                if (res == false)
                {                    
                    var plist = loaddata();
                    list.ItemsSource = plist;                              
                }
            }
            
        }
    }
}
