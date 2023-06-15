using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKO_Admin.Models;
using TAKO_Admin.MySql;

namespace TAKO_Admin.UserControlls
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            show_products();
        }

        public void show_products()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();

            List<Product_Model> list = new List<Product_Model>();
            list = Product_MySql.get_products();

            if (list != null)
            {
                foreach (Product_Model m in list)
                {
                    dataGridView1.Rows.Add(m.Id.ToString(), m.Name, m.Price.ToString());
                }
            }
            UserControl1.AutosizeColumns(dataGridView1);
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox4.Text;
            string price = textBox2.Text;

            Product_MySql.delete_product(id);
            show_products();

            textBox1.Clear();
            textBox4.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text;
            string price = textBox2.Text;

            Product_MySql.add_product(name, price);

            show_products();

            textBox4.Clear();
            textBox2.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox4.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox4.Text;
            string price = textBox2.Text;

            Product_MySql.update_product(id, name, price);
            show_products();

            textBox1.Clear();
            textBox4.Clear();
            textBox2.Clear();
        }
    }
}
