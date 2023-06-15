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
    internal class Product_MySql
    {
        public static string connestion_string = Connection_String.ConnectionString;
        public static MySqlConnection conn = new MySqlConnection(connestion_string);

        public static List<Product_Model> get_products()
        {
            try
            {
                List<Product_Model> list = new List<Product_Model>();

                string querry = "SELECT * FROM products";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Connection = conn;

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Product_Model m = new Product_Model();
                        m.Id = int.Parse(dr[0].ToString());
                        m.Name = dr[1].ToString();
                        m.Price = int.Parse(dr[2].ToString());
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
                List<Product_Model> list = null;
                return list;
            }
        }

        public static void add_product(string name, string price)
        {
            try
            {
                string querry = "INSERT INTO products(Name,Price) VALUES('" + name + "', "+price+");";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("New product added.");

                conn.Close();                
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1062))
                {
                    MessageBox.Show("This product alwready exists.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

        public static void update_product(string id, string name, string price)
        {
            try
            {
                string querry = "UPDATE products SET Name = '" + name + "', Price = "+price+" WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product has been updated!");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }

        public static void delete_product(string id)
        {
            try
            {
                string querry = "DELETE FROM products WHERE ID = " + id + ";";

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(querry, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product has been deleted!");

                conn.Close();
            }
            catch (MySqlException ex)
            {
                if ((ex.Source == "MySql.Data") && (ex.Number == 1451))
                {
                    MessageBox.Show("This product can't be deleted!");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
        }

    }
}
