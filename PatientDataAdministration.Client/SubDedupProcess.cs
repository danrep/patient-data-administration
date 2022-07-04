using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Client
{
    public partial class SubDedupProcess : MetroFramework.Forms.MetroForm
    {
        readonly int _userId;
        readonly string _operationGuid;
        bool _statusPolling = true;

        public DialogResult DialogResultMessage;
        private ResponseData _response;
        private Thread _thread;
        private delegate void UpdateListBox(List<DuplicationSuspect> suspects);
        private DateTime _operationStart;

        public SubDedupProcess(int userId, string operationGuid)
        {
            try
            {
                _userId = userId;
                _operationGuid = operationGuid;

                DialogResultMessage = DialogResult.Cancel;

                InitializeComponent();
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userId);
            }
        }

        private void SubDedupProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResultMessage == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

            _thread.Abort();
        }

        private void SubDedupProcess_Shown(object sender, EventArgs e)
        {
            try
            {
                _operationStart = DateTime.Now;

                _thread = new Thread(()=> {
                    while (_statusPolling)
                    {
                        _response = LocalCore.Get($"/ClientCommunication/InstantBioCheck/CheckOperationStatus?operationGuid={_operationGuid}").Result;
                        Thread.Sleep(5000);
                    }
                });

                _thread.Start();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userId);
            }
        }

        private void btnAcceptProcess_Click(object sender, EventArgs e)
        {
            DialogResultMessage = DialogResult.Abort;
            CloseOperation();
        }

        private void CloseOperation()
        {
            this.Close();
        }

        private void btnDenyReg_Click(object sender, EventArgs e)
        {
            DialogResultMessage = DialogResult.No;
            CloseOperation();
        }

        private void btnAbortProcess_Click(object sender, EventArgs e)
        {
            DialogResultMessage = DialogResult.Yes;
            CloseOperation();
        }

        private void tmrResponse_Tick(object sender, EventArgs e)
        {
            tmrResponse.Enabled = false;

            try
            {
                if (_response.Data == null)
                    return;

                var _dedupResponse = JsonConvert.DeserializeObject<InstantDudupModel>(_response.Data.ToString());

                lblProcessIndicator.Text = _dedupResponse.ProcessingStatus.DisplayName();
                switch (_dedupResponse.ProcessingStatus)
                {
                    case EnumLibrary.ProcessingStatus.Submitted:
                        lblProcessIndicator.BackColor = Color.SteelBlue;
                        rtbInfoWindow.Text = "Biometric Data has been Submitted. If process does not change in 5 minutes, please ABORT and try again.\n";
                        rtbInfoWindow.Text = $"{DateTime.Now.Subtract(_operationStart):mm:ss}: Elapsed";
                        break;
                    case EnumLibrary.ProcessingStatus.Processing:
                        lblProcessIndicator.BackColor = Color.DarkOrange;
                        rtbInfoWindow.Text = $"{_operationGuid}: Processing\n";
                        rtbInfoWindow.Text = $"{DateTime.Now.Subtract(_operationStart):mm:ss}: Elapsed";
                        break;
                    case EnumLibrary.ProcessingStatus.Completed:
                        lblProcessIndicator.BackColor = Color.DarkGreen;

                        _statusPolling = lblProcessGraphic.Visible = btnAbortProcess.Visible = false;
                        btnAcceptReg.Visible = btnDenyReg.Visible = true;

                        rtbInfoWindow.Text = $"{_operationGuid}: ALL DONE\n";
                        rtbInfoWindow.Text = $"Duration: {DateTime.Now.Subtract(_operationStart):mm:ss}";

                        UpdateListBox updateListBox = new UpdateListBox(UpdateListBoxItems);
                        updateListBox(_dedupResponse.DuplicationSuspects);

                        rtbInfoWindow.Text = $"{DateTime.Now.ToLongDateString()}: Found {_dedupResponse.DuplicationSuspects.Count} Suspects.\n";
                        return;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userId);
            }

            tmrResponse.Enabled = true;
        }

        private void SubDedupProcess_Load(object sender, EventArgs e)
        {

        }

        private void UpdateListBoxItems(List<DuplicationSuspect> suspects)
        {
            if (suspects.Any())
            {
                foreach (var suspect in suspects)
                {
                    lstBoxSuspects.Items.Add($"{suspect.PepId} | {suspect.MatchScore / 100 }%");
                }
                Application.DoEvents();
            }
            else
                lstBoxSuspects.Items.Add($"No Suspects Found");
        }

        private void lstBoxSuspects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userId);
            }
        }

        private void lstBoxSuspects_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxSuspects.SelectedIndex <= -1)
                    return;

                var pepId = lstBoxSuspects.SelectedItem.ToString().Split('|')[0].Trim();

                if (string.IsNullOrEmpty(pepId))
                    return;

                var data = JsonConvert.DeserializeObject<InstantDudupModel>(_response.Data.ToString()).DuplicationSuspects.FirstOrDefault(x => x.PepId == pepId);

                rtbInfoWindow.Text = "Suspected Patient Details\n";
                rtbInfoWindow.Text += "\n";
                rtbInfoWindow.Text += JsonConvert.DeserializeObject(data.Data.ToString());

            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userId);
            }
        }
    }
}
