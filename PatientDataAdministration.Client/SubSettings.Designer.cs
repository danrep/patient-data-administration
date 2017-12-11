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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubSettings));
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.btnSaveConnections = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemoteApi = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblInformation = new System.Windows.Forms.Label();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Location = new System.Drawing.Point(30, 77);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(588, 250);
            this.metroTabControl1.TabIndex = 0;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.btnSaveConnections);
            this.metroTabPage1.Controls.Add(this.label11);
            this.metroTabPage1.Controls.Add(this.txtRemoteApi);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(580, 211);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Connections";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // btnSaveConnections
            // 
            this.btnSaveConnections.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveConnections.BackColor = System.Drawing.Color.White;
            this.btnSaveConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveConnections.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnections.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Save_24px;
            this.btnSaveConnections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveConnections.Location = new System.Drawing.Point(471, 167);
            this.btnSaveConnections.Name = "btnSaveConnections";
            this.btnSaveConnections.Size = new System.Drawing.Size(106, 41);
            this.btnSaveConnections.TabIndex = 58;
            this.btnSaveConnections.Text = "Save";
            this.btnSaveConnections.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveConnections.UseVisualStyleBackColor = false;
            this.btnSaveConnections.Click += new System.EventHandler(this.btnSaveConnections_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Remote API";
            // 
            // txtRemoteApi
            // 
            this.txtRemoteApi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRemoteApi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteApi.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemoteApi.Location = new System.Drawing.Point(3, 20);
            this.txtRemoteApi.Name = "txtRemoteApi";
            this.txtRemoteApi.Size = new System.Drawing.Size(574, 30);
            this.txtRemoteApi.TabIndex = 57;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(512, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 41);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblInformation.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(37, 330);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(16, 13);
            this.lblInformation.TabIndex = 57;
            this.lblInformation.Text = "...";
            // 
            // SubSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 355);
            this.ControlBox = false;
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
            this.Shown += new System.EventHandler(this.SubSettings_Shown);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
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
    }
}