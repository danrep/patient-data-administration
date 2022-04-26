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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpSyncController = new System.Windows.Forms.GroupBox();
            this.chkEndPointExecutionControl = new System.Windows.Forms.CheckedListBox();
            this.btnPatientManagement = new System.Windows.Forms.Button();
            this.btnPopulationStatusRegister = new System.Windows.Forms.Button();
            this.btnAdministratorSettings = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tmrPostInfoLogs = new System.Windows.Forms.Timer(this.components);
            this.tmrSync = new System.Windows.Forms.Timer(this.components);
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.picConnectionAvailable = new System.Windows.Forms.PictureBox();
            this.picSyncInProcess = new System.Windows.Forms.PictureBox();
            this.picIndUpdate = new System.Windows.Forms.PictureBox();
            this.picDataReady = new System.Windows.Forms.PictureBox();
            this.picDataWait = new System.Windows.Forms.PictureBox();
            this.lblUserInformation = new System.Windows.Forms.Label();
            this.metroToolTip = new MetroFramework.Components.MetroToolTip();
            this.tmrCheckConnection = new System.Windows.Forms.Timer(this.components);
            this.tmrLaunchUpdate = new System.Windows.Forms.Timer(this.components);
            this.bgwUpdatePatient = new System.ComponentModel.BackgroundWorker();
            this.bgwNewPatient = new System.ComponentModel.BackgroundWorker();
            this.btnCancelSync = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearDump = new System.Windows.Forms.Button();
            this.btnTogglePushPull = new System.Windows.Forms.Button();
            this.btnForceUpdate = new System.Windows.Forms.Button();
            this.bgwPing = new System.ComponentModel.BackgroundWorker();
            this.bgwFreshPatient = new System.ComponentModel.BackgroundWorker();
            this.tmrFreshPatient = new System.Windows.Forms.Timer(this.components);
            this.bgwContentManager = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstBoxInfoLog = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnAdminSettings = new System.Windows.Forms.Button();
            this.tmrEndPointExecutionEffect = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.grpSyncController.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIndUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataReady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataWait)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.grpSyncController);
            this.flowLayoutPanel1.Controls.Add(this.btnPatientManagement);
            this.flowLayoutPanel1.Controls.Add(this.btnPopulationStatusRegister);
            this.flowLayoutPanel1.Controls.Add(this.btnAdministratorSettings);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 70);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(746, 203);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // grpSyncController
            // 
            this.grpSyncController.Controls.Add(this.chkEndPointExecutionControl);
            this.grpSyncController.Location = new System.Drawing.Point(3, 3);
            this.grpSyncController.Name = "grpSyncController";
            this.grpSyncController.Size = new System.Drawing.Size(400, 195);
            this.grpSyncController.TabIndex = 7;
            this.grpSyncController.TabStop = false;
            this.grpSyncController.Text = "End Point Execution Control";
            this.grpSyncController.Visible = false;
            // 
            // chkEndPointExecutionControl
            // 
            this.chkEndPointExecutionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEndPointExecutionControl.FormattingEnabled = true;
            this.chkEndPointExecutionControl.Location = new System.Drawing.Point(3, 16);
            this.chkEndPointExecutionControl.Name = "chkEndPointExecutionControl";
            this.chkEndPointExecutionControl.Size = new System.Drawing.Size(394, 176);
            this.chkEndPointExecutionControl.TabIndex = 0;
            // 
            // btnPatientManagement
            // 
            this.btnPatientManagement.BackColor = System.Drawing.Color.Black;
            this.btnPatientManagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPatientManagement.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPatientManagement.FlatAppearance.BorderSize = 2;
            this.btnPatientManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientManagement.ForeColor = System.Drawing.Color.White;
            this.btnPatientManagement.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Counselor_96px;
            this.btnPatientManagement.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPatientManagement.Location = new System.Drawing.Point(409, 3);
            this.btnPatientManagement.Name = "btnPatientManagement";
            this.btnPatientManagement.Size = new System.Drawing.Size(106, 195);
            this.btnPatientManagement.TabIndex = 2;
            this.btnPatientManagement.Text = "Patient Information Management\r\n\r\n";
            this.btnPatientManagement.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPatientManagement.UseVisualStyleBackColor = false;
            this.btnPatientManagement.Click += new System.EventHandler(this.btnPatientManagement_Click);
            // 
            // btnPopulationStatusRegister
            // 
            this.btnPopulationStatusRegister.BackColor = System.Drawing.Color.Black;
            this.btnPopulationStatusRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPopulationStatusRegister.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPopulationStatusRegister.FlatAppearance.BorderSize = 2;
            this.btnPopulationStatusRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPopulationStatusRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPopulationStatusRegister.ForeColor = System.Drawing.Color.White;
            this.btnPopulationStatusRegister.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_AIDS_Ribbon_96px;
            this.btnPopulationStatusRegister.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPopulationStatusRegister.Location = new System.Drawing.Point(521, 3);
            this.btnPopulationStatusRegister.Name = "btnPopulationStatusRegister";
            this.btnPopulationStatusRegister.Size = new System.Drawing.Size(106, 195);
            this.btnPopulationStatusRegister.TabIndex = 5;
            this.btnPopulationStatusRegister.Text = "HIV Testing && Counselling Register\r\n \r\n";
            this.btnPopulationStatusRegister.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPopulationStatusRegister.UseVisualStyleBackColor = false;
            this.btnPopulationStatusRegister.Click += new System.EventHandler(this.btnPopulationStatusRegister_Click);
            // 
            // btnAdministratorSettings
            // 
            this.btnAdministratorSettings.BackColor = System.Drawing.Color.White;
            this.btnAdministratorSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdministratorSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAdministratorSettings.FlatAppearance.BorderSize = 2;
            this.btnAdministratorSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdministratorSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdministratorSettings.ForeColor = System.Drawing.Color.Black;
            this.btnAdministratorSettings.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Protect_96px;
            this.btnAdministratorSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdministratorSettings.Location = new System.Drawing.Point(633, 3);
            this.btnAdministratorSettings.Name = "btnAdministratorSettings";
            this.btnAdministratorSettings.Size = new System.Drawing.Size(106, 195);
            this.btnAdministratorSettings.TabIndex = 6;
            this.btnAdministratorSettings.Text = "Admin Mode\r\n \r\n";
            this.btnAdministratorSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdministratorSettings.UseVisualStyleBackColor = false;
            this.btnAdministratorSettings.Click += new System.EventHandler(this.btnAdministratorSettings_Click);
            // 
            // btnSync
            // 
            this.btnSync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSync.BackColor = System.Drawing.Color.Silver;
            this.btnSync.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.ForeColor = System.Drawing.Color.White;
            this.btnSync.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnSync.Location = new System.Drawing.Point(384, 3);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(40, 40);
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
            // btnProfile
            // 
            this.btnProfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProfile.BackColor = System.Drawing.Color.Silver;
            this.btnProfile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Person_24px;
            this.btnProfile.Location = new System.Drawing.Point(430, 3);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(40, 40);
            this.btnProfile.TabIndex = 7;
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Visible = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(476, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 6;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.flowLayoutPanel2);
            this.panel2.Controls.Add(this.lblUserInformation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 390);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(748, 38);
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(521, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(222, 32);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // picConnectionAvailable
            // 
            this.picConnectionAvailable.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Server_20px;
            this.picConnectionAvailable.Location = new System.Drawing.Point(195, 3);
            this.picConnectionAvailable.Name = "picConnectionAvailable";
            this.picConnectionAvailable.Size = new System.Drawing.Size(24, 24);
            this.picConnectionAvailable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picConnectionAvailable.TabIndex = 11;
            this.picConnectionAvailable.TabStop = false;
            this.metroToolTip.SetToolTip(this.picConnectionAvailable, "Connection to Server Available");
            this.picConnectionAvailable.Visible = false;
            // 
            // picSyncInProcess
            // 
            this.picSyncInProcess.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.picSyncInProcess.Location = new System.Drawing.Point(165, 3);
            this.picSyncInProcess.Name = "picSyncInProcess";
            this.picSyncInProcess.Size = new System.Drawing.Size(24, 24);
            this.picSyncInProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSyncInProcess.TabIndex = 12;
            this.picSyncInProcess.TabStop = false;
            this.metroToolTip.SetToolTip(this.picSyncInProcess, "Data Synchronization in Progress");
            this.picSyncInProcess.Visible = false;
            // 
            // picIndUpdate
            // 
            this.picIndUpdate.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Download_20px;
            this.picIndUpdate.Location = new System.Drawing.Point(135, 3);
            this.picIndUpdate.Name = "picIndUpdate";
            this.picIndUpdate.Size = new System.Drawing.Size(24, 24);
            this.picIndUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIndUpdate.TabIndex = 13;
            this.picIndUpdate.TabStop = false;
            this.metroToolTip.SetToolTip(this.picIndUpdate, "System Update in Progress");
            this.picIndUpdate.Visible = false;
            // 
            // picDataReady
            // 
            this.picDataReady.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Checked_Checkbox_24px;
            this.picDataReady.Location = new System.Drawing.Point(105, 3);
            this.picDataReady.Name = "picDataReady";
            this.picDataReady.Size = new System.Drawing.Size(24, 24);
            this.picDataReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDataReady.TabIndex = 14;
            this.picDataReady.TabStop = false;
            this.metroToolTip.SetToolTip(this.picDataReady, "Information Data Management is Ready");
            this.picDataReady.Visible = false;
            // 
            // picDataWait
            // 
            this.picDataWait.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.picDataWait.Location = new System.Drawing.Point(75, 3);
            this.picDataWait.Name = "picDataWait";
            this.picDataWait.Size = new System.Drawing.Size(24, 24);
            this.picDataWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDataWait.TabIndex = 15;
            this.picDataWait.TabStop = false;
            this.metroToolTip.SetToolTip(this.picDataWait, "Loading Patient Data. You may need to Wait");
            this.picDataWait.Visible = false;
            // 
            // lblUserInformation
            // 
            this.lblUserInformation.AutoSize = true;
            this.lblUserInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblUserInformation.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserInformation.ForeColor = System.Drawing.Color.White;
            this.lblUserInformation.Location = new System.Drawing.Point(3, 13);
            this.lblUserInformation.Name = "lblUserInformation";
            this.lblUserInformation.Size = new System.Drawing.Size(0, 13);
            this.lblUserInformation.TabIndex = 9;
            // 
            // metroToolTip
            // 
            this.metroToolTip.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.bgwUpdatePatient.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdatePatient_RunWorkerCompleted);
            // 
            // bgwNewPatient
            // 
            this.bgwNewPatient.WorkerReportsProgress = true;
            this.bgwNewPatient.WorkerSupportsCancellation = true;
            this.bgwNewPatient.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwNewPatient_DoWork);
            this.bgwNewPatient.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwNewPatient_RunWorkerCompleted);
            // 
            // btnCancelSync
            // 
            this.btnCancelSync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelSync.BackColor = System.Drawing.Color.Black;
            this.btnCancelSync.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancelSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnCancelSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelSync.ForeColor = System.Drawing.Color.White;
            this.btnCancelSync.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnCancelSync.Location = new System.Drawing.Point(200, 3);
            this.btnCancelSync.Name = "btnCancelSync";
            this.btnCancelSync.Size = new System.Drawing.Size(40, 40);
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
            this.flowLayoutPanel3.Controls.Add(this.btnClearDump);
            this.flowLayoutPanel3.Controls.Add(this.btnTogglePushPull);
            this.flowLayoutPanel3.Controls.Add(this.btnForceUpdate);
            this.flowLayoutPanel3.Controls.Add(this.btnCancelSync);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(247, 17);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(519, 47);
            this.flowLayoutPanel3.TabIndex = 18;
            // 
            // btnClearDump
            // 
            this.btnClearDump.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearDump.BackColor = System.Drawing.Color.Silver;
            this.btnClearDump.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClearDump.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnClearDump.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDump.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDump.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Page_24px;
            this.btnClearDump.Location = new System.Drawing.Point(338, 3);
            this.btnClearDump.Name = "btnClearDump";
            this.btnClearDump.Size = new System.Drawing.Size(40, 40);
            this.btnClearDump.TabIndex = 18;
            this.btnClearDump.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearDump.UseVisualStyleBackColor = false;
            this.btnClearDump.Click += new System.EventHandler(this.btnClearDump_Click);
            // 
            // btnTogglePushPull
            // 
            this.btnTogglePushPull.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTogglePushPull.BackColor = System.Drawing.Color.Silver;
            this.btnTogglePushPull.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTogglePushPull.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnTogglePushPull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePushPull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTogglePushPull.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Todo_List_24px;
            this.btnTogglePushPull.Location = new System.Drawing.Point(292, 3);
            this.btnTogglePushPull.Name = "btnTogglePushPull";
            this.btnTogglePushPull.Size = new System.Drawing.Size(40, 40);
            this.btnTogglePushPull.TabIndex = 19;
            this.btnTogglePushPull.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTogglePushPull.UseVisualStyleBackColor = false;
            this.btnTogglePushPull.Click += new System.EventHandler(this.btnTogglePushPull_Click);
            // 
            // btnForceUpdate
            // 
            this.btnForceUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnForceUpdate.BackColor = System.Drawing.Color.Silver;
            this.btnForceUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnForceUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnForceUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForceUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForceUpdate.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Download_From_FTP_24px;
            this.btnForceUpdate.Location = new System.Drawing.Point(246, 3);
            this.btnForceUpdate.Name = "btnForceUpdate";
            this.btnForceUpdate.Size = new System.Drawing.Size(40, 40);
            this.btnForceUpdate.TabIndex = 20;
            this.btnForceUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnForceUpdate.UseVisualStyleBackColor = false;
            this.btnForceUpdate.Click += new System.EventHandler(this.btnForceUpdate_Click);
            // 
            // bgwPing
            // 
            this.bgwPing.WorkerReportsProgress = true;
            this.bgwPing.WorkerSupportsCancellation = true;
            this.bgwPing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPing_DoWork);
            // 
            // bgwFreshPatient
            // 
            this.bgwFreshPatient.WorkerReportsProgress = true;
            this.bgwFreshPatient.WorkerSupportsCancellation = true;
            this.bgwFreshPatient.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwFreshPatient_DoWork);
            // 
            // tmrFreshPatient
            // 
            this.tmrFreshPatient.Enabled = true;
            this.tmrFreshPatient.Interval = 60000;
            this.tmrFreshPatient.Tag = "";
            this.tmrFreshPatient.Tick += new System.EventHandler(this.tmrFreshPatient_Tick);
            // 
            // bgwContentManager
            // 
            this.bgwContentManager.WorkerReportsProgress = true;
            this.bgwContentManager.WorkerSupportsCancellation = true;
            this.bgwContentManager.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwContentManager_DoWork);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstBoxInfoLog);
            this.groupBox1.Location = new System.Drawing.Point(20, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 104);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs and Information";
            // 
            // lstBoxInfoLog
            // 
            this.lstBoxInfoLog.FormattingEnabled = true;
            this.lstBoxInfoLog.Location = new System.Drawing.Point(6, 19);
            this.lstBoxInfoLog.Name = "lstBoxInfoLog";
            this.lstBoxInfoLog.Size = new System.Drawing.Size(736, 82);
            this.lstBoxInfoLog.TabIndex = 0;
            // 
            // btnAdminSettings
            // 
            this.btnAdminSettings.BackColor = System.Drawing.Color.White;
            this.btnAdminSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdminSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAdminSettings.FlatAppearance.BorderSize = 2;
            this.btnAdminSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminSettings.ForeColor = System.Drawing.Color.Black;
            this.btnAdminSettings.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Protect_96px;
            this.btnAdminSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdminSettings.Location = new System.Drawing.Point(227, 3);
            this.btnAdminSettings.Name = "btnAdminSettings";
            this.btnAdminSettings.Size = new System.Drawing.Size(106, 195);
            this.btnAdminSettings.TabIndex = 6;
            this.btnAdminSettings.Text = "Admin Mode\r\n \r\n";
            this.btnAdminSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdminSettings.UseVisualStyleBackColor = false;
            // 
            // tmrEndPointExecutionEffect
            // 
            this.tmrEndPointExecutionEffect.Enabled = true;
            this.tmrEndPointExecutionEffect.Interval = 500;
            this.tmrEndPointExecutionEffect.Tick += new System.EventHandler(this.tmrEndPointExecutionEffect_Tick);
            // 
            // DataCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 448);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataCentral";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Text = "Data Central";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataCentral_FormClosing);
            this.Load += new System.EventHandler(this.DataCentral_Load);
            this.Shown += new System.EventHandler(this.DataCentral_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.grpSyncController.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIndUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataReady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataWait)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPatientManagement;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblUserInformation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox picConnectionAvailable;
        private System.Windows.Forms.PictureBox picSyncInProcess;
        private System.Windows.Forms.PictureBox picIndUpdate;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Timer tmrPostInfoLogs;
        private System.Windows.Forms.Timer tmrSync;
        private MetroFramework.Components.MetroToolTip metroToolTip;
        private System.Windows.Forms.Timer tmrCheckConnection;
        private System.Windows.Forms.Timer tmrLaunchUpdate;
        private System.Windows.Forms.Button btnSync;
        private System.ComponentModel.BackgroundWorker bgwUpdatePatient;
        private System.ComponentModel.BackgroundWorker bgwNewPatient;
        private System.Windows.Forms.Button btnCancelSync;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.PictureBox picDataReady;
        private System.Windows.Forms.PictureBox picDataWait;
        private System.ComponentModel.BackgroundWorker bgwPing;
        private System.ComponentModel.BackgroundWorker bgwFreshPatient;
        private System.Windows.Forms.Timer tmrFreshPatient;
        private System.ComponentModel.BackgroundWorker bgwContentManager;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPopulationStatusRegister;
        private System.Windows.Forms.Button btnClearDump;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnAdministratorSettings;
        private System.Windows.Forms.Button btnAdminSettings;
        private System.Windows.Forms.GroupBox grpSyncController;
        private System.Windows.Forms.CheckedListBox chkEndPointExecutionControl;
        private System.Windows.Forms.Button btnTogglePushPull;
        private System.Windows.Forms.Timer tmrEndPointExecutionEffect;
        private System.Windows.Forms.Button btnForceUpdate;
        private System.Windows.Forms.ListBox lstBoxInfoLog;
    }
}