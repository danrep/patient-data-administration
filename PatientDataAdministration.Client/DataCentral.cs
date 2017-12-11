using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public partial class DataCentral : MetroFramework.Forms.MetroForm
    {
        private readonly UserCredential _userCredential;

        public DataCentral(UserCredential userCredential)
        {
            InitializeComponent();
            this._userCredential = userCredential;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            CollectGarbage();
        }

        private void DataCentral_Load(object sender, EventArgs e)
        {
            lblUserInformation.Text = @"Welcome, " +
                                      _userCredential.AdministrationStaffInformation.Surname + @" " +
                                      _userCredential.AdministrationStaffInformation.FirstName + @". You are working from " + 
                                      _userCredential.AdministrationSiteInformation.SiteName;
        }

        private void btnPatientManagement_Click(object sender, EventArgs e)
        {
            var subInfoMan = new SubInformationManagement(_userCredential);
            subInfoMan.ShowDialog();
            CollectGarbage();
        }

        private void CollectGarbage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {

        }
    }
}
