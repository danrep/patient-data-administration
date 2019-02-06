using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using PatientDataAdministration.Client.Properties;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using SecuGen.FDxSDKPro.Windows;

namespace PatientDataAdministration.Client
{
    public partial class SubHtc : MetroFramework.Forms.MetroForm
    {
        #region Variables

        private readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        private string _bioDataPrimary;
        private string _bioDataSecondary;
        public bool _isBusy;

        private readonly SGFingerPrintManager _fingerPrintManager;
        private SGFPMDeviceInfoParam _deviceInfoParam;
        private SGFPMDeviceList _device;
        private Thread _bioReaderThread;
        private string _deviceName;
        private int _deviceId;
        private Color _lblBioDeviceInfoForeColor, _lblInformationForeColor;
        private string _lblBioDeviceInfoText, _lblInformationText;

        private System_BioDataStore_PopulationRegister _systemBioDataStorePopulationRegister;
        private readonly Administration_StaffInformation _administrationStaffInformation;
        private List<System_BioDataStore_PopulationRegister> _systemBioDataStorePopulationRegisters;

        private Thread _dataLoadthread;

        #endregion

        #region Form Events

        public SubHtc(Administration_StaffInformation administrationStaffInformation)
        {
            try
            {

                this._administrationStaffInformation = administrationStaffInformation;
                InitializeComponent();
                _fingerPrintManager = new SGFingerPrintManager();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void SubInformationManagement_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'localPDADataSet.System_State' table. You can move, or remove it, as needed.
                this.system_StateTableAdapter.Fill(this.localPDADataSet.System_State);

                _systemBioDataStorePopulationRegister = new System_BioDataStore_PopulationRegister();
                _bioReaderThread = new Thread(() => { });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void SubInformationManagement_Shown(object sender, EventArgs e)
        {
            persistLoad.Enabled = true;
            persistLoad.Start();
        }

        private void SubInformationManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                btnClear_Click(this, e);
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
            finally
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        #endregion

        #region Control Events

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void btnSearchBiometrics_Click(object sender, EventArgs e)
        {
            try
            {
                var image = GetBioData();
                if (string.IsNullOrEmpty(image))
                    return;

                var capturedBioData = Convert.FromBase64String(image);
                var matched = false;
                System_BioDataStore_PopulationRegister patientData = null;
                pnlWaiting.Visible = true;
                Application.DoEvents();

                foreach (var pds in _systemBioDataStorePopulationRegisters)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(pds.PrimaryFinger) || string.IsNullOrEmpty(pds.SecondaryFinger))
                            continue;

                        _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.PrimaryFinger), capturedBioData,
                            SGFPMSecurityLevel.HIGH, ref matched);

                        if (!matched)
                            _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.SecondaryFinger), capturedBioData,
                                SGFPMSecurityLevel.HIGH, ref matched);

                        if (!matched)
                            continue;

                        lblInformation.Text = $@"Patient with HTS ID {pds.HtsId} is a Match.";

                        patientData = pds;
                        break;
                    }
                    catch(Exception exception)
                    {
                        LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                    }
                }

                if (matched)
                {
                    LoadPatientData(patientData);
                }
                else
                {
                    lblInformation.Text = @"No Match Found";
                    MessageBox.Show(lblInformation.Text);
                    ClearContents();
                }

                pnlWaiting.Visible = false;
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {

                ClearContents();
                txtPhoneNumber.Text = txtHtsId.Text = txtSearch.Text = "";
                txtSex.SelectedIndex = txtTestResult.SelectedIndex = -1;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnRefreshBioDevice_Click(object sender, EventArgs e)
        {
            try
            {
                _deviceName = "";
                persistLoad.Enabled = true;
                persistLoad.Start();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var textBoxes = pnlPersonalInformation.Controls.OfType<TextBox>().ToList();
                if (textBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    textBoxes.OrderBy(x => x.TabIndex).FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all text fields have been chosen");
                    return;
                }

                var comboBoxes = pnlPersonalInformation.Controls.OfType<ComboBox>().ToList();
                if (comboBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    comboBoxes.OrderBy(x => x.TabIndex).FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all select fields have been chosen");
                    return;
                }

                comboBoxes = grpBoxSpecialInformation.Controls.OfType<ComboBox>().ToList();
                if (comboBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    comboBoxes.OrderBy(x => x.TabIndex).FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all select fields have been chosen");
                    return;
                }

                if (txtPhoneNumber.Text.Trim().Length > 14)
                {
                    txtPhoneNumber.Focus();
                    MessageBox.Show(@"This Phone Number is just too long. Please Check");
                    return;
                }

                if (!Regex.Match(txtPhoneNumber.Text, @"\d{11}").Success)
                {
                    txtPhoneNumber.Focus();
                    MessageBox.Show(@"Please enter a valid Phone Number");
                    return;
                }

                #region Patient Data Composition

                var patientData = new PatientInformationPatientPopulationRegister()
                {
                    Patient_PatientInformationPopulationRegister = new Patient_PatientInformationPopulationRegister()
                    {
                        IsDeleted = false,
                        Sex = txtSex.Text,
                        SiteId = _administrationStaffInformation.SiteId,
                        PhoneNumber = txtPhoneNumber.Text,
                        Id = 0,
                        HtsId = txtHtsId.Text.ToUpper(),
                        LastUpdated = DateTime.Now,
                        WhenCreated = DateTime.Now,
                        HouseAddress = txtAddress.Text,
                        OtherName = txtOtherNames.Text,
                        Surname = txtSurname.Text
                    }
                };

                if (!string.IsNullOrEmpty(_bioDataPrimary) && !string.IsNullOrEmpty(_bioDataSecondary))
                {
                    if (ValidateBioData(_bioDataPrimary, _bioDataSecondary))
                    {
                        pnlWaiting.Visible = false;
                        MessageBox.Show(@"The Captured Biodata are either a match or a match with some other Biometric Data and that's not proper. Please confirm!");
                        return;
                    }

                    patientData.Patient_PatientBiometricDataPopulationRegister = new Patient_PatientBiometricDataPopulationRegister()
                    {
                        FingerPrimary = _bioDataPrimary,
                        FingerSecondary = _bioDataSecondary,
                        IsDeleted = false,
                        IsValid = true, 
                        FingerPrimaryPosition = cmbDataFingerSelector1.SelectedText, 
                        FingerSecondaryPosition = cmbDataFingerSelector2.SelectedText
                    };

                    if (_systemBioDataStorePopulationRegister.Id == 0)
                        patientData.Patient_PatientBiometricDataPopulationRegister.DateRegistered = DateTime.Now;
                }
                else
                {
                    MessageBox.Show(@"Biometric Data is Essential and Compulsory for this Process");
                    return;
                }

                patientData.Administration_StaffInformation = _administrationStaffInformation;

                #endregion Patient Data Composition

                #region Patient Local CRUD

                pnlWaiting.Visible = true;

                if (_systemBioDataStorePopulationRegister.Id == 0)
                {
                    _systemBioDataStorePopulationRegister = new System_BioDataStore_PopulationRegister()
                    {
                        IsDeleted = false,
                        PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientData),
                        IsSync = false,
                        LastSync = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        PrimaryFinger = _bioDataPrimary ?? "",
                        SecondaryFinger = _bioDataSecondary ?? "",
                        PrimaryFingerPosition = cmbDataFingerSelector1.SelectedText,
                        SecondaryFingerPosition = cmbDataFingerSelector2.SelectedText,
                        SiteId = _administrationStaffInformation.SiteId, 
                        HtsId = txtHtsId.Text, 
                        TestResult = txtTestResult.Text
                    };
                    _localPdaEntities.System_BioDataStore_PopulationRegister.Add(_systemBioDataStorePopulationRegister);
                }
                else
                {
                    if (DateTime.Now.Date.Subtract(_systemBioDataStorePopulationRegister.LastUpdate).Days < 180)
                    {
                        MessageBox.Show(
                            $@"This Patient just did a Test within the last 3 months. Please advise Patients to return from {
                                    _systemBioDataStorePopulationRegister.LastUpdate.AddMonths(3).Date
                                        .ToLongDateString()
                                }");
                        return;
                    }

                    _systemBioDataStorePopulationRegister = new System_BioDataStore_PopulationRegister()
                    {
                        IsDeleted = false,
                        PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientData),
                        IsSync = false,
                        LastSync = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        PrimaryFinger = _bioDataPrimary ?? "",
                        SecondaryFinger = _bioDataSecondary ?? "",
                        PrimaryFingerPosition = cmbDataFingerSelector1.SelectedText,
                        SecondaryFingerPosition = cmbDataFingerSelector2.SelectedText,
                        SiteId = _administrationStaffInformation.SiteId,
                        HtsId = txtHtsId.Text,
                        TestResult = txtTestResult.SelectedText
                    };
                    _localPdaEntities.System_BioDataStore_PopulationRegister.Add(_systemBioDataStorePopulationRegister);
                }
                _localPdaEntities.SaveChanges();

                pnlWaiting.Visible = false;

                #endregion

                _lblInformationText = @"Patient Saved Successfully";
                _lblInformationForeColor = Color.DarkGreen;
                System.Media.SystemSounds.Exclamation.Play();

                MessageBox.Show(_lblInformationText);
                btnClear_Click(this, e);

                UpdatePersistedData();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                lblInformation.Text = @"An unexpected error has occured. Please contact the Administrator";
                lblInformation.ForeColor = Color.DarkRed;
                pnlWaiting.Visible = false;
            }
        }

        private void btnDataFinger1_Click(object sender, EventArgs e)
        {
            _bioDataPrimary = GetBioData();
        }

        private void btnDataFinger2_Click(object sender, EventArgs e)
        {
            _bioDataSecondary = GetBioData();
        }

        private void persistLoad_Tick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_deviceName))
                {
                    if (_bioReaderThread.ThreadState == ThreadState.Running)
                    {
                        _bioReaderThread.Abort();
                        _bioReaderThread = null;
                    }

                    _bioReaderThread = new Thread(AquireBioDevice);
                    _bioReaderThread.Start();
                }
                else
                {
                    persistLoad.Stop();
                    persistLoad.Enabled = false;
                    picBoxFingerPrint.Image = Resources.icons8_Fingerprint_48px;
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void persistPatientElectronicData_Tick(object sender, EventArgs e)
        {
            btnDataFinger1.BackColor = string.IsNullOrEmpty(_bioDataPrimary) ? Color.Black : Color.SteelBlue;
            btnDataFinger2.BackColor = string.IsNullOrEmpty(_bioDataSecondary) ? Color.Black : Color.SteelBlue;
        }

        private void lstBoxSearchResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxSearchResult.SelectedIndex <= -1)
                    return;

                var htsId = lstBoxSearchResult.SelectedItem.ToString().Split('|')[0];
                LoadPatientData(_systemBioDataStorePopulationRegisters.FirstOrDefault(x => x.HtsId.ToLower() == htsId.ToLower()));
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void timerUpdateInformation_Tick(object sender, EventArgs e)
        {
            lblBioDeviceInfo.ForeColor = _lblBioDeviceInfoForeColor;
            lblBioDeviceInfo.Text = _lblBioDeviceInfoText;

            lblInformation.Text = _lblInformationText;
            lblInformation.ForeColor = _lblInformationForeColor;
        }

        #endregion

        #region Methods

        public bool ValidateBioData(string bioDataPrimary, string bioDataSecondary)
        {
            try
            {
                pnlWaiting.Visible = true;

                var capturedBioDataPrimary = Convert.FromBase64String(bioDataPrimary);
                var capturedBioDataSecondary = Convert.FromBase64String(bioDataSecondary);

                var matched = false;

                foreach (var pds in _systemBioDataStorePopulationRegisters.ToList())
                {
                    if (string.IsNullOrEmpty(pds.PrimaryFinger) || string.IsNullOrEmpty(pds.SecondaryFinger))
                        continue;
                    
                    _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.PrimaryFinger),
                        capturedBioDataPrimary,
                        SGFPMSecurityLevel.HIGH, ref matched);

                    if (matched)
                        break;

                    _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.SecondaryFinger),
                    capturedBioDataPrimary,
                    SGFPMSecurityLevel.HIGH, ref matched);

                    if (matched)
                        break;

                    _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.PrimaryFinger),
                    capturedBioDataSecondary,
                    SGFPMSecurityLevel.HIGH, ref matched);

                    if (matched)
                        break;

                    _fingerPrintManager.MatchTemplate(Convert.FromBase64String(pds.SecondaryFinger),
                    capturedBioDataSecondary,
                    SGFPMSecurityLevel.HIGH, ref matched);

                    if (matched)
                        break;
                }

                pnlWaiting.Visible = false;

                return matched;
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
                return false;
            }
        }

        public void UpdatePersistedData()
        {
            if (_isBusy)
                return;

            _dataLoadthread = new Thread(() =>
            {
                try
                {
                    _isBusy = true;
                    _systemBioDataStorePopulationRegisters = new List<System_BioDataStore_PopulationRegister>();

                    _lblInformationText = "Data is Updating";
                    _lblInformationForeColor = Color.DarkOrange;

                    var totalPatients = _localPdaEntities.System_BioDataStore_PopulationRegister.Count();
                    var orderedSystemBioDataStore =
                        _localPdaEntities.System_BioDataStore_PopulationRegister.OrderBy(x => x.Id).ToList();
                    for (var i = 0; i < totalPatients; i += 100)
                    {
                        _systemBioDataStorePopulationRegisters.AddRange(
                            orderedSystemBioDataStore.Skip(i).Take(100).ToList());
                    }

                    _lblInformationText =
                        $"Quick Search has been Updated. Loaded {totalPatients:#,###} Patients Successfully";
                    _lblInformationForeColor = Color.DarkGreen;

                    _isBusy = false;

                    orderedSystemBioDataStore.Clear();
                    orderedSystemBioDataStore.TrimExcess();

                    GC.Collect();
                }
                catch (Exception e)
                {
                    LocalCore.TreatError(e, _administrationStaffInformation.Id);
                }
            });
            _dataLoadthread.Start();
        }

        public ThreadState UpdatePersistenceStatus => _dataLoadthread.ThreadState;

        private void ClearContents()
        {
            try
            {
                _systemBioDataStorePopulationRegister = new System_BioDataStore_PopulationRegister();

                chkPriFin.Checked = chkSecFin.Checked = false;

                picBoxFingerPrint.Image = Resources.icons8_Fingerprint_48px;

                _bioDataPrimary = _bioDataSecondary = "";
                foreach (var ctl in pnlPersonalInformation.Controls.OfType<TextBox>().ToList())
                    ctl.Text = "";
                foreach (var ctl in pnlPersonalInformation.Controls.OfType<ComboBox>().ToList())
                    ctl.Text = "";

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }
        }

        private bool CheckForDevice()
        {
            try
            {
                var deviceFound = false;
                if (_fingerPrintManager.EnumerateDevice() != (int)SGFPMError.ERROR_NONE)
                    return false;

                if (_fingerPrintManager.NumberOfDevice == 0)
                    return false;

                // Get enumeration info into SGFPMDeviceList
                _device = new SGFPMDeviceList();
                _deviceName = "";

                for (var i = 0; i < _fingerPrintManager.NumberOfDevice; i++)
                {
                    if (_fingerPrintManager.GetEnumDeviceInfo(i, _device) != (int)SGFPMError.ERROR_NONE)
                        continue;

                    _deviceName = _device.DevName + " : " + _device.DevID;
                    deviceFound = true;
                    break;
                }

                return deviceFound;
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
                return false;
            }
        }

        private bool InitializeBioDevice()
        {
            try
            {
                var sgDeviceName = _device.DevName;
                _deviceId = _device.DevID;

                if (_fingerPrintManager.Init(sgDeviceName) != (int)SGFPMError.ERROR_NONE)
                    return false;

                if (_fingerPrintManager.OpenDevice(_deviceId) != (int)SGFPMError.ERROR_NONE)
                    return false;

                _deviceInfoParam = new SGFPMDeviceInfoParam();

                return _fingerPrintManager.GetDeviceInfo(_deviceInfoParam) == (int)SGFPMError.ERROR_NONE;
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
                return false;
            }
        }


        private void LoadPatientData(System_BioDataStore_PopulationRegister patientData)
        {
            try
            {
                ClearContents();

                if (patientData == null)
                {
                    System.Media.SystemSounds.Hand.Play();
                    lblInformation.Text = @"No Information Found about this Patient";
                    return;
                }

                var patientDataResolved =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<PatientInformationPatientPopulationRegister>(
                        patientData.PatientData);

                txtPhoneNumber.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.PhoneNumber;
                txtSex.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.Sex;
                txtHtsId.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.HtsId;
                txtSurname.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.Surname;
                txtOtherNames.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.OtherName;
                txtAddress.Text = patientDataResolved.Patient_PatientInformationPopulationRegister.HouseAddress;

                if (patientDataResolved.Patient_PatientBiometricDataPopulationRegister != null)
                {
                    _bioDataPrimary = patientDataResolved.Patient_PatientBiometricDataPopulationRegister.FingerPrimary;
                    _bioDataSecondary = patientDataResolved.Patient_PatientBiometricDataPopulationRegister
                        .FingerSecondary;
                    cmbDataFingerSelector1.Text = patientDataResolved.Patient_PatientBiometricDataPopulationRegister
                        .FingerPrimaryPosition;
                    cmbDataFingerSelector2.Text = patientDataResolved.Patient_PatientBiometricDataPopulationRegister
                        .FingerSecondaryPosition;

                    if (!string.IsNullOrEmpty(_bioDataPrimary))
                        chkPriFin.Checked = true;
                    if (!string.IsNullOrEmpty(_bioDataSecondary))
                        chkSecFin.Checked = true;
                }

                txtTestResult.Text = _systemBioDataStorePopulationRegisters.FirstOrDefault(x =>
                                             x.HtsId.ToLower() == patientDataResolved
                                                 .Patient_PatientInformationPopulationRegister.HtsId.ToLower())
                                         ?.TestResult ?? "";

                _systemBioDataStorePopulationRegister =
                    _localPdaEntities.System_BioDataStore_PopulationRegister.FirstOrDefault(
                        x => x.HtsId == patientDataResolved.Patient_PatientInformationPopulationRegister.HtsId) ??
                    new System_BioDataStore_PopulationRegister();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
                ClearContents();
            }
        }

        delegate void OnErrorInvoker(string text, string caption);
        void OnError(string text, string caption)
        {
            lblNfcStatus.Text = text;
            lblNfcStatus.ForeColor = Color.DarkOrange;
        }

        private void AquireBioDevice()
        {
            if (!CheckForDevice())
            {
                _lblBioDeviceInfoForeColor = Color.DarkRed;
                _lblBioDeviceInfoText = @"No Device Found. Please Re-attach";

                return;
            }

            _lblBioDeviceInfoForeColor = Color.Green;
            _lblBioDeviceInfoText = @"Device Connected";

            if (!InitializeBioDevice())
            {
                _lblBioDeviceInfoForeColor = Color.Orange;
                _lblBioDeviceInfoText = @"Device is Not Ready. Please Re-attach";

                return;
            }

            _lblBioDeviceInfoForeColor = Color.Green;
            _lblBioDeviceInfoText = @"Fingerprint Device Ready for Capture";
        }

        private void tmrSecureWindow_Tick(object sender, EventArgs e)
        {
            groupBox1.Enabled = groupBox2.Enabled = _isBusy;
            pnlWaiting.Visible = !_isBusy;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter)
                    return;

                lstBoxSearchResult.Visible = true;

                lstBoxSearchResult.Items.Clear();

                var query = txtSearch.Text.Trim().ToLower();
                var results =
                    _systemBioDataStorePopulationRegisters.Where(
                        x =>
                            (x.HtsId.ToLower().Contains(query) ||
                             x.PatientData.ToLower()
                             .Replace("\"patient_patientinformation\"", "")
                             .Replace("\"id\"", "")
                             .Replace("\"htsid\"", "")
                             .Replace("\"previousid\"", "")
                             .Replace("\"siteid\"", "")
                             .Replace("\"title\"", "")
                             .Replace("\"surname\"", "")
                             .Replace("\"othername\"", "")
                             .Replace("\"sex\"", "")
                             .Replace("\"phonenumber\"", "")
                             .Replace("\"maritalstatus\"", "")
                             .Replace("\"dateofbirth\"", "")
                             .Replace("\"stateoforigin\"", "")
                             .Replace("\"houseaddress\"", "")
                             .Replace("\"houseaddressstate\"", "")
                             .Replace("\"houseaddresslga\"", "")
                             .Replace("\"hospitalnumber\"", "")
                             .Replace("\"passportdata\"", "")
                             .Replace("\"isdeleted\"", "")
                             .Replace("\"whencreated\"", "")
                             .Replace("\"lastupdated\"", "")
                             .Replace("\"patient_patientbiometricdata\"", "")
                             .Replace("\"patient_patientNearfieldcommunicationdata\"", "")
                             .Replace("\"administration_staffinformation\"", "")
                             .Contains(query)) &&
                            !x.IsDeleted).Take(10).Select(s => new {s.HtsId}).ToList();

                if (!results.Any())
                    lstBoxSearchResult.Items.Add($"No Match");
                else
                    foreach (var result in results)
                        lstBoxSearchResult.Items.Add($"{result.HtsId.ToUpper()}");

                results.Clear();

                if (string.IsNullOrEmpty(query))
                    lstBoxSearchResult.Visible = false;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void txtStateOfResidence_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtStateOfResidence.SelectedValue == null)
                    return;

                var selectedValue = (System.Convert.ChangeType(txtStateOfResidence.SelectedValue, typeof(int)));

                if (selectedValue != null)
                    this.system_LocalGovermentAreaTableAdapter.FillBy(this.localPDADataSet.System_LocalGovermentArea,
                        ((int)selectedValue));
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private string GetBioData()
        {
            try
            {
                lstBoxSearchResult.Visible = false;

                var mDataMin = new byte[400];

                var fpImage = new byte[_deviceInfoParam.ImageWidth * _deviceInfoParam.ImageHeight];

                var iError = _fingerPrintManager.GetImage(fpImage);

                if (iError == (int)SGFPMError.ERROR_NONE)
                {
                    DrawImage(fpImage);

                    if (_fingerPrintManager.CreateTemplate(fpImage, mDataMin) == 0)
                        return Convert.ToBase64String(mDataMin);
                    else
                    {
                        picBoxFingerPrint.Image = Resources.icons8_Cancel_48px;
                        _lblBioDeviceInfoForeColor = Color.DarkRed;
                        _lblBioDeviceInfoText = @"Cant Read Bio-Data";
                        return string.Empty;
                    }
                }
                else
                {
                    if (iError == (int) SGFPMError.ERROR_WRONG_IMAGE)
                    {
                        _lblBioDeviceInfoForeColor = Color.DarkOrange;
                        _lblBioDeviceInfoText = @"No Image Captured";
                        picBoxFingerPrint.Image = Resources.icons8_Cancel_48px;
                        System.Media.SystemSounds.Question.Play();
                        return string.Empty;
                    }
                    else
                    {
                        _lblBioDeviceInfoForeColor = Color.DarkRed;
                        _lblBioDeviceInfoText = @"Device Unreachable. Please Re-attach.";
                        picBoxFingerPrint.Image = Resources.icons8_Cancel_48px;
                        return string.Empty;
                    }
                }
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);

                _lblBioDeviceInfoForeColor = Color.DarkRed;
                _lblBioDeviceInfoText = @"Unrecognised Error. Please Re-attach.";
                picBoxFingerPrint.Image = Resources.icons8_Cancel_48px;

                return string.Empty;
            }
        }

        private void DrawImage(byte[] imgData)
        {
            try
            {
                var bmp = new Bitmap(_deviceInfoParam.ImageWidth, _deviceInfoParam.ImageHeight);
                picBoxFingerPrint.Image = (Image)bmp;

                for (var i = 0; i < bmp.Width; i++)
                {
                    for (var j = 0; j < bmp.Height; j++)
                    {
                        var colorval = (int)imgData[(j * _deviceInfoParam.ImageWidth) + i];
                        bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                    }
                }
                picBoxFingerPrint.Refresh();
                GC.Collect();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
                picBoxFingerPrint.Image = PatientDataAdministration.Client.Properties.Resources.icons8_Fingerprint_48px;
            }
        }

        #endregion
    }
}
