using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class SubInformationManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubInformationManagement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstBoxSearchResult = new System.Windows.Forms.ListBox();
            this.picBoxFingerPrint = new System.Windows.Forms.PictureBox();
            this.btnSearchBiometrics = new System.Windows.Forms.Button();
            this.lblBioDeviceInfo = new System.Windows.Forms.Label();
            this.lblNfcStatus = new System.Windows.Forms.Label();
            this.lblTagUid = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label13 = new System.Windows.Forms.Label();
            this.chkInstantDedup = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefreshNfcDevice = new System.Windows.Forms.Button();
            this.btnRefreshBioDevice = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlPersonalInformation = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLgaOfResidence = new System.Windows.Forms.ComboBox();
            this.systemLocalGovermentAreaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localPDADataSet = new PatientDataAdministration.Client.LocalPDADataSet();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStateOfResidence = new System.Windows.Forms.ComboBox();
            this.systemStateBindingSourceResidence = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.txtSex = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtOtherNames = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlDataControl = new System.Windows.Forms.Panel();
            this.grpDataControl = new System.Windows.Forms.GroupBox();
            this.cmbDataFingerSelector2 = new System.Windows.Forms.ComboBox();
            this.cmbDataFingerSelector1 = new System.Windows.Forms.ComboBox();
            this.btnDataFinger1 = new System.Windows.Forms.Button();
            this.btnNfcData = new System.Windows.Forms.Button();
            this.btnDataFinger2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.chkNfc = new System.Windows.Forms.CheckBox();
            this.chkSecFin = new System.Windows.Forms.CheckBox();
            this.chkPriFin = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlOfficialInformation = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHospitalNumber = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPepId = new System.Windows.Forms.TextBox();
            this.txtPreviousNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.systemStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblInformation = new System.Windows.Forms.Label();
            this.persistLoad = new System.Windows.Forms.Timer(this.components);
            this.persistPatientElectronicData = new System.Windows.Forms.Timer(this.components);
            this.system_StateTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_StateTableAdapter();
            this.system_LocalGovermentAreaTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_LocalGovermentAreaTableAdapter();
            this.persistNfcState = new System.Windows.Forms.Timer(this.components);
            this.system_BioDataStoreTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_BioDataStoreTableAdapter();
            this.btnClose = new System.Windows.Forms.Button();
            this.timerUpdateInformation = new System.Windows.Forms.Timer(this.components);
            this.tmrSecureWindow = new System.Windows.Forms.Timer(this.components);
            this.lblDataState = new System.Windows.Forms.Label();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFingerPrint)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlPersonalInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemLocalGovermentAreaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSourceResidence)).BeginInit();
            this.pnlDataControl.SuspendLayout();
            this.grpDataControl.SuspendLayout();
            this.pnlOfficialInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quick Actions";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.txtSearch);
            this.flowLayoutPanel1.Controls.Add(this.lstBoxSearchResult);
            this.flowLayoutPanel1.Controls.Add(this.picBoxFingerPrint);
            this.flowLayoutPanel1.Controls.Add(this.btnSearchBiometrics);
            this.flowLayoutPanel1.Controls.Add(this.lblBioDeviceInfo);
            this.flowLayoutPanel1.Controls.Add(this.lblNfcStatus);
            this.flowLayoutPanel1.Controls.Add(this.lblTagUid);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(170, 376);
            this.flowLayoutPanel1.TabIndex = 137;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 136;
            this.label2.Text = "Search by Text (Press Enter)";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(3, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(161, 26);
            this.txtSearch.TabIndex = 135;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lstBoxSearchResult
            // 
            this.lstBoxSearchResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxSearchResult.FormattingEnabled = true;
            this.lstBoxSearchResult.ItemHeight = 15;
            this.lstBoxSearchResult.Location = new System.Drawing.Point(3, 48);
            this.lstBoxSearchResult.Name = "lstBoxSearchResult";
            this.lstBoxSearchResult.Size = new System.Drawing.Size(161, 169);
            this.lstBoxSearchResult.TabIndex = 136;
            this.lstBoxSearchResult.Visible = false;
            this.lstBoxSearchResult.Click += new System.EventHandler(this.lstBoxSearchResult_Click);
            this.lstBoxSearchResult.SelectedIndexChanged += new System.EventHandler(this.lstBoxSearchResult_SelectedIndexChanged);
            // 
            // picBoxFingerPrint
            // 
            this.picBoxFingerPrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxFingerPrint.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Fingerprint_48px;
            this.picBoxFingerPrint.Location = new System.Drawing.Point(170, 3);
            this.picBoxFingerPrint.Name = "picBoxFingerPrint";
            this.picBoxFingerPrint.Size = new System.Drawing.Size(161, 204);
            this.picBoxFingerPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxFingerPrint.TabIndex = 80;
            this.picBoxFingerPrint.TabStop = false;
            // 
            // btnSearchBiometrics
            // 
            this.btnSearchBiometrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchBiometrics.Location = new System.Drawing.Point(170, 213);
            this.btnSearchBiometrics.Name = "btnSearchBiometrics";
            this.btnSearchBiometrics.Size = new System.Drawing.Size(161, 38);
            this.btnSearchBiometrics.TabIndex = 137;
            this.btnSearchBiometrics.Text = "Capture and Search by Biometrics";
            this.btnSearchBiometrics.UseVisualStyleBackColor = true;
            this.btnSearchBiometrics.Click += new System.EventHandler(this.btnSearchBiometrics_Click);
            // 
            // lblBioDeviceInfo
            // 
            this.lblBioDeviceInfo.AutoSize = true;
            this.lblBioDeviceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBioDeviceInfo.Location = new System.Drawing.Point(170, 254);
            this.lblBioDeviceInfo.Name = "lblBioDeviceInfo";
            this.lblBioDeviceInfo.Size = new System.Drawing.Size(16, 15);
            this.lblBioDeviceInfo.TabIndex = 83;
            this.lblBioDeviceInfo.Text = "...";
            // 
            // lblNfcStatus
            // 
            this.lblNfcStatus.AutoSize = true;
            this.lblNfcStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNfcStatus.Location = new System.Drawing.Point(170, 269);
            this.lblNfcStatus.Name = "lblNfcStatus";
            this.lblNfcStatus.Size = new System.Drawing.Size(16, 15);
            this.lblNfcStatus.TabIndex = 138;
            this.lblNfcStatus.Text = "...";
            // 
            // lblTagUid
            // 
            this.lblTagUid.AutoSize = true;
            this.lblTagUid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagUid.Location = new System.Drawing.Point(170, 284);
            this.lblTagUid.Name = "lblTagUid";
            this.lblTagUid.Size = new System.Drawing.Size(16, 15);
            this.lblTagUid.TabIndex = 139;
            this.lblTagUid.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.pnlPersonalInformation);
            this.groupBox2.Controls.Add(this.pnlDataControl);
            this.groupBox2.Controls.Add(this.pnlOfficialInformation);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(218, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 411);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patient Information";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.chkInstantDedup);
            this.panel2.Location = new System.Drawing.Point(735, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(163, 180);
            this.panel2.TabIndex = 137;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 60);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(154, 115);
            this.listView1.TabIndex = 138;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SteelBlue;
            this.label13.Location = new System.Drawing.Point(3, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 13);
            this.label13.TabIndex = 140;
            this.label13.Text = "Verification Mode";
            // 
            // chkInstantDedup
            // 
            this.chkInstantDedup.AutoSize = true;
            this.chkInstantDedup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInstantDedup.ForeColor = System.Drawing.Color.Black;
            this.chkInstantDedup.Location = new System.Drawing.Point(5, 34);
            this.chkInstantDedup.Name = "chkInstantDedup";
            this.chkInstantDedup.Size = new System.Drawing.Size(148, 17);
            this.chkInstantDedup.TabIndex = 62;
            this.chkInstantDedup.Text = "Use Instant Deduplication";
            this.chkInstantDedup.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRefreshNfcDevice);
            this.panel1.Controls.Add(this.btnRefreshBioDevice);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Location = new System.Drawing.Point(735, 208);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 192);
            this.panel1.TabIndex = 136;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Silver;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(3, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(155, 40);
            this.btnSave.TabIndex = 129;
            this.btnSave.Text = "Save Information";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefreshNfcDevice
            // 
            this.btnRefreshNfcDevice.BackColor = System.Drawing.Color.Silver;
            this.btnRefreshNfcDevice.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRefreshNfcDevice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRefreshNfcDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshNfcDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshNfcDevice.ForeColor = System.Drawing.Color.Black;
            this.btnRefreshNfcDevice.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshNfcDevice.Image")));
            this.btnRefreshNfcDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshNfcDevice.Location = new System.Drawing.Point(3, 52);
            this.btnRefreshNfcDevice.Name = "btnRefreshNfcDevice";
            this.btnRefreshNfcDevice.Size = new System.Drawing.Size(155, 40);
            this.btnRefreshNfcDevice.TabIndex = 133;
            this.btnRefreshNfcDevice.Text = "Refreah NFC Tag\r\nDevice";
            this.btnRefreshNfcDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshNfcDevice.UseVisualStyleBackColor = false;
            this.btnRefreshNfcDevice.Click += new System.EventHandler(this.btnRefreshNfcDevice_Click);
            // 
            // btnRefreshBioDevice
            // 
            this.btnRefreshBioDevice.BackColor = System.Drawing.Color.Silver;
            this.btnRefreshBioDevice.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRefreshBioDevice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRefreshBioDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshBioDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshBioDevice.ForeColor = System.Drawing.Color.Black;
            this.btnRefreshBioDevice.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshBioDevice.Image")));
            this.btnRefreshBioDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshBioDevice.Location = new System.Drawing.Point(3, 98);
            this.btnRefreshBioDevice.Name = "btnRefreshBioDevice";
            this.btnRefreshBioDevice.Size = new System.Drawing.Size(155, 40);
            this.btnRefreshBioDevice.TabIndex = 134;
            this.btnRefreshBioDevice.Text = "Refresh Fingerprint \r\nDevice";
            this.btnRefreshBioDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshBioDevice.UseVisualStyleBackColor = false;
            this.btnRefreshBioDevice.Click += new System.EventHandler(this.btnRefreshBioDevice_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Silver;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Empty_Trash_24px;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(3, 144);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 40);
            this.btnClear.TabIndex = 131;
            this.btnClear.Text = "Clear all Inputs";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlPersonalInformation
            // 
            this.pnlPersonalInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPersonalInformation.Controls.Add(this.label10);
            this.pnlPersonalInformation.Controls.Add(this.label7);
            this.pnlPersonalInformation.Controls.Add(this.txtLgaOfResidence);
            this.pnlPersonalInformation.Controls.Add(this.label6);
            this.pnlPersonalInformation.Controls.Add(this.txtStateOfResidence);
            this.pnlPersonalInformation.Controls.Add(this.label5);
            this.pnlPersonalInformation.Controls.Add(this.txtAddress);
            this.pnlPersonalInformation.Controls.Add(this.label17);
            this.pnlPersonalInformation.Controls.Add(this.txtPhoneNumber);
            this.pnlPersonalInformation.Controls.Add(this.label12);
            this.pnlPersonalInformation.Controls.Add(this.txtDateOfBirth);
            this.pnlPersonalInformation.Controls.Add(this.txtSex);
            this.pnlPersonalInformation.Controls.Add(this.label14);
            this.pnlPersonalInformation.Controls.Add(this.label11);
            this.pnlPersonalInformation.Controls.Add(this.label15);
            this.pnlPersonalInformation.Controls.Add(this.txtSurname);
            this.pnlPersonalInformation.Controls.Add(this.txtOtherNames);
            this.pnlPersonalInformation.Controls.Add(this.label16);
            this.pnlPersonalInformation.Location = new System.Drawing.Point(6, 165);
            this.pnlPersonalInformation.Name = "pnlPersonalInformation";
            this.pnlPersonalInformation.Size = new System.Drawing.Size(723, 236);
            this.pnlPersonalInformation.TabIndex = 75;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Maroon;
            this.label10.Location = new System.Drawing.Point(536, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 13);
            this.label10.TabIndex = 135;
            this.label10.Text = "Verify all Data Properly before Saving";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(299, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 105;
            this.label7.Text = "LGA of Residence";
            // 
            // txtLgaOfResidence
            // 
            this.txtLgaOfResidence.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLgaOfResidence.DataSource = this.systemLocalGovermentAreaBindingSource;
            this.txtLgaOfResidence.DisplayMember = "LocalGovermentAreaName";
            this.txtLgaOfResidence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtLgaOfResidence.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLgaOfResidence.FormattingEnabled = true;
            this.txtLgaOfResidence.Location = new System.Drawing.Point(299, 202);
            this.txtLgaOfResidence.Name = "txtLgaOfResidence";
            this.txtLgaOfResidence.Size = new System.Drawing.Size(281, 23);
            this.txtLgaOfResidence.TabIndex = 104;
            this.txtLgaOfResidence.ValueMember = "Id";
            // 
            // systemLocalGovermentAreaBindingSource
            // 
            this.systemLocalGovermentAreaBindingSource.DataMember = "System_LocalGovermentArea";
            this.systemLocalGovermentAreaBindingSource.DataSource = this.localPDADataSet;
            // 
            // localPDADataSet
            // 
            this.localPDADataSet.DataSetName = "LocalPDADataSet";
            this.localPDADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 103;
            this.label6.Text = "State of Residence";
            // 
            // txtStateOfResidence
            // 
            this.txtStateOfResidence.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStateOfResidence.DataSource = this.systemStateBindingSourceResidence;
            this.txtStateOfResidence.DisplayMember = "StateName";
            this.txtStateOfResidence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtStateOfResidence.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateOfResidence.FormattingEnabled = true;
            this.txtStateOfResidence.Location = new System.Drawing.Point(7, 202);
            this.txtStateOfResidence.Name = "txtStateOfResidence";
            this.txtStateOfResidence.Size = new System.Drawing.Size(281, 23);
            this.txtStateOfResidence.TabIndex = 102;
            this.txtStateOfResidence.ValueMember = "Id";
            this.txtStateOfResidence.SelectedValueChanged += new System.EventHandler(this.txtStateOfResidence_SelectedValueChanged);
            // 
            // systemStateBindingSourceResidence
            // 
            this.systemStateBindingSourceResidence.DataMember = "System_State";
            this.systemStateBindingSourceResidence.DataSource = this.localPDADataSet;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(7, 150);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(708, 24);
            this.txtAddress.TabIndex = 101;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(7, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 13);
            this.label17.TabIndex = 62;
            this.label17.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(7, 101);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(282, 24);
            this.txtPhoneNumber.TabIndex = 63;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(299, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 74;
            this.label12.Text = "Date of Birth";
            // 
            // txtDateOfBirth
            // 
            this.txtDateOfBirth.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateOfBirth.Location = new System.Drawing.Point(299, 101);
            this.txtDateOfBirth.Name = "txtDateOfBirth";
            this.txtDateOfBirth.ShowCheckBox = true;
            this.txtDateOfBirth.Size = new System.Drawing.Size(152, 24);
            this.txtDateOfBirth.TabIndex = 73;
            // 
            // txtSex
            // 
            this.txtSex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSex.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.FormattingEnabled = true;
            this.txtSex.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.txtSex.Location = new System.Drawing.Point(589, 49);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(126, 23);
            this.txtSex.TabIndex = 70;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.SteelBlue;
            this.label14.Location = new System.Drawing.Point(7, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "Personal Information";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Surname";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(589, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 58;
            this.label15.Text = "Sex";
            // 
            // txtSurname
            // 
            this.txtSurname.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSurname.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSurname.Location = new System.Drawing.Point(7, 50);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(281, 24);
            this.txtSurname.TabIndex = 55;
            // 
            // txtOtherNames
            // 
            this.txtOtherNames.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOtherNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherNames.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherNames.Location = new System.Drawing.Point(299, 50);
            this.txtOtherNames.Name = "txtOtherNames";
            this.txtOtherNames.Size = new System.Drawing.Size(281, 24);
            this.txtOtherNames.TabIndex = 57;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Location = new System.Drawing.Point(299, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "Other Names";
            // 
            // pnlDataControl
            // 
            this.pnlDataControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDataControl.Controls.Add(this.grpDataControl);
            this.pnlDataControl.Controls.Add(this.label9);
            this.pnlDataControl.Controls.Add(this.chkNfc);
            this.pnlDataControl.Controls.Add(this.chkSecFin);
            this.pnlDataControl.Controls.Add(this.chkPriFin);
            this.pnlDataControl.Controls.Add(this.label8);
            this.pnlDataControl.Location = new System.Drawing.Point(248, 23);
            this.pnlDataControl.Name = "pnlDataControl";
            this.pnlDataControl.Size = new System.Drawing.Size(481, 136);
            this.pnlDataControl.TabIndex = 76;
            // 
            // grpDataControl
            // 
            this.grpDataControl.Controls.Add(this.cmbDataFingerSelector2);
            this.grpDataControl.Controls.Add(this.cmbDataFingerSelector1);
            this.grpDataControl.Controls.Add(this.btnDataFinger1);
            this.grpDataControl.Controls.Add(this.btnNfcData);
            this.grpDataControl.Controls.Add(this.btnDataFinger2);
            this.grpDataControl.Location = new System.Drawing.Point(3, 25);
            this.grpDataControl.Name = "grpDataControl";
            this.grpDataControl.Size = new System.Drawing.Size(250, 106);
            this.grpDataControl.TabIndex = 135;
            this.grpDataControl.TabStop = false;
            // 
            // cmbDataFingerSelector2
            // 
            this.cmbDataFingerSelector2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbDataFingerSelector2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataFingerSelector2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataFingerSelector2.FormattingEnabled = true;
            this.cmbDataFingerSelector2.Items.AddRange(new object[] {
            "Left Thumb",
            "Left Index Finger",
            "Left Middle Finger",
            "Left Ring Finger",
            "Left Baby Finger",
            "Right Thumb",
            "Right Index Finger",
            "Right Middle Finger",
            "Right Ring Finger",
            "Right Baby Finger"});
            this.cmbDataFingerSelector2.Location = new System.Drawing.Point(48, 61);
            this.cmbDataFingerSelector2.Name = "cmbDataFingerSelector2";
            this.cmbDataFingerSelector2.Size = new System.Drawing.Size(152, 23);
            this.cmbDataFingerSelector2.TabIndex = 140;
            // 
            // cmbDataFingerSelector1
            // 
            this.cmbDataFingerSelector1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbDataFingerSelector1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataFingerSelector1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataFingerSelector1.FormattingEnabled = true;
            this.cmbDataFingerSelector1.Items.AddRange(new object[] {
            "Left Thumb",
            "Left Index Finger",
            "Left Middle Finger",
            "Left Ring Finger",
            "Left Baby Finger",
            "Right Thumb",
            "Right Index Finger",
            "Right Middle Finger",
            "Right Ring Finger",
            "Right Baby Finger"});
            this.cmbDataFingerSelector1.Location = new System.Drawing.Point(48, 19);
            this.cmbDataFingerSelector1.Name = "cmbDataFingerSelector1";
            this.cmbDataFingerSelector1.Size = new System.Drawing.Size(152, 23);
            this.cmbDataFingerSelector1.TabIndex = 139;
            // 
            // btnDataFinger1
            // 
            this.btnDataFinger1.BackColor = System.Drawing.Color.Gray;
            this.btnDataFinger1.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.icons8_Fingerprint_Scan_30px;
            this.btnDataFinger1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDataFinger1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDataFinger1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnDataFinger1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataFinger1.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataFinger1.Location = new System.Drawing.Point(6, 17);
            this.btnDataFinger1.Name = "btnDataFinger1";
            this.btnDataFinger1.Size = new System.Drawing.Size(38, 38);
            this.btnDataFinger1.TabIndex = 136;
            this.btnDataFinger1.UseVisualStyleBackColor = false;
            this.btnDataFinger1.Click += new System.EventHandler(this.btnDataFinger1_Click);
            // 
            // btnNfcData
            // 
            this.btnNfcData.BackColor = System.Drawing.Color.Gray;
            this.btnNfcData.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.icons8_NFC_30px;
            this.btnNfcData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNfcData.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNfcData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnNfcData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNfcData.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNfcData.Location = new System.Drawing.Point(206, 19);
            this.btnNfcData.Name = "btnNfcData";
            this.btnNfcData.Size = new System.Drawing.Size(38, 38);
            this.btnNfcData.TabIndex = 138;
            this.btnNfcData.UseVisualStyleBackColor = false;
            this.btnNfcData.Click += new System.EventHandler(this.btnNfcData_Click);
            // 
            // btnDataFinger2
            // 
            this.btnDataFinger2.BackColor = System.Drawing.Color.Gray;
            this.btnDataFinger2.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.icons8_Fingerprint_Scan_30px;
            this.btnDataFinger2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDataFinger2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDataFinger2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnDataFinger2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataFinger2.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataFinger2.Location = new System.Drawing.Point(6, 62);
            this.btnDataFinger2.Name = "btnDataFinger2";
            this.btnDataFinger2.Size = new System.Drawing.Size(38, 38);
            this.btnDataFinger2.TabIndex = 137;
            this.btnDataFinger2.UseVisualStyleBackColor = false;
            this.btnDataFinger2.Click += new System.EventHandler(this.btnDataFinger2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(333, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 13);
            this.label9.TabIndex = 139;
            this.label9.Text = "Click buttons for Fingerprints";
            // 
            // chkNfc
            // 
            this.chkNfc.AutoSize = true;
            this.chkNfc.Enabled = false;
            this.chkNfc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNfc.ForeColor = System.Drawing.Color.DimGray;
            this.chkNfc.Location = new System.Drawing.Point(259, 85);
            this.chkNfc.Name = "chkNfc";
            this.chkNfc.Size = new System.Drawing.Size(120, 17);
            this.chkNfc.TabIndex = 63;
            this.chkNfc.Text = "Is NFC Tag Issued?";
            this.chkNfc.UseVisualStyleBackColor = true;
            // 
            // chkSecFin
            // 
            this.chkSecFin.AutoSize = true;
            this.chkSecFin.Enabled = false;
            this.chkSecFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecFin.ForeColor = System.Drawing.Color.DimGray;
            this.chkSecFin.Location = new System.Drawing.Point(259, 59);
            this.chkSecFin.Name = "chkSecFin";
            this.chkSecFin.Size = new System.Drawing.Size(192, 17);
            this.chkSecFin.TabIndex = 62;
            this.chkSecFin.Text = "Is Secondary Fingerprint Captured?";
            this.chkSecFin.UseVisualStyleBackColor = true;
            // 
            // chkPriFin
            // 
            this.chkPriFin.AutoSize = true;
            this.chkPriFin.Enabled = false;
            this.chkPriFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPriFin.ForeColor = System.Drawing.Color.DimGray;
            this.chkPriFin.Location = new System.Drawing.Point(259, 33);
            this.chkPriFin.Name = "chkPriFin";
            this.chkPriFin.Size = new System.Drawing.Size(175, 17);
            this.chkPriFin.TabIndex = 61;
            this.chkPriFin.Text = "Is Primary Fingerprint Captured?";
            this.chkPriFin.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "Data Control";
            // 
            // pnlOfficialInformation
            // 
            this.pnlOfficialInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOfficialInformation.Controls.Add(this.pictureBox1);
            this.pnlOfficialInformation.Controls.Add(this.label4);
            this.pnlOfficialInformation.Controls.Add(this.txtHospitalNumber);
            this.pnlOfficialInformation.Controls.Add(this.label40);
            this.pnlOfficialInformation.Controls.Add(this.label3);
            this.pnlOfficialInformation.Controls.Add(this.txtPepId);
            this.pnlOfficialInformation.Controls.Add(this.txtPreviousNumber);
            this.pnlOfficialInformation.Controls.Add(this.label1);
            this.pnlOfficialInformation.Location = new System.Drawing.Point(6, 23);
            this.pnlOfficialInformation.Name = "pnlOfficialInformation";
            this.pnlOfficialInformation.Size = new System.Drawing.Size(234, 136);
            this.pnlOfficialInformation.TabIndex = 60;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Info_16px;
            this.pictureBox1.Location = new System.Drawing.Point(210, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "Official Information";
            // 
            // txtHospitalNumber
            // 
            this.txtHospitalNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHospitalNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHospitalNumber.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHospitalNumber.Location = new System.Drawing.Point(124, 99);
            this.txtHospitalNumber.Name = "txtHospitalNumber";
            this.txtHospitalNumber.Size = new System.Drawing.Size(106, 24);
            this.txtHospitalNumber.TabIndex = 59;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(3, 34);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(41, 13);
            this.label40.TabIndex = 54;
            this.label40.Text = "PeP ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(124, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Hospital Number";
            // 
            // txtPepId
            // 
            this.txtPepId.BackColor = System.Drawing.Color.LightGray;
            this.txtPepId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPepId.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPepId.ForeColor = System.Drawing.Color.Black;
            this.txtPepId.Location = new System.Drawing.Point(3, 50);
            this.txtPepId.Name = "txtPepId";
            this.txtPepId.Size = new System.Drawing.Size(227, 24);
            this.txtPepId.TabIndex = 55;
            // 
            // txtPreviousNumber
            // 
            this.txtPreviousNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPreviousNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreviousNumber.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviousNumber.Location = new System.Drawing.Point(3, 99);
            this.txtPreviousNumber.Name = "txtPreviousNumber";
            this.txtPreviousNumber.Size = new System.Drawing.Size(113, 24);
            this.txtPreviousNumber.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Previous ID";
            // 
            // systemStateBindingSource
            // 
            this.systemStateBindingSource.DataMember = "System_State";
            this.systemStateBindingSource.DataSource = this.localPDADataSet;
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(30, 478);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(16, 13);
            this.lblInformation.TabIndex = 132;
            this.lblInformation.Text = "...";
            // 
            // persistLoad
            // 
            this.persistLoad.Interval = 3000;
            this.persistLoad.Tick += new System.EventHandler(this.persistLoad_Tick);
            // 
            // persistPatientElectronicData
            // 
            this.persistPatientElectronicData.Enabled = true;
            this.persistPatientElectronicData.Interval = 1000;
            this.persistPatientElectronicData.Tick += new System.EventHandler(this.persistPatientElectronicData_Tick);
            // 
            // system_StateTableAdapter
            // 
            this.system_StateTableAdapter.ClearBeforeFill = true;
            // 
            // system_LocalGovermentAreaTableAdapter
            // 
            this.system_LocalGovermentAreaTableAdapter.ClearBeforeFill = true;
            // 
            // persistNfcState
            // 
            this.persistNfcState.Enabled = true;
            this.persistNfcState.Interval = 1000;
            this.persistNfcState.Tick += new System.EventHandler(this.persistNfcState_Tick);
            // 
            // system_BioDataStoreTableAdapter
            // 
            this.system_BioDataStoreTableAdapter.ClearBeforeFill = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(1082, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 135;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timerUpdateInformation
            // 
            this.timerUpdateInformation.Enabled = true;
            this.timerUpdateInformation.Interval = 500;
            this.timerUpdateInformation.Tick += new System.EventHandler(this.timerUpdateInformation_Tick);
            // 
            // tmrSecureWindow
            // 
            this.tmrSecureWindow.Interval = 1000;
            this.tmrSecureWindow.Tick += new System.EventHandler(this.tmrSecureWindow_Tick);
            // 
            // lblDataState
            // 
            this.lblDataState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataState.Location = new System.Drawing.Point(646, 478);
            this.lblDataState.Name = "lblDataState";
            this.lblDataState.Size = new System.Drawing.Size(476, 15);
            this.lblDataState.TabIndex = 136;
            this.lblDataState.Text = "...";
            this.lblDataState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.ForeColor = System.Drawing.Color.Maroon;
            this.lblPleaseWait.Location = new System.Drawing.Point(904, 37);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(172, 13);
            this.lblPleaseWait.TabIndex = 106;
            this.lblPleaseWait.Text = "Operation In Progress. Please Wait";
            this.lblPleaseWait.Visible = false;
            // 
            // SubInformationManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 505);
            this.ControlBox = false;
            this.Controls.Add(this.lblPleaseWait);
            this.Controls.Add(this.lblDataState);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubInformationManagement";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.ShowIcon = false;
            this.Text = "Information Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubInformationManagement_FormClosing);
            this.Load += new System.EventHandler(this.SubInformationManagement_Load);
            this.Shown += new System.EventHandler(this.SubInformationManagement_Shown);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFingerPrint)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlPersonalInformation.ResumeLayout(false);
            this.pnlPersonalInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemLocalGovermentAreaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSourceResidence)).EndInit();
            this.pnlDataControl.ResumeLayout(false);
            this.pnlDataControl.PerformLayout();
            this.grpDataControl.ResumeLayout(false);
            this.pnlOfficialInformation.ResumeLayout(false);
            this.pnlOfficialInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblBioDeviceInfo;
        private System.Windows.Forms.PictureBox picBoxFingerPrint;
        private System.Windows.Forms.ListBox lstBoxSearchResult;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchBiometrics;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtHospitalNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPreviousNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPepId;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel pnlOfficialInformation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlPersonalInformation;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker txtDateOfBirth;
        private System.Windows.Forms.ComboBox txtSex;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtOtherNames;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlDataControl;
        private System.Windows.Forms.CheckBox chkNfc;
        private System.Windows.Forms.CheckBox chkSecFin;
        private System.Windows.Forms.CheckBox chkPriFin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox txtLgaOfResidence;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtStateOfResidence;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnNfcData;
        private System.Windows.Forms.Button btnDataFinger2;
        private System.Windows.Forms.Button btnDataFinger1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Label lblNfcStatus;
        private System.Windows.Forms.Button btnRefreshNfcDevice;
        private System.Windows.Forms.Button btnRefreshBioDevice;
        private System.Windows.Forms.Timer persistLoad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpDataControl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer persistPatientElectronicData;
        private LocalPDADataSet localPDADataSet;
        private System.Windows.Forms.BindingSource systemStateBindingSource;
        private LocalPDADataSetTableAdapters.System_StateTableAdapter system_StateTableAdapter;
        private System.Windows.Forms.BindingSource systemStateBindingSourceResidence;
        private System.Windows.Forms.BindingSource systemLocalGovermentAreaBindingSource;
        private LocalPDADataSetTableAdapters.System_LocalGovermentAreaTableAdapter system_LocalGovermentAreaTableAdapter;
        private System.Windows.Forms.Label lblTagUid;
        private System.Windows.Forms.Timer persistNfcState;
        private LocalPDADataSetTableAdapters.System_BioDataStoreTableAdapter system_BioDataStoreTableAdapter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timerUpdateInformation;
        private System.Windows.Forms.Timer tmrSecureWindow;
        private System.Windows.Forms.Label lblDataState;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.ComboBox cmbDataFingerSelector2;
        private System.Windows.Forms.ComboBox cmbDataFingerSelector1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkInstantDedup;
        private System.Windows.Forms.ListView listView1;
    }
}