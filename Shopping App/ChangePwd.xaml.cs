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
    /// Interaction logic for ChangePwd.xaml
    /// </summary>
    public partial class ChangePwd : Window
    {
        private string userid;
        public ChangePwd()
        {
            InitializeComponent();
        }
        public ChangePwd(string userid)
        {
            InitializeComponent();
            tuserid.Text = userid;
        }

        private void Bchange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userid = tuserid.Text;
                string opwd = toldpwd.Password;
                string pwd = tpwd.Password;
                string cpwd = tcpwd.Password;
                DataRow row= DbConfig.FindSingleRecord("users", $"userid='{userid}' and pwd='{opwd}'");
                if (row == null)
                {
                    MessageBox.Show("Current Password not match","Error");
                }
                else if (pwd != cpwd)
                {
                    MessageBox.Show("Password not confirmed with new password","Error");
                }
                else
                {
                    SqlConnection con = DbConfig.Connect();
                    
                    SqlCommand cmd = new SqlCommand($"update users set pwd='{pwd}' where userid='{userid}'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password changed successfully","Information");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error");
            }
        }
    }
}
