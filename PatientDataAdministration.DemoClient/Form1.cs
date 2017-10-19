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
            if (_runningForms.Any())
            {
                _runningForms = _runningForms.Where(x => x.CanFocus).ToList();
                _runningForms.TrimExcess();
            }

            var patientInfo = new PatientInfo()
            {
                Tag = Guid.NewGuid().ToString()
            };

            _runningForms.Add(patientInfo);
            patientInfo.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_runningForms.Any(x => x.CanFocus))
                return;

            var messageForm = System.Windows.Forms.MessageBox.Show(
                @"Please Wait. It seems you have some Patient Information Windows Still Open. Do you want to Continue working on them? Your current progress will not be saved!",
                @"Just a Moment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (messageForm == DialogResult.Yes)
            {
                e.Cancel = true;

                var form = _runningForms.FirstOrDefault();

                if (form == null)
                    return;

                form.WindowState = FormWindowState.Normal;
                form.Activate();
                form.BringToFront();
            }
            else
                foreach (var form in _runningForms.Where(form => form != null).ToList())
                    form.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
