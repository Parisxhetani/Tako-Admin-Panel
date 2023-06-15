using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TAKO_Admin.Models;

namespace TAKO_Admin.MySql
{
    internal class Balance_MySql
    {

        public static string connestion_string = Connection_String.ConnectionString;
        public static MySqlConnection conn = new MySqlConnection(connestion_string);

        public static List<Refill_Model> get_refills()
        {
            try
            {
                List<Refill_Model> list = new List<Refill_Model>();

                string querry = "SELECT refills.ID, refills.Amount, users.Name, qr_details.Scan_Value " +
                    "FROM refills " +
                    "INNER JOIN users on refills.User_ID = users.ID " +
                    "INNER JOIN qr_details on refills.QR_ID = qr_details.ID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Refill_Model m = new Refill_Model();
                        m.Id = int.Parse(dr[0].ToString());
                        m.Amount = int.Parse(dr[1].ToString());
                        m.User = dr[2].ToString();
                        m.Qr = dr[3].ToString();
                        list.Add(m);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list;
                }
                else
                {
                    MessageBox.Show("Database is empty.");
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Refill_Model> list = null;
                return list;
            }
        }

        public static List<Refill_Model> get_withdraws()
        {
            try
            {
                List<Refill_Model> list = new List<Refill_Model>();

                string querry = "SELECT withdraws.ID, withdraws.Credits, users.Name, qr_details.Scan_Value " +
                    "FROM withdraws " +
                    "INNER JOIN users on withdraws.User_ID = users.ID " +
                    "INNER JOIN qr_details on withdraws.QR_ID = qr_details.ID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Refill_Model m = new Refill_Model();
                        m.Id = int.Parse(dr[0].ToString());
                        m.Amount = int.Parse(dr[1].ToString());
                        m.User = dr[2].ToString();
                        m.Qr = dr[3].ToString();
                        list.Add(m);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list;
                }
                else
                {
                    MessageBox.Show("Database is empty.");
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Refill_Model> list = null;
                return list;
            }
        }

        public static List<Sales_Model> get_sales()
        {
            try
            {
                List<Sales_Model> list = new List<Sales_Model>();

                string querry = "SELECT sales.ID, users.Name, products.Name, sales.Quantity, qr_details.Scan_Value " +
                    "FROM sales " +
                    "INNER JOIN users on sales.User_ID = users.ID " +
                    "INNER JOIN products on sales.Product_ID = products.ID " +
                    "INNER JOIN qr_details on sales.QR_ID = qr_details.ID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Sales_Model m = new Sales_Model();
                        m.Id = int.Parse(dr[0].ToString());
                        m.User = dr[1].ToString();
                        m.Product = dr[2].ToString();
                        m.Quantity = int.Parse(dr[3].ToString());
                        m.Qr = dr[4].ToString();
                        list.Add(m);
                    }
                    conn.Close();
                    //MessageBox.Show("done");
                    return list;
                }
                else
                {
                    MessageBox.Show("Database is empty.");
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                List<Sales_Model> list = null;
                return list;
            }
        }

        public static int get_refills_total_amount()
        {
            try
            {
                int amount = 0;
                string querry = "SELECT SUM(Amount) FROM refills";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    amount = int.Parse(dr[0].ToString());                   
                }
                conn.Close();
                return amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
                return 0;
            }
        }

        public static int get_withdraws_total_amount()
        {
            try
            {
                int amount = 0;
                string querry = "SELECT SUM(Credits) FROM withdraws";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    amount = int.Parse(dr[0].ToString());
                }
                conn.Close();
                return amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
                return 0;
            }
        }

        public static int get_sales_total_amount()
        {
            try
            {
                int amount = 0;
                string querry = "SELECT SUM(sales.Quantity * products.Price) " +
                    "FROM sales " +
                    "INNER JOIN products on sales.Product_ID = products.ID";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    amount = int.Parse(dr[0].ToString());
                }
                conn.Close();
                return amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
                return 0;
            }
        }
    }
}
