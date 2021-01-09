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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shopping_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tuser.Text == "")
            {
                MessageBox.Show("User Name must be given","Error");
            }
            else if (tpwd.Password == "")
            {
                MessageBox.Show("Password must be given","Error");
            }
            else
            {
                try
                {
                    SqlConnection con = DbConfig.Connect();
                    SqlCommand cmd = new SqlCommand("select * from users where userid=@userid and pwd=@pwd", con);
                    cmd.Parameters.AddWithValue("@userid", tuser.Text);
                    cmd.Parameters.AddWithValue("@pwd", tpwd.Password);
                    DataTable table = new DataTable();
                    table.Load(cmd.ExecuteReader());
                    con.Close();
                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid username or password", "Error");                        
                    }
                    else
                    {
                        MessageBox.Show("Login Successful to the System", "Information");
                        if (table.Rows[0]["role"].ToString() == "admin")
                        {
                            Dashboard ad = new Dashboard(tuser.Text);
                            ad.Show();
                            this.Hide();
                        }
                        else
                        {
                            CustomerHome ch = new CustomerHome(tuser.Text);
                            ch.Show();
                            this.Hide();
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error "+ex.Message, "Error");
                }                               
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterPage reg = new RegisterPage();
            reg.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
