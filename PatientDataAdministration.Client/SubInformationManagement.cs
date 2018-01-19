using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using PatientDataAdministration.Client.Properties;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using SecuGen.FDxSDKPro.Windows;
using Codesistance.NFC;

namespace PatientDataAdministration.Client
{
    public partial class SubInformationManagement : MetroFramework.Forms.MetroForm
    {
        #region Variables

        private readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        private string _bioDataPrimary;
        private string _bioDataSecondary;
        private string _nfcUid;
        public bool _isBusy;

        private readonly SGFingerPrintManager _fingerPrintManager;
        private SGFPMDeviceInfoParam _deviceInfoParam;
        private SGFPMDeviceList _device;
        private Thread _bioReaderThread;
        private string _deviceName;
        private int _deviceId;
        private Color _lblBioDeviceInfoForeColor, _lblInformationForeColor;
        private string _lblBioDeviceInfoText, _lblInformationText;

        private System_BioDataStore _systemBioDataStore;
        private readonly Administration_StaffInformation _administrationStaffInformation;
        private List<System_BioDataStore> _systemBioDataStores;

        private string _selectedReader;
        private string _tagAtr;
        private SCardReader _reader;
        private SCardChannel _cardchannel = null;
        private NfcTag _tag = null;
        private Thread _cardthread;
        private bool _inLoop = false;

        #endregion

        #region Form Events

        public SubInformationManagement(Administration_StaffInformation administrationStaffInformation)
        {
            this._administrationStaffInformation = administrationStaffInformation;
            InitializeComponent();
            _fingerPrintManager = new SGFingerPrintManager();
        }

        private void SubInformationManagement_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'localPDADataSet.System_State' table. You can move, or remove it, as needed.
                this.system_StateTableAdapter.Fill(this.localPDADataSet.System_State);

                _systemBioDataStore = new System_BioDataStore();
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

            LoadReaderList();
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
                System_BioDataStore patientData = null;
                pnlWaiting.Visible = true;
                Application.DoEvents();

                foreach (var pds in _systemBioDataStores)
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

                        lblInformation.Text = $@"Patient with PEP ID {pds.PepId} is a Match.";

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
            ClearContents();
            txtSearch.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnRefreshBioDevice_Click(object sender, EventArgs e)
        {
            _deviceName = "";
            persistLoad.Enabled = true;
            persistLoad.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var textBoxes = pnlOfficialInformation.Controls.OfType<TextBox>().ToList();
                if (textBoxes.Any(x => string.IsNullOrEmpty(x.Text) && x.Name != txtPepId.Name))
                {
                    textBoxes.FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all input fields have been filled");
                    return;
                }

                textBoxes = pnlPersonalInformation.Controls.OfType<TextBox>().ToList();
                if (textBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    textBoxes.FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all input fields have been filled");
                    return;
                }

                var comboBoxes = pnlPersonalInformation.Controls.OfType<ComboBox>().ToList();
                if (comboBoxes.Any(x => string.IsNullOrEmpty(x.Text)))
                {
                    textBoxes.FirstOrDefault(x => string.IsNullOrEmpty(x.Text))?.Focus();
                    MessageBox.Show(@"Please ensure that all select fields have been chosen");
                    return;
                }

                if (!Regex.Match(txtPhoneNumber.Text, @"\d{11}").Success)
                {
                    txtPhoneNumber.Focus();
                    MessageBox.Show(@"Please enter a valid Phone Number");
                    return;
                }

                #region Patient Data Composition

                var patientData = new PatientInformation
                {
                    Patient_PatientInformation = new Patient_PatientInformation
                    {
                        PepId = txtPepId.Text,
                        DateOfBirth = txtDateOfBirth.Value,
                        HospitalNumber = txtHospitalNumber.Text,
                        HouseAddresLga = (int) txtLgaOfResidence.SelectedValue,
                        HouseAddress = txtAddress.Text,
                        HouseAddressState = (int) txtStateOfResidence.SelectedValue,
                        IsDeleted = false,
                        MaritalStatus = string.Empty,
                        Othername = txtOtherNames.Text,
                        PassportData = null,
                        PreviousId = txtPreviousNumber.Text,
                        Sex = txtSex.Text,
                        Title = string.Empty,
                        SiteId = _administrationStaffInformation.SiteId,
                        Surname = txtSurname.Text,
                        StateOfOrigin = 0,
                        PhoneNumber = txtPhoneNumber.Text, 
                        Id = 0,  
                    }
                };

                if (!string.IsNullOrEmpty(_bioDataPrimary) && !string.IsNullOrEmpty(_bioDataSecondary))
                {
                    if (ValidateBioData(_bioDataPrimary, _bioDataSecondary))
                    {
                        MessageBox.Show(@"The Captured Biodata have a match and that's not proper. Please confirm");
                        pnlWaiting.Visible = false;
                        return;
                    }

                    patientData.Patient_PatientBiometricData = new Patient_PatientBiometricData
                    {
                        PepId = txtPepId.Text,
                        FingerPrimary = _bioDataPrimary,
                        FingerSecondary = _bioDataSecondary,
                        IsDeleted = false,
                        IsValid = true
                    };

                    if (_systemBioDataStore.Id == 0)
                        patientData.Patient_PatientBiometricData.DateRegistered = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(_nfcUid))
                {
                    patientData.Patient_PatientNearFieldCommunicationData =
                        new Patient_PatientNearFieldCommunicationData
                        {
                            PepId = txtPepId.Text,
                            CardId = _nfcUid,
                            CardData = Newtonsoft.Json.JsonConvert.SerializeObject(_tag)
                        };

                    if (_systemBioDataStore.Id == 0)
                        patientData.Patient_PatientNearFieldCommunicationData.DateRegistered = DateTime.Now;

                    patientData.Patient_PatientNearFieldCommunicationData.IsDeleted = false;
                    patientData.Patient_PatientNearFieldCommunicationData.IsValid = true;
                }

                patientData.Administration_StaffInformation = _administrationStaffInformation;

                #endregion Patient Data Composition

                pnlWaiting.Visible = true;
                var result = LocalCore.Post(@"/ClientCommunication/Patient/PostPatient",
                    Newtonsoft.Json.JsonConvert.SerializeObject(patientData));
                pnlWaiting.Visible = false;

                if (result.Status)
                {
                    if (_systemBioDataStore.Id == 0)
                    {
                        var postInfo =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Patient_PatientInformation>(
                                Newtonsoft.Json.JsonConvert.SerializeObject(result.Data));

                        _systemBioDataStore = new System_BioDataStore()
                        {
                            IsDeleted = false,
                            PepId = postInfo.PepId,
                            PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientData),
                            FullName = txtSurname.Text + @" " + txtOtherNames.Text,
                            IsSync = true,
                            LastSync = DateTime.Now,
                            LastUpdate = DateTime.Now,
                            NfcUid = _nfcUid ?? "",
                            PrimaryFinger = _bioDataPrimary ?? "",
                            SecondaryFinger = _bioDataSecondary ?? "",
                            SiteId = _administrationStaffInformation.SiteId
                        };
                        _localPdaEntities.System_BioDataStore.Add(_systemBioDataStore);
                    }
                    else
                    {
                        _systemBioDataStore =
                            _localPdaEntities.System_BioDataStore.FirstOrDefault(
                                x => x.PepId == txtPepId.Text && !x.IsDeleted);

                        if (_systemBioDataStore == null)
                        {
                            MessageBox.Show(@"This Patient does not exist. Please try again");
                            return;
                        }

                        _systemBioDataStore.PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientData);
                        _systemBioDataStore.FullName = txtSurname.Text = @" " + txtOtherNames.Text;
                        _systemBioDataStore.IsSync = true;
                        _systemBioDataStore.LastUpdate = DateTime.Now;
                        _systemBioDataStore.NfcUid = _nfcUid ?? "";
                        _systemBioDataStore.PrimaryFinger = _bioDataPrimary ?? "";
                        _systemBioDataStore.SecondaryFinger = _bioDataSecondary ?? "";

                        _localPdaEntities.Entry(_systemBioDataStore).State = EntityState.Modified;
                    }

                    _localPdaEntities.SaveChanges();

                    _lblInformationText = @"Operation was Completed Successfully";
                    _lblInformationForeColor = Color.DarkGreen;
                    System.Media.SystemSounds.Exclamation.Play();

                    MessageBox.Show(_lblInformationText);
                    btnClear_Click(this, e);
                    UpdatePersistedData();
                }
                else
                {
                    lblInformation.Text = result.Message;
                    lblInformation.ForeColor = Color.DarkRed;
                    System.Media.SystemSounds.Beep.Play();
                }
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

        private void btnNfcData_Click(object sender, EventArgs e)
        {
            //
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

        private void btnRefreshNfcDevice_Click(object sender, EventArgs e)
        {
            LoadReaderList();
        }

        private void persistPatientElectronicData_Tick(object sender, EventArgs e)
        {
            btnDataFinger1.BackColor = string.IsNullOrEmpty(_bioDataPrimary) ? Color.Black : Color.SteelBlue;
            btnDataFinger2.BackColor = string.IsNullOrEmpty(_bioDataSecondary) ? Color.Black : Color.SteelBlue;
            btnNfcData.BackColor = string.IsNullOrEmpty(_nfcUid) ? Color.Black : Color.SteelBlue;
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

        private void persistNfcState_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedReader))
                LoadReaderList();
        }

        private void lstBoxSearchResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxSearchResult.SelectedIndex <= -1)
                    return;

                var pepId = lstBoxSearchResult.SelectedItem.ToString().Split('|')[0];
                LoadPatientData(_systemBioDataStores.FirstOrDefault(x => x.PepId == pepId));
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

                foreach (var pds in _systemBioDataStores.Where(x => x.PepId != txtPepId.Text.Trim()))
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
            try
            {
                if (!_isBusy)
                    new Thread(() =>
                    {
                        _isBusy = true;
                        _systemBioDataStores = new List<System_BioDataStore>();

                        _lblInformationText = "Data is Updating";
                        _lblInformationForeColor = Color.DarkOrange;

                        var totalPatients = _localPdaEntities.System_BioDataStore.Count();
                        for (var i = 0; i < totalPatients; i+= 100)
                        {
                            _systemBioDataStores.AddRange(
                                _localPdaEntities.System_BioDataStore.OrderBy(x => x.Id).Skip(i).Take(100).Where(
                                    x => x.SiteId == _administrationStaffInformation.SiteId).ToList());
                        }
                        
                        _lblInformationText = $"Quick Search has been Updated. Loaded {totalPatients:#,###} Patients Successfully";
                        _lblInformationForeColor = Color.DarkGreen;

                        _isBusy = false;
                    }).Start();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }
        }

        private void ClearContents()
        {
            try
            {
                _systemBioDataStore = new System_BioDataStore();

                chkNfc.Checked = chkPriFin.Checked = chkSecFin.Checked = false;

                picBoxFingerPrint.Image = Resources.icons8_Fingerprint_48px;

                _bioDataPrimary = _bioDataSecondary = "";

                foreach (var ctl in pnlOfficialInformation.Controls.OfType<TextBox>().ToList())
                    ctl.Text = "";
                foreach (var ctl in pnlPersonalInformation.Controls.OfType<TextBox>().ToList())
                    ctl.Text = "";
                foreach (var ctl in pnlPersonalInformation.Controls.OfType<ComboBox>().ToList())
                    ctl.Text = "";

                txtSex.SelectedIndex =
                            txtStateOfResidence.SelectedIndex = txtLgaOfResidence.SelectedIndex = -1;

                txtDateOfBirth.Value = DateTime.Now;

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

        private bool InitializeBioDevice()
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

        private void LoadReaderList()
        {
            try
            {
                var readers = SCARD.Readers;

                if (readers != null)
                {
                    foreach (var reader in readers)
                    {
                        if (SetReader(reader))
                        {
                            lblNfcStatus.Text = @"NFC Device Connected";
                            lblNfcStatus.ForeColor = Color.Green;
                            _selectedReader = reader;
                            break;
                        }
                        else
                        {
                            lblNfcStatus.Text = @"Not a Valid NFC Device will Try another";
                            lblNfcStatus.ForeColor = Color.DarkOrange;
                            _selectedReader = null;
                            continue;
                        }
                    }
                }
                else
                {
                    lblNfcStatus.Text = @"No NFC Device Connected";
                    lblNfcStatus.ForeColor = Color.Brown;
                    _selectedReader = null;
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        public bool SetReader(string readerName)
        {
            try
            {
                _reader = new SCardReader(readerName);
                _reader.StartMonitor(ReaderStatusChanged);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string RemoveSpaces(string text)
        {
            return text.Where(t => t.ToString() != " ").Aggregate("", (current, t) => current + t);
        }

        private void Card_read_proc()
        {
            NfcTag tag = null;
            string msg = null;
            try
            {
                if (NfcTagType2.RecognizeAtr(_cardchannel))
                {
                    if (NfcTagType2.Recognize(_cardchannel))
                    {
                        tag = NfcTagType2.Create(_cardchannel);
                        if (tag == null)
                            msg = "An error has occured while reading the Tag's content";
                    }
                    else
                    {
                        msg = "From the ATR it may be a NFC type 2 Tag, but the content is invalid";
                    }
                }
                else
                {
                    if (NfcTagType4.Recognize(_cardchannel))
                    {
                        tag = NfcTagType4.Create(_cardchannel);
                        if (tag == null)
                            msg = "An error has occured while reading the Tag's content";
                    }
                    else
                    {
                        msg = "Unrecognized or unsupported card";
                    }
                }

                if (tag != null)
                {
                    this.BeginInvoke(new OnTagReadInvoker(OnTagRead), tag);
                }
                else
                {
                    this.BeginInvoke(new OnErrorInvoker(OnError), msg, "This is not a valid NFC Tag");
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void Card_write_proc(object tagParam)
        {
            try
            {
                var tag = _tag as NfcTag;

                var start = new DateTime();

                if (tag.Format())
                {
                    while (DateTime.Now.Subtract(start).TotalSeconds < 2)
                    {
                        //
                    }

                    if (tag.Write())
                        this.BeginInvoke(new OnTagWriteInvoker(OnTagWrite), tag);
                    else
                    {
                        lblNfcStatus.Text = @"Tag Write Failure. Please try again or contact Support.";
                        lblNfcStatus.ForeColor = Color.Brown;
                    }
                }
                else
                {
                    lblNfcStatus.Text = @"Tag Write Failure. Please try again or contact Support.";
                    lblNfcStatus.ForeColor = Color.Brown;
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void Card_format_proc(object tagParam)
        {
            try
            {
                if (_tag.Format())
                    this.BeginInvoke(new OnTagFormatInvoker(OnTagFormat), _tag);
                else
                    MessageBox.Show(@"Tag Format Error. Please try again");
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        delegate void ReaderStatusChangedInvoker(uint readerState, CardBuffer cardAtr);
        void ReaderStatusChanged(uint readerState, CardBuffer cardAtr)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.BeginInvoke(new ReaderStatusChangedInvoker(ReaderStatusChanged), readerState, cardAtr);
                    return;
                }

                SCARD.ReaderStatusToString(readerState);

                if (cardAtr != null)
                    _tagAtr = RemoveSpaces(cardAtr.AsString(" "));

                if (readerState == SCARD.STATE_UNAWARE)
                {
                    if (_cardchannel != null)
                    {
                        _cardchannel.Disconnect();
                        _cardchannel = null;
                    }

                    if (_inLoop)
                        return;

                    _selectedReader = null;
                    lblNfcStatus.Text = @"The reader we were working has gone AWOL from the system.";
                    lblNfcStatus.ForeColor = Color.DarkOrange;
                    _inLoop = true;
                }
                else if ((readerState & SCARD.STATE_EMPTY) != 0)
                {
                    _tag = null;
                    _inLoop = false;

                    if (_cardchannel == null)
                        return;

                    _cardchannel.Disconnect();
                    _cardchannel = null;

                    btnClear_Click(this, EventArgs.Empty);

                    lblInformation.Text = @"No Card Detected. Use Card for Instant Matching";
                    lblNfcStatus.Text = @"NFC Device Connected";
                    lblNfcStatus.ForeColor = Color.SteelBlue;
                    lblTagUid.Text = @"No Card Found";
                }
                else if ((readerState & SCARD.STATE_UNAVAILABLE) != 0)
                {
                    //
                }
                else if ((readerState & SCARD.STATE_MUTE) != 0)
                {
                    //
                }
                else if ((readerState & SCARD.STATE_INUSE) != 0)
                {
                    _inLoop = false;
                }
                else if ((readerState & SCARD.STATE_PRESENT) != 0)
                {
                    _inLoop = false;
                    if (_cardchannel == null)
                    {
                        _cardchannel = new SCardChannel(_reader);

                        if (_cardchannel.Connect())
                        {
                            MiFareCardProg mifare = new MiFareCardProg();
                            _nfcUid = RemoveSpaces(mifare.GetUID(_selectedReader).Trim());

                            lblTagUid.Text = _nfcUid;

                            _cardthread = new Thread(Card_read_proc);
                            _cardthread.Start();

                            lblNfcStatus.Text = @"Card Connected. UID: " + _nfcUid;
                            lblNfcStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblNfcStatus.Text = @"NearField failed to connect to the card in the reader";
                            lblNfcStatus.ForeColor = Color.Brown;

                            _cardchannel = null;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        delegate void OnTagWriteInvoker(NfcTag _tag);
        void OnTagWrite(NfcTag _tag)
        {
            try
            {
                MessageBox.Show(@"Tag Write Successful.");
                Card_read_proc();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        delegate void OnTagFormatInvoker(NfcTag _tag);
        void OnTagFormat(NfcTag _tag)
        {
            try
            {
                lblNfcStatus.Text = @"Tag Successfully Formatted";
                lblNfcStatus.ForeColor = Color.Green;
                Card_read_proc();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        delegate void OnTagReadInvoker(NfcTag tag);
        void OnTagRead(NfcTag tag)
        {
            try
            {
                this._tag = tag;

                if (this._tag == null)
                {
                    lblNfcStatus.Text = @"Bad/Unreadable Tag";
                    lblNfcStatus.ForeColor = Color.DarkOrange;
                    return;
                }

                if ((this._tag.Content != null) && (this._tag.Content.Count != 0))
                {
                    foreach (var ndef in this._tag.Content)
                    {
                        if (!(ndef is RtdVCard))
                            continue;

                        var smart = (RtdVCard)ndef;
                        lblInformation.Text = smart.Family_name + @" " + smart.First_name;
                    }

                    var patientData = _systemBioDataStores.FirstOrDefault(x => x.NfcUid == _nfcUid);
                    
                    if (patientData != null)
                        LoadPatientData(patientData);
                    else
                    {
                        lblNfcStatus.Text = @"No Patient Information Found";
                        lblNfcStatus.ForeColor = Color.SteelBlue;

                        lblInformation.Text = $@"Tag {_nfcUid} is from a different State. Please Use Query";
                    }
                }
                else
                    lblInformation.Text = @"This Tag is Empty. You can proceed with attaching a Patient to it!";
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void LoadPatientData(System_BioDataStore patientData)
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

                var patientDataResolved = Newtonsoft.Json.JsonConvert.DeserializeObject<PatientInformation>(patientData.PatientData);

                txtAddress.Text = patientDataResolved.Patient_PatientInformation.HouseAddress;
                txtDateOfBirth.Value = patientDataResolved.Patient_PatientInformation.DateOfBirth;
                txtHospitalNumber.Text = patientDataResolved.Patient_PatientInformation.HospitalNumber;
                //txtMaritalStatus.Text = patientDataResolved.Patient_PatientInformation.MaritalStatus;
                txtOtherNames.Text = patientDataResolved.Patient_PatientInformation.Othername;
                txtPepId.Text = patientData.PepId;
                txtPhoneNumber.Text = patientDataResolved.Patient_PatientInformation.PhoneNumber;
                txtPreviousNumber.Text = patientDataResolved.Patient_PatientInformation.PreviousId;
                txtSex.Text = patientDataResolved.Patient_PatientInformation.Sex;
                txtSurname.Text = patientDataResolved.Patient_PatientInformation.Surname;

                txtStateOfResidence.SelectedValue = patientDataResolved.Patient_PatientInformation.HouseAddressState;
                txtStateOfResidence_SelectedValueChanged(this, EventArgs.Empty);
                txtLgaOfResidence.SelectedValue = patientDataResolved.Patient_PatientInformation.HouseAddresLga;
                //txtStateOfOrigin.SelectedValue = patientDataResolved.Patient_PatientInformation.StateOfOrigin;

                if (patientDataResolved.Patient_PatientBiometricData != null)
                {
                    _bioDataPrimary = patientDataResolved.Patient_PatientBiometricData.FingerPrimary;
                    _bioDataSecondary = patientDataResolved.Patient_PatientBiometricData.FingerSecondary;

                    if (!string.IsNullOrEmpty(_bioDataPrimary))
                        chkPriFin.Checked = true;
                    if (!string.IsNullOrEmpty(_bioDataSecondary))
                        chkSecFin.Checked = true;
                }

                if (patientDataResolved.Patient_PatientNearFieldCommunicationData != null)
                {
                    _nfcUid = patientDataResolved.Patient_PatientNearFieldCommunicationData.CardId;

                    if (!string.IsNullOrEmpty(_nfcUid))
                        chkNfc.Checked = true;
                }

                Application.DoEvents();

                _systemBioDataStore =
                    _localPdaEntities.System_BioDataStore.FirstOrDefault(
                        x => x.PepId == patientDataResolved.Patient_PatientInformation.PepId) ?? new System_BioDataStore();
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
                    _systemBioDataStores.Where(
                        x =>
                            (x.PepId.ToLower().Contains(query) ||
                             x.PatientData.ToLower().Contains(query)) &&
                            !x.IsDeleted).Take(10).Select(s => new { s.PepId, s.FullName }).ToList();

                if (!results.Any())
                    lstBoxSearchResult.Items.Add($"No Match");
                else
                    foreach (var result in results)
                        lstBoxSearchResult.Items.Add($"{result.PepId.ToUpper()}| {result.FullName}");

                if (string.IsNullOrEmpty(query))
                    lstBoxSearchResult.Visible = false;
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
            catch (Exception)
            {
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
                bmp.Dispose();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }
        }

        #endregion
    }
}
