using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class DataCentral
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCentral));
            this.lstBoxInfoLog = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPatientManagement = new System.Windows.Forms.Button();
            this.btnDispensationHistory = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tmrPostInfoLogs = new System.Windows.Forms.Timer(this.components);
            this.tmrSync = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.picConnectionAvailable = new System.Windows.Forms.PictureBox();
            this.picSyncInProcess = new System.Windows.Forms.PictureBox();
            this.picIndUpdate = new System.Windows.Forms.PictureBox();
            this.lblUserInformation = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.tmrCheckConnection = new System.Windows.Forms.Timer(this.components);
            this.tmrLaunchUpdate = new System.Windows.Forms.Timer(this.components);
            this.bgwUpdatePatient = new System.ComponentModel.BackgroundWorker();
            this.bgwNewPatient = new System.ComponentModel.BackgroundWorker();
            this.btnCancelSync = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.picDataReady = new System.Windows.Forms.PictureBox();
            this.picDataWait = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIndUpdate)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDataReady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataWait)).BeginInit();
            this.SuspendLayout();
            // 
            // lstBoxInfoLog
            // 
            this.lstBoxInfoLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBoxInfoLog.ForeColor = System.Drawing.Color.Black;
            this.lstBoxInfoLog.FormattingEnabled = true;
            this.lstBoxInfoLog.Location = new System.Drawing.Point(20, 295);
            this.lstBoxInfoLog.Name = "lstBoxInfoLog";
            this.lstBoxInfoLog.ScrollAlwaysVisible = true;
            this.lstBoxInfoLog.Size = new System.Drawing.Size(557, 106);
            this.lstBoxInfoLog.TabIndex = 12;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnPatientManagement);
            this.flowLayoutPanel1.Controls.Add(this.btnDispensationHistory);
            this.flowLayoutPanel1.Controls.Add(this.btnSchedule);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 63);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(555, 211);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // btnPatientManagement
            // 
            this.btnPatientManagement.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnPatientManagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPatientManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPatientManagement.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPatientManagement.FlatAppearance.BorderSize = 2;
            this.btnPatientManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientManagement.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientManagement.ForeColor = System.Drawing.Color.White;
            this.btnPatientManagement.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Counselor_96px;
            this.btnPatientManagement.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPatientManagement.Location = new System.Drawing.Point(3, 3);
            this.btnPatientManagement.Name = "btnPatientManagement";
            this.btnPatientManagement.Size = new System.Drawing.Size(106, 195);
            this.btnPatientManagement.TabIndex = 2;
            this.btnPatientManagement.Text = "Information Management\r\n\r\n";
            this.btnPatientManagement.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPatientManagement.UseVisualStyleBackColor = true;
            this.btnPatientManagement.Click += new System.EventHandler(this.btnPatientManagement_Click);
            // 
            // btnDispensationHistory
            // 
            this.btnDispensationHistory.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnDispensationHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDispensationHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDispensationHistory.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDispensationHistory.FlatAppearance.BorderSize = 2;
            this.btnDispensationHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDispensationHistory.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDispensationHistory.ForeColor = System.Drawing.Color.White;
            this.btnDispensationHistory.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Pill_Bottle_96px;
            this.btnDispensationHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDispensationHistory.Location = new System.Drawing.Point(115, 3);
            this.btnDispensationHistory.Name = "btnDispensationHistory";
            this.btnDispensationHistory.Size = new System.Drawing.Size(106, 195);
            this.btnDispensationHistory.TabIndex = 3;
            this.btnDispensationHistory.Text = "Consultation and Dispensation Register\r\n\r\n";
            this.btnDispensationHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDispensationHistory.UseVisualStyleBackColor = true;
            // 
            // btnSchedule
            // 
            this.btnSchedule.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSchedule.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSchedule.FlatAppearance.BorderSize = 2;
            this.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSchedule.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSchedule.ForeColor = System.Drawing.Color.White;
            this.btnSchedule.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Calendar_96px;
            this.btnSchedule.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSchedule.Location = new System.Drawing.Point(227, 3);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(106, 195);
            this.btnSchedule.TabIndex = 4;
            this.btnSchedule.Text = "Appointments and Messaging \r\n";
            this.btnSchedule.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSchedule.UseVisualStyleBackColor = true;
            // 
            // btnSync
            // 
            this.btnSync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSync.BackColor = System.Drawing.Color.White;
            this.btnSync.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnSync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSync.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.ForeColor = System.Drawing.Color.White;
            this.btnSync.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnSync.Location = new System.Drawing.Point(98, 3);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(41, 41);
            this.btnSync.TabIndex = 16;
            this.btnSync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSync.UseVisualStyleBackColor = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // tmrPostInfoLogs
            // 
            this.tmrPostInfoLogs.Enabled = true;
            this.tmrPostInfoLogs.Interval = 1000;
            this.tmrPostInfoLogs.Tick += new System.EventHandler(this.tmrPostInfoLogs_Tick);
            // 
            // tmrSync
            // 
            this.tmrSync.Enabled = true;
            this.tmrSync.Interval = 60000;
            this.tmrSync.Tag = "";
            this.tmrSync.Tick += new System.EventHandler(this.tmrSync_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.VOnwNgeg;
            this.pictureBox1.Location = new System.Drawing.Point(584, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 338);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Info_16px;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(17, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Information and Logs";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnProfile
            // 
            this.btnProfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProfile.BackColor = System.Drawing.Color.White;
            this.btnProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProfile.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Person_24px;
            this.btnProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.Location = new System.Drawing.Point(145, 3);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(106, 41);
            this.btnProfile.TabIndex = 7;
            this.btnProfile.Text = "Profile";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Visible = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(257, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 41);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.panel2.Controls.Add(this.flowLayoutPanel2);
            this.panel2.Controls.Add(this.lblUserInformation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 409);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(782, 38);
            this.panel2.TabIndex = 11;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.picConnectionAvailable);
            this.flowLayoutPanel2.Controls.Add(this.picSyncInProcess);
            this.flowLayoutPanel2.Controls.Add(this.picIndUpdate);
            this.flowLayoutPanel2.Controls.Add(this.picDataReady);
            this.flowLayoutPanel2.Controls.Add(this.picDataWait);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(564, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(215, 32);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // picConnectionAvailable
            // 
            this.picConnectionAvailable.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Server_20px;
            this.picConnectionAvailable.Location = new System.Drawing.Point(188, 3);
            this.picConnectionAvailable.Name = "picConnectionAvailable";
            this.picConnectionAvailable.Size = new System.Drawing.Size(24, 24);
            this.picConnectionAvailable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picConnectionAvailable.TabIndex = 11;
            this.picConnectionAvailable.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picConnectionAvailable, "Connection to Server Available");
            this.picConnectionAvailable.Visible = false;
            // 
            // picSyncInProcess
            // 
            this.picSyncInProcess.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.picSyncInProcess.Location = new System.Drawing.Point(158, 3);
            this.picSyncInProcess.Name = "picSyncInProcess";
            this.picSyncInProcess.Size = new System.Drawing.Size(24, 24);
            this.picSyncInProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSyncInProcess.TabIndex = 12;
            this.picSyncInProcess.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picSyncInProcess, "Data Synchronization in Progress");
            this.picSyncInProcess.Visible = false;
            // 
            // picIndUpdate
            // 
            this.picIndUpdate.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Download_20px;
            this.picIndUpdate.Location = new System.Drawing.Point(128, 3);
            this.picIndUpdate.Name = "picIndUpdate";
            this.picIndUpdate.Size = new System.Drawing.Size(24, 24);
            this.picIndUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIndUpdate.TabIndex = 13;
            this.picIndUpdate.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picIndUpdate, "System Update in Progress");
            this.picIndUpdate.Visible = false;
            // 
            // lblUserInformation
            // 
            this.lblUserInformation.AutoSize = true;
            this.lblUserInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblUserInformation.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserInformation.ForeColor = System.Drawing.Color.White;
            this.lblUserInformation.Location = new System.Drawing.Point(3, 13);
            this.lblUserInformation.Name = "lblUserInformation";
            this.lblUserInformation.Size = new System.Drawing.Size(0, 15);
            this.lblUserInformation.TabIndex = 9;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tmrCheckConnection
            // 
            this.tmrCheckConnection.Enabled = true;
            this.tmrCheckConnection.Interval = 5000;
            this.tmrCheckConnection.Tag = "";
            this.tmrCheckConnection.Tick += new System.EventHandler(this.tmrCheckConnection_Tick);
            // 
            // tmrLaunchUpdate
            // 
            this.tmrLaunchUpdate.Interval = 5000;
            this.tmrLaunchUpdate.Tag = "";
            this.tmrLaunchUpdate.Tick += new System.EventHandler(this.tmrLaunchUpdate_Tick);
            // 
            // bgwUpdatePatient
            // 
            this.bgwUpdatePatient.WorkerReportsProgress = true;
            this.bgwUpdatePatient.WorkerSupportsCancellation = true;
            this.bgwUpdatePatient.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdatePatient_DoWork);
            // 
            // bgwNewPatient
            // 
            this.bgwNewPatient.WorkerReportsProgress = true;
            this.bgwNewPatient.WorkerSupportsCancellation = true;
            this.bgwNewPatient.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwNewPatient_DoWork);
            // 
            // btnCancelSync
            // 
            this.btnCancelSync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelSync.BackColor = System.Drawing.Color.Maroon;
            this.btnCancelSync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelSync.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelSync.ForeColor = System.Drawing.Color.White;
            this.btnCancelSync.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnCancelSync.Location = new System.Drawing.Point(51, 3);
            this.btnCancelSync.Name = "btnCancelSync";
            this.btnCancelSync.Size = new System.Drawing.Size(41, 41);
            this.btnCancelSync.TabIndex = 17;
            this.btnCancelSync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelSync.UseVisualStyleBackColor = false;
            this.btnCancelSync.Visible = false;
            this.btnCancelSync.Click += new System.EventHandler(this.btnCancelSync_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnClose);
            this.flowLayoutPanel3.Controls.Add(this.btnProfile);
            this.flowLayoutPanel3.Controls.Add(this.btnSync);
            this.flowLayoutPanel3.Controls.Add(this.btnCancelSync);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(436, 10);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(366, 47);
            this.flowLayoutPanel3.TabIndex = 18;
            // 
            // picDataReady
            // 
            this.picDataReady.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Checked_Checkbox_24px;
            this.picDataReady.Location = new System.Drawing.Point(98, 3);
            this.picDataReady.Name = "picDataReady";
            this.picDataReady.Size = new System.Drawing.Size(24, 24);
            this.picDataReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDataReady.TabIndex = 14;
            this.picDataReady.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picDataReady, "Information Data Management is Ready");
            this.picDataReady.Visible = false;
            // 
            // picDataWait
            // 
            this.picDataWait.Image = global::PatientDataAdministration.Client.Properties.Resources._800px_COLOURBOX18331728;
            this.picDataWait.Location = new System.Drawing.Point(68, 3);
            this.picDataWait.Name = "picDataWait";
            this.picDataWait.Size = new System.Drawing.Size(24, 24);
            this.picDataWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDataWait.TabIndex = 15;
            this.picDataWait.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picDataWait, "Loading Patient Data. You may need to Wait");
            this.picDataWait.Visible = false;
            // 
            // DataCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 467);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstBoxInfoLog);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataCentral";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Text = "APIN Patient Data Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataCentral_FormClosing);
            this.Load += new System.EventHandler(this.DataCentral_Load);
            this.Shown += new System.EventHandler(this.DataCentral_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIndUpdate)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDataReady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPatientManagement;
        private System.Windows.Forms.Button btnDispensationHistory;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblUserInformation;
        private System.Windows.Forms.ListBox lstBoxInfoLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox picConnectionAvailable;
        private System.Windows.Forms.PictureBox picSyncInProcess;
        private System.Windows.Forms.PictureBox picIndUpdate;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Timer tmrPostInfoLogs;
        private System.Windows.Forms.Timer tmrSync;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Timer tmrCheckConnection;
        private System.Windows.Forms.Timer tmrLaunchUpdate;
        private System.Windows.Forms.Button btnSync;
        private System.ComponentModel.BackgroundWorker bgwUpdatePatient;
        private System.ComponentModel.BackgroundWorker bgwNewPatient;
        private System.Windows.Forms.Button btnCancelSync;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.PictureBox picDataReady;
        private System.Windows.Forms.PictureBox picDataWait;
    }
}