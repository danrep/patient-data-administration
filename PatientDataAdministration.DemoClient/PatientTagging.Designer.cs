namespace PatientDataAdministration.DemoClient
{
    partial class PatientTagging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientTagging));
            this.label1 = new System.Windows.Forms.Label();
            this.gradientPanel11 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.txtTagUid = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBioDeviceInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNfcDeviceInfo = new System.Windows.Forms.Label();
            this.lblTagData = new System.Windows.Forms.Label();
            this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.txtAddress = new System.Windows.Forms.Label();
            this.txtPepId = new System.Windows.Forms.Label();
            this.txtInfoMedical = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.Label();
            this.txtInfoPersonal = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.Label();
            this.txtPassport = new System.Windows.Forms.PictureBox();
            this.gradientLabel2 = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.gradientPanel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.monthCalendarAdv1 = new Syncfusion.Windows.Forms.Tools.MonthCalendarAdv();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tmrCheckBioCon = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrCheckNfcDevice = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel11)).BeginInit();
            this.gradientPanel11.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).BeginInit();
            this.gradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthCalendarAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(-4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(997, 5);
            this.label1.TabIndex = 54;
            // 
            // gradientPanel11
            // 
            this.gradientPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientPanel11.BackColor = System.Drawing.Color.White;
            this.gradientPanel11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(178)))), ((int)(((byte)(227)))));
            this.gradientPanel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPanel11.Controls.Add(this.button4);
            this.gradientPanel11.Controls.Add(this.txtTagUid);
            this.gradientPanel11.Controls.Add(this.flowLayoutPanel2);
            this.gradientPanel11.Controls.Add(this.lblTagData);
            this.gradientPanel11.Controls.Add(this.gradientPanel1);
            this.gradientPanel11.Controls.Add(this.gradientLabel2);
            this.gradientPanel11.Controls.Add(this.label1);
            this.gradientPanel11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientPanel11.Location = new System.Drawing.Point(179, 7);
            this.gradientPanel11.Name = "gradientPanel11";
            this.gradientPanel11.Size = new System.Drawing.Size(772, 354);
            this.gradientPanel11.TabIndex = 125;
            this.gradientPanel11.Click += new System.EventHandler(this.PatientInfo_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Maroon;
            this.button4.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.button4.Location = new System.Drawing.Point(6, 280);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 34);
            this.button4.TabIndex = 136;
            this.button4.Text = "Format Card";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtTagUid
            // 
            this.txtTagUid.AutoSize = true;
            this.txtTagUid.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTagUid.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.txtTagUid.Location = new System.Drawing.Point(8, 260);
            this.txtTagUid.Name = "txtTagUid";
            this.txtTagUid.Size = new System.Drawing.Size(20, 17);
            this.txtTagUid.TabIndex = 133;
            this.txtTagUid.Text = "...";
            this.txtTagUid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblBioDeviceInfo);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.lblNfcDeviceInfo);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 320);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(770, 32);
            this.flowLayoutPanel2.TabIndex = 129;
            // 
            // lblBioDeviceInfo
            // 
            this.lblBioDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBioDeviceInfo.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBioDeviceInfo.Location = new System.Drawing.Point(3, 0);
            this.lblBioDeviceInfo.Name = "lblBioDeviceInfo";
            this.lblBioDeviceInfo.Size = new System.Drawing.Size(368, 32);
            this.lblBioDeviceInfo.TabIndex = 129;
            this.lblBioDeviceInfo.Text = "...";
            this.lblBioDeviceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(377, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 17);
            this.label2.TabIndex = 134;
            this.label2.Text = "|";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNfcDeviceInfo
            // 
            this.lblNfcDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNfcDeviceInfo.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNfcDeviceInfo.Location = new System.Drawing.Point(396, 0);
            this.lblNfcDeviceInfo.Name = "lblNfcDeviceInfo";
            this.lblNfcDeviceInfo.Size = new System.Drawing.Size(368, 32);
            this.lblNfcDeviceInfo.TabIndex = 130;
            this.lblNfcDeviceInfo.Text = "...";
            this.lblNfcDeviceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTagData
            // 
            this.lblTagData.AutoSize = true;
            this.lblTagData.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagData.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.lblTagData.Location = new System.Drawing.Point(6, 237);
            this.lblTagData.Name = "lblTagData";
            this.lblTagData.Size = new System.Drawing.Size(28, 23);
            this.lblTagData.TabIndex = 132;
            this.lblTagData.Text = "...";
            this.lblTagData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPanel1.Controls.Add(this.txtAddress);
            this.gradientPanel1.Controls.Add(this.txtPepId);
            this.gradientPanel1.Controls.Add(this.txtInfoMedical);
            this.gradientPanel1.Controls.Add(this.txtSiteName);
            this.gradientPanel1.Controls.Add(this.txtInfoPersonal);
            this.gradientPanel1.Controls.Add(this.txtFullName);
            this.gradientPanel1.Controls.Add(this.txtPassport);
            this.gradientPanel1.Location = new System.Drawing.Point(11, 37);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(751, 197);
            this.gradientPanel1.TabIndex = 99;
            // 
            // txtAddress
            // 
            this.txtAddress.AutoSize = true;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(133, 103);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(28, 23);
            this.txtAddress.TabIndex = 84;
            this.txtAddress.Text = "...";
            this.txtAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPepId
            // 
            this.txtPepId.AutoSize = true;
            this.txtPepId.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPepId.Location = new System.Drawing.Point(133, 172);
            this.txtPepId.Name = "txtPepId";
            this.txtPepId.Size = new System.Drawing.Size(20, 17);
            this.txtPepId.TabIndex = 83;
            this.txtPepId.Text = "...";
            this.txtPepId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfoMedical
            // 
            this.txtInfoMedical.AutoSize = true;
            this.txtInfoMedical.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoMedical.Location = new System.Drawing.Point(133, 129);
            this.txtInfoMedical.Name = "txtInfoMedical";
            this.txtInfoMedical.Size = new System.Drawing.Size(28, 23);
            this.txtInfoMedical.TabIndex = 82;
            this.txtInfoMedical.Text = "...";
            this.txtInfoMedical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSiteName
            // 
            this.txtSiteName.AutoSize = true;
            this.txtSiteName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSiteName.Location = new System.Drawing.Point(133, 5);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(20, 17);
            this.txtSiteName.TabIndex = 81;
            this.txtSiteName.Text = "...";
            this.txtSiteName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfoPersonal
            // 
            this.txtInfoPersonal.AutoSize = true;
            this.txtInfoPersonal.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoPersonal.Location = new System.Drawing.Point(133, 77);
            this.txtInfoPersonal.Name = "txtInfoPersonal";
            this.txtInfoPersonal.Size = new System.Drawing.Size(28, 23);
            this.txtInfoPersonal.TabIndex = 80;
            this.txtInfoPersonal.Text = "...";
            this.txtInfoPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            this.txtFullName.AutoSize = true;
            this.txtFullName.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(133, 22);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(39, 33);
            this.txtFullName.TabIndex = 79;
            this.txtFullName.Text = "...";
            this.txtFullName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassport
            // 
            this.txtPassport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassport.Image = global::PatientDataAdministration.DemoClient.Properties.Resources.icons8_Image_File_48px;
            this.txtPassport.Location = new System.Drawing.Point(-1, -1);
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(128, 197);
            this.txtPassport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.txtPassport.TabIndex = 71;
            this.txtPassport.TabStop = false;
            // 
            // gradientLabel2
            // 
            this.gradientLabel2.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Vertical, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(240)))), ((int)(((byte)(247))))), System.Drawing.Color.LightCyan);
            this.gradientLabel2.BeforeTouchSize = new System.Drawing.Size(224, 23);
            this.gradientLabel2.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.gradientLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientLabel2.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientLabel2.Location = new System.Drawing.Point(11, 8);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(224, 23);
            this.gradientLabel2.TabIndex = 91;
            this.gradientLabel2.Text = "Patient Information";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gradientLabel2.UseMnemonic = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DimGray;
            this.btnClear.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnClear.Location = new System.Drawing.Point(0, 328);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(173, 34);
            this.btnClear.TabIndex = 128;
            this.btnClear.Text = "Clear/Reset";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPanel2.Controls.Add(this.button1);
            this.gradientPanel2.Controls.Add(this.btnCapture);
            this.gradientPanel2.Controls.Add(this.pictureBox1);
            this.gradientPanel2.Location = new System.Drawing.Point(0, 7);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(173, 235);
            this.gradientPanel2.TabIndex = 98;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(139, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 30);
            this.button1.TabIndex = 78;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapture.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapture.ForeColor = System.Drawing.Color.White;
            this.btnCapture.Location = new System.Drawing.Point(4, 3);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(136, 30);
            this.btnCapture.TabIndex = 77;
            this.btnCapture.Text = "Capture Fingerprint";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::PatientDataAdministration.DemoClient.Properties.Resources.icons8_Fingerprint_48px;
            this.pictureBox1.Location = new System.Drawing.Point(5, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 76;
            this.pictureBox1.TabStop = false;
            // 
            // monthCalendarAdv1
            // 
            this.monthCalendarAdv1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.monthCalendarAdv1.Culture = new System.Globalization.CultureInfo("");
            this.monthCalendarAdv1.DaysFont = new System.Drawing.Font("Verdana", 8F);
            this.monthCalendarAdv1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendarAdv1.HeaderFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendarAdv1.HighlightColor = System.Drawing.Color.Black;
            this.monthCalendarAdv1.Iso8601CalenderFormat = false;
            this.monthCalendarAdv1.Location = new System.Drawing.Point(477, 100);
            this.monthCalendarAdv1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.monthCalendarAdv1.Name = "monthCalendarAdv1";
            this.monthCalendarAdv1.Size = new System.Drawing.Size(239, 187);
            this.monthCalendarAdv1.TabIndex = 75;
            this.monthCalendarAdv1.WeekFont = new System.Drawing.Font("Verdana", 8F);
            // 
            // 
            // 
            this.monthCalendarAdv1.NoneButton.BeforeTouchSize = new System.Drawing.Size(75, 23);
            this.monthCalendarAdv1.NoneButton.IsBackStageButton = false;
            this.monthCalendarAdv1.NoneButton.Location = new System.Drawing.Point(167, 0);
            this.monthCalendarAdv1.NoneButton.Size = new System.Drawing.Size(72, 20);
            this.monthCalendarAdv1.NoneButton.Text = "None";
            // 
            // 
            // 
            this.monthCalendarAdv1.TodayButton.BeforeTouchSize = new System.Drawing.Size(75, 23);
            this.monthCalendarAdv1.TodayButton.IsBackStageButton = false;
            this.monthCalendarAdv1.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.monthCalendarAdv1.TodayButton.Size = new System.Drawing.Size(167, 20);
            this.monthCalendarAdv1.TodayButton.Text = "Today";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Patient Passport";
            this.openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif;" +
    " *.png";
            // 
            // tmrCheckBioCon
            // 
            this.tmrCheckBioCon.Enabled = true;
            this.tmrCheckBioCon.Interval = 3000;
            this.tmrCheckBioCon.Tick += new System.EventHandler(this.tmrCheckBioCon_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Visible = true;
            // 
            // tmrCheckNfcDevice
            // 
            this.tmrCheckNfcDevice.Enabled = true;
            this.tmrCheckNfcDevice.Interval = 3000;
            this.tmrCheckNfcDevice.Tick += new System.EventHandler(this.tmrCheckNfcDevice_Tick);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DimGray;
            this.button2.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.button2.Location = new System.Drawing.Point(0, 248);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 34);
            this.button2.TabIndex = 134;
            this.button2.Text = "Attach Card";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DimGray;
            this.button3.Font = new System.Drawing.Font("Calibri Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.button3.Location = new System.Drawing.Point(0, 288);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 34);
            this.button3.TabIndex = 135;
            this.button3.Text = "Detach Card";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // PatientTagging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 362);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gradientPanel2);
            this.Controls.Add(this.gradientPanel11);
            this.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PatientTagging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "Patient Information Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientInfo_FormClosing);
            this.Load += new System.EventHandler(this.PatientInfo_Load);
            this.Click += new System.EventHandler(this.PatientInfo_Click);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel11)).EndInit();
            this.gradientPanel11.ResumeLayout(false);
            this.gradientPanel11.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).EndInit();
            this.gradientPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthCalendarAdv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox txtPassport;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel11;
        private Syncfusion.Windows.Forms.Tools.MonthCalendarAdv monthCalendarAdv1;
        private Syncfusion.Windows.Forms.Tools.GradientLabel gradientLabel2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer tmrCheckBioCon;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel2;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblBioDeviceInfo;
        private System.Windows.Forms.Label txtInfoPersonal;
        private System.Windows.Forms.Label txtFullName;
        private System.Windows.Forms.Label txtSiteName;
        private System.Windows.Forms.Label txtInfoMedical;
        private System.Windows.Forms.Label txtPepId;
        private System.Windows.Forms.Label txtAddress;
        private System.Windows.Forms.Timer tmrCheckNfcDevice;
        private System.Windows.Forms.Label lblNfcDeviceInfo;
        private System.Windows.Forms.Label lblTagData;
        private System.Windows.Forms.Label txtTagUid;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
    }
}