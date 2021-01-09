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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {       
        public OrderDetails(int orderid)
        {
            InitializeComponent();
            list.ItemsSource=loaddata(orderid);            
        }

        private ObservableCollection<OrderDetail> loaddata(int orderid)
        {
            DataTable data = DbConfig.FindRecords("vorders",$"orderid={orderid}");
            ObservableCollection<OrderDetail> plist = new ObservableCollection<OrderDetail>();
            foreach (DataRow dr in data.Rows)
            {
                plist.Add(new OrderDetail
                {
                    OrderId = (int)dr["orderid"],                                        
                    ProdId = (int)dr["prodid"],
                    Pname = (string)dr["pname"],
                    Quantity = (int)dr["qty"],
                    Price=(int)dr["price"]
                });
            }
            return plist;
        }
    }
}
