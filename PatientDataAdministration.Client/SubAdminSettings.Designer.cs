using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class SubAdminSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubAdminSettings));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFormatNfc = new System.Windows.Forms.Button();
            this.lblInformation = new System.Windows.Forms.Label();
            this.tmrInformation = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(639, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 136;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFormatNfc
            // 
            this.btnFormatNfc.BackColor = System.Drawing.Color.Black;
            this.btnFormatNfc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFormatNfc.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFormatNfc.FlatAppearance.BorderSize = 2;
            this.btnFormatNfc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormatNfc.Font = new System.Drawing.Font("Lato", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormatNfc.ForeColor = System.Drawing.Color.White;
            this.btnFormatNfc.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_NFC_96px;
            this.btnFormatNfc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFormatNfc.Location = new System.Drawing.Point(30, 79);
            this.btnFormatNfc.Name = "btnFormatNfc";
            this.btnFormatNfc.Size = new System.Drawing.Size(106, 195);
            this.btnFormatNfc.TabIndex = 137;
            this.btnFormatNfc.Text = "Format NFC\r\n\r\n";
            this.btnFormatNfc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFormatNfc.UseVisualStyleBackColor = false;
            this.btnFormatNfc.Click += new System.EventHandler(this.btnFormatNfc_Click);
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(29, 57);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(0, 13);
            this.lblInformation.TabIndex = 138;
            // 
            // tmrInformation
            // 
            this.tmrInformation.Enabled = true;
            this.tmrInformation.Tick += new System.EventHandler(this.tmrInformation_Tick);
            // 
            // SubAdminSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 302);
            this.ControlBox = false;
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.btnFormatNfc);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubAdminSettings";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.Text = "Administrator Settings and Operations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFormatNfc;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Timer tmrInformation;
    }
}