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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCentral));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMessage = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnDispensationHistory = new System.Windows.Forms.Button();
            this.btnPatientManagement = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 409);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 38);
            this.panel2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Welcome, Musa Yakubu Salaudeen";
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(20, 295);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(749, 106);
            this.listBox1.TabIndex = 12;
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
            this.btnProfile.Location = new System.Drawing.Point(568, 20);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(95, 28);
            this.btnProfile.TabIndex = 7;
            this.btnProfile.Text = "Profile";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProfile.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(674, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 28);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSettings.BackColor = System.Drawing.Color.White;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Administrative_Tools_24px;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(462, 20);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(95, 28);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.VOnwNgeg;
            this.pictureBox1.Location = new System.Drawing.Point(568, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // btnMessage
            // 
            this.btnMessage.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMessage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMessage.FlatAppearance.BorderSize = 2;
            this.btnMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMessage.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessage.ForeColor = System.Drawing.Color.White;
            this.btnMessage.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Envelope_96px;
            this.btnMessage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMessage.Location = new System.Drawing.Point(359, 63);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(106, 195);
            this.btnMessage.TabIndex = 5;
            this.btnMessage.Text = "Messaging and Paging\r\n\r\n";
            this.btnMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMessage.UseVisualStyleBackColor = true;
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
            this.btnSchedule.Location = new System.Drawing.Point(246, 63);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(106, 195);
            this.btnSchedule.TabIndex = 4;
            this.btnSchedule.Text = "Schedule and Appointments\r\n\r\n";
            this.btnSchedule.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSchedule.UseVisualStyleBackColor = true;
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
            this.btnDispensationHistory.Location = new System.Drawing.Point(133, 63);
            this.btnDispensationHistory.Name = "btnDispensationHistory";
            this.btnDispensationHistory.Size = new System.Drawing.Size(106, 195);
            this.btnDispensationHistory.TabIndex = 3;
            this.btnDispensationHistory.Text = "Dispensation History\r\n\r\n";
            this.btnDispensationHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDispensationHistory.UseVisualStyleBackColor = true;
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
            this.btnPatientManagement.Location = new System.Drawing.Point(20, 63);
            this.btnPatientManagement.Name = "btnPatientManagement";
            this.btnPatientManagement.Size = new System.Drawing.Size(106, 195);
            this.btnPatientManagement.TabIndex = 2;
            this.btnPatientManagement.Text = "Information Management\r\n\r\n";
            this.btnPatientManagement.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPatientManagement.UseVisualStyleBackColor = true;
            // 
            // DataCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 467);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.btnDispensationHistory);
            this.Controls.Add(this.btnPatientManagement);
            this.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataCentral";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Text = "APIN Data Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataCentral_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPatientManagement;
        private System.Windows.Forms.Button btnDispensationHistory;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
    }
}