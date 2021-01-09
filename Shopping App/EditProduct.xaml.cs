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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        private int prodid;
        public EditProduct()
        {
            InitializeComponent();
        }
             
        public EditProduct(int prodid)
        {
            InitializeComponent();
            this.prodid = prodid;
            DataRow row = DbConfig.FindSingleRecord("products", $"prodid='{prodid}'");
            tprodid.Text = "" + prodid;
            tpname.Text = (string)row["pname"];
            tpcat.Text = (string)row["pcat"];
            tpdesc.Text = (string)row["descr"];
            tprice.Text = ""+(int)row["price"];
            tdisc.Text = ""+(int)row["discount"];
            taqty.Text = ""+(int)row["aqty"];
            tmin.Text = row["minqty"].GetType()== typeof(DBNull)? "":""+ (int)row["minqty"];
            tmax.Text = row["maxqty"].GetType() == typeof(DBNull) ? "":""+(int)row["maxqty"];
            tstart.SelectedDate = row["stdate"].GetType() == typeof(DBNull) ? DateTime.Now:(DateTime)row["stdate"];
            tend.SelectedDate = row["enddate"].GetType() == typeof(DBNull) ? DateTime.Now : (DateTime)row["enddate"];
        }

        private void Bsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand("update products set  pname=@pname,pcat=@cat,descr=@desc,discount=@disc,price=@price,aqty=@aqty," +
                    "minqty=@min,maxqty=@max,stdate=@start,enddate=@end where prodid=@prodid", con);
                cmd.Parameters.AddWithValue("@pname", tpname.Text);
                cmd.Parameters.AddWithValue("@cat", tpcat.Text);
                cmd.Parameters.AddWithValue("@desc", tpdesc.Text);
                cmd.Parameters.AddWithValue("@price", tprice.Text);
                cmd.Parameters.AddWithValue("@disc", tdisc.Text);
                cmd.Parameters.AddWithValue("@aqty", taqty.Text);
                cmd.Parameters.AddWithValue("@prodid", prodid);
                cmd.Parameters.AddWithValue("@min", tmin.Text);
                cmd.Parameters.AddWithValue("@max", tmax.Text);
                cmd.Parameters.AddWithValue("@start",tstart.SelectedDate);
                cmd.Parameters.AddWithValue("@end", tend.SelectedDate);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product Saved Successfully..!!", "Information");
                this.Close();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error");
            }
            
        }
    }
}
