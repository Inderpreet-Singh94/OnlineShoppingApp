using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddtoCart.xaml
    /// </summary>
    public partial class AddtoCart : Window
    {
        private int prodid;
        private string userid;
        public AddtoCart()
        {
            InitializeComponent();
        }

        public AddtoCart(int prodid,string userid)
        {
            this.prodid = prodid;
            this.userid = userid;
            InitializeComponent();
            DataRow row = DbConfig.FindSingleRecord("products", $"prodid='{prodid}'");
            tprodid.Text = "" + prodid;
            tpname.Text = (string)row["pname"];
            tpcat.Text = (string)row["pcat"];            
            tprice.Text = "" + (int)row["price"];
            int disc = (int)row["discount"];
            tdisc.Text =  disc==0 ? "No Discount" : ""+(int)row["discount"];
            taqty.Text = "" + (int)row["aqty"];
            lblMin.Content = disc==0 ? "" :"On Min Qty " + (int)row["minqty"];
            lblOffer.Content =disc==0 ? "": "Offer Available till " + ((DateTime)row["enddate"]).ToString("dd-MMM-yyyy");
        }

        private void Baddtocart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand("insert into cart values(@userid,@prodid,@qty)", con);
                cmd.Parameters.AddWithValue("@userid", userid);                
                cmd.Parameters.AddWithValue("@qty", tqty.Text);
                cmd.Parameters.AddWithValue("@prodid", prodid);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product Added to Cart Successfully..!!", "Information");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error");
            }
        }
    }
}
