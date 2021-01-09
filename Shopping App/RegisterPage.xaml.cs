using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        public RegisterPage()
        {
            InitializeComponent();
            tgender.Items.Add("Male");
            tgender.Items.Add("Female");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userid = tuserid.Text;
            if (!DbConfig.IsAvailable(userid))
            {
                MessageBox.Show("User ID is not available");
                return;
            }       
            try
            {
                SqlConnection con = DbConfig.Connect();
                SqlCommand cmd = new SqlCommand("insert into customers values(@cname,@add,@gender,@email,@phone,@userid)", con);
                cmd.Parameters.AddWithValue("@cname", tcname.Text);
                cmd.Parameters.AddWithValue("@add", taddress.Text);
                cmd.Parameters.AddWithValue("@gender", tgender.SelectedValue);
                cmd.Parameters.AddWithValue("@email", temail.Text);
                cmd.Parameters.AddWithValue("@phone", tphone.Text);
                cmd.Parameters.AddWithValue("@userid", tuserid.Text);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO users VALUES(@userid,@pwd,'customer')",con);
                cmd.Parameters.AddWithValue("@userid", tuserid.Text);                
                cmd.Parameters.AddWithValue("@pwd", tpwd.Password);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User Registered Successfully..!!","Information");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }

    }
}
