using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TAKO_Admin.Models;
using TAKO_Admin.MySql;

namespace TAKO_Admin.UserControlls
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            show_refills();
            show_withdraws();
            show_sales();

            make_chart();
        }
        public void show_refills()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();

            List<Refill_Model> list = new List<Refill_Model>();
            list = Balance_MySql.get_refills();

            if (list != null)
            {
                foreach (Refill_Model m in list)
                {
                    dataGridView1.Rows.Add(m.Id.ToString(), m.Amount.ToString(), m.User, m.Qr);
                }
            }
            AutosizeColumns(dataGridView1);
        }

        public void show_withdraws()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.ClearSelection();

            List<Refill_Model> list = new List<Refill_Model>();
            list = Balance_MySql.get_withdraws();

            if (list != null)
            {
                foreach (Refill_Model m in list)
                {
                    dataGridView2.Rows.Add(m.Id.ToString(), m.Amount.ToString(), m.User, m.Qr);
                }
            }
            AutosizeColumns(dataGridView2);
        }

        public void show_sales()
        {
            dataGridView3.Rows.Clear();
            dataGridView3.ClearSelection();

            List<Sales_Model> list = new List<Sales_Model>();
            list = Balance_MySql.get_sales();

            if (list != null)
            {
                foreach (Sales_Model m in list)
                {
                    dataGridView3.Rows.Add(m.Id.ToString(), m.User, m.Product, m.Quantity, m.Qr);
                }
            }
            AutosizeColumns(dataGridView3);
        }

        public static void AutosizeColumns(DataGridView grid)
        {
            grid.SuspendLayout();

            int width = 0;

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                width += grid.Columns[i].Width;
            }

            if (width > 775)
            {
                float scale = 775.0f / width;
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    grid.Columns[i].Width = (int)(grid.Columns[i].Width * scale);
                }
            }

            grid.ResumeLayout();
        }

        public void make_chart()
        {
            int total_refills = Balance_MySql.get_refills_total_amount();
            label4.Text = total_refills.ToString();

            int total_withdraws = Balance_MySql.get_withdraws_total_amount();
            int withdraw_percentage = total_withdraws * 100 / total_refills;
            label3.Text = total_withdraws.ToString();

            int total_sales = Balance_MySql.get_sales_total_amount();
            int sales_percentage = total_sales * 100 / total_refills;
            label5.Text = total_sales.ToString();

            int unused_credits = total_refills - total_withdraws - total_sales;
            int unused_percentage = unused_credits * 100 / total_refills;

            //chart1.ChartAreas.Add("test");
            chart1.Titles.Add("Revenue");
            //chart1.Legends.Add("demo");
            //chart1.Series.Add("Series1");

            // add data to the chart
           

            chart1.Series["Series1"].Points.AddXY("Withdraws", withdraw_percentage);
            chart1.Series["Series1"].Points.AddXY("Sales", sales_percentage);
            chart1.Series["Series1"].Points.AddXY("Unused", unused_percentage);

            // customize the appearance of the chart
            chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
            chart1.Series["Series1"].Label = "#VALX (#PERCENT)";
            chart1.Series["Series1"].LegendText = "#VALX";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
