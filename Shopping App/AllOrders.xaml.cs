using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for AllOrders.xaml
    /// </summary>
    public partial class AllOrders : Window
    {
        public AllOrders()
        {
            InitializeComponent();
            list.ItemsSource = loaddata();
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = list.SelectedItem as Order;
            if (item != null)
            {
                OrderDetails ap = new OrderDetails(item.OrderId);
                ap.Owner = this;
                ap.ShowDialog();                
            }
        }

        private ObservableCollection<Order> loaddata()
        {
            DataTable data = DbConfig.AllRecords("orders");
            ObservableCollection<Order> plist = new ObservableCollection<Order>();
            foreach (DataRow dr in data.Rows)
            {
                plist.Add(new Order
                {
                    OrderId = (int)dr["orderid"],
                    OrderDate = ((DateTime)dr["orderdate"]).ToString("dd-MMM-yyyy"),                    
                    UserId = (string)dr["userid"]
                });
            }
            return plist;
        }
    }
}
