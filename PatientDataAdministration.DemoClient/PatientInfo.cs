using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using SecuGen.FDxSDKPro.Windows;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace PatientDataAdministration.DemoClient
{
    public partial class PatientInfo : MetroForm
    {
        private readonly DemoDbEntities _demoDb = new DemoDbEntities();

        private string _bioFinger1;
        private string _bioFinger2;
        private string _dataOwner;
        private string _cardUid;
        private string _cardChip;
        private string _passportImage;

        private List<PatientData> _patientDataStore = new List<PatientData>(); 

        private SGFingerPrintManager _mFpm;
        private SGFPMDeviceInfoParam _pInfo;
        private PatientData _patientData;

        private Thread _thread;

        private int _deviceId = 0;
        private int _iError = 0;

        private bool _lockAndLoad;

        public PatientInfo(bool lockAndLoad)
        {
            _lockAndLoad = lockAndLoad;
            InitializeComponent();
        }

        private void PatientInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mFpm.CloseDevice();
            GC.Collect();
            GC.WaitForPendingFinalizers();
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

                if (!Regex.Match(txtPhoneNumber.Text, @"\d{11}").Success)
                {
                    txtPhoneNumber.Focus();
                    MessageBox.Show("Please enter a valid Phone Number");
                    return;
                }

                if (txtPassport.Image == null)
                    _passportImage = string.Empty;

                if (_patientData.Id == 0)
                {
                    var patientInfo =
                        _demoDb.PatientDatas.FirstOrDefault(
                            x => x.PhoneHumber == txtPhoneNumber.Text.Trim() && !x.IsDeleted);

                    if (patientInfo != null)
                    {
                        MessageBox.Show("There is a Patient with the same Phone Number. Please confirm");
                        return;
                    }

                    _patientData = new PatientData()
                    {
                        PhoneHumber = txtPhoneNumber.Text.Trim(), 
                        IsDeleted = false, 
                        BioDataFingerPrimary = _bioFinger1, 
                        BioDataFingerSecondary = _bioFinger2, 
                        CardDataChip = _cardChip, 
                        CardDataUid = _cardUid,
                        ClientNumber = txtPatientHospitalNumber.Text.Trim(), 
                        DateOfBirth = txtDateOfBirth.Value, 
                        Email = txtEmail.Text.Trim(),
                        FacilityNumber = txtFacilityNumber.Text.Trim(), 
                        HospitalNumber = txtHospitalNumber.Text.Trim(), 
                        HouseAddress = txtHouseAddress.Text.Trim(), 
                        Othernames = txtOthername.Text.Trim(), 
                        PepId = txtPepId.Text.Trim(),
                        Sex = txtSex.Text,
                        SiteName = txtSiteName.Text, 
                        StateOfOrigin = txtStateOfOrigin.Text.Trim(),
                        Surname = txtSurname.Text.Trim(), 
                        VitalsHeight = Convert.ToInt32(txtHeight.Text.Trim()), 
                        VitalsWeight = Convert.ToInt32(txtWeight.Text.Trim()),
                        PassportImage = _passportImage,
                        MaritalStatus = txtMaritalStatus.Text
                    };

                    _demoDb.PatientDatas.Add(_patientData);
                }
                else
                {
                    var patientInfo =
                        _demoDb.PatientDatas.FirstOrDefault(
                            x => x.PhoneHumber == txtPhoneNumber.Text.Trim() && !x.IsDeleted);

                    if (patientInfo == null)
                    {
                        MessageBox.Show("This Patient does not exist. Please try again");
                        return;
                    }

                    patientInfo.BioDataFingerPrimary = _bioFinger1;
                    patientInfo.BioDataFingerSecondary = _bioFinger2;
                    patientInfo.CardDataChip = _cardChip;
                    patientInfo.CardDataUid = _cardUid;
                    patientInfo.ClientNumber = txtPatientHospitalNumber.Text.Trim();
                    patientInfo.DateOfBirth = txtDateOfBirth.Value;
                    patientInfo.Email = txtEmail.Text.Trim();
                    patientInfo.FacilityNumber = txtFacilityNumber.Text.Trim();
                    patientInfo.HospitalNumber = txtHospitalNumber.Text.Trim();
                    patientInfo.HouseAddress = txtHouseAddress.Text.Trim();
                    patientInfo.Othernames = txtOthername.Text.Trim();
                    patientInfo.Sex = txtSex.Text;
                    patientInfo.StateOfOrigin = txtStateOfOrigin.Text.Trim();
                    patientInfo.Surname = txtSurname.Text.Trim();
                    patientInfo.VitalsHeight = Convert.ToInt32(txtHeight.Text.Trim());
                    patientInfo.VitalsWeight = Convert.ToInt32(txtWeight.Text.Trim());
                    patientInfo.PassportImage = _passportImage;
                    patientInfo.MaritalStatus = txtMaritalStatus.Text;
                    patientInfo.SiteName = txtSiteName.Text;

                    _demoDb.Entry(patientInfo).State = EntityState.Modified;
                }

                _demoDb.SaveChanges();
                MessageBox.Show("Patient Configuration Successful");
                btnClear_Click(this, e);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (var ctl in gradientPanel11.Controls.OfType<TextBoxExt>().ToList())
                ctl.Text = "";
            foreach (var ctl in gradientPanel11.Controls.OfType<ComboBox>().ToList())
                ctl.Text = "";

            txtSex.SelectedIndex = txtMaritalStatus.SelectedIndex = txtStateOfOrigin.SelectedIndex = -1;

            txtDateOfBirth.Value = DateTime.Now;

            _bioFinger1 = _bioFinger2 = string.Empty;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btnPassport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtPassport.Image = Image.FromFile(openFileDialog1.FileName);

            if (txtPassport.Image.Height > txtPassport.Height || txtPassport.Image.Width > txtPassport.Width)
                txtPassport.SizeMode = PictureBoxSizeMode.Zoom;
            else
                txtPassport.SizeMode = PictureBoxSizeMode.CenterImage;

            _passportImage = Convert.ToBase64String(File.ReadAllBytes(openFileDialog1.FileName));
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
                var mRegMin = new byte[400];

                var fpImage = new byte[_pInfo.ImageWidth * _pInfo.ImageHeight];

                var iError = _mFpm.GetImage(fpImage);

                if (iError == (int)SGFPMError.ERROR_NONE)
                {
                    if (_mFpm.CreateTemplate(fpImage, mRegMin) == 0)
                    {
                        DrawImage(fpImage);
                        return Convert.ToBase64String(_lockAndLoad ? fpImage : mRegMin);
                    }
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

        private void btnDataFinger1_Click(object sender, EventArgs e)
        {
            if (lblBioDeviceInfo.ForeColor != Color.DarkGreen)
                return;

            _dataOwner = btnDataFinger1.Name;
            gradientPanel2.Visible = true;
            label21.Text = @"Primay Finger Template";
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            var data = GetBioData();

            if (_lockAndLoad)
            {
                LoadPatientByFingerprint(data);
                return;
            }

            if (_dataOwner == btnDataFinger1.Name)
                _bioFinger1 = !string.IsNullOrEmpty(data) ? data : _bioFinger1;
            if (_dataOwner == btnDataFinger2.Name)
                _bioFinger2 = !string.IsNullOrEmpty(data) ? data : _bioFinger2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gradientPanel2.Visible = false;
            pictureBox1.Image = DemoClient.Properties.Resources.icons8_Fingerprint_48px;
        }

        private void btnDataFinger2_Click(object sender, EventArgs e)
        {
            if (lblBioDeviceInfo.ForeColor != Color.DarkGreen)
                return;

            _dataOwner = btnDataFinger2.Name;
            gradientPanel2.Visible = true;
            label21.Text = @"Secondary Finger Template";
        }

        private void PatientInfo_Click(object sender, EventArgs e)
        {
            gradientPanel2.Visible = true;
        }

        private void checkBioData_Tick(object sender, EventArgs e)
        {
            btnDataFinger1.BackColor = string.IsNullOrEmpty(_bioFinger1) ? Color.Black : Color.SteelBlue;
            btnDataFinger2.BackColor = string.IsNullOrEmpty(_bioFinger2) ? Color.Black : Color.SteelBlue;
        }

        private void PatientInfo_Shown(object sender, EventArgs e)
        {
            if (_lockAndLoad)
            {
                gradientPanel2.Visible = true;
                var cancelRequested = false;
                label21.Text = @"Search for Patient";

                while (lblBioDeviceInfo.ForeColor != Color.DarkGreen)
                {
                    var dialogueResult = MessageBox.Show(
                        @"The Biometric Device is not ready yet. Please wait a little while longer. You can Cancel if you like.",
                        @"Wait a Minute", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);

                    if (dialogueResult != DialogResult.Cancel)
                        continue;

                    cancelRequested = true;
                    break;
                }

                if (cancelRequested) { this.Close(); }
                btnFind.Visible = true;
            }
            else
            {
                _patientData = new PatientData();
                btnClear.Visible = true;
            }
        }

        private void LoadPatientByFingerprint(string fingerPrintData)
        {
            var capturedBioData = Convert.FromBase64String(fingerPrintData);
            var matched = false;

            _patientDataStore = _demoDb.PatientDatas.Where(x => !x.IsDeleted).ToList();

            var matchingPatient = new PatientData();

            foreach (var pds in _patientDataStore)
            {
                _mFpm.MatchTemplate(Convert.FromBase64String(pds.BioDataFingerPrimary), capturedBioData,
                    SGFPMSecurityLevel.HIGH, ref matched);

                if (!matched)
                    _mFpm.MatchTemplate(Convert.FromBase64String(pds.BioDataFingerSecondary), capturedBioData,
                        SGFPMSecurityLevel.HIGH, ref matched);

                if (!matched)
                    continue;

                MessageBox.Show("Patient Found");

                matchingPatient = pds;
                gradientPanel2.Visible = false;
                break;
            }

            if (matchingPatient.Id != 0)
            {
                txtEmail.Text = matchingPatient.Email;
                txtDateOfBirth.Value = matchingPatient.DateOfBirth.Date;
                txtFacilityNumber.Text = matchingPatient.FacilityNumber;
                txtHeight.Text = matchingPatient.VitalsHeight?.ToString("0.00") ?? "0.00";
                txtWeight.Text = matchingPatient.VitalsWeight?.ToString("0.00") ?? "0.00";
                txtHospitalNumber.Text = matchingPatient.HospitalNumber;
                txtHouseAddress.Text = matchingPatient.HouseAddress;
                txtMaritalStatus.Text = matchingPatient.MaritalStatus;

                txtPassport.Image = ByteToImage(Convert.FromBase64String(matchingPatient.PassportImage));

                txtPatientHospitalNumber.Text = matchingPatient.ClientNumber;
                txtPepId.Text = matchingPatient.PepId;
                txtOthername.Text = matchingPatient.Othernames;
                txtPhoneNumber.Text = matchingPatient.PhoneHumber;
                txtSex.Text = matchingPatient.Sex;
                txtSiteName.Text = matchingPatient.SiteName;
                txtSurname.Text = matchingPatient.Surname;

                _bioFinger1 = matchingPatient.BioDataFingerPrimary;
                _bioFinger2 = matchingPatient.BioDataFingerSecondary;
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

        private void buttonFind_Click(object sender, EventArgs e)
        {
            gradientPanel2.Visible = false;
        }
    }
}
