using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Shopping_App
{
    /// <summary>
    /// Interaction logic for ViewCart.xaml
    /// </summary>
    public partial class ViewCart : Window
    {
        private string userid;
        public ViewCart()
        {
            InitializeComponent();
        }

        public ViewCart(string userid)
        {
            this.userid = userid;
            InitializeComponent();
            list.ItemsSource=loaddata();
            calcBill();
        }

        private void calcBill()
        {
            SqlConnection con = DbConfig.Connect();
            SqlCommand cmd = new SqlCommand($"select sum(amount) from vcart where userid='{userid}'",con);
            object result = cmd.ExecuteScalar();
            if (result.GetType() == typeof(DBNull))
            {
                lbltotal.Content = "Total Bill $ 0.00";
                lblTds.Content = "TDS @ 10% $ 0.00";
                lblNet.Content = "Net Bill $ 0.00";
            }
            else
            {
                int total = (int)result;
                double tds = total * .10;
                double net = total + tds;
                lbltotal.Content = "Total Bill $ "+total;
                lblTds.Content = "TDS @ 10% $ "+tds;
                lblNet.Content = "Net Bill $ "+net;
            }
        }

        private ObservableCollection<CartItem> loaddata()
        {
            DataTable data = DbConfig.FindRecords("vcart", $"userid='{userid}'");
            ObservableCollection<CartItem> plist = new ObservableCollection<CartItem>();
            foreach (DataRow dr in data.Rows)
            {
                plist.Add(new CartItem
                {
                    ProdId = (int)dr["prodid"],
                    Pname = (string)dr["pname"],                                        
                    Price = (int)dr["price"],
                    Discount = (int)dr["discount"],
                    Quantity = (int)dr["qty"],
                    Amount = (int)dr["amount"],
                    Userid = (string)dr["userid"]
                });
            }
            return plist;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = list.SelectedItem as CartItem;
            if (item != null)
            {
                ChangeQuantity ap = new ChangeQuantity(item.ProdId,userid);
                ap.Owner = this;
                var res = ap.ShowDialog();                
                var plist = loaddata();
                list.ItemsSource = plist;
                calcBill();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand($"delete from cart where userid='{userid}'",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cart is Empty Now","Information");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            } 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int orderid = DbConfig.GenerateOrderNo();
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand("insert into orders(orderid,userid) values(@orderid,@userid)", con);
                
                cmd.Parameters.AddWithValue("@userid", userid);                
                cmd.Parameters.AddWithValue("@orderid",orderid);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"insert into order_details select {orderid},prodid,qty from cart where userid='{userid}'", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"delete from cart where userid='{userid}'", con);
                cmd.ExecuteNonQuery();                
                con.Close();
                DbConfig.UpdateQty(userid); 
                MessageBox.Show("Order Placed Successfully..!!", "Information");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error");
            }
        }
    }
}
