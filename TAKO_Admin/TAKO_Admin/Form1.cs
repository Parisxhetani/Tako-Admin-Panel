using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKO_Admin.UserControlls;

namespace TAKO_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.BackColor = Color.SpringGreen;
            var myControl = new UserControl1();
            panel3.Controls.Clear();
            panel3.Controls.Add(myControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.SpringGreen;
            button3.BackColor = Color.SlateGray;
            button2.BackColor = Color.SlateGray;

            var myControl = new UserControl1();
            panel3.Controls.Clear();
            panel3.Controls.Add(myControl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.SlateBlue;
            button1.BackColor = Color.SlateGray;
            button3.BackColor = Color.SlateGray;

            var myControl = new UserControl2();
            panel3.Controls.Clear();
            panel3.Controls.Add(myControl);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.IndianRed;
            button2.BackColor = Color.SlateGray;
            button1.BackColor = Color.SlateGray;

            var myControl = new UserControl3();
            panel3.Controls.Clear();
            panel3.Controls.Add(myControl);
        }

        private void name_TAKO_Click(object sender, EventArgs e)
        {

        }
    }
}
