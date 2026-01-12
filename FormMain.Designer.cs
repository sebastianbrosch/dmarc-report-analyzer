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
            ToolStripStatusLabelDatabaseName = new ToolStripStatusLabel();
            ToolStripStatusLabelReportCount = new ToolStripStatusLabel();
            PlotMessagesOverTime = new ScottPlot.WinForms.FormsPlot();
            DateTimePickerStart = new DateTimePicker();
            DateTimePickerEnd = new DateTimePicker();
            LabelStart = new Label();
            LabelEnd = new Label();
            PlotDKIM = new ScottPlot.WinForms.FormsPlot();
            PlotSPF = new ScottPlot.WinForms.FormsPlot();
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
            MenuStripMain.Size = new Size(1307, 24);
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
            ButtonExportXML.Location = new Point(1220, 27);
            ButtonExportXML.Name = "ButtonExportXML";
            ButtonExportXML.Size = new Size(75, 23);
            ButtonExportXML.TabIndex = 7;
            ButtonExportXML.Text = "Export XML";
            ButtonExportXML.UseVisualStyleBackColor = true;
            ButtonExportXML.Click += ButtonExportXML_Click;
            // 
            // ButtonImportXML
            // 
            ButtonImportXML.Location = new Point(1134, 27);
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
            StatusStripMain.Location = new Point(0, 795);
            StatusStripMain.Name = "StatusStripMain";
            StatusStripMain.Size = new Size(1307, 24);
            StatusStripMain.TabIndex = 9;
            StatusStripMain.Text = "statusStrip1";
            // 
            // ToolStripStatusLabelDatabaseName
            // 
            ToolStripStatusLabelDatabaseName.BorderSides = ToolStripStatusLabelBorderSides.Right;
            ToolStripStatusLabelDatabaseName.Name = "ToolStripStatusLabelDatabaseName";
            ToolStripStatusLabelDatabaseName.Size = new Size(59, 19);
            ToolStripStatusLabelDatabaseName.Text = "Database";
            // 
            // ToolStripStatusLabelReportCount
            // 
            ToolStripStatusLabelReportCount.Name = "ToolStripStatusLabelReportCount";
            ToolStripStatusLabelReportCount.Size = new Size(78, 19);
            ToolStripStatusLabelReportCount.Text = "Report Count";
            // 
            // PlotMessagesOverTime
            // 
            PlotMessagesOverTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlotMessagesOverTime.DisplayScale = 1F;
            PlotMessagesOverTime.Location = new Point(375, 56);
            PlotMessagesOverTime.Name = "PlotMessagesOverTime";
            PlotMessagesOverTime.Size = new Size(920, 382);
            PlotMessagesOverTime.TabIndex = 10;
            // 
            // DateTimePickerStart
            // 
            DateTimePickerStart.Format = DateTimePickerFormat.Short;
            DateTimePickerStart.Location = new Point(49, 27);
            DateTimePickerStart.Name = "DateTimePickerStart";
            DateTimePickerStart.Size = new Size(101, 23);
            DateTimePickerStart.TabIndex = 11;
            DateTimePickerStart.ValueChanged += DateTimePickerStart_ValueChanged;
            // 
            // DateTimePickerEnd
            // 
            DateTimePickerEnd.Format = DateTimePickerFormat.Short;
            DateTimePickerEnd.Location = new Point(189, 27);
            DateTimePickerEnd.Name = "DateTimePickerEnd";
            DateTimePickerEnd.Size = new Size(104, 23);
            DateTimePickerEnd.TabIndex = 12;
            DateTimePickerEnd.ValueChanged += DateTimePickerEnd_ValueChanged;
            // 
            // LabelStart
            // 
            LabelStart.AutoSize = true;
            LabelStart.Location = new Point(12, 31);
            LabelStart.Name = "LabelStart";
            LabelStart.Size = new Size(31, 15);
            LabelStart.TabIndex = 13;
            LabelStart.Text = "Start";
            // 
            // LabelEnd
            // 
            LabelEnd.AutoSize = true;
            LabelEnd.Location = new Point(156, 31);
            LabelEnd.Name = "LabelEnd";
            LabelEnd.Size = new Size(27, 15);
            LabelEnd.TabIndex = 14;
            LabelEnd.Text = "End";
            // 
            // PlotDKIM
            // 
            PlotDKIM.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlotDKIM.DisplayScale = 1F;
            PlotDKIM.Location = new Point(12, 444);
            PlotDKIM.Name = "PlotDKIM";
            PlotDKIM.Size = new Size(639, 348);
            PlotDKIM.TabIndex = 15;
            // 
            // PlotSPF
            // 
            PlotSPF.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlotSPF.DisplayScale = 1F;
            PlotSPF.Location = new Point(657, 444);
            PlotSPF.Name = "PlotSPF";
            PlotSPF.Size = new Size(638, 348);
            PlotSPF.TabIndex = 16;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 819);
            Controls.Add(PlotSPF);
            Controls.Add(PlotDKIM);
            Controls.Add(LabelEnd);
            Controls.Add(LabelStart);
            Controls.Add(DateTimePickerEnd);
            Controls.Add(DateTimePickerStart);
            Controls.Add(PlotMessagesOverTime);
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
        private ScottPlot.WinForms.FormsPlot PlotMessagesOverTime;
        private DateTimePicker DateTimePickerStart;
        private DateTimePicker DateTimePickerEnd;
        private Label LabelStart;
        private Label LabelEnd;
        private ScottPlot.WinForms.FormsPlot PlotDKIM;
        private ScottPlot.WinForms.FormsPlot PlotSPF;
    }
}
