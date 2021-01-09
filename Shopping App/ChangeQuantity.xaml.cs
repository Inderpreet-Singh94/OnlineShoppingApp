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
    /// Interaction logic for ChangeQuantity.xaml
    /// </summary>
    public partial class ChangeQuantity : Window
    {
        private int prodid;
        private string userid;
        public ChangeQuantity(int prodid,string userid)
        {
            InitializeComponent();
            this.prodid = prodid;
            this.userid = userid;
            DataRow row = DbConfig.FindSingleRecord("vcart", $"prodid='{prodid}' and userid='{userid}'");
            tprodid.Text = "" + prodid;
            tpname.Text = (string)row["pname"];                        
            tprice.Text = "" + (int)row["price"];
            tdisc.Text = "" + (int)row["discount"];
            tqty.Text = "" + (int)row["qty"];
            taqty.Text = "" + (int)row["aqty"];
        }

        private void Bsaveqty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand($"update cart set qty='{tqty.Text}' where prodid='{prodid}' and userid='{userid}'",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item quantity updated..!!", "Information");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void Bcancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand($"delete from cart where userid='{userid}' and prodid='{prodid}' ",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item deleted from cart", "Information");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
    }
}
