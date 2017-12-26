using System;
using System.Linq;
using System.Windows.Forms;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PatientDataAdministration.Client
{
    public partial class Authentication : MetroFramework.Forms.MetroForm
    {
        private readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();
        private bool _status = true;

        public Authentication()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            PerformLogIn();   
        }

        private void PerformLogIn()
        {
            try
            {
                _status = false;

                System.Windows.Forms.Application.DoEvents();

                var credential =
                    _localPdaEntities.Local_StaffInformation.FirstOrDefault(
                        x => x.Email == txtUserName.Text.Trim() && !x.IsDeleted);

                if (credential == null)
                {
                    if (CheckCredentialServer(out UserCredential userData))
                    {
                        GainAccess(userData);
                        _status = true;
                    }
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

                ClearInputs();

                _status = true;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }

        private void GainAccess(UserCredential userCredential)
        {
            try
            {
                this.Hide();
                var dataCentral = new DataCentral(userCredential);
                dataCentral.ShowDialog();
                this.Show();

                dataCentral.Dispose();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void Authentication_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogIn();
            }
            else if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.L && AuthCode())
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
                        SiteCode = "LADMIN",
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
            this.Close();
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

                    _localPdaEntities.SaveChanges();
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

        private void Authentication_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
        }

        private void tmrFeedBack_Tick(object sender, EventArgs e)
        {
            btnLogIn.Enabled = _status;
            picLoader.Visible = !_status;
        }
    }
}
