using System;
using System.Data;
using System.Data.SqlClient;

namespace Shopping_App
{
    class DbConfig
    {
        public static SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection("server=B1FVMT2\\SQLEXPRESS;database=shopdb;integrated security=true;");
            con.Open();
            return con;
        }

        public static void UpdateQty(string userid)
        {
            DataTable data = DbConfig.FindRecords("cart",$"userid='{userid}'");
            SqlConnection con = Connect();
            foreach (DataRow row in data.Rows)
            {
                int prodid = (int)row["prodid"];
                int qty = (int)row["qty"];
                SqlCommand cmd = new SqlCommand($"update products set aqty=aqty-{qty} where prodid={prodid}",con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public static int GenerateOrderNo()
        {
            SqlConnection con = Connect();
            SqlCommand cmd = new SqlCommand("select max(orderid) from orders",con);
            //DataTable data = new DataTable();
            //data.Load(cmd.ExecuteReader());
            //DataRow row = data.Rows[0];
            object max=cmd.ExecuteScalar();
            con.Close();
            if (max.GetType() == typeof(DBNull))
                return 1001;
            else
                return (int)max + 1;
        }

        public static DataTable AllRecords(string table)
        {
            SqlConnection con = Connect();
            SqlCommand cmd = new SqlCommand("select * from "+table, con);
            DataTable data = new DataTable();
            data.Load(cmd.ExecuteReader());
            con.Close();
            return data;
        }

        public static DataTable FindRecords(string table,string condition)
        {
            SqlConnection con = Connect();
            SqlCommand cmd = new SqlCommand("select * from " + table+" where "+condition, con);
            DataTable data = new DataTable();
            data.Load(cmd.ExecuteReader());
            con.Close();
            return data;
        }

        public static DataRow FindSingleRecord(string table, string condition)
        {
            SqlConnection con = Connect();
            SqlCommand cmd = new SqlCommand("select * from " + table + " where " + condition, con);
            DataTable data = new DataTable();
            data.Load(cmd.ExecuteReader());
            con.Close();
            if (data.Rows.Count == 0)
                return null;
            else
                return data.Rows[0];
        }

        public static bool IsAvailable(string userid)
        {
            SqlConnection con = Connect();
            SqlCommand cmd = new SqlCommand("select * from users where userid='" + userid + "'", con);
            DataTable data = new DataTable();
            data.Load(cmd.ExecuteReader());
            con.Close();
            if (data.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
