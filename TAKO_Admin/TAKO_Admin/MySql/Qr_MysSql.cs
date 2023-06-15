using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKO_Admin.Models;

namespace TAKO_Admin.MySql
{
    internal class Qr_MysSql
    {
        public static string connestion_string = Connection_String.ConnectionString;
        public static MySqlConnection conn = new MySqlConnection(connestion_string);

        public static List<Qr_Model> get_refills()
        {
            try
            {
                List<Qr_Model> list = new List<Qr_Model>();

                string querry = "SELECT * FROM qr_details";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Qr_Model m = new Qr_Model();
                        m.Id = int.Parse(dr[0].ToString());                        
                        m.Scan_Value = dr[1].ToString();
                        m.Credits = int.Parse(dr[2].ToString());
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
                List<Qr_Model> list = null;
                return list;
            }
        }

        public static bool add_qr(string qr)
        {
            try
            {
                string querry = "INSERT INTO qr_details(Scan_Value, Credits) VALUES('" + qr + "', 0);";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();               

                conn.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1062))
                {
                    MessageBox.Show("This qr alwready exists.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
                return false;
            }
        }
    }
}
