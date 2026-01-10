namespace DMARCReportAnalyzer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewSenderOverview = new DataGridView();
            dgvSenderOverview_SourceIp = new DataGridViewTextBoxColumn();
            dgvSenderOverview_MessageCount = new DataGridViewTextBoxColumn();
            MenuStripMain = new MenuStrip();
            datenbankToolStripMenuItem = new ToolStripMenuItem();
            NewDatabaseToolStripMenuItem = new ToolStripMenuItem();
            OpenDatabaseToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            ButtonExportXML = new Button();
            ButtonImportXML = new Button();
            StatusStripMain = new StatusStrip();
            ToolStripStatusLabelReportCount = new ToolStripStatusLabel();
            ToolStripStatusLabelDatabaseName = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)DataGridViewSenderOverview).BeginInit();
            MenuStripMain.SuspendLayout();
            StatusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridViewSenderOverview
            // 
            DataGridViewSenderOverview.AllowUserToAddRows = false;
            DataGridViewSenderOverview.AllowUserToDeleteRows = false;
            DataGridViewSenderOverview.AllowUserToResizeRows = false;
            DataGridViewSenderOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewSenderOverview.Columns.AddRange(new DataGridViewColumn[] { dgvSenderOverview_SourceIp, dgvSenderOverview_MessageCount });
            DataGridViewSenderOverview.Location = new Point(12, 56);
            DataGridViewSenderOverview.Name = "DataGridViewSenderOverview";
            DataGridViewSenderOverview.RowHeadersVisible = false;
            DataGridViewSenderOverview.Size = new Size(357, 382);
            DataGridViewSenderOverview.TabIndex = 5;
            // 
            // dgvSenderOverview_SourceIp
            // 
            dgvSenderOverview_SourceIp.DataPropertyName = "source_ip";
            dgvSenderOverview_SourceIp.HeaderText = "IP";
            dgvSenderOverview_SourceIp.Name = "dgvSenderOverview_SourceIp";
            dgvSenderOverview_SourceIp.Width = 200;
            // 
            // dgvSenderOverview_MessageCount
            // 
            dgvSenderOverview_MessageCount.DataPropertyName = "message_count";
            dgvSenderOverview_MessageCount.HeaderText = "Messages";
            dgvSenderOverview_MessageCount.Name = "dgvSenderOverview_MessageCount";
            // 
            // MenuStripMain
            // 
            MenuStripMain.Items.AddRange(new ToolStripItem[] { datenbankToolStripMenuItem, importToolStripMenuItem });
            MenuStripMain.Location = new Point(0, 0);
            MenuStripMain.Name = "MenuStripMain";
            MenuStripMain.Size = new Size(800, 24);
            MenuStripMain.TabIndex = 6;
            // 
            // datenbankToolStripMenuItem
            // 
            datenbankToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewDatabaseToolStripMenuItem, OpenDatabaseToolStripMenuItem });
            datenbankToolStripMenuItem.Name = "datenbankToolStripMenuItem";
            datenbankToolStripMenuItem.Size = new Size(76, 20);
            datenbankToolStripMenuItem.Text = "Datenbank";
            // 
            // NewDatabaseToolStripMenuItem
            // 
            NewDatabaseToolStripMenuItem.Name = "NewDatabaseToolStripMenuItem";
            NewDatabaseToolStripMenuItem.Size = new Size(120, 22);
            NewDatabaseToolStripMenuItem.Text = "Neu...";
            NewDatabaseToolStripMenuItem.Click += NewDatabaseToolStripMenuItem_Click;
            // 
            // OpenDatabaseToolStripMenuItem
            // 
            OpenDatabaseToolStripMenuItem.Name = "OpenDatabaseToolStripMenuItem";
            OpenDatabaseToolStripMenuItem.Size = new Size(120, 22);
            OpenDatabaseToolStripMenuItem.Text = "Öffnen...";
            OpenDatabaseToolStripMenuItem.Click += OpenDatabaseToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(55, 20);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // ButtonExportXML
            // 
            ButtonExportXML.Location = new Point(713, 27);
            ButtonExportXML.Name = "ButtonExportXML";
            ButtonExportXML.Size = new Size(75, 23);
            ButtonExportXML.TabIndex = 7;
            ButtonExportXML.Text = "Export XML";
            ButtonExportXML.UseVisualStyleBackColor = true;
            ButtonExportXML.Click += ButtonExportXML_Click;
            // 
            // ButtonImportXML
            // 
            ButtonImportXML.Location = new Point(627, 27);
            ButtonImportXML.Name = "ButtonImportXML";
            ButtonImportXML.Size = new Size(80, 23);
            ButtonImportXML.TabIndex = 8;
            ButtonImportXML.Text = "Import XML";
            ButtonImportXML.UseVisualStyleBackColor = true;
            ButtonImportXML.Click += ButtonImportXML_Click;
            // 
            // StatusStripMain
            // 
            StatusStripMain.Items.AddRange(new ToolStripItem[] { ToolStripStatusLabelDatabaseName, ToolStripStatusLabelReportCount });
            StatusStripMain.Location = new Point(0, 473);
            StatusStripMain.Name = "StatusStripMain";
            StatusStripMain.Size = new Size(800, 24);
            StatusStripMain.TabIndex = 9;
            StatusStripMain.Text = "statusStrip1";
            // 
            // ToolStripStatusLabelReportCount
            // 
            ToolStripStatusLabelReportCount.Name = "ToolStripStatusLabelReportCount";
            ToolStripStatusLabelReportCount.Size = new Size(78, 19);
            ToolStripStatusLabelReportCount.Text = "Report Count";
            // 
            // ToolStripStatusLabelDatabaseName
            // 
            ToolStripStatusLabelDatabaseName.BorderSides = ToolStripStatusLabelBorderSides.Right;
            ToolStripStatusLabelDatabaseName.Name = "ToolStripStatusLabelDatabaseName";
            ToolStripStatusLabelDatabaseName.Size = new Size(59, 19);
            ToolStripStatusLabelDatabaseName.Text = "Database";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 497);
            Controls.Add(StatusStripMain);
            Controls.Add(ButtonImportXML);
            Controls.Add(ButtonExportXML);
            Controls.Add(DataGridViewSenderOverview);
            Controls.Add(MenuStripMain);
            MainMenuStrip = MenuStripMain;
            Name = "FormMain";
            Text = "DMARC Report Analyzer";
            ((System.ComponentModel.ISupportInitialize)DataGridViewSenderOverview).EndInit();
            MenuStripMain.ResumeLayout(false);
            MenuStripMain.PerformLayout();
            StatusStripMain.ResumeLayout(false);
            StatusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView DataGridViewSenderOverview;
        private MenuStrip MenuStripMain;
        private ToolStripMenuItem datenbankToolStripMenuItem;
        private ToolStripMenuItem OpenDatabaseToolStripMenuItem;
        private ToolStripMenuItem NewDatabaseToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private Button ButtonExportXML;
        private Button ButtonImportXML;
        private DataGridViewTextBoxColumn dgvSenderOverview_SourceIp;
        private DataGridViewTextBoxColumn dgvSenderOverview_MessageCount;
        private StatusStrip StatusStripMain;
        private ToolStripStatusLabel ToolStripStatusLabelReportCount;
        private ToolStripStatusLabel ToolStripStatusLabelDatabaseName;
    }
}
