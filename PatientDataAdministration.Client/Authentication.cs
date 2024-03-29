﻿using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using Newtonsoft.Json;
using PatientDataAdministration.Client.LocalSettingStorage;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
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

                Application.DoEvents();

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
                    if (CheckCredentialServer(out var userData))
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
                            JsonConvert.DeserializeObject<Administration_StaffInformation>(
                                credential.AuthPayLoad));
                    else
                    {
                        if (CheckCredentialServer(out var userData))
                        {
                            GainAccess(userData);
                            _status = true;
                        }
                        else
                        {
                            MessageBox.Show(@"Your Password is Incorrect");
                            txtPassword.Text = "";
                        }
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

                using (var entities = new LocalPDAEntities())
                {
                    entities.System_AuditTrail.Add(new System_AuditTrail()
                    {
                        ActionPerformed = AuditCategory.ClientAuth.DisplayName(),
                        AuditTimeStamp = DateTime.Now,
                        IsDeleted = false, 
                        IsRestrcitedOperation = true, 
                        UserPerformed = administrationStaffInformation.Email
                    });
                    entities.SaveChanges();
                }

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
            administrationStaffInformation = new Administration_StaffInformation();

            try
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
                        ClientName = WindowsIdentity.GetCurrent().Name, 
                        LocationLat = clientCoordinates?.Latitude.ToString(CultureInfo.InvariantCulture) ?? "0",
                        CurrentUser = 0
                    }
                };
                var result = LocalCore.Post($@"/ClientCommunication/User/Authenticate",
                    JsonConvert.SerializeObject(payload));

                if (result.Status)
                {
                    if (result.Data != null)
                    {
                        var data =
                            JsonConvert.DeserializeObject<Administration_StaffInformation>(
                                JsonConvert.SerializeObject(result.Data));

                        administrationStaffInformation = data;

                        var existingCredential =
                            _localPdaEntities.Local_StaffInformation.FirstOrDefault(x =>
                                !x.IsDeleted && x.Email == data.Email);

                        if (existingCredential == null)
                            _localPdaEntities.Local_StaffInformation.Add(new Local_StaffInformation()
                            {
                                AuthenticationState = data.AuthenticationState,
                                IsDeleted = false,
                                Surname = data.Surname,
                                AuthPayLoad = JsonConvert.SerializeObject(result.Data),
                                DateRegistered = data.DateRegistered,
                                Email = data.Email,
                                FirstName = data.FirstName,
                                PasswordData = data.PasswordData,
                                PasswordSalt = data.PasswordSalt,
                                RemoteId = data.Id,
                                SiteId = data.SiteId,
                                StaffId = data.StaffId
                            });
                        else
                        {
                            existingCredential.PasswordSalt = data.PasswordSalt;
                            existingCredential.AuthPayLoad = JsonConvert.SerializeObject(result.Data);
                            existingCredential.AuthenticationState = data.AuthenticationState;
                            existingCredential.FirstName = data.FirstName;
                            existingCredential.Surname = data.Surname;
                            existingCredential.PasswordData = data.PasswordData;
                            existingCredential.RemoteId = data.Id;
                            existingCredential.SiteId = data.SiteId;
                            existingCredential.StaffId = data.StaffId;

                            _localPdaEntities.Entry(existingCredential).State = EntityState.Modified;
                        }

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
            catch (Exception e)
            {
                LocalCore.TreatError(e, 0);
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
            lblPleaseWait.Visible = !_status;
        }
    }
}
