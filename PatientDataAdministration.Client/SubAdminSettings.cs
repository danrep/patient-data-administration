using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Codesistance.NFC;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Client
{
    public partial class SubAdminSettings : MetroFramework.Forms.MetroForm
    {
        private readonly Administration_StaffInformation _administrationStaffInformation;

        private string _selectedReader;
        private string _nfcUid;
        private SCardReader _reader;
        private SCardChannel _cardchannel = null;
        private NfcTag _tag = null;
        private Thread _cardthread;
        private bool _inLoop = false;

        private string _messageText = "";
        private Color _messageColor;

        public SubAdminSettings(Administration_StaffInformation administrationStaffInformation)
        {
            InitializeComponent();
            _administrationStaffInformation = administrationStaffInformation;
            LoadReaderList();
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
                            _messageText = @"NFC Device Connected";
                            _messageColor = Color.Green;
                            _selectedReader = reader;
                            break;
                        }
                        else
                        {
                            _messageText = @"Not a Valid NFC Device will Try another";
                            _messageColor = Color.DarkOrange;
                            _selectedReader = null;
                            continue;
                        }
                    }
                }
                else
                {
                    _messageText = @"No NFC Device Connected";
                    _messageColor = Color.Maroon;
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
                    _messageText = @"The reader we were working has gone AWOL from the system.";
                    _messageColor = Color.DarkOrange;
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

                    _messageText = @"No Card Detected. NFC Device Connected";
                    _messageColor = Color.SteelBlue;
                    _nfcUid = "";
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
                            
                            _cardthread = new Thread(Card_read_proc);
                            _cardthread.Start();

                            _messageText = @"Card Connected. UID: " + _nfcUid;
                            _messageColor = Color.Green;
                        }
                        else
                        {
                            _messageText = @"NearField failed to connect to the card in the reader";
                            _messageColor = Color.Maroon;

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

        private void btnFormatNfc_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_tag.Content.Count() != 0)
                {
                    var dialogResult = MessageBox.Show(
                        $@"This Tag has data on it. Do you want to continue with the Formatting? All Data will be lost!", @"Please Decide",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult != DialogResult.Yes)
                        return;
                }

                if (DetachFromPatient(_nfcUid))
                {
                    LocalCore.LogAudit("Formatted Tag with TAGUID: " + _nfcUid, true);
                    Card_format_proc();
                }
                else
                {
                    MessageBox.Show(@"This Tag cannot be formatted at this time. A connection to the server is required to achieve this.");
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private bool DetachFromPatient(string tagUid)
        {
            try
            {
                // frist detach patient from server then local

                if (!LocalCore
                    .Get(
                        $@"/ClientCommunication/Sync/DetatchPatientTag?tagUid={tagUid}&userId={
                                _administrationStaffInformation.Id
                            }").Result.Status) return false;

                using (var entity = new LocalPDAEntities())
                {
                    var matchingPatients = entity.System_BioDataStore.Where(x => x.NfcUid == tagUid);
                    foreach (var matchingPatient in matchingPatients)
                    {
                        matchingPatient.NfcUid = string.Empty;
                    }

                    entity.Entry(matchingPatients).State = EntityState.Modified;
                    entity.SaveChanges();
                }

                return true;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                return false;
            }
        }

        private void Card_format_proc()
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

        delegate void OnTagFormatInvoker(NfcTag _tag);
        void OnTagFormat(NfcTag _tag)
        {
            try
            {
                Card_read_proc();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
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

        delegate void OnTagReadInvoker(NfcTag tag);
        void OnTagRead(NfcTag tag)
        {
            try
            {
                this._tag = tag;

                if (this._tag == null)
                {
                    _messageText = @"Bad/Unreadable Tag";
                    _messageColor = Color.Maroon;
                    return;
                }

                if ((this._tag.Content != null) && (this._tag.Content.Count != 0))
                {
                    var ndef = this._tag.Content.FirstOrDefault();
                    var smart = (RtdVCard)ndef;
                    _messageText = smart?.Nickname;
                }
                else
                    _messageText = @"This Tag is Empty. You can proceed with attaching a Patient to it!";

                _messageColor = Color.Green;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);

                if (this._tag == null)
                    return;

                _messageText = @"This Tag is not empty, but its contant is not recognised";
                _messageColor = Color.Maroon;
            }
        }

        delegate void OnErrorInvoker(string text, string caption);
        void OnError(string text, string caption)
        {
            _messageText = text;
            _messageColor = Color.DarkOrange;
        }

        private void tmrInformation_Tick(object sender, EventArgs e)
        {
            if (lblInformation.Text == _messageText)
                return;

            lblInformation.Text = _messageText;
            lblInformation.ForeColor = _messageColor;
            Application.DoEvents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
