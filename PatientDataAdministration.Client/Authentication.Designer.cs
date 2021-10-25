using MetroFramework.Controls;

namespace PatientDataAdministration.Client
{
    partial class Authentication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Authentication));
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tmrFeedBack = new System.Windows.Forms.Timer(this.components);
            this.pnlWaiting = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlWaiting.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(11, 81);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(432, 38);
            this.txtUserName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(11, 130);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(432, 38);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Silver;
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Administrative_Tools_24px;
            this.btnSetting.Location = new System.Drawing.Point(306, 184);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(43, 31);
            this.btnSetting.TabIndex = 16;
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.Silver;
            this.btnLogIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogIn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLogIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Key_24px;
            this.btnLogIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogIn.Location = new System.Drawing.Point(353, 184);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(90, 31);
            this.btnLogIn.TabIndex = 17;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::PatientDataAdministration.Client.Properties.Resources.icons8_Close_Window_24px;
            this.btnClose.Location = new System.Drawing.Point(403, 20);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 9;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tmrFeedBack
            // 
            this.tmrFeedBack.Enabled = true;
            this.tmrFeedBack.Interval = 500;
            this.tmrFeedBack.Tick += new System.EventHandler(this.tmrFeedBack_Tick);
            // 
            // pnlWaiting
            // 
            this.pnlWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnlWaiting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWaiting.Controls.Add(this.label13);
            this.pnlWaiting.Location = new System.Drawing.Point(-2, 93);
            this.pnlWaiting.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.pnlWaiting.Name = "pnlWaiting";
            this.pnlWaiting.Size = new System.Drawing.Size(459, 34);
            this.pnlWaiting.TabIndex = 137;
            this.pnlWaiting.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(139, 10);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(172, 13);
            this.label13.TabIndex = 106;
            this.label13.Text = "Operation In Progress. Please Wait";
            // 
            // Authentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 229);
            this.ControlBox = false;
            this.Controls.Add(this.pnlWaiting);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Authentication";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 12);
            this.Resizable = false;
            this.Text = "Authentication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Authentication_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Authentication_KeyDown);
            this.pnlWaiting.ResumeLayout(false);
            this.pnlWaiting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Timer tmrFeedBack;
        private System.Windows.Forms.Panel pnlWaiting;
        private System.Windows.Forms.Label label13;
    }
}