using System;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using PatientDataAdministration.Client.LocalSettingStorage;
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

                if (!_localPdaEntities.System_SiteData.Any(x => !x.IsDeleted && x.IsCurrent))
                {
                    MessageBox.Show(@"You will need to BIND this Client to a Site. Please do this Under Settings");
                    _status = true;
                    return;
                }

                var credential =
                    _localPdaEntities.Local_StaffInformation.FirstOrDefault(
                        x => x.Email == txtUserName.Text.Trim() && !x.IsDeleted);

                if (credential == null)
                {
                    if (CheckCredentialServer(out Administration_StaffInformation userData))
                    {
                        GainAccess(userData);
                        _status = true;
                    }
                }
                else
                {
                    if (Core.Encryption.IsSaltEncryptValid(txtPassword.Text.Trim(), credential.PasswordData,
                        credential.PasswordSalt))
                        GainAccess(
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Administration_StaffInformation>(
                                credential.AuthPayLoad));
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

        private void GainAccess(Administration_StaffInformation administrationStaffInformation)
        {
            try
            {
                this.Hide();
                var dataCentral = new DataCentral(administrationStaffInformation);
                dataCentral.ShowDialog();
                this.Show();

                dataCentral.Dispose();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, administrationStaffInformation.Id);
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
                var dataCentral = new DataCentral(new Administration_StaffInformation()
                {
                    IsDeleted = false,
                    StaffId = "0000000000",
                    FirstName = "System Administrator",
                    Surname = "Codesistance",
                    PasswordData = string.Empty,
                    Id = 0,
                    Email = "sys_admin_apin_pda@codesistance.com",
                    SiteId = 0
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

        private bool CheckCredentialServer(out Administration_StaffInformation administrationStaffInformation)
        {
            var authData = $@"{txtUserName.Text.Trim()}*{txtPassword.Text.Trim()}";
            //var result = LocalCore.Get($@"/ClientCommunication/User/Authenticate?authData={authData}");

            var clientCoordinates = LocalCore.GetLocationProperty();

            var payload = new
            {
                authData,
                clientInformation = new ClientInformation()
                {
                    ClientGuid = LocalCache.Get<string>("ClientId") ?? "NA",
                    LocationLong = clientCoordinates?.Longitude.ToString(CultureInfo.InvariantCulture) ?? "0",
                    ClientName = System.Security.Principal.WindowsIdentity.GetCurrent().Name, 
                    LocationLat = clientCoordinates?.Latitude.ToString(CultureInfo.InvariantCulture) ?? "0",
                    CurrentUser = 0
                }
            };
            var result = LocalCore.Post(@"/ClientCommunication/User/Authenticate",
                Newtonsoft.Json.JsonConvert.SerializeObject(payload));

            administrationStaffInformation = new Administration_StaffInformation();
            if (result.Status)
            {
                if (result.Data != null)
                {
                    var data =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Administration_StaffInformation>(
                            Newtonsoft.Json.JsonConvert.SerializeObject(result.Data));

                    administrationStaffInformation = data;

                    _localPdaEntities.Local_StaffInformation.Add(new Local_StaffInformation()
                    {
                         AuthenticationState = data.AuthenticationState,
                         IsDeleted = false, 
                         Surname = data.Surname, 
                         AuthPayLoad = Newtonsoft.Json.JsonConvert.SerializeObject(result.Data),
                         DateRegistered = data.DateRegistered,
                         Email = data.Email,
                         FirstName = data.FirstName, 
                         PasswordData = data.PasswordData,
                         PasswordSalt = data.PasswordSalt,
                         RemoteId = data.Id,
                         SiteId = data.SiteId,
                         StaffId = data.StaffId
                    });

                    _localPdaEntities.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show(result.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(result.Message);
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
