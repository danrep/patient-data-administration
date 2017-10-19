using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace PatientDataAdministration.DemoClient
{
    public partial class PatientInfo : MetroForm
    {
        private DemoDbEntities _demoDb = new DemoDbEntities();

        public PatientInfo()
        {
            InitializeComponent();
        }

        public PatientInfo(PatientData patientData)
        {
            InitializeComponent();
        }

        private void PatientInfo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void gradientPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text))
                    txtEmail.Text = @"NA";

                var gradientPanel1TextBoxes = gradientPanel11.Controls.OfType<TextBoxExt>().ToList();
                if (gradientPanel1TextBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    gradientPanel1TextBoxes.FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show("Please ensure that all input fields have been filled");
                    return;
                }

                var gradientPanelComboBoxes = gradientPanel11.Controls.OfType<ComboBox>().ToList();
                if (gradientPanelComboBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    gradientPanelComboBoxes.FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show("Please ensure that all input fields have been filled");
                    return;
                }

                var patientInfo = _demoDb.PatientDatas.FirstOrDefault(x => x.PhoneHumber == );
            }
            catch (Exception ex)
            {

            }
        }
    }
}
