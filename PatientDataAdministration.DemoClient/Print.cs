using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDataAdministration.DemoClient
{
    public partial class Print : Form
    {
        private readonly PatientData _patientData;
        Bitmap _memoryImage;

        private static Bitmap ByteToImage(byte[] blob)
        {
            var mStream = new MemoryStream();
            var pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            var bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public Print(PatientData patientData)
        {
            _patientData = patientData;
            InitializeComponent();
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            buttonAdv1.Visible = false;
            buttonAdv2.Visible = false;
            Application.DoEvents();

            CaptureScreen();
            try
            {
                printDocument1.Print();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check your Printer");

                buttonAdv1.Visible = true;
                buttonAdv2.Visible = true;
            }
        }
        
        private void CaptureScreen()
        {
            var myGraphics = this.CreateGraphics();
            var s = this.Size;
            _memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            var memoryGraphics = Graphics.FromImage(_memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender,
               System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_memoryImage, 0, 0);
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Print_Shown(object sender, EventArgs e)
        {
            label4.Text = _patientData.Surname;
            label5.Text = _patientData.Othernames;
            label6.Text = _patientData.PepId;

            txtPassport.Image = ByteToImage(Convert.FromBase64String(_patientData.PassportImage));

            if (txtPassport.Image.Height > txtPassport.Height || txtPassport.Image.Width > txtPassport.Width)
                txtPassport.SizeMode = PictureBoxSizeMode.Zoom;
            else
                txtPassport.SizeMode = PictureBoxSizeMode.CenterImage;
        }
    }
}
