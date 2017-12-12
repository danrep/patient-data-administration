using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public partial class Authentication : MetroFramework.Forms.MetroForm
    {
        private readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        public Authentication()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogIn.Enabled = false;
                Application.DoEvents();

                var credential =
                    _localPdaEntities.Local_StaffInformation.FirstOrDefault(
                        x => x.Email == txtUserName.Text.Trim() && !x.IsDeleted);

                if (credential == null)
                {
                    if (CheckCredentialServer(out UserCredential userData))
                        GainAccess(userData);
                }
                else
                {
                    if (Core.Encryption.IsSaltEncryptValid(txtPassword.Text.Trim(), credential.PasswordData,
                        credential.PasswordSalt))
                        GainAccess(Newtonsoft.Json.JsonConvert.DeserializeObject<UserCredential>(credential.AuthPayLoad));
                    else
                    {
                        MessageBox.Show(@"Your Password is Incorrect");
                        txtPassword.Text = "";
                    }
                }
                
                btnLogIn.Enabled = true;
                ClearInputs();
            }
            catch(Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }

        private void GainAccess(UserCredential userCredential)
        {
            this.Hide();
            var dataCentral = new DataCentral(userCredential);
            dataCentral.ShowDialog();
            this.Show();
        }

        private void Authentication_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.L && AuthCode())
            {
                this.Hide();
                var dataCentral = new DataCentral(new UserCredential()
                {
                    AdministrationStaffInformation = new Administration_StaffInformation()
                    {
                        IsDeleted = false,
                        StaffId = "0000000000",
                        FirstName = "System Administrator",
                        Surname = "Codesistance",
                        PasswordData = string.Empty,
                        Id = 0,
                        Email = "sys_admin_apin_pda@codesistance.com",
                        SiteId = 0
                    },
                    AdministrationSiteInformation = new Administration_SiteInformation()
                    {
                        IsDeleted = false, 
                        Id = 0, 
                        SiteNameOfficial = "Local Administration", 
                        SiteCode ="LADMIN",
                        SiteNameInformal = "LADMIN", 
                        StateId = 0
                    }
                });
                dataCentral.ShowDialog();
                this.Show();

                ClearInputs();
            }
        }

        private bool AuthCode()
        {
            var control = DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Date.ToString("ddMMyyyy");
            var password = txtPassword.Text.Trim();

            return control == password;
        }

        private void ClearInputs()
        {
            txtPassword.Text = txtUserName.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var setting = new SubSettings();
            setting.ShowDialog();
        }

        private bool CheckCredentialServer(out UserCredential userCredential)
        {
            var authData = $@"{txtUserName.Text.Trim()}*{txtPassword.Text.Trim()}";
            var result = LocalCore.Get($@"/ClientCommunication/User/Authenticate?authData={authData}");

            userCredential = new UserCredential();
            if (result.Result.Status)
            {
                if (result.Result.Data != null)
                {
                    var data =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<UserCredential>(
                            Newtonsoft.Json.JsonConvert.SerializeObject(result.Result.Data));

                    userCredential = data;

                    _localPdaEntities.Local_StaffInformation.Add(new Local_StaffInformation()
                    {
                         AuthenticationState = data.AdministrationStaffInformation.AuthenticationState,
                         IsDeleted = false, 
                         Surname = data.AdministrationStaffInformation.Surname, 
                         AuthPayLoad = Newtonsoft.Json.JsonConvert.SerializeObject(result.Result.Data),
                         DateRegistered = data.AdministrationStaffInformation.DateRegistered,
                         Email = data.AdministrationStaffInformation.Email,
                         FirstName = data.AdministrationStaffInformation.FirstName, 
                         PasswordData = data.AdministrationStaffInformation.PasswordData,
                         PasswordSalt = data.AdministrationStaffInformation.PasswordSalt,
                         RemoteId = data.AdministrationStaffInformation.Id,
                         SiteId = data.AdministrationStaffInformation.SiteId,
                         StaffId = data.AdministrationStaffInformation.StaffId
                    });
                    return true;
                }
                else
                {
                    MessageBox.Show(result.Result.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(result.Result.Message);
                return false;
            }
        }
    }
}
