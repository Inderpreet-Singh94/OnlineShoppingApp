using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void Bsave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand("insert into products values(@pname,@cat,@desc,@disc,@price,@aqty,@min,@max,@start,@end)", con);
                cmd.Parameters.AddWithValue("@pname", tpname.Text);
                cmd.Parameters.AddWithValue("@cat", tpcat.Text);
                cmd.Parameters.AddWithValue("@desc", tpdesc.Text);
                cmd.Parameters.AddWithValue("@price", tprice.Text);
                cmd.Parameters.AddWithValue("@disc", tdisc.Text);
                cmd.Parameters.AddWithValue("@aqty", taqty.Text);
                cmd.Parameters.AddWithValue("@min", tmin.Text);
                cmd.Parameters.AddWithValue("@max", tmax.Text);
                cmd.Parameters.AddWithValue("@start", tstart.SelectedDate);
                cmd.Parameters.AddWithValue("@end", tend.SelectedDate);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product Added Successfully..!!","Information");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error");
            }
        }
    }
}
