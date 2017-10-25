using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDataAdministration.DemoClient
{
    public partial class LogOn : Form
    {
        public LogOn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == @"testuser@apin.com" && textBox2.Text.Trim() == @"testuser")
            {
                var frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
            else MessageBox.Show("Invalid Credentials. Try Again");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
