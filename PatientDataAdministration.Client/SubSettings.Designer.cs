using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class SubSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubSettings));
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.btnReInit = new System.Windows.Forms.Button();
            this.btnSaveConnections = new System.Windows.Forms.Button();
            this.btnSyncSystemData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCurrentOnDemandSync = new MetroFramework.Controls.MetroCheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemoteApi = new System.Windows.Forms.TextBox();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSaveSite = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCurrentSite = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlState = new System.Windows.Forms.ComboBox();
            this.systemStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localPDADataSet = new PatientDataAdministration.Client.LocalPDADataSet();
            this.ddlSite = new System.Windows.Forms.ComboBox();
            this.systemSiteDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.btnSaveEndPoints = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoxEndpointUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEndPoint = new System.Windows.Forms.ComboBox();
            this.lblInformation = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.system_SiteDataTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_SiteDataTableAdapter();
            this.system_StateTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_StateTableAdapter();
            this.btnExportLogs = new System.Windows.Forms.Button();
            this.infoMsgTip = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).BeginInit();
            this.metroTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(30, 62);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(588, 271);
            this.metroTabControl1.TabIndex = 1;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.metroTabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage1.Controls.Add(this.btnReInit);
            this.metroTabPage1.Controls.Add(this.btnSaveConnections);
            this.metroTabPage1.Controls.Add(this.btnSyncSystemData);
            this.metroTabPage1.Controls.Add(this.label2);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.Controls.Add(this.chkCurrentOnDemandSync);
            this.metroTabPage1.Controls.Add(this.label11);
            this.metroTabPage1.Controls.Add(this.txtRemoteApi);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(580, 232);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "General Settings";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // btnReInit
            // 
            this.btnReInit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReInit.BackColor = System.Drawing.Color.DarkRed;
            this.btnReInit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReInit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnReInit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReInit.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReInit.ForeColor = System.Drawing.Color.White;
            this.btnReInit.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Add_Database_24px;
            this.btnReInit.Location = new System.Drawing.Point(3, 187);
            this.btnReInit.Name = "btnReInit";
            this.btnReInit.Size = new System.Drawing.Size(40, 40);
            this.btnReInit.TabIndex = 59;
            this.btnReInit.Text = " ";
            this.btnReInit.UseVisualStyleBackColor = false;
            this.btnReInit.Click += new System.EventHandler(this.btnReInit_Click);
            // 
            // btnSaveConnections
            // 
            this.btnSaveConnections.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveConnections.BackColor = System.Drawing.Color.Silver;
            this.btnSaveConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveConnections.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSaveConnections.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSaveConnections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConnections.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnections.ForeColor = System.Drawing.Color.White;
            this.btnSaveConnections.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveConnections.Location = new System.Drawing.Point(535, 187);
            this.btnSaveConnections.Name = "btnSaveConnections";
            this.btnSaveConnections.Size = new System.Drawing.Size(40, 40);
            this.btnSaveConnections.TabIndex = 58;
            this.btnSaveConnections.Text = " ";
            this.btnSaveConnections.UseVisualStyleBackColor = false;
            this.btnSaveConnections.Click += new System.EventHandler(this.btnSaveConnections_Click);
            // 
            // btnSyncSystemData
            // 
            this.btnSyncSystemData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSyncSystemData.BackColor = System.Drawing.Color.White;
            this.btnSyncSystemData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSyncSystemData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncSystemData.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncSystemData.ForeColor = System.Drawing.Color.White;
            this.btnSyncSystemData.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnSyncSystemData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSyncSystemData.Location = new System.Drawing.Point(-2558, -1199);
            this.btnSyncSystemData.Name = "btnSyncSystemData";
            this.btnSyncSystemData.Size = new System.Drawing.Size(218, 32);
            this.btnSyncSystemData.TabIndex = 59;
            this.btnSyncSystemData.Text = "Sync System Data";
            this.btnSyncSystemData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSyncSystemData.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lato", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(4, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "Please confirm the actual URI of the Server in Question\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lato", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 12);
            this.label1.TabIndex = 60;
            this.label1.Text = "Minimum Requirements for this Feature are: RAM of 8Gb and CPU speed of over 2.5GH" +
    "z";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkCurrentOnDemandSync
            // 
            this.chkCurrentOnDemandSync.AutoSize = true;
            this.chkCurrentOnDemandSync.Location = new System.Drawing.Point(7, 82);
            this.chkCurrentOnDemandSync.Name = "chkCurrentOnDemandSync";
            this.chkCurrentOnDemandSync.Size = new System.Drawing.Size(195, 15);
            this.chkCurrentOnDemandSync.TabIndex = 59;
            this.chkCurrentOnDemandSync.Text = "Enable Realtime Synchronization";
            this.chkCurrentOnDemandSync.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Remote Server Connection";
            // 
            // txtRemoteApi
            // 
            this.txtRemoteApi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRemoteApi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteApi.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemoteApi.Location = new System.Drawing.Point(7, 20);
            this.txtRemoteApi.Name = "txtRemoteApi";
            this.txtRemoteApi.Size = new System.Drawing.Size(568, 30);
            this.txtRemoteApi.TabIndex = 57;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage2.Controls.Add(this.btnRefresh);
            this.metroTabPage2.Controls.Add(this.btnSaveSite);
            this.metroTabPage2.Controls.Add(this.label3);
            this.metroTabPage2.Controls.Add(this.label6);
            this.metroTabPage2.Controls.Add(this.lblCurrentSite);
            this.metroTabPage2.Controls.Add(this.label5);
            this.metroTabPage2.Controls.Add(this.label4);
            this.metroTabPage2.Controls.Add(this.ddlState);
            this.metroTabPage2.Controls.Add(this.ddlSite);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(580, 232);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Site Management";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefresh.BackColor = System.Drawing.Color.Silver;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnRefresh.Location = new System.Drawing.Point(3, 187);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 40);
            this.btnRefresh.TabIndex = 59;
            this.btnRefresh.Text = " ";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSaveSite
            // 
            this.btnSaveSite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveSite.BackColor = System.Drawing.Color.Silver;
            this.btnSaveSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSite.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSaveSite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSaveSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSite.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSite.ForeColor = System.Drawing.Color.White;
            this.btnSaveSite.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveSite.Location = new System.Drawing.Point(535, 187);
            this.btnSaveSite.Name = "btnSaveSite";
            this.btnSaveSite.Size = new System.Drawing.Size(40, 40);
            this.btnSaveSite.TabIndex = 58;
            this.btnSaveSite.Text = " ";
            this.btnSaveSite.UseVisualStyleBackColor = false;
            this.btnSaveSite.Click += new System.EventHandler(this.btnSaveSite_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(49, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 26);
            this.label3.TabIndex = 64;
            this.label3.Text = "Pull Settings and Local Data Required for \r\nOperations";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(349, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 26);
            this.label6.TabIndex = 65;
            this.label6.Text = "A modification of this Setting will Clear the \r\nPatient Data held by this Unit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentSite
            // 
            this.lblCurrentSite.AutoSize = true;
            this.lblCurrentSite.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentSite.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentSite.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCurrentSite.Location = new System.Drawing.Point(3, 119);
            this.lblCurrentSite.Name = "lblCurrentSite";
            this.lblCurrentSite.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentSite.TabIndex = 68;
            this.lblCurrentSite.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Available Sites";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "States";
            // 
            // ddlState
            // 
            this.ddlState.DataSource = this.systemStateBindingSource;
            this.ddlState.DisplayMember = "StateName";
            this.ddlState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlState.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlState.FormattingEnabled = true;
            this.ddlState.Location = new System.Drawing.Point(6, 22);
            this.ddlState.Name = "ddlState";
            this.ddlState.Size = new System.Drawing.Size(561, 31);
            this.ddlState.TabIndex = 64;
            this.ddlState.ValueMember = "Id";
            this.ddlState.SelectedValueChanged += new System.EventHandler(this.ddlState_SelectedValueChanged);
            // 
            // systemStateBindingSource
            // 
            this.systemStateBindingSource.DataMember = "System_State";
            this.systemStateBindingSource.DataSource = this.localPDADataSet;
            // 
            // localPDADataSet
            // 
            this.localPDADataSet.DataSetName = "LocalPDADataSet";
            this.localPDADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ddlSite
            // 
            this.ddlSite.DataSource = this.systemSiteDataBindingSource;
            this.ddlSite.DisplayMember = "SiteNameOfficial";
            this.ddlSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSite.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSite.FormattingEnabled = true;
            this.ddlSite.Location = new System.Drawing.Point(6, 85);
            this.ddlSite.Name = "ddlSite";
            this.ddlSite.Size = new System.Drawing.Size(561, 31);
            this.ddlSite.TabIndex = 65;
            this.ddlSite.ValueMember = "Id";
            // 
            // systemSiteDataBindingSource
            // 
            this.systemSiteDataBindingSource.DataMember = "System_SiteData";
            this.systemSiteDataBindingSource.DataSource = this.localPDADataSet;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage3.Controls.Add(this.btnSaveEndPoints);
            this.metroTabPage3.Controls.Add(this.label8);
            this.metroTabPage3.Controls.Add(this.txtBoxEndpointUrl);
            this.metroTabPage3.Controls.Add(this.label7);
            this.metroTabPage3.Controls.Add(this.cmbEndPoint);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(580, 232);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Local Content End Points";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // btnSaveEndPoints
            // 
            this.btnSaveEndPoints.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveEndPoints.BackColor = System.Drawing.Color.Silver;
            this.btnSaveEndPoints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveEndPoints.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSaveEndPoints.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSaveEndPoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveEndPoints.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveEndPoints.ForeColor = System.Drawing.Color.White;
            this.btnSaveEndPoints.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveEndPoints.Location = new System.Drawing.Point(536, 188);
            this.btnSaveEndPoints.Name = "btnSaveEndPoints";
            this.btnSaveEndPoints.Size = new System.Drawing.Size(40, 40);
            this.btnSaveEndPoints.TabIndex = 58;
            this.btnSaveEndPoints.UseVisualStyleBackColor = false;
            this.btnSaveEndPoints.Click += new System.EventHandler(this.btnSaveEndPoints_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Endpoint URL";
            // 
            // txtBoxEndpointUrl
            // 
            this.txtBoxEndpointUrl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBoxEndpointUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxEndpointUrl.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxEndpointUrl.Location = new System.Drawing.Point(6, 92);
            this.txtBoxEndpointUrl.Name = "txtBoxEndpointUrl";
            this.txtBoxEndpointUrl.Size = new System.Drawing.Size(569, 30);
            this.txtBoxEndpointUrl.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Endpoint";
            // 
            // cmbEndPoint
            // 
            this.cmbEndPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndPoint.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEndPoint.FormattingEnabled = true;
            this.cmbEndPoint.Location = new System.Drawing.Point(6, 26);
            this.cmbEndPoint.Name = "cmbEndPoint";
            this.cmbEndPoint.Size = new System.Drawing.Size(569, 31);
            this.cmbEndPoint.TabIndex = 67;
            this.cmbEndPoint.SelectedValueChanged += new System.EventHandler(this.cmbEndPoint_SelectedValueChanged);
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblInformation.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(115, 32);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(13, 13);
            this.lblInformation.TabIndex = 57;
            this.lblInformation.Text = "...";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(583, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = " ";
            this.infoMsgTip.SetToolTip(this.btnClose, "Click Here to Close");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // system_SiteDataTableAdapter
            // 
            this.system_SiteDataTableAdapter.ClearBeforeFill = true;
            // 
            // system_StateTableAdapter
            // 
            this.system_StateTableAdapter.ClearBeforeFill = true;
            // 
            // btnExportLogs
            // 
            this.btnExportLogs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportLogs.BackColor = System.Drawing.Color.Silver;
            this.btnExportLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportLogs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExportLogs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnExportLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportLogs.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportLogs.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Page_24px;
            this.btnExportLogs.Location = new System.Drawing.Point(537, 16);
            this.btnExportLogs.Name = "btnExportLogs";
            this.btnExportLogs.Size = new System.Drawing.Size(40, 40);
            this.btnExportLogs.TabIndex = 58;
            this.btnExportLogs.Text = " ";
            this.infoMsgTip.SetToolTip(this.btnExportLogs, "Export Saved Logs");
            this.btnExportLogs.UseVisualStyleBackColor = false;
            this.btnExportLogs.Click += new System.EventHandler(this.btnExportLogs_Click);
            // 
            // infoMsgTip
            // 
            this.infoMsgTip.BackColor = System.Drawing.Color.SteelBlue;
            this.infoMsgTip.ForeColor = System.Drawing.Color.White;
            this.infoMsgTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.infoMsgTip.ToolTipTitle = "What\'s this?";
            // 
            // SubSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(648, 360);
            this.ControlBox = false;
            this.Controls.Add(this.btnExportLogs);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.metroTabControl1);
            this.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubSettings";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubSettings_FormClosing);
            this.Load += new System.EventHandler(this.SubSettings_Load);
            this.Shown += new System.EventHandler(this.SubSettings_Shown);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).EndInit();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroTabControl metroTabControl1;
        private MetroTabPage metroTabPage1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRemoteApi;
        private System.Windows.Forms.Button btnSaveConnections;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MetroCheckBox chkCurrentOnDemandSync;
        private System.Windows.Forms.Button btnSyncSystemData;
        private MetroTabPage metroTabPage2;
        private System.Windows.Forms.ComboBox ddlSite;
        private LocalPDADataSet localPDADataSet;
        private System.Windows.Forms.BindingSource systemSiteDataBindingSource;
        private LocalPDADataSetTableAdapters.System_SiteDataTableAdapter system_SiteDataTableAdapter;
        private System.Windows.Forms.Button btnSaveSite;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox ddlState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource systemStateBindingSource;
        private LocalPDADataSetTableAdapters.System_StateTableAdapter system_StateTableAdapter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrentSite;
        private System.Windows.Forms.Label label6;
        private MetroTabPage metroTabPage3;
        private System.Windows.Forms.Button btnSaveEndPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxEndpointUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEndPoint;
        private System.Windows.Forms.ToolTip infoMsgTip;
        private System.Windows.Forms.Button btnExportLogs;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnReInit;
    }
}