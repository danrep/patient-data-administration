using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        private string _bioDataPrimary;
        private string _bioDataSecondary;
        private string _nfcUid;

        private SGFingerPrintManager _fingerPrintManager;
        private SGFPMDeviceInfoParam _deviceInfoParam;
        private SGFPMDeviceList _device;
        private Thread _bioReaderThread;
        private string _deviceName;
        private int _deviceId;
        private Color _lblBioDeviceInfoForeColor, _lblInformationForeColor;
        private string _lblBioDeviceInfoText, _lblInformationText;

        private System_BioDataStore _systemBioDataStore;
        private readonly UserCredential _userCredential;

        private string _selectedReader;
        private string _tagAtr;
        private SCardReader _reader;
        private SCardChannel _cardchannel = null;
        private NfcTag _tag = null;
        private Thread _cardthread;
        private bool _inLoop = false;

        #region Form Events

        public SubInformationManagement(UserCredential userCredential)
        {
            this._userCredential = userCredential;
            InitializeComponent();
            _fingerPrintManager = new SGFingerPrintManager();
        }

        private void SubInformationManagement_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                this.system_BioDataStoreTableAdapter.Fill(this.localPDADataSet.System_BioDataStore);
            }).Start();

            // TODO: This line of code loads data into the 'localPDADataSet.System_LocalGovermentArea' table. You can move, or remove it, as needed.
            this.system_LocalGovermentAreaTableAdapter.Fill(this.localPDADataSet.System_LocalGovermentArea);
            // TODO: This line of code loads data into the 'localPDADataSet.System_State' table. You can move, or remove it, as needed.
            this.system_StateTableAdapter.Fill(this.localPDADataSet.System_State);

            _systemBioDataStore = new System_BioDataStore();
            _bioReaderThread = new Thread(() => {});
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
                _bioReaderThread.Abort();
                _cardthread.Abort();
                _fingerPrintManager.CloseDevice();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _userCredential.AdministrationStaffInformation.Id);
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region Control Events

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lstBoxSearchResult.Visible = true;

                lstBoxSearchResult.Items.Clear();

                var query = txtSearch.Text.Trim();
                var results =
                    localPDADataSet.System_BioDataStore.Where(
                        x =>
                            (x.PepId.Contains(query) ||
                             x.PatientData.Contains(query) ) &&
                            !x.IsDeleted).Select(s => new { s.PepId, s.FullName }).ToList();

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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void btnSearchBiometrics_Click(object sender, EventArgs e)
        {
            try
            {
                var capturedBioData = Convert.FromBase64String(GetBioData());
                var matched = false;
                LocalPDADataSet.System_BioDataStoreRow patientData = null;

                foreach (var pds in this.localPDADataSet.System_BioDataStore.ToList())
                {
                    try
                    {
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
                        LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
                    }
                }

                if (matched)
                {
                    LoadPatientData(patientData);
                }
                else
                {
                    lblInformation.Text = @"No Match Found";
                    ClearContents();
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearContents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.Close();
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
                        SiteId = _userCredential.AdministrationSiteInformation.Id,
                        Surname = txtSurname.Text,
                        StateOfOrigin = 0,
                        PhoneNumber = txtPhoneNumber.Text, 
                        Id = 0
                    }
                };

                if (!string.IsNullOrEmpty(_bioDataPrimary) && !string.IsNullOrEmpty(_bioDataSecondary))
                {
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

                patientData.Administration_StaffInformation = _userCredential.AdministrationStaffInformation;

                #endregion Patient Data Composition

                var result = LocalCore.Post("/ClientCommunication/Patient/PostPatient", Newtonsoft.Json.JsonConvert.SerializeObject(patientData));

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
                            SiteId = _userCredential.AdministrationSiteInformation.Id
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
                    btnClear_Click(this, e);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
                lblInformation.Text = @"An unexpected error has occured. Please contact the Administrator";
                lblInformation.ForeColor = Color.DarkRed;
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

        }

        private void persistLoad_Tick(object sender, EventArgs e)
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
                var o = (System.Convert.ChangeType(txtStateOfResidence.SelectedValue, typeof(int)));
                if (o != null)
                    this.system_LocalGovermentAreaTableAdapter.FillBy(this.localPDADataSet.System_LocalGovermentArea,
                        ((int)o));
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LoadPatientData(this.localPDADataSet.System_BioDataStore.FirstOrDefault(x => x.PepId == pepId));
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void timerUpdateInformation_Tick(object sender, EventArgs e)
        {
            lblBioDeviceInfo.ForeColor = _lblBioDeviceInfoForeColor;
            lblBioDeviceInfo.Text = _lblBioDeviceInfoText;

            lblInformation.Text = _lblInformationText;

        }

        #endregion

        #region Methods

        private void ClearContents()
        {
            new Thread(() =>
            {
                this.system_BioDataStoreTableAdapter.Fill(this.localPDADataSet.System_BioDataStore);
            }).Start();

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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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

                    var patientData = this.localPDADataSet.System_BioDataStore.FirstOrDefault(x => x.NfcUid == _nfcUid);
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void LoadPatientData(LocalPDADataSet.System_BioDataStoreRow patientData)
        {
            if (patientData == null)
            {
                System.Media.SystemSounds.Hand.Play();
                btnClear_Click(this, EventArgs.Empty);
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

        private string GetBioData()
        {
            try
            {
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
                    _lblBioDeviceInfoForeColor = Color.DarkRed;
                    _lblBioDeviceInfoText = @"Device Unreachable";
                    picBoxFingerPrint.Image = Resources.icons8_Cancel_48px;
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void DrawImage(byte[] imgData)
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

        #endregion
    }
}
