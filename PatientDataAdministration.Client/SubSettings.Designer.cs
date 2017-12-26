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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCurrentOnDemandSync = new MetroFramework.Controls.MetroCheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemoteApi = new System.Windows.Forms.TextBox();
            this.lblInformation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSyncSystemData = new System.Windows.Forms.Button();
            this.btnSaveConnections = new System.Windows.Forms.Button();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.localPDADataSet = new PatientDataAdministration.Client.LocalPDADataSet();
            this.systemSiteDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.system_SiteDataTableAdapter = new PatientDataAdministration.Client.LocalPDADataSetTableAdapters.System_SiteDataTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveSite = new System.Windows.Forms.Button();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Location = new System.Drawing.Point(30, 65);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(588, 271);
            this.metroTabControl1.TabIndex = 0;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BackColor = System.Drawing.Color.Gray;
            this.metroTabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage1.Controls.Add(this.label3);
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
            this.metroTabPage1.Text = "Connections && Synchronization";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
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
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(352, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 26);
            this.label1.TabIndex = 60;
            this.label1.Text = "Minimum Requirements for this Feature are:\r\nRAM of 8Gb and CPU speed of over 2.5G" +
    "Hz";
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
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Remote Server";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(352, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 26);
            this.label3.TabIndex = 63;
            this.label3.Text = "Pull Settings and Local Data Required for \r\nOperations";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(512, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 41);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.btnSyncSystemData.Location = new System.Drawing.Point(352, 91);
            this.btnSyncSystemData.Name = "btnSyncSystemData";
            this.btnSyncSystemData.Size = new System.Drawing.Size(218, 32);
            this.btnSyncSystemData.TabIndex = 59;
            this.btnSyncSystemData.Text = "Sync System Data";
            this.btnSyncSystemData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSyncSystemData.UseVisualStyleBackColor = false;
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
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.groupBox2);
            this.metroTabPage2.Controls.Add(this.comboBox1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(580, 232);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Operational Site";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.systemSiteDataBindingSource;
            this.comboBox1.DisplayMember = "SiteNameOfficial";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Lato", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(577, 31);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.ValueMember = "Id";
            // 
            // localPDADataSet
            // 
            this.localPDADataSet.DataSetName = "LocalPDADataSet";
            this.localPDADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // systemSiteDataBindingSource
            // 
            this.systemSiteDataBindingSource.DataMember = "System_SiteData";
            this.systemSiteDataBindingSource.DataSource = this.localPDADataSet;
            // 
            // system_SiteDataTableAdapter
            // 
            this.system_SiteDataTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnSaveSite);
            this.groupBox2.Location = new System.Drawing.Point(8, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 58);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
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
            this.btnSaveSite.Location = new System.Drawing.Point(457, 17);
            this.btnSaveSite.Name = "btnSaveSite";
            this.btnSaveSite.Size = new System.Drawing.Size(106, 32);
            this.btnSaveSite.TabIndex = 58;
            this.btnSaveSite.Text = "Save";
            this.btnSaveSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSite.UseVisualStyleBackColor = false;
            // 
            // SubSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 360);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubSettings_FormClosing);
            this.Load += new System.EventHandler(this.SubSettings_Load);
            this.Shown += new System.EventHandler(this.SubSettings_Shown);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.localPDADataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemSiteDataBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSyncSystemData;
        private MetroTabPage metroTabPage2;
        private System.Windows.Forms.ComboBox comboBox1;
        private LocalPDADataSet localPDADataSet;
        private System.Windows.Forms.BindingSource systemSiteDataBindingSource;
        private LocalPDADataSetTableAdapters.System_SiteDataTableAdapter system_SiteDataTableAdapter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSaveSite;
    }
}