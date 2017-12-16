namespace PatientDataAdministration.Client
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.pnlUpdate = new System.Windows.Forms.Panel();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.lblUpdateInfoHead = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlUpdate
            // 
            this.pnlUpdate.Controls.Add(this.lblUpdateInfo);
            this.pnlUpdate.Controls.Add(this.lblUpdateInfoHead);
            this.pnlUpdate.Controls.Add(this.btnProfile);
            this.pnlUpdate.Controls.Add(this.btnYes);
            this.pnlUpdate.Controls.Add(this.pictureBox2);
            this.pnlUpdate.Location = new System.Drawing.Point(125, 87);
            this.pnlUpdate.Name = "pnlUpdate";
            this.pnlUpdate.Size = new System.Drawing.Size(349, 122);
            this.pnlUpdate.TabIndex = 3;
            this.pnlUpdate.Visible = false;
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.BackColor = System.Drawing.Color.White;
            this.lblUpdateInfo.Enabled = false;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(117, 37);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(218, 39);
            this.lblUpdateInfo.TabIndex = 4;
            this.lblUpdateInfo.Text = "...";
            this.lblUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUpdateInfoHead
            // 
            this.lblUpdateInfoHead.BackColor = System.Drawing.Color.White;
            this.lblUpdateInfoHead.Enabled = false;
            this.lblUpdateInfoHead.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateInfoHead.Location = new System.Drawing.Point(117, 14);
            this.lblUpdateInfoHead.Name = "lblUpdateInfoHead";
            this.lblUpdateInfoHead.Size = new System.Drawing.Size(218, 23);
            this.lblUpdateInfoHead.TabIndex = 4;
            this.lblUpdateInfoHead.Text = "New Update";
            this.lblUpdateInfoHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnProfile
            // 
            this.btnProfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProfile.BackColor = System.Drawing.Color.White;
            this.btnProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProfile.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.Location = new System.Drawing.Point(117, 79);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(106, 31);
            this.btnProfile.TabIndex = 9;
            this.btnProfile.Text = "No";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnYes.BackColor = System.Drawing.Color.White;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Checked_Checkbox_24px;
            this.btnYes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYes.Location = new System.Drawing.Point(229, 79);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(106, 31);
            this.btnYes.TabIndex = 8;
            this.btnYes.Text = "Yes";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PatientDataAdministration.Client.Properties.Resources.Downloading_Updates_96px;
            this.pictureBox2.Location = new System.Drawing.Point(14, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(96, 96);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.loadingBackground;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(599, 296);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 296);
            this.ControlBox = false;
            this.Controls.Add(this.pnlUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.Text = "APIN PDA Loading...";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Splash_FormClosed);
            this.Load += new System.EventHandler(this.Splash_Load);
            this.Shown += new System.EventHandler(this.Splash_Shown);
            this.pnlUpdate.ResumeLayout(false);
            this.pnlUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlUpdate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Label lblUpdateInfoHead;
        private System.Windows.Forms.Label lblUpdateInfo;
    }
}