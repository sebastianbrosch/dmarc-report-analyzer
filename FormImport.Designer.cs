namespace DMARCReportAnalyzer
{
    partial class FormImport
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
            grpMailServerIMAP = new GroupBox();
            cmbName = new ComboBox();
            lblName = new Label();
            lblEncryption = new Label();
            cmbEncryption = new ComboBox();
            lblServerPort = new Label();
            lblSourceFolder = new Label();
            lblPassword = new Label();
            lblUsername = new Label();
            lblIncomingServer = new Label();
            cmbServerPort = new ComboBox();
            txtSourceFolder = new TextBox();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            txtIncomingServer = new TextBox();
            chkMarkAsRead = new CheckBox();
            txtArchiveFolder = new TextBox();
            btnImport = new Button();
            grpActionAfterImport = new GroupBox();
            lblArchiveFolder = new Label();
            chkDeleteMessage = new CheckBox();
            btnSave = new Button();
            grpMailServerIMAP.SuspendLayout();
            grpActionAfterImport.SuspendLayout();
            SuspendLayout();
            // 
            // grpMailServerIMAP
            // 
            grpMailServerIMAP.Controls.Add(cmbName);
            grpMailServerIMAP.Controls.Add(lblName);
            grpMailServerIMAP.Controls.Add(lblEncryption);
            grpMailServerIMAP.Controls.Add(cmbEncryption);
            grpMailServerIMAP.Controls.Add(lblServerPort);
            grpMailServerIMAP.Controls.Add(lblSourceFolder);
            grpMailServerIMAP.Controls.Add(lblPassword);
            grpMailServerIMAP.Controls.Add(lblUsername);
            grpMailServerIMAP.Controls.Add(lblIncomingServer);
            grpMailServerIMAP.Controls.Add(cmbServerPort);
            grpMailServerIMAP.Controls.Add(txtSourceFolder);
            grpMailServerIMAP.Controls.Add(txtPassword);
            grpMailServerIMAP.Controls.Add(txtUsername);
            grpMailServerIMAP.Controls.Add(txtIncomingServer);
            grpMailServerIMAP.Location = new Point(12, 12);
            grpMailServerIMAP.Name = "grpMailServerIMAP";
            grpMailServerIMAP.Size = new Size(478, 172);
            grpMailServerIMAP.TabIndex = 0;
            grpMailServerIMAP.TabStop = false;
            grpMailServerIMAP.Text = "Mail Server (IMAP)";
            // 
            // cmbName
            // 
            cmbName.FormattingEnabled = true;
            cmbName.Location = new Point(113, 22);
            cmbName.Name = "cmbName";
            cmbName.Size = new Size(352, 23);
            cmbName.TabIndex = 16;
            cmbName.SelectionChangeCommitted += cmbName_SelectionChangeCommitted;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(68, 25);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 15;
            lblName.Text = "Name";
            // 
            // lblEncryption
            // 
            lblEncryption.AutoSize = true;
            lblEncryption.Location = new Point(312, 83);
            lblEncryption.Name = "lblEncryption";
            lblEncryption.Size = new Size(64, 15);
            lblEncryption.TabIndex = 13;
            lblEncryption.Text = "Encryption";
            // 
            // cmbEncryption
            // 
            cmbEncryption.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEncryption.FormattingEnabled = true;
            cmbEncryption.Location = new Point(382, 80);
            cmbEncryption.Name = "cmbEncryption";
            cmbEncryption.Size = new Size(83, 23);
            cmbEncryption.TabIndex = 12;
            // 
            // lblServerPort
            // 
            lblServerPort.AutoSize = true;
            lblServerPort.Location = new Point(347, 54);
            lblServerPort.Name = "lblServerPort";
            lblServerPort.Size = new Size(29, 15);
            lblServerPort.TabIndex = 11;
            lblServerPort.Text = "Port";
            // 
            // lblSourceFolder
            // 
            lblSourceFolder.AutoSize = true;
            lblSourceFolder.Location = new Point(28, 141);
            lblSourceFolder.Name = "lblSourceFolder";
            lblSourceFolder.Size = new Size(79, 15);
            lblSourceFolder.TabIndex = 10;
            lblSourceFolder.Text = "Source Folder";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(50, 112);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(47, 83);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 8;
            lblUsername.Text = "Username";
            // 
            // lblIncomingServer
            // 
            lblIncomingServer.AutoSize = true;
            lblIncomingServer.Location = new Point(6, 54);
            lblIncomingServer.Name = "lblIncomingServer";
            lblIncomingServer.Size = new Size(101, 15);
            lblIncomingServer.TabIndex = 7;
            lblIncomingServer.Text = "Server (Incoming)";
            // 
            // cmbServerPort
            // 
            cmbServerPort.FormattingEnabled = true;
            cmbServerPort.Location = new Point(382, 51);
            cmbServerPort.Name = "cmbServerPort";
            cmbServerPort.Size = new Size(50, 23);
            cmbServerPort.TabIndex = 6;
            // 
            // txtSourceFolder
            // 
            txtSourceFolder.Location = new Point(113, 138);
            txtSourceFolder.Name = "txtSourceFolder";
            txtSourceFolder.Size = new Size(193, 23);
            txtSourceFolder.TabIndex = 5;
            txtSourceFolder.Text = "INBOX";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(113, 109);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(193, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(113, 80);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(193, 23);
            txtUsername.TabIndex = 1;
            // 
            // txtIncomingServer
            // 
            txtIncomingServer.Location = new Point(113, 51);
            txtIncomingServer.Name = "txtIncomingServer";
            txtIncomingServer.Size = new Size(193, 23);
            txtIncomingServer.TabIndex = 0;
            // 
            // chkMarkAsRead
            // 
            chkMarkAsRead.AutoSize = true;
            chkMarkAsRead.CheckAlign = ContentAlignment.MiddleRight;
            chkMarkAsRead.Location = new Point(255, 51);
            chkMarkAsRead.Name = "chkMarkAsRead";
            chkMarkAsRead.Size = new Size(96, 19);
            chkMarkAsRead.TabIndex = 6;
            chkMarkAsRead.Text = "Mark as Read";
            chkMarkAsRead.UseVisualStyleBackColor = true;
            // 
            // txtArchiveFolder
            // 
            txtArchiveFolder.Location = new Point(113, 22);
            txtArchiveFolder.Name = "txtArchiveFolder";
            txtArchiveFolder.Size = new Size(352, 23);
            txtArchiveFolder.TabIndex = 4;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(418, 272);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 1;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += BtnImport_Click;
            // 
            // grpActionAfterImport
            // 
            grpActionAfterImport.Controls.Add(lblArchiveFolder);
            grpActionAfterImport.Controls.Add(chkDeleteMessage);
            grpActionAfterImport.Controls.Add(chkMarkAsRead);
            grpActionAfterImport.Controls.Add(txtArchiveFolder);
            grpActionAfterImport.Location = new Point(12, 190);
            grpActionAfterImport.Name = "grpActionAfterImport";
            grpActionAfterImport.Size = new Size(478, 76);
            grpActionAfterImport.TabIndex = 2;
            grpActionAfterImport.TabStop = false;
            grpActionAfterImport.Text = "Action after Import";
            // 
            // lblArchiveFolder
            // 
            lblArchiveFolder.AutoSize = true;
            lblArchiveFolder.Location = new Point(24, 25);
            lblArchiveFolder.Name = "lblArchiveFolder";
            lblArchiveFolder.Size = new Size(83, 15);
            lblArchiveFolder.TabIndex = 9;
            lblArchiveFolder.Text = "Archive Folder";
            // 
            // chkDeleteMessage
            // 
            chkDeleteMessage.AutoSize = true;
            chkDeleteMessage.CheckAlign = ContentAlignment.MiddleRight;
            chkDeleteMessage.Location = new Point(357, 51);
            chkDeleteMessage.Name = "chkDeleteMessage";
            chkDeleteMessage.Size = new Size(108, 19);
            chkDeleteMessage.TabIndex = 8;
            chkDeleteMessage.Text = "Delete Message";
            chkDeleteMessage.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 272);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 16;
            btnSave.Text = "Speichern";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FormImport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 305);
            Controls.Add(btnSave);
            Controls.Add(grpActionAfterImport);
            Controls.Add(btnImport);
            Controls.Add(grpMailServerIMAP);
            Name = "FormImport";
            Text = "Import from Mail Account";
            Load += FormImport_Load;
            grpMailServerIMAP.ResumeLayout(false);
            grpMailServerIMAP.PerformLayout();
            grpActionAfterImport.ResumeLayout(false);
            grpActionAfterImport.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpMailServerIMAP;
        private TextBox txtIncomingServer;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnImport;
        private TextBox txtArchiveFolder;
        private TextBox txtSourceFolder;
        private CheckBox chkMarkAsRead;
        private GroupBox grpActionAfterImport;
        private CheckBox chkDeleteMessage;
        private ComboBox cmbServerPort;
        private Label lblIncomingServer;
        private Label lblUsername;
        private Label lblSourceFolder;
        private Label lblPassword;
        private Label lblServerPort;
        private Label lblEncryption;
        private ComboBox cmbEncryption;
        private Label lblArchiveFolder;
        private Label lblName;
        private Button btnSave;
        private ComboBox cmbName;
    }
}