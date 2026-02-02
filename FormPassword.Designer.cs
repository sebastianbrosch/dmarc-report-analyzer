namespace DMARCReportAnalyzer
{
    partial class FormPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            txtPassword = new TextBox();
            lblPassword = new Label();
            BtnOK = new Button();
            BtnCancel = new Button();
            CbNoPassword = new CheckBox();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(75, 12);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(251, 23);
            txtPassword.TabIndex = 0;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(12, 15);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password";
            // 
            // BtnOK
            // 
            BtnOK.Location = new Point(170, 66);
            BtnOK.Name = "BtnOK";
            BtnOK.Size = new Size(75, 23);
            BtnOK.TabIndex = 2;
            BtnOK.Text = "OK";
            BtnOK.UseVisualStyleBackColor = true;
            BtnOK.Click += BtnOK_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(251, 66);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(75, 23);
            BtnCancel.TabIndex = 3;
            BtnCancel.Text = "Abbrechen";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // CbNoPassword
            // 
            CbNoPassword.AutoSize = true;
            CbNoPassword.Location = new Point(75, 41);
            CbNoPassword.Name = "CbNoPassword";
            CbNoPassword.Size = new Size(95, 19);
            CbNoPassword.TabIndex = 4;
            CbNoPassword.Text = "No Password";
            CbNoPassword.UseVisualStyleBackColor = true;
            CbNoPassword.CheckStateChanged += CbNoPassword_CheckStateChanged;
            // 
            // FormPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 96);
            Controls.Add(CbNoPassword);
            Controls.Add(BtnCancel);
            Controls.Add(BtnOK);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormPassword";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPassword;
        private Label lblPassword;
        private Button BtnOK;
        private Button BtnCancel;
        private CheckBox CbNoPassword;
    }
}