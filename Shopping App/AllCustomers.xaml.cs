using System;
using System.Collections.Generic;
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
    /// Interaction logic for AllCustomers.xaml
    /// </summary>
    public partial class AllCustomers : Window
    {
        public AllCustomers()
        {
            InitializeComponent();
            DataTable data = DbConfig.AllRecords("customers");
            List<Customer> clist = new List<Customer>();
            foreach(DataRow row in data.Rows)
            {
                clist.Add(new Customer {
                    CustId = (int)row[0],
                    Name = (string)row[1],
                    Address = (string)row[2],
                    Gender = (string)row[3],
                    Email = (string)row[4],
                    Phone = (string)row[5],
                    UserId = (string)row[6]
                });
            }

            list.ItemsSource = clist;
        }
    }
}
