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
            this.btnSyncSystemData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveConnections = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCurrentOnDemandSync = new MetroFramework.Controls.MetroCheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemoteApi = new System.Windows.Forms.TextBox();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCurrentSite = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlState = new System.Windows.Forms.ComboBox();
            this.systemStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localPDADataSet = new PatientDataAdministration.Client.LocalPDADataSet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveSite = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.ddlSite = new System.Windows.Forms.ComboBox();
            this.systemSiteDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).BeginInit();
            this.metroTabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(30, 65);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(588, 271);
            this.metroTabControl1.TabIndex = 1;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BackColor = System.Drawing.Color.Gray;
            this.metroTabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage1.Controls.Add(this.btnSyncSystemData);
            this.metroTabPage1.Controls.Add(this.groupBox1);
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
            // btnSyncSystemData
            // 
            this.btnSyncSystemData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSyncSystemData.BackColor = System.Drawing.Color.White;
            this.btnSyncSystemData.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources._800px_COLOURBOX18331728;
            this.btnSyncSystemData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSyncSystemData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncSystemData.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncSystemData.ForeColor = System.Drawing.Color.White;
            this.btnSyncSystemData.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnSyncSystemData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSyncSystemData.Location = new System.Drawing.Point(-1006, -511);
            this.btnSyncSystemData.Name = "btnSyncSystemData";
            this.btnSyncSystemData.Size = new System.Drawing.Size(218, 32);
            this.btnSyncSystemData.TabIndex = 59;
            this.btnSyncSystemData.Text = "Sync System Data";
            this.btnSyncSystemData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSyncSystemData.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnSaveConnections);
            this.groupBox1.Location = new System.Drawing.Point(7, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 58);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // btnSaveConnections
            // 
            this.btnSaveConnections.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveConnections.BackColor = System.Drawing.Color.White;
            this.btnSaveConnections.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnSaveConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveConnections.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnections.ForeColor = System.Drawing.Color.White;
            this.btnSaveConnections.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveConnections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveConnections.Location = new System.Drawing.Point(457, 17);
            this.btnSaveConnections.Name = "btnSaveConnections";
            this.btnSaveConnections.Size = new System.Drawing.Size(106, 32);
            this.btnSaveConnections.TabIndex = 58;
            this.btnSaveConnections.Text = "Save";
            this.btnSaveConnections.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveConnections.UseVisualStyleBackColor = false;
            this.btnSaveConnections.Click += new System.EventHandler(this.btnSaveConnections_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Please confirm the actual URI of the Server in Question\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(352, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 26);
            this.label1.TabIndex = 60;
            this.label1.Text = "Minimum Requirements for this Feature are:\r\nRAM of 8Gb and CPU speed of over 2.5G" +
    "Hz";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkCurrentOnDemandSync
            // 
            this.chkCurrentOnDemandSync.AutoSize = true;
            this.chkCurrentOnDemandSync.Location = new System.Drawing.Point(352, 22);
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
            this.label11.Location = new System.Drawing.Point(7, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 13);
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
            this.txtRemoteApi.Size = new System.Drawing.Size(339, 30);
            this.txtRemoteApi.TabIndex = 57;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.label6);
            this.metroTabPage2.Controls.Add(this.lblCurrentSite);
            this.metroTabPage2.Controls.Add(this.label5);
            this.metroTabPage2.Controls.Add(this.label4);
            this.metroTabPage2.Controls.Add(this.ddlState);
            this.metroTabPage2.Controls.Add(this.groupBox2);
            this.metroTabPage2.Controls.Add(this.ddlSite);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(580, 232);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Site Management";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(349, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 26);
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
            this.lblCurrentSite.Size = new System.Drawing.Size(16, 13);
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
            this.label5.Size = new System.Drawing.Size(78, 13);
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
            this.label4.Size = new System.Drawing.Size(37, 13);
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
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSaveSite);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Location = new System.Drawing.Point(0, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(577, 58);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Location = new System.Drawing.Point(44, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 26);
            this.label3.TabIndex = 64;
            this.label3.Text = "Pull Settings and Local Data Required for \r\nOperations";
            // 
            // btnSaveSite
            // 
            this.btnSaveSite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveSite.BackColor = System.Drawing.Color.White;
            this.btnSaveSite.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnSaveSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSite.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSite.ForeColor = System.Drawing.Color.White;
            this.btnSaveSite.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveSite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveSite.Location = new System.Drawing.Point(461, 17);
            this.btnSaveSite.Name = "btnSaveSite";
            this.btnSaveSite.Size = new System.Drawing.Size(106, 32);
            this.btnSaveSite.TabIndex = 58;
            this.btnSaveSite.Text = "Save Site";
            this.btnSaveSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSite.UseVisualStyleBackColor = false;
            this.btnSaveSite.Click += new System.EventHandler(this.btnSaveSite_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.btnRefresh.Location = new System.Drawing.Point(6, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 59;
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.metroTabPage3.Controls.Add(this.groupBox3);
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
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnSaveEndPoints);
            this.groupBox3.Location = new System.Drawing.Point(3, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(577, 58);
            this.groupBox3.TabIndex = 71;
            this.groupBox3.TabStop = false;
            // 
            // btnSaveEndPoints
            // 
            this.btnSaveEndPoints.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveEndPoints.BackColor = System.Drawing.Color.White;
            this.btnSaveEndPoints.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnSaveEndPoints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveEndPoints.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveEndPoints.ForeColor = System.Drawing.Color.White;
            this.btnSaveEndPoints.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveEndPoints.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveEndPoints.Location = new System.Drawing.Point(436, 17);
            this.btnSaveEndPoints.Name = "btnSaveEndPoints";
            this.btnSaveEndPoints.Size = new System.Drawing.Size(131, 32);
            this.btnSaveEndPoints.TabIndex = 58;
            this.btnSaveEndPoints.Text = "Save End Points";
            this.btnSaveEndPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Endpoint URL";
            // 
            // txtBoxEndpointUrl
            // 
            this.txtBoxEndpointUrl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBoxEndpointUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxEndpointUrl.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxEndpointUrl.Location = new System.Drawing.Point(3, 92);
            this.txtBoxEndpointUrl.Name = "txtBoxEndpointUrl";
            this.txtBoxEndpointUrl.Size = new System.Drawing.Size(564, 30);
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
            this.cmbEndPoint.Size = new System.Drawing.Size(561, 31);
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
            this.lblInformation.Size = new System.Drawing.Size(16, 13);
            this.lblInformation.TabIndex = 57;
            this.lblInformation.Text = "...";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(583, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 7;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnExportLogs.BackColor = System.Drawing.Color.White;
            this.btnExportLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportLogs.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportLogs.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Add_Database_24px;
            this.btnExportLogs.Location = new System.Drawing.Point(542, 16);
            this.btnExportLogs.Name = "btnExportLogs";
            this.btnExportLogs.Size = new System.Drawing.Size(35, 35);
            this.btnExportLogs.TabIndex = 58;
            this.btnExportLogs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.groupBox1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).EndInit();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MetroCheckBox chkCurrentOnDemandSync;
        private System.Windows.Forms.Button btnSyncSystemData;
        private MetroTabPage metroTabPage2;
        private System.Windows.Forms.ComboBox ddlSite;
        private LocalPDADataSet localPDADataSet;
        private System.Windows.Forms.BindingSource systemSiteDataBindingSource;
        private LocalPDADataSetTableAdapters.System_SiteDataTableAdapter system_SiteDataTableAdapter;
        private System.Windows.Forms.GroupBox groupBox2;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveEndPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxEndpointUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEndPoint;
        private System.Windows.Forms.ToolTip infoMsgTip;
        private System.Windows.Forms.Button btnExportLogs;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}