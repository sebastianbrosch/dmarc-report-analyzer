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
            GroupBoxMailServer = new GroupBox();
            TextBoxPort = new TextBox();
            TextBoxPassword = new TextBox();
            TextBoxUsername = new TextBox();
            TextBoxServer = new TextBox();
            ButtonImport = new Button();
            GroupBoxMailServer.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBoxMailServer
            // 
            GroupBoxMailServer.Controls.Add(TextBoxPort);
            GroupBoxMailServer.Controls.Add(TextBoxPassword);
            GroupBoxMailServer.Controls.Add(TextBoxUsername);
            GroupBoxMailServer.Controls.Add(TextBoxServer);
            GroupBoxMailServer.Location = new Point(12, 12);
            GroupBoxMailServer.Name = "GroupBoxMailServer";
            GroupBoxMailServer.Size = new Size(266, 112);
            GroupBoxMailServer.TabIndex = 0;
            GroupBoxMailServer.TabStop = false;
            GroupBoxMailServer.Text = "Mail-Server";
            // 
            // TextBoxPort
            // 
            TextBoxPort.Location = new Point(205, 22);
            TextBoxPort.Name = "TextBoxPort";
            TextBoxPort.PlaceholderText = "Port";
            TextBoxPort.Size = new Size(50, 23);
            TextBoxPort.TabIndex = 3;
            // 
            // TextBoxPassword
            // 
            TextBoxPassword.Location = new Point(6, 80);
            TextBoxPassword.Name = "TextBoxPassword";
            TextBoxPassword.PlaceholderText = "Password";
            TextBoxPassword.Size = new Size(193, 23);
            TextBoxPassword.TabIndex = 2;
            TextBoxPassword.UseSystemPasswordChar = true;
            // 
            // TextBoxUsername
            // 
            TextBoxUsername.Location = new Point(6, 51);
            TextBoxUsername.Name = "TextBoxUsername";
            TextBoxUsername.PlaceholderText = "Username";
            TextBoxUsername.Size = new Size(193, 23);
            TextBoxUsername.TabIndex = 1;
            // 
            // TextBoxServer
            // 
            TextBoxServer.Location = new Point(6, 22);
            TextBoxServer.Name = "TextBoxServer";
            TextBoxServer.PlaceholderText = "Server";
            TextBoxServer.Size = new Size(193, 23);
            TextBoxServer.TabIndex = 0;
            // 
            // ButtonImport
            // 
            ButtonImport.Location = new Point(203, 130);
            ButtonImport.Name = "ButtonImport";
            ButtonImport.Size = new Size(75, 23);
            ButtonImport.TabIndex = 1;
            ButtonImport.Text = "Import";
            ButtonImport.UseVisualStyleBackColor = true;
            ButtonImport.Click += ButtonImport_Click;
            // 
            // FormImport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 161);
            Controls.Add(ButtonImport);
            Controls.Add(GroupBoxMailServer);
            Name = "FormImport";
            Text = "Import";
            GroupBoxMailServer.ResumeLayout(false);
            GroupBoxMailServer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GroupBoxMailServer;
        private TextBox TextBoxServer;
        private TextBox TextBoxPort;
        private TextBox TextBoxPassword;
        private TextBox TextBoxUsername;
        private Button ButtonImport;
    }
}