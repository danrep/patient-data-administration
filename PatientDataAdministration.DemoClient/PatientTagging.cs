using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using SecuGen.FDxSDKPro.Windows;
using System.Threading;
using ThreadState = System.Threading.ThreadState;
using Codesistance.NFC;
using PatientDataAdministration.DemoClient.Properties;

namespace PatientDataAdministration.DemoClient
{
    public partial class PatientTagging : MetroForm
    {
        //private readonly DemoDbEntities _demoDb = new DemoDbEntities(); 

        private string _bioFinger1;
        private string _bioFinger2;
        private string _dataOwner;
        private string _passportImage;

        private string _tagAtr;
        private string _selectedReader;
        private SCardReader _reader;
        private SCardChannel _cardchannel = null;
        private NfcTag _tag = null;
        private Thread _cardthread;
        private bool _tagStatusChange = false;
        private bool _deviceStatusChange = false;
        private bool _networkStatusChange = false;
        private bool _downloadStatusChange = false;
        private bool _mailStatusChange = false;
        private bool _customerPulling = false;
        private bool _inLoop = false;
        private bool _warrantyShow = false;
        private string _cardUid;
        private string _cardChip;

        private List<PatientData> _patientDataStore = new List<PatientData>();
        private readonly DemoDbContext _demoDb = new DemoDbContext();

        private SGFingerPrintManager _mFpm;
        private SGFPMDeviceInfoParam _pInfo;
        private PatientData _patientData;

        private Thread _thread;

        private int _deviceId = 0;
        private int _iError = 0;


        public PatientTagging()
        { 
            InitializeComponent();
        }

        private void PatientInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mFpm.CloseDevice();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void ShowBalloon(string message, ToolTipIcon icon)
        {
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.BalloonTipTitle = icon.ToString();
            notifyIcon1.ShowBalloonTip(3500);

            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PatientInfo_Load(object sender, EventArgs e)
        {
            tmrCheckBioCon.Start();
        }

        private void ReloadDevice()
        {
            _thread = new Thread(() =>
            {
                _mFpm = new SGFingerPrintManager();
                _mFpm.EnumerateDevice();

                if (_mFpm.NumberOfDevice == 0)
                    return;

                var iError = 0;
                _deviceId = (int)(SGFPMPortAddr.USB_AUTO_DETECT);
                const SGFPMDeviceName deviceName = SGFPMDeviceName.DEV_AUTO;

                iError = _mFpm.Init(deviceName);
                iError = _mFpm.OpenDevice(_deviceId);
            });
            _thread.Start();
        }

        private bool GetDeviceInfo()
        {
            _iError = _mFpm.OpenDevice(_deviceId);
            if (_iError != (int)SGFPMError.ERROR_NONE)
                return false;

            _pInfo = new SGFPMDeviceInfoParam();
            _iError = _mFpm.GetDeviceInfo(_pInfo);

            if (_iError != (int)SGFPMError.ERROR_NONE)
                return false;
            else
                return true;
        }

        private void DrawImage(byte[] imgData)
        {
            var bmp = new Bitmap(_pInfo.ImageWidth, _pInfo.ImageHeight);
            pictureBox1.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    var colorval = (int)imgData[(j * _pInfo.ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            pictureBox1.Refresh();
        }

        private string GetBioData()
        {
            try
            {
                var mDataMin = new byte[400];

                var fpImage = new byte[_pInfo.ImageWidth * _pInfo.ImageHeight];

                var iError = _mFpm.GetImage(fpImage);

                if (iError == (int)SGFPMError.ERROR_NONE)
                {
                    DrawImage(fpImage);

                    if (_mFpm.CreateTemplate(null, fpImage, mDataMin) == 0)
                        return Convert.ToBase64String(mDataMin);
                    else
                    {
                        pictureBox1.Image = DemoClient.Properties.Resources.icons8_Cancel_48px;
                        return string.Empty;
                    }
                }
                else
                {
                    pictureBox1.Image = DemoClient.Properties.Resources.icons8_Cancel_48px;
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void tmrCheckBioCon_Tick(object sender, EventArgs e)
        {
            if (_thread == null)
            {
                ReloadDevice();
                lblBioDeviceInfo.ForeColor = Color.DarkSlateBlue;
                lblBioDeviceInfo.Text = @"Wait a minute. Initializing Biometric Device";
                return;
            }

            if (_thread.ThreadState == ThreadState.Running)
            {
                lblBioDeviceInfo.ForeColor = Color.DarkRed;
                lblBioDeviceInfo.Text = @"Can't wait for the device. Refreshing at the moment";

                while(_thread.ThreadState == ThreadState.Running)
                    _thread.Abort();

                ReloadDevice();
                return;
            }

            _thread.Join();
            
            if (_mFpm.NumberOfDevice == 1)
            {
                if (GetDeviceInfo())
                {
                    lblBioDeviceInfo.ForeColor = Color.DarkGreen;
                    lblBioDeviceInfo.Text = @"Device is Active";
                }
                else
                {
                    lblBioDeviceInfo.ForeColor = Color.DarkRed;
                    lblBioDeviceInfo.Text = @"Oops! Re-checking... You may need to Restart this App";
                    ReloadDevice();
                }
            }
            else if (_mFpm.NumberOfDevice > 1)
            {
                tmrCheckBioCon.Enabled = false;
                lblBioDeviceInfo.ForeColor = Color.DarkOrange;
                lblBioDeviceInfo.Text = @"More than 1 device found. Remove all but 1";
            }
            else
            {
                lblBioDeviceInfo.ForeColor = Color.DarkRed;
                lblBioDeviceInfo.Text = @"No Device Found. Please Re-attach";
                ReloadDevice();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            var data = GetBioData();
            LoadPatientByFingerprint(data);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gradientPanel2.Visible = false;
            pictureBox1.Image = DemoClient.Properties.Resources.icons8_Fingerprint_48px;
        }

        private void PatientInfo_Click(object sender, EventArgs e)
        {
            gradientPanel2.Visible = true;
        }

        private void LoadPatientData(PatientData patientData)
        {
            txtFullName.Text = patientData.Surname + @" " + patientData.Othernames;
            txtSiteName.Text = patientData.SiteName;

            txtPepId.Text = patientData.PepId;

            txtAddress.Text = patientData.HouseAddress;

            txtPassport.Image = ByteToImage(Convert.FromBase64String(patientData.PassportImage));

            txtInfoPersonal.Text = patientData.PhoneHumber + @" | " + patientData.Sex + @" | " +
                                   patientData.StateOfOrigin;
            txtInfoMedical.Text = patientData.ClientNumber + @" | " + patientData.FacilityNumber + @" | " +
                                  patientData.HospitalNumber;

            _bioFinger1 = patientData.BioDataFingerPrimary;
            _bioFinger2 = patientData.BioDataFingerSecondary;

            if (txtPassport.Image.Height > txtPassport.Height || txtPassport.Image.Width > txtPassport.Width)
                txtPassport.SizeMode = PictureBoxSizeMode.Zoom;
            else
                txtPassport.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void LoadPatientByFingerprint(string fingerPrintData)
        {
            var capturedBioData = Convert.FromBase64String(fingerPrintData);
            var matched = false;

            _patientDataStore = _demoDb.PatientDatas.Where(x => !x.IsDeleted).ToList();

            foreach (var pds in _patientDataStore)
            {
                try
                {
                    _mFpm.MatchTemplate(Convert.FromBase64String(pds.BioDataFingerPrimary), capturedBioData,
                        SGFPMSecurityLevel.HIGH, ref matched);

                    if (!matched)
                        _mFpm.MatchTemplate(Convert.FromBase64String(pds.BioDataFingerSecondary), capturedBioData,
                            SGFPMSecurityLevel.HIGH, ref matched);

                    if (!matched)
                        continue;
                    
                    ShowBalloon($"Patient with PEP ID {pds.PepId} is a Match.", ToolTipIcon.Info);

                    _patientData = pds;
                    break;
                }
                catch
                {
                    // ignored
                }
            }

            if (matched)
            {
                LoadPatientData(_patientData);
            }
            else
            {
                MessageBox.Show(@"No Match Found");
                btnClear_Click(this, EventArgs.Empty);
            }
        }

        private static Bitmap ByteToImage(byte[] blob)
        {
            var mStream = new MemoryStream();
            var pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            var bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInfoPersonal.Text  = txtAddress.Text = txtFullName.Text = txtPepId.Text = txtSiteName.Text = lblTagData.Text = txtInfoMedical.Text = "";
            txtPassport.Image = Properties.Resources.icons8_Image_File_48px;
            pictureBox1.Image = Resources.icons8_Fingerprint_48px;
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
                            lblNfcDeviceInfo.Text = @"NFC Device Connected";
                            lblNfcDeviceInfo.ForeColor = Color.Green;
                            _selectedReader = reader;
                            break;
                        }
                        else
                        {
                            lblNfcDeviceInfo.Text = @"Not a Valid NFC Device will Try another";
                            lblNfcDeviceInfo.ForeColor = Color.DarkOrange;
                            _selectedReader = null;
                            continue;
                        }
                    }
                }
                else
                {
                    lblNfcDeviceInfo.Text = @"No NFC Device Connected";
                    lblNfcDeviceInfo.ForeColor = Color.Brown;
                    _selectedReader = null;
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
        }

        private void tmrCheckNfcDevice_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedReader))
                LoadReaderList();
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

        private void card_read_proc()
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
            catch (Exception )
            {
            }
        }

        private void card_write_proc(object _tagParam)
        {
            try
            {
                NfcTag tag = _tag as NfcTag;
                bool again = true;

                WriteAgain:
                DateTime start = new DateTime();

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
                        if (again)
                        {
                            again = false;
                            goto WriteAgain;
                        }

                        lblNfcDeviceInfo.Text = @"Tag Write Failure. Please try again or contact Support.";
                        lblNfcDeviceInfo.ForeColor = Color.Brown;
                    }
                }
                else
                {
                    lblNfcDeviceInfo.Text = @"Tag Write Failure. Please try again or contact Support.";
                    lblNfcDeviceInfo.ForeColor = Color.Brown;
                }
            }
            catch (Exception ex)
            {//
            }
        }

        private void card_format_proc(object _tagParam)
        {
            try
            {
                bool again = true;

                FormatAgain:
                if (_tag.Format())
                    this.BeginInvoke(new OnTagFormatInvoker(OnTagFormat), _tag);
                else
                {
                    if (again)
                    {
                        again = false;
                        goto FormatAgain;
                    }

                    MessageBox.Show(@"Tag Format Error. Please try again");
                }
            }
            catch (Exception ex)
            {//
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
                    lblNfcDeviceInfo.Text = @"The reader we were working has gone AWOL from the system.";
                    lblNfcDeviceInfo.ForeColor = Color.DarkOrange;
                    _inLoop = true;
                }
                else if ((readerState & SCARD.STATE_EMPTY) != 0)
                {
                    _tag = null;
                    _tagStatusChange = false;
                    _deviceStatusChange = true;
                    _inLoop = false;

                    if (_cardchannel == null)
                        return;

                    _cardchannel.Disconnect();
                    _cardchannel = null;

                    btnClear_Click(this, EventArgs.Empty);

                    txtTagUid.Text = @"No Card Detected";
                    lblTagData.Text = @"Reader Active. Please Use Card";
                    lblNfcDeviceInfo.Text = @"NFC Device Connected";
                }
                else if ((readerState & SCARD.STATE_UNAVAILABLE) != 0)
                {
                    _deviceStatusChange = false;
                    _customerPulling = _tagStatusChange = false;
                }
                else if ((readerState & SCARD.STATE_MUTE) != 0)
                {

                }
                else if ((readerState & SCARD.STATE_INUSE) != 0)
                {
                    _deviceStatusChange = true;
                    _inLoop = false;
                }
                else if ((readerState & SCARD.STATE_PRESENT) != 0)
                {
                    _tagStatusChange = true;
                    _deviceStatusChange = true;
                    _inLoop = false;
                    if (_cardchannel == null)
                    {
                        _cardchannel = new SCardChannel(_reader);

                        if (_cardchannel.Connect())
                        {
                            MiFareCardProg mifare = new MiFareCardProg();
                            _cardUid = RemoveSpaces(mifare.GetUID(_selectedReader).Trim());

                            txtTagUid.Text = _cardUid;

                            _cardthread = new Thread(card_read_proc);
                            _cardthread.Start();
                            _tagStatusChange = true;

                            lblNfcDeviceInfo.Text = @"Card Connected. UID: " + _cardUid;
                            lblNfcDeviceInfo.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblNfcDeviceInfo.Text = @"NearField failed to connect to the card in the reader";
                            lblNfcDeviceInfo.ForeColor = Color.Brown;

                            _cardchannel = null;
                            _tagStatusChange = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {//
            }
        }

        delegate void OnTagWriteInvoker(NfcTag _tag);
        void OnTagWrite(NfcTag _tag)
        {
            try
            {
                MessageBox.Show(@"Tag Write Successful.");

                _patientData.CardDataUid = txtTagUid.Text;
                _patientData.TagData = txtTagUid.Text;
                _patientData.CardDataChip = _tag.ToString();

                _demoDb.Entry(_patientData).State = EntityState.Modified;
                _demoDb.SaveChanges();

                _patientData = null;
                pictureBox1.Image = Resources.icons8_Fingerprint_48px;

                card_read_proc();
            }
            catch (Exception ex)
            {//
            }
        }

        delegate void OnTagFormatInvoker(NfcTag _tag);
        void OnTagFormat(NfcTag _tag)
        {
            try
            {
                lblNfcDeviceInfo.Text = @"Tag Successfully Formatted";
                lblNfcDeviceInfo.ForeColor = Color.Green;
                card_read_proc();
            }
            catch (Exception ex)
            {//
            }
        }

        delegate void OnTagReadInvoker(NfcTag _tag);
        void OnTagRead(NfcTag _tag)
        {
            try
            {
                this._tag = _tag;

                if (this._tag == null)
                {
                    lblNfcDeviceInfo.Text = @"Bad Tag";
                    lblNfcDeviceInfo.ForeColor = Color.DarkOrange;
                    return;
                }

                btnClear_Click(this, EventArgs.Empty);

                if ((this._tag.Content != null) && (this._tag.Content.Count != 0))
                {
                    foreach (var ndef in this._tag.Content)
                    {
                        if (!(ndef is RtdVCard))
                            continue;

                        var smart = (RtdVCard)ndef;
                        lblTagData.Text = smart.Family_name + @" " + smart.First_name;
                    }

                    var patientData =_demoDb.PatientDatas.FirstOrDefault(x => x.TagData == _cardUid);
                    if (patientData != null)
                        LoadPatientData(patientData);
                }
            }
            catch (Exception)
            {
                //
            }
        }

        delegate void OnErrorInvoker(string text, string caption);
        void OnError(string text, string caption)
        {
            lblNfcDeviceInfo.Text = text;
            lblNfcDeviceInfo.ForeColor = Color.DarkOrange;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _tag.Content.Clear();
                _cardthread = new Thread(new ParameterizedThreadStart(card_format_proc));
                _cardthread.Start(_tag);

                btnClear_Click(this, e);
            }
            catch (Exception exception)
            {
                //
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_patientData == null)
                {
                    MessageBox.Show(@"Load a Patient using Biometrics before Proceeding");
                    return;
                }

                var tagData = new RtdVCard();
                tagData.set_input_texts("Family_name", _patientData.Surname);
                tagData.set_input_texts("First_name", _patientData.Othernames);
                tagData.set_input_texts("Role", _patientData.PepId);

                _tag.Content.Clear();
                _tag.Content.Add(tagData);

                _cardthread = new Thread(new ParameterizedThreadStart(card_write_proc));
                _cardthread.Start(_tag);
            }
            catch (Exception exception)
            {
                //
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var patientData = _demoDb.PatientDatas.FirstOrDefault(x => x.TagData == _cardUid);

                if (patientData == null)
                {
                    MessageBox.Show(@"The Patient was not found. Please try again");
                    return;
                }

                patientData.TagData = patientData.CardDataUid = patientData.CardDataChip = string.Empty;
                _demoDb.Entry(patientData).State = EntityState.Modified;

                _demoDb.SaveChanges();
                button4_Click(this, e);
            }
            catch (Exception )
            {
                //
            }
        }
    }
}
