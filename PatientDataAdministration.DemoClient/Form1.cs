using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace PatientDataAdministration.DemoClient
{
    public partial class Form1 : RibbonForm
    {
        List<Form> _runningForms = new List<Form>(); 

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            var patientInfo = new PatientInfo(false)
            {
                Tag = Guid.NewGuid().ToString()
            };
            patientInfo.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            var patientInfo = new PatientInfo(true)
            {
                Tag = Guid.NewGuid().ToString()
            };
            patientInfo.ShowDialog();
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            var patientTagging = new PatientTagging();
            patientTagging.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(Core.Encryption.SaltDecrypt(textBox1.Text, textBox2.Text));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
