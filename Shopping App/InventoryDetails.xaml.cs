using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InventoryDetails.xaml
    /// </summary>
    public partial class InventoryDetails : Window
    {
        public InventoryDetails()
        {
            InitializeComponent();
            list.ItemsSource = loaddata();
            calctotal();
        }

        private void calctotal()
        {
            SqlConnection con = DbConfig.Connect();
            SqlCommand cmd = new SqlCommand("select sum(amount) from inventory", con);
            object amount = cmd.ExecuteScalar();
            con.Close();
            lblTotal.Content = "Rs."+amount.ToString();
        }

        private ObservableCollection<Inventory> loaddata()
        {
            DataTable table = DbConfig.AllRecords("inventory");
            ObservableCollection<Inventory> plist = new ObservableCollection<Inventory>();
            foreach (DataRow dr in table.Rows)
            {
                plist.Add(new Inventory
                {
                    ProdId = (int)dr["prodid"],
                    Pname = (string)dr["pname"],                                        
                    Price = (int)dr["price"],
                    Amount = (int)dr["amount"],
                    InStock = (int)dr["aqty"],
                    SoldQty = (int)dr["qtysold"]
                });
            }
            return plist;
        }
    }
}
