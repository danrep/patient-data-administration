using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace PatientDataAdministration.DemoClient
{
    public partial class PatientInfo : MetroForm
    {
        private readonly DemoDbEntities _demoDb = new DemoDbEntities();
        private string bioFinger1;
        private string bioFinger2;

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
                    MessageBox.Show("Please ensure that all input fields have been selected");
                    return;
                }

                if (!Regex.Match(txtPhoneNumber.Text, "\\d11").Success)
                {
                    txtPhoneNumber.Focus();
                    MessageBox.Show("Please enter a valid Phone Number");
                    return;
                }

                var patientInfo = _demoDb.PatientDatas.FirstOrDefault(x => x.PhoneHumber == txtPhoneNumber.Text.Trim() && !x.IsDeleted);
                if (patientInfo == null)
                {
                    patientInfo = new PatientData()
                    {
                        PhoneHumber = txtPhoneNumber.Text.Trim(), 
                        IsDeleted = false, 
                        BioDataFingerPrimary = bioFinger1, 
                        BioDataFingerSecondary = bioFinger2, 
                        CardDataChip = string.Empty, 
                        CardDataUid = string.Empty,
                        ClientNumber = txtPatientHospitalNumber.Text.Trim(), 
                        DateOfBirth = txtDateOfBirth.Value, 
                        Email = txtEmail.Text.Trim(),
                        FacilityNumber = txtFacilityNumber.Text.Trim(), 
                        HospitalNumber = txtHospitalNumber.Text.Trim(), 
                        HouseAddress = txtHouseAddress.Text.Trim(), 
                        Othernames = txtOthername.Text.Trim(), 
                        PepId = txtPepId.Text.Trim(),
                        Sex = txtSex.Text,
                        SiteId = 0, 
                        StateOfOrigin = txtStateOfOrigin.Text.Trim(),
                        Surname = txtSurname.Text.Trim(), 
                        VitalsHeight = Convert.ToInt32(txtHeight.Text.Trim()), 
                        VitalsWeight = Convert.ToInt32(txtWeight.Text.Trim())
                    };
                    _demoDb.PatientDatas.Add(patientInfo);
                }
                else
                {
                    
                }

                _demoDb.SaveChanges();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
