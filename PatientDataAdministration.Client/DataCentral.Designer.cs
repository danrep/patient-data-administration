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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPatientManagement = new System.Windows.Forms.Button();
            this.btnDispensationHistory = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnMessage = new System.Windows.Forms.Button();
            this.btnOperations = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.picConnectionAvailable = new System.Windows.Forms.PictureBox();
            this.picSyncInProcess = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblUserInformation = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(20, 295);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(782, 106);
            this.listBox1.TabIndex = 12;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnPatientManagement);
            this.flowLayoutPanel1.Controls.Add(this.btnDispensationHistory);
            this.flowLayoutPanel1.Controls.Add(this.btnSchedule);
            this.flowLayoutPanel1.Controls.Add(this.btnMessage);
            this.flowLayoutPanel1.Controls.Add(this.btnOperations);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 63);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(694, 195);
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
            this.btnDispensationHistory.Text = "Dispensation History\r\n\r\n";
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
            this.btnSchedule.Text = "Schedule and Appointments\r\n\r\n";
            this.btnSchedule.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSchedule.UseVisualStyleBackColor = true;
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
            this.btnMessage.Location = new System.Drawing.Point(339, 3);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(106, 195);
            this.btnMessage.TabIndex = 5;
            this.btnMessage.Text = "Messaging and Paging\r\n\r\n";
            this.btnMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMessage.UseVisualStyleBackColor = true;
            // 
            // btnOperations
            // 
            this.btnOperations.BackgroundImage = global::PatientDataAdministration.Client.Properties.Resources.W6Fuk;
            this.btnOperations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOperations.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOperations.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOperations.FlatAppearance.BorderSize = 2;
            this.btnOperations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOperations.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOperations.ForeColor = System.Drawing.Color.White;
            this.btnOperations.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Flow_Chart_96px;
            this.btnOperations.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOperations.Location = new System.Drawing.Point(451, 3);
            this.btnOperations.Name = "btnOperations";
            this.btnOperations.Size = new System.Drawing.Size(106, 195);
            this.btnOperations.TabIndex = 14;
            this.btnOperations.Text = "Queries and Operations\r\n\r\n";
            this.btnOperations.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOperations.UseVisualStyleBackColor = true;
            this.btnOperations.Click += new System.EventHandler(this.btnOperations_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PatientDataAdministration.Client.Properties.Resources.VOnwNgeg;
            this.pictureBox1.Location = new System.Drawing.Point(584, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 195);
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
            this.btnProfile.Location = new System.Drawing.Point(584, 16);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(106, 41);
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
            this.btnClose.Location = new System.Drawing.Point(696, 16);
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
            this.flowLayoutPanel2.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(645, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(134, 32);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // picConnectionAvailable
            // 
            this.picConnectionAvailable.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Server_20px;
            this.picConnectionAvailable.Location = new System.Drawing.Point(107, 3);
            this.picConnectionAvailable.Name = "picConnectionAvailable";
            this.picConnectionAvailable.Size = new System.Drawing.Size(24, 24);
            this.picConnectionAvailable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picConnectionAvailable.TabIndex = 11;
            this.picConnectionAvailable.TabStop = false;
            // 
            // picSyncInProcess
            // 
            this.picSyncInProcess.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Sync_20px;
            this.picSyncInProcess.Location = new System.Drawing.Point(77, 3);
            this.picSyncInProcess.Name = "picSyncInProcess";
            this.picSyncInProcess.Size = new System.Drawing.Size(24, 24);
            this.picSyncInProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSyncInProcess.TabIndex = 12;
            this.picSyncInProcess.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Download_20px;
            this.pictureBox2.Location = new System.Drawing.Point(47, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
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
            // DataCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 467);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataCentral";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Text = "APIN Data Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataCentral_FormClosing);
            this.Load += new System.EventHandler(this.DataCentral_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSyncInProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPatientManagement;
        private System.Windows.Forms.Button btnDispensationHistory;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblUserInformation;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOperations;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox picConnectionAvailable;
        private System.Windows.Forms.PictureBox picSyncInProcess;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}