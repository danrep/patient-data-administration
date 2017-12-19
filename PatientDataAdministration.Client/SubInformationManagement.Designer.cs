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
            this.label10 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlDataControl = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDataFinger1 = new System.Windows.Forms.Button();
            this.btnNfcData = new System.Windows.Forms.Button();
            this.btnDataFinger2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.chkNfc = new System.Windows.Forms.CheckBox();
            this.chkSecFin = new System.Windows.Forms.CheckBox();
            this.chkPriFin = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlPersonalInformation = new System.Windows.Forms.Panel();
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
            this.btnRefreshNfcDevice = new System.Windows.Forms.Button();
            this.btnRefreshBioDevice = new System.Windows.Forms.Button();
            this.persistLoad = new System.Windows.Forms.Timer(this.components);
            this.persistPatientElectronicData = new System.Windows.Forms.Timer(this.components);
            this.system_StateTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_StateTableAdapter();
            this.system_LocalGovermentAreaTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_LocalGovermentAreaTableAdapter();
            this.persistNfcState = new System.Windows.Forms.Timer(this.components);
            this.system_BioDataStoreTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_BioDataStoreTableAdapter();
            this.btnClose = new System.Windows.Forms.Button();
            this.timerUpdateInformation = new System.Windows.Forms.Timer(this.components);
            this.pnlWaiting = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tmrSecureWindow = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFingerPrint)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlDataControl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlPersonalInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemLocalGovermentAreaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSourceResidence)).BeginInit();
            this.pnlOfficialInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).BeginInit();
            this.pnlWaiting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 393);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(170, 360);
            this.flowLayoutPanel1.TabIndex = 137;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 136;
            this.label2.Text = "Search by Text (Press Enter)";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(3, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(161, 30);
            this.txtSearch.TabIndex = 135;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lstBoxSearchResult
            // 
            this.lstBoxSearchResult.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxSearchResult.FormattingEnabled = true;
            this.lstBoxSearchResult.ItemHeight = 15;
            this.lstBoxSearchResult.Location = new System.Drawing.Point(3, 52);
            this.lstBoxSearchResult.Name = "lstBoxSearchResult";
            this.lstBoxSearchResult.Size = new System.Drawing.Size(161, 169);
            this.lstBoxSearchResult.TabIndex = 136;
            this.lstBoxSearchResult.Visible = false;
            this.lstBoxSearchResult.Click += new System.EventHandler(this.lstBoxSearchResult_Click);
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
            this.btnSearchBiometrics.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchBiometrics.Location = new System.Drawing.Point(170, 213);
            this.btnSearchBiometrics.Name = "btnSearchBiometrics";
            this.btnSearchBiometrics.Size = new System.Drawing.Size(161, 23);
            this.btnSearchBiometrics.TabIndex = 137;
            this.btnSearchBiometrics.Text = "Search by Biometrics";
            this.btnSearchBiometrics.UseVisualStyleBackColor = true;
            this.btnSearchBiometrics.Click += new System.EventHandler(this.btnSearchBiometrics_Click);
            // 
            // lblBioDeviceInfo
            // 
            this.lblBioDeviceInfo.AutoSize = true;
            this.lblBioDeviceInfo.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBioDeviceInfo.Location = new System.Drawing.Point(170, 239);
            this.lblBioDeviceInfo.Name = "lblBioDeviceInfo";
            this.lblBioDeviceInfo.Size = new System.Drawing.Size(16, 15);
            this.lblBioDeviceInfo.TabIndex = 83;
            this.lblBioDeviceInfo.Text = "...";
            // 
            // lblNfcStatus
            // 
            this.lblNfcStatus.AutoSize = true;
            this.lblNfcStatus.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNfcStatus.Location = new System.Drawing.Point(170, 254);
            this.lblNfcStatus.Name = "lblNfcStatus";
            this.lblNfcStatus.Size = new System.Drawing.Size(16, 15);
            this.lblNfcStatus.TabIndex = 138;
            this.lblNfcStatus.Text = "...";
            // 
            // lblTagUid
            // 
            this.lblTagUid.AutoSize = true;
            this.lblTagUid.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagUid.Location = new System.Drawing.Point(170, 269);
            this.lblTagUid.Name = "lblTagUid";
            this.lblTagUid.Size = new System.Drawing.Size(16, 15);
            this.lblTagUid.TabIndex = 139;
            this.lblTagUid.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.pnlDataControl);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.pnlPersonalInformation);
            this.groupBox2.Controls.Add(this.pnlOfficialInformation);
            this.groupBox2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(218, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(721, 393);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patient Information";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(524, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(190, 13);
            this.label10.TabIndex = 135;
            this.label10.Text = "Verify all Data Properly before Saving";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(583, 296);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 40);
            this.btnSave.TabIndex = 129;
            this.btnSave.Text = "Save Information";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlDataControl
            // 
            this.pnlDataControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDataControl.Controls.Add(this.groupBox3);
            this.pnlDataControl.Controls.Add(this.label9);
            this.pnlDataControl.Controls.Add(this.chkNfc);
            this.pnlDataControl.Controls.Add(this.chkSecFin);
            this.pnlDataControl.Controls.Add(this.chkPriFin);
            this.pnlDataControl.Controls.Add(this.label8);
            this.pnlDataControl.Location = new System.Drawing.Point(6, 212);
            this.pnlDataControl.Name = "pnlDataControl";
            this.pnlDataControl.Size = new System.Drawing.Size(234, 170);
            this.pnlDataControl.TabIndex = 76;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDataFinger1);
            this.groupBox3.Controls.Add(this.btnNfcData);
            this.groupBox3.Controls.Add(this.btnDataFinger2);
            this.groupBox3.Location = new System.Drawing.Point(3, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 62);
            this.groupBox3.TabIndex = 135;
            this.groupBox3.TabStop = false;
            // 
            // btnDataFinger1
            // 
            this.btnDataFinger1.BackColor = System.Drawing.Color.Gray;
            this.btnDataFinger1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDataFinger1.BackgroundImage")));
            this.btnDataFinger1.FlatAppearance.BorderSize = 0;
            this.btnDataFinger1.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataFinger1.Location = new System.Drawing.Point(6, 17);
            this.btnDataFinger1.Name = "btnDataFinger1";
            this.btnDataFinger1.Size = new System.Drawing.Size(36, 36);
            this.btnDataFinger1.TabIndex = 136;
            this.btnDataFinger1.UseVisualStyleBackColor = false;
            this.btnDataFinger1.Click += new System.EventHandler(this.btnDataFinger1_Click);
            // 
            // btnNfcData
            // 
            this.btnNfcData.BackColor = System.Drawing.Color.Gray;
            this.btnNfcData.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.icons8_Contact_48px;
            this.btnNfcData.FlatAppearance.BorderSize = 0;
            this.btnNfcData.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNfcData.Location = new System.Drawing.Point(182, 17);
            this.btnNfcData.Name = "btnNfcData";
            this.btnNfcData.Size = new System.Drawing.Size(36, 36);
            this.btnNfcData.TabIndex = 138;
            this.btnNfcData.UseVisualStyleBackColor = false;
            this.btnNfcData.Click += new System.EventHandler(this.btnNfcData_Click);
            // 
            // btnDataFinger2
            // 
            this.btnDataFinger2.BackColor = System.Drawing.Color.Gray;
            this.btnDataFinger2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDataFinger2.BackgroundImage")));
            this.btnDataFinger2.FlatAppearance.BorderSize = 0;
            this.btnDataFinger2.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataFinger2.Location = new System.Drawing.Point(48, 17);
            this.btnDataFinger2.Name = "btnDataFinger2";
            this.btnDataFinger2.Size = new System.Drawing.Size(36, 36);
            this.btnDataFinger2.TabIndex = 137;
            this.btnDataFinger2.UseVisualStyleBackColor = false;
            this.btnDataFinger2.Click += new System.EventHandler(this.btnDataFinger2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(79, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 13);
            this.label9.TabIndex = 139;
            this.label9.Text = "Click buttons for Fingerprints";
            // 
            // chkNfc
            // 
            this.chkNfc.AutoSize = true;
            this.chkNfc.Enabled = false;
            this.chkNfc.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNfc.ForeColor = System.Drawing.Color.DimGray;
            this.chkNfc.Location = new System.Drawing.Point(8, 142);
            this.chkNfc.Name = "chkNfc";
            this.chkNfc.Size = new System.Drawing.Size(123, 19);
            this.chkNfc.TabIndex = 63;
            this.chkNfc.Text = "Is NFC Tag Issued?";
            this.chkNfc.UseVisualStyleBackColor = true;
            // 
            // chkSecFin
            // 
            this.chkSecFin.AutoSize = true;
            this.chkSecFin.Enabled = false;
            this.chkSecFin.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecFin.ForeColor = System.Drawing.Color.DimGray;
            this.chkSecFin.Location = new System.Drawing.Point(9, 116);
            this.chkSecFin.Name = "chkSecFin";
            this.chkSecFin.Size = new System.Drawing.Size(211, 19);
            this.chkSecFin.TabIndex = 62;
            this.chkSecFin.Text = "Is Secondary Fingerprint Captured?";
            this.chkSecFin.UseVisualStyleBackColor = true;
            // 
            // chkPriFin
            // 
            this.chkPriFin.AutoSize = true;
            this.chkPriFin.Enabled = false;
            this.chkPriFin.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPriFin.ForeColor = System.Drawing.Color.DimGray;
            this.chkPriFin.Location = new System.Drawing.Point(9, 90);
            this.chkPriFin.Name = "chkPriFin";
            this.chkPriFin.Size = new System.Drawing.Size(195, 19);
            this.chkPriFin.TabIndex = 61;
            this.chkPriFin.Text = "Is Primary Fingerprint Captured?";
            this.chkPriFin.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 60;
            this.label8.Text = "Data Control";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Black;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Empty_Trash_24px;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(582, 342);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(132, 40);
            this.btnClear.TabIndex = 131;
            this.btnClear.Text = "Clear all Inputs";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlPersonalInformation
            // 
            this.pnlPersonalInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.pnlPersonalInformation.Location = new System.Drawing.Point(246, 22);
            this.pnlPersonalInformation.Name = "pnlPersonalInformation";
            this.pnlPersonalInformation.Size = new System.Drawing.Size(468, 236);
            this.pnlPersonalInformation.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(233, 183);
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
            this.txtLgaOfResidence.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLgaOfResidence.FormattingEnabled = true;
            this.txtLgaOfResidence.Location = new System.Drawing.Point(236, 199);
            this.txtLgaOfResidence.Name = "txtLgaOfResidence";
            this.txtLgaOfResidence.Size = new System.Drawing.Size(227, 31);
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
            this.label6.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 183);
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
            this.txtStateOfResidence.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateOfResidence.FormattingEnabled = true;
            this.txtStateOfResidence.Location = new System.Drawing.Point(4, 199);
            this.txtStateOfResidence.Name = "txtStateOfResidence";
            this.txtStateOfResidence.Size = new System.Drawing.Size(227, 31);
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
            this.label5.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(4, 150);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(459, 30);
            this.txtAddress.TabIndex = 101;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(105, 84);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 62;
            this.label17.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(105, 100);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(200, 30);
            this.txtPhoneNumber.TabIndex = 63;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(308, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 74;
            this.label12.Text = "Date of Birth";
            // 
            // txtDateOfBirth
            // 
            this.txtDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateOfBirth.Location = new System.Drawing.Point(311, 100);
            this.txtDateOfBirth.Name = "txtDateOfBirth";
            this.txtDateOfBirth.ShowCheckBox = true;
            this.txtDateOfBirth.Size = new System.Drawing.Size(152, 23);
            this.txtDateOfBirth.TabIndex = 73;
            // 
            // txtSex
            // 
            this.txtSex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSex.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.FormattingEnabled = true;
            this.txtSex.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.txtSex.Location = new System.Drawing.Point(4, 100);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(95, 31);
            this.txtSex.TabIndex = 70;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.SteelBlue;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 15);
            this.label14.TabIndex = 60;
            this.label14.Text = "Personal Information";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Surname";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(4, 85);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 58;
            this.label15.Text = "Sex";
            // 
            // txtSurname
            // 
            this.txtSurname.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSurname.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSurname.Location = new System.Drawing.Point(4, 52);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(227, 30);
            this.txtSurname.TabIndex = 55;
            // 
            // txtOtherNames
            // 
            this.txtOtherNames.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOtherNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherNames.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherNames.Location = new System.Drawing.Point(236, 52);
            this.txtOtherNames.Name = "txtOtherNames";
            this.txtOtherNames.Size = new System.Drawing.Size(227, 30);
            this.txtOtherNames.TabIndex = 57;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Location = new System.Drawing.Point(233, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "Other Names";
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
            this.pnlOfficialInformation.Location = new System.Drawing.Point(6, 22);
            this.pnlOfficialInformation.Name = "pnlOfficialInformation";
            this.pnlOfficialInformation.Size = new System.Drawing.Size(234, 184);
            this.pnlOfficialInformation.TabIndex = 60;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Lock_16px;
            this.pictureBox1.Location = new System.Drawing.Point(210, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 15);
            this.label4.TabIndex = 60;
            this.label4.Text = "Official Information";
            // 
            // txtHospitalNumber
            // 
            this.txtHospitalNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHospitalNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHospitalNumber.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHospitalNumber.Location = new System.Drawing.Point(3, 150);
            this.txtHospitalNumber.Name = "txtHospitalNumber";
            this.txtHospitalNumber.Size = new System.Drawing.Size(227, 30);
            this.txtHospitalNumber.TabIndex = 59;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(3, 36);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(41, 13);
            this.label40.TabIndex = 54;
            this.label40.Text = "PeP ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Hospital Number";
            // 
            // txtPepId
            // 
            this.txtPepId.BackColor = System.Drawing.Color.Gray;
            this.txtPepId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPepId.Enabled = false;
            this.txtPepId.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPepId.ForeColor = System.Drawing.Color.White;
            this.txtPepId.Location = new System.Drawing.Point(3, 52);
            this.txtPepId.Name = "txtPepId";
            this.txtPepId.Size = new System.Drawing.Size(227, 30);
            this.txtPepId.TabIndex = 55;
            // 
            // txtPreviousNumber
            // 
            this.txtPreviousNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPreviousNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreviousNumber.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviousNumber.Location = new System.Drawing.Point(3, 101);
            this.txtPreviousNumber.Name = "txtPreviousNumber";
            this.txtPreviousNumber.Size = new System.Drawing.Size(227, 30);
            this.txtPreviousNumber.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
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
            this.lblInformation.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(30, 455);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(16, 15);
            this.lblInformation.TabIndex = 132;
            this.lblInformation.Text = "...";
            // 
            // btnRefreshNfcDevice
            // 
            this.btnRefreshNfcDevice.BackColor = System.Drawing.Color.DimGray;
            this.btnRefreshNfcDevice.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshNfcDevice.ForeColor = System.Drawing.Color.White;
            this.btnRefreshNfcDevice.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshNfcDevice.Image")));
            this.btnRefreshNfcDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshNfcDevice.Location = new System.Drawing.Point(547, 11);
            this.btnRefreshNfcDevice.Name = "btnRefreshNfcDevice";
            this.btnRefreshNfcDevice.Size = new System.Drawing.Size(137, 41);
            this.btnRefreshNfcDevice.TabIndex = 133;
            this.btnRefreshNfcDevice.Text = "Refreah NFC Tag\r\nDevice";
            this.btnRefreshNfcDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshNfcDevice.UseVisualStyleBackColor = false;
            this.btnRefreshNfcDevice.Click += new System.EventHandler(this.btnRefreshNfcDevice_Click);
            // 
            // btnRefreshBioDevice
            // 
            this.btnRefreshBioDevice.BackColor = System.Drawing.Color.DimGray;
            this.btnRefreshBioDevice.Font = new System.Drawing.Font("Lato", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshBioDevice.ForeColor = System.Drawing.Color.White;
            this.btnRefreshBioDevice.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshBioDevice.Image")));
            this.btnRefreshBioDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshBioDevice.Location = new System.Drawing.Point(690, 11);
            this.btnRefreshBioDevice.Name = "btnRefreshBioDevice";
            this.btnRefreshBioDevice.Size = new System.Drawing.Size(137, 41);
            this.btnRefreshBioDevice.TabIndex = 134;
            this.btnRefreshBioDevice.Text = "Refresh Fingerprint \r\nDevice";
            this.btnRefreshBioDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshBioDevice.UseVisualStyleBackColor = false;
            this.btnRefreshBioDevice.Click += new System.EventHandler(this.btnRefreshBioDevice_Click);
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
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(833, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 41);
            this.btnClose.TabIndex = 135;
            this.btnClose.Text = "Close";
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
            // pnlWaiting
            // 
            this.pnlWaiting.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.pnlWaiting.Controls.Add(this.label13);
            this.pnlWaiting.Controls.Add(this.pictureBox2);
            this.pnlWaiting.Location = new System.Drawing.Point(-19, 210);
            this.pnlWaiting.Name = "pnlWaiting";
            this.pnlWaiting.Size = new System.Drawing.Size(1007, 75);
            this.pnlWaiting.TabIndex = 136;
            this.pnlWaiting.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(408, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(190, 15);
            this.label13.TabIndex = 106;
            this.label13.Text = "Operation In Progress. Please Wait";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Load_96px;
            this.pictureBox2.Location = new System.Drawing.Point(479, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // tmrSecureWindow
            // 
            this.tmrSecureWindow.Interval = 1000;
            this.tmrSecureWindow.Tick += new System.EventHandler(this.tmrSecureWindow_Tick);
            // 
            // SubInformationManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 485);
            this.ControlBox = false;
            this.Controls.Add(this.pnlWaiting);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.btnRefreshNfcDevice);
            this.Controls.Add(this.btnRefreshBioDevice);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBox2.PerformLayout();
            this.pnlDataControl.ResumeLayout(false);
            this.pnlDataControl.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.pnlPersonalInformation.ResumeLayout(false);
            this.pnlPersonalInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemLocalGovermentAreaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSourceResidence)).EndInit();
            this.pnlOfficialInformation.ResumeLayout(false);
            this.pnlOfficialInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemStateBindingSource)).EndInit();
            this.pnlWaiting.ResumeLayout(false);
            this.pnlWaiting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox3;
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
        private System.Windows.Forms.Panel pnlWaiting;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer tmrSecureWindow;
    }
}