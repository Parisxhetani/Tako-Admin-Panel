using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAKO_Admin.Models;
using TAKO_Admin.MySql;
using ZXing;
using ZXing.QrCode;
using static System.Net.Mime.MediaTypeNames;

namespace TAKO_Admin.UserControlls
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            show_qr_details();
        }

        public void show_qr_details()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();

            List<Qr_Model> list = new List<Qr_Model>();
            list = Qr_MysSql.get_refills();

            if (list != null)
            {
                foreach (Qr_Model m in list)
                {
                    dataGridView1.Rows.Add(m.Id.ToString(), m.Scan_Value, m.Credits.ToString());
                }
            }
            UserControl1.AutosizeColumns(dataGridView1);
        }

        private string GenerateRandomAlphaNumeric()
        {
            var guid = Guid.NewGuid();
            var hexString = guid.ToString().Replace("-", "");
            var alphaNumeric = "";
            for (int i = 0; i < hexString.Length; i++)
            {
                var c = hexString[i];
                alphaNumeric += char.IsDigit(c) ? c : (char)(c % 26 + 'A');
            }
            return string.Format("{0}-{1}-{2}-{3}-{4}", alphaNumeric.Substring(0, 8), alphaNumeric.Substring(8, 4), alphaNumeric.Substring(12, 4), alphaNumeric.Substring(16, 4), alphaNumeric.Substring(20));
        }

        private Bitmap GenerateQRCode(string text)
        {
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 200,
                Height = 200
            };
            writer.Options = options;
            var bitmap = writer.Write(text);
            pictureBox1.Image = bitmap;
            return bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var text = GenerateRandomAlphaNumeric();
            var bitmap = GenerateQRCode(text);
            textBox1.Text = text;

            if (bitmap != null)
            {
                var fileName = string.Format("{0}.png", text);
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveDialog.Title = "Save Barcode Image";
                saveDialog.FileName = fileName; // set the default file name to the generated QR code text
                saveDialog.ShowDialog();
                if (!string.IsNullOrEmpty(saveDialog.FileName))
                {
                    var format = ImageFormat.Png;
                    switch (saveDialog.FilterIndex)
                    {
                        case 2:
                            format = ImageFormat.Jpeg;
                            break;
                        case 3:
                            format = ImageFormat.Bmp;
                            break;
                    }

                    try
                    {
                        
                        bool ret = Qr_MysSql.add_qr(text);
                        if(ret = true)
                        {
                            bitmap.Save(saveDialog.FileName, format);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    show_qr_details();
                    MessageBox.Show("QR saved succesfuly!");
                }
            }
        }
    }
}

