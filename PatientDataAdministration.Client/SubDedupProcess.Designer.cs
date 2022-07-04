using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class SubDedupProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubDedupProcess));
            this.persistNfcState = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateInformation = new System.Windows.Forms.Timer(this.components);
            this.tmrSecureWindow = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbInfoWindow = new System.Windows.Forms.RichTextBox();
            this.lblProcessGraphic = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDenyReg = new System.Windows.Forms.Button();
            this.btnAcceptReg = new System.Windows.Forms.Button();
            this.btnAbortProcess = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblProcessIndicator = new System.Windows.Forms.Label();
            this.lstBoxSuspects = new System.Windows.Forms.ListBox();
            this.tmrResponse = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblProcessGraphic)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // persistNfcState
            // 
            this.persistNfcState.Enabled = true;
            this.persistNfcState.Interval = 1000;
            // 
            // timerUpdateInformation
            // 
            this.timerUpdateInformation.Enabled = true;
            this.timerUpdateInformation.Interval = 500;
            // 
            // tmrSecureWindow
            // 
            this.tmrSecureWindow.Interval = 1000;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbInfoWindow);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(218, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 252);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information Window";
            // 
            // rtbInfoWindow
            // 
            this.rtbInfoWindow.BackColor = System.Drawing.Color.Black;
            this.rtbInfoWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInfoWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfoWindow.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfoWindow.ForeColor = System.Drawing.Color.White;
            this.rtbInfoWindow.Location = new System.Drawing.Point(3, 17);
            this.rtbInfoWindow.Name = "rtbInfoWindow";
            this.rtbInfoWindow.ReadOnly = true;
            this.rtbInfoWindow.Size = new System.Drawing.Size(569, 232);
            this.rtbInfoWindow.TabIndex = 3;
            this.rtbInfoWindow.Text = "";
            // 
            // lblProcessGraphic
            // 
            this.lblProcessGraphic.Image = global::PatientDataAdministration.Client.Properties.Resources.Glass_lines;
            this.lblProcessGraphic.Location = new System.Drawing.Point(6, 138);
            this.lblProcessGraphic.Name = "lblProcessGraphic";
            this.lblProcessGraphic.Size = new System.Drawing.Size(187, 10);
            this.lblProcessGraphic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.lblProcessGraphic.TabIndex = 4;
            this.lblProcessGraphic.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnDenyReg);
            this.flowLayoutPanel1.Controls.Add(this.btnAcceptReg);
            this.flowLayoutPanel1.Controls.Add(this.btnAbortProcess);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(569, 44);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnDenyReg
            // 
            this.btnDenyReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDenyReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDenyReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDenyReg.ForeColor = System.Drawing.Color.White;
            this.btnDenyReg.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnDenyReg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDenyReg.Location = new System.Drawing.Point(415, 3);
            this.btnDenyReg.Name = "btnDenyReg";
            this.btnDenyReg.Size = new System.Drawing.Size(151, 36);
            this.btnDenyReg.TabIndex = 1;
            this.btnDenyReg.Text = "Deny Registration";
            this.btnDenyReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDenyReg.UseVisualStyleBackColor = false;
            this.btnDenyReg.Visible = false;
            this.btnDenyReg.Click += new System.EventHandler(this.btnDenyReg_Click);
            // 
            // btnAcceptReg
            // 
            this.btnAcceptReg.BackColor = System.Drawing.Color.Green;
            this.btnAcceptReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcceptReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcceptReg.ForeColor = System.Drawing.Color.White;
            this.btnAcceptReg.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Checked_Checkbox_24px;
            this.btnAcceptReg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcceptReg.Location = new System.Drawing.Point(258, 3);
            this.btnAcceptReg.Name = "btnAcceptReg";
            this.btnAcceptReg.Size = new System.Drawing.Size(151, 36);
            this.btnAcceptReg.TabIndex = 2;
            this.btnAcceptReg.Text = "Accept Registration";
            this.btnAcceptReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcceptReg.UseVisualStyleBackColor = false;
            this.btnAcceptReg.Visible = false;
            this.btnAcceptReg.Click += new System.EventHandler(this.btnAbortProcess_Click);
            // 
            // btnAbortProcess
            // 
            this.btnAbortProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAbortProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbortProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbortProcess.ForeColor = System.Drawing.Color.White;
            this.btnAbortProcess.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Empty_Trash_24px;
            this.btnAbortProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbortProcess.Location = new System.Drawing.Point(101, 3);
            this.btnAbortProcess.Name = "btnAbortProcess";
            this.btnAbortProcess.Size = new System.Drawing.Size(151, 36);
            this.btnAbortProcess.TabIndex = 0;
            this.btnAbortProcess.Text = "Abort Process";
            this.btnAbortProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbortProcess.UseVisualStyleBackColor = false;
            this.btnAbortProcess.Click += new System.EventHandler(this.btnAcceptProcess_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblProcessGraphic);
            this.groupBox3.Controls.Add(this.lblProcessIndicator);
            this.groupBox3.Controls.Add(this.lstBoxSuspects);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 305);
            this.groupBox3.TabIndex = 137;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Suspected Duplicates";
            // 
            // lblProcessIndicator
            // 
            this.lblProcessIndicator.BackColor = System.Drawing.Color.SteelBlue;
            this.lblProcessIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProcessIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessIndicator.ForeColor = System.Drawing.Color.White;
            this.lblProcessIndicator.Location = new System.Drawing.Point(6, 272);
            this.lblProcessIndicator.Name = "lblProcessIndicator";
            this.lblProcessIndicator.Size = new System.Drawing.Size(187, 24);
            this.lblProcessIndicator.TabIndex = 4;
            this.lblProcessIndicator.Text = "Submitted";
            this.lblProcessIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstBoxSuspects
            // 
            this.lstBoxSuspects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxSuspects.FormattingEnabled = true;
            this.lstBoxSuspects.ItemHeight = 15;
            this.lstBoxSuspects.Location = new System.Drawing.Point(6, 21);
            this.lstBoxSuspects.Name = "lstBoxSuspects";
            this.lstBoxSuspects.Size = new System.Drawing.Size(187, 244);
            this.lstBoxSuspects.TabIndex = 136;
            this.lstBoxSuspects.Click += new System.EventHandler(this.lstBoxSuspects_Click);
            this.lstBoxSuspects.SelectedIndexChanged += new System.EventHandler(this.lstBoxSuspects_SelectedIndexChanged);
            // 
            // tmrResponse
            // 
            this.tmrResponse.Enabled = true;
            this.tmrResponse.Interval = 1000;
            this.tmrResponse.Tick += new System.EventHandler(this.tmrResponse_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(218, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 64);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // SubDedupProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 375);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubDedupProcess";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.ShowIcon = false;
            this.Text = "Deduplication Processing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubDedupProcess_FormClosing);
            this.Load += new System.EventHandler(this.SubDedupProcess_Load);
            this.Shown += new System.EventHandler(this.SubDedupProcess_Shown);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblProcessGraphic)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer persistNfcState;
        private System.Windows.Forms.Timer timerUpdateInformation;
        private System.Windows.Forms.Timer tmrSecureWindow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbInfoWindow;
        private System.Windows.Forms.Button btnAcceptReg;
        private System.Windows.Forms.Button btnDenyReg;
        private System.Windows.Forms.Button btnAbortProcess;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstBoxSuspects;
        private System.Windows.Forms.Timer tmrResponse;
        private System.Windows.Forms.Label lblProcessIndicator;
        private System.Windows.Forms.PictureBox lblProcessGraphic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}