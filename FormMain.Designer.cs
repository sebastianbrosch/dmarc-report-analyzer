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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dgvSenderOverview = new DataGridView();
            dgvSenderOverview_SourceIp = new DataGridViewTextBoxColumn();
            dgvSenderOverview_MessageCount = new DataGridViewTextBoxColumn();
            MenuStripMain = new MenuStrip();
            datenbankToolStripMenuItem = new ToolStripMenuItem();
            NewDatabaseToolStripMenuItem = new ToolStripMenuItem();
            OpenDatabaseToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            xMLToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem1 = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            mnuItemLogs = new ToolStripMenuItem();
            StatusStripMain = new StatusStrip();
            tsslblDatabaseName = new ToolStripStatusLabel();
            tsslblReportCount = new ToolStripStatusLabel();
            tsslblDomainCount = new ToolStripStatusLabel();
            PlotMessagesOverTime = new ScottPlot.WinForms.FormsPlot();
            DateTimePickerStart = new DateTimePicker();
            DateTimePickerEnd = new DateTimePicker();
            LabelStart = new Label();
            LabelEnd = new Label();
            PlotDKIM = new ScottPlot.WinForms.FormsPlot();
            PlotSPF = new ScottPlot.WinForms.FormsPlot();
            lblDKIM_Information = new Label();
            lblSPF_Information = new Label();
            dgvReporterOverview = new DataGridView();
            dgvReporterOverview_Reporter = new DataGridViewTextBoxColumn();
            dgvReporterOverview_MessageCount = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvSenderOverview).BeginInit();
            MenuStripMain.SuspendLayout();
            StatusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporterOverview).BeginInit();
            SuspendLayout();
            // 
            // dgvSenderOverview
            // 
            dgvSenderOverview.AllowUserToAddRows = false;
            dgvSenderOverview.AllowUserToDeleteRows = false;
            dgvSenderOverview.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSenderOverview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSenderOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSenderOverview.Columns.AddRange(new DataGridViewColumn[] { dgvSenderOverview_SourceIp, dgvSenderOverview_MessageCount });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvSenderOverview.DefaultCellStyle = dataGridViewCellStyle2;
            dgvSenderOverview.EnableHeadersVisualStyles = false;
            dgvSenderOverview.Location = new Point(12, 56);
            dgvSenderOverview.Name = "dgvSenderOverview";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvSenderOverview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvSenderOverview.RowHeadersVisible = false;
            dgvSenderOverview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSenderOverview.Size = new Size(357, 195);
            dgvSenderOverview.TabIndex = 5;
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
            MenuStripMain.Items.AddRange(new ToolStripItem[] { datenbankToolStripMenuItem, importToolStripMenuItem, reportsToolStripMenuItem, xMLToolStripMenuItem, mnuItemLogs });
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
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(59, 20);
            reportsToolStripMenuItem.Text = "Reports";
            reportsToolStripMenuItem.Click += reportsToolStripMenuItem_Click;
            // 
            // xMLToolStripMenuItem
            // 
            xMLToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem1, exportToolStripMenuItem });
            xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            xMLToolStripMenuItem.Size = new Size(43, 20);
            xMLToolStripMenuItem.Text = "XML";
            // 
            // importToolStripMenuItem1
            // 
            importToolStripMenuItem1.Name = "importToolStripMenuItem1";
            importToolStripMenuItem1.Size = new Size(110, 22);
            importToolStripMenuItem1.Text = "Import";
            importToolStripMenuItem1.Click += importToolStripMenuItem1_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(110, 22);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // mnuItemLogs
            // 
            mnuItemLogs.Name = "mnuItemLogs";
            mnuItemLogs.Size = new Size(44, 20);
            mnuItemLogs.Text = "Logs";
            mnuItemLogs.Click += mnuItemLogs_Click;
            // 
            // StatusStripMain
            // 
            StatusStripMain.Items.AddRange(new ToolStripItem[] { tsslblDatabaseName, tsslblReportCount, tsslblDomainCount });
            StatusStripMain.Location = new Point(0, 795);
            StatusStripMain.Name = "StatusStripMain";
            StatusStripMain.Size = new Size(1307, 24);
            StatusStripMain.TabIndex = 9;
            StatusStripMain.Text = "statusStrip1";
            // 
            // tsslblDatabaseName
            // 
            tsslblDatabaseName.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tsslblDatabaseName.Name = "tsslblDatabaseName";
            tsslblDatabaseName.Size = new Size(59, 19);
            tsslblDatabaseName.Text = "Database";
            tsslblDatabaseName.Click += tsslblDatabaseName_Click;
            // 
            // tsslblReportCount
            // 
            tsslblReportCount.BorderSides = ToolStripStatusLabelBorderSides.Right;
            tsslblReportCount.Name = "tsslblReportCount";
            tsslblReportCount.Size = new Size(82, 19);
            tsslblReportCount.Text = "Report Count";
            // 
            // tsslblDomainCount
            // 
            tsslblDomainCount.Name = "tsslblDomainCount";
            tsslblDomainCount.Size = new Size(85, 19);
            tsslblDomainCount.Text = "Domain Count";
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
            PlotDKIM.Size = new Size(639, 314);
            PlotDKIM.TabIndex = 15;
            // 
            // PlotSPF
            // 
            PlotSPF.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlotSPF.DisplayScale = 1F;
            PlotSPF.Location = new Point(657, 444);
            PlotSPF.Name = "PlotSPF";
            PlotSPF.Size = new Size(638, 314);
            PlotSPF.TabIndex = 16;
            // 
            // lblDKIM_Information
            // 
            lblDKIM_Information.Location = new Point(12, 761);
            lblDKIM_Information.Name = "lblDKIM_Information";
            lblDKIM_Information.Size = new Size(639, 21);
            lblDKIM_Information.TabIndex = 17;
            lblDKIM_Information.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSPF_Information
            // 
            lblSPF_Information.Location = new Point(657, 761);
            lblSPF_Information.Name = "lblSPF_Information";
            lblSPF_Information.Size = new Size(638, 21);
            lblSPF_Information.TabIndex = 18;
            lblSPF_Information.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvReporterOverview
            // 
            dgvReporterOverview.AllowUserToAddRows = false;
            dgvReporterOverview.AllowUserToDeleteRows = false;
            dgvReporterOverview.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvReporterOverview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvReporterOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporterOverview.Columns.AddRange(new DataGridViewColumn[] { dgvReporterOverview_Reporter, dgvReporterOverview_MessageCount });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvReporterOverview.DefaultCellStyle = dataGridViewCellStyle5;
            dgvReporterOverview.EnableHeadersVisualStyles = false;
            dgvReporterOverview.Location = new Point(12, 257);
            dgvReporterOverview.Name = "dgvReporterOverview";
            dgvReporterOverview.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvReporterOverview.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvReporterOverview.RowHeadersVisible = false;
            dgvReporterOverview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporterOverview.Size = new Size(357, 181);
            dgvReporterOverview.TabIndex = 19;
            // 
            // dgvReporterOverview_Reporter
            // 
            dgvReporterOverview_Reporter.DataPropertyName = "organization";
            dgvReporterOverview_Reporter.HeaderText = "Reporter";
            dgvReporterOverview_Reporter.Name = "dgvReporterOverview_Reporter";
            dgvReporterOverview_Reporter.ReadOnly = true;
            dgvReporterOverview_Reporter.Width = 200;
            // 
            // dgvReporterOverview_MessageCount
            // 
            dgvReporterOverview_MessageCount.DataPropertyName = "message_count";
            dgvReporterOverview_MessageCount.HeaderText = "Messages";
            dgvReporterOverview_MessageCount.Name = "dgvReporterOverview_MessageCount";
            dgvReporterOverview_MessageCount.ReadOnly = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 819);
            Controls.Add(dgvReporterOverview);
            Controls.Add(lblSPF_Information);
            Controls.Add(lblDKIM_Information);
            Controls.Add(PlotSPF);
            Controls.Add(PlotDKIM);
            Controls.Add(LabelEnd);
            Controls.Add(LabelStart);
            Controls.Add(DateTimePickerEnd);
            Controls.Add(DateTimePickerStart);
            Controls.Add(PlotMessagesOverTime);
            Controls.Add(StatusStripMain);
            Controls.Add(dgvSenderOverview);
            Controls.Add(MenuStripMain);
            MainMenuStrip = MenuStripMain;
            Name = "FormMain";
            Text = "DMARC Report Analyzer";
            FormClosing += FormMain_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dgvSenderOverview).EndInit();
            MenuStripMain.ResumeLayout(false);
            MenuStripMain.PerformLayout();
            StatusStripMain.ResumeLayout(false);
            StatusStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporterOverview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvSenderOverview;
        private MenuStrip MenuStripMain;
        private ToolStripMenuItem datenbankToolStripMenuItem;
        private ToolStripMenuItem OpenDatabaseToolStripMenuItem;
        private ToolStripMenuItem NewDatabaseToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private DataGridViewTextBoxColumn dgvSenderOverview_SourceIp;
        private DataGridViewTextBoxColumn dgvSenderOverview_MessageCount;
        private StatusStrip StatusStripMain;
        private ToolStripStatusLabel tsslblReportCount;
        private ToolStripStatusLabel tsslblDatabaseName;
        private ScottPlot.WinForms.FormsPlot PlotMessagesOverTime;
        private DateTimePicker DateTimePickerStart;
        private DateTimePicker DateTimePickerEnd;
        private Label LabelStart;
        private Label LabelEnd;
        private ScottPlot.WinForms.FormsPlot PlotDKIM;
        private ScottPlot.WinForms.FormsPlot PlotSPF;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem xMLToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem1;
        private ToolStripMenuItem exportToolStripMenuItem;
        private Label lblDKIM_Information;
        private Label lblSPF_Information;
        private DataGridView dgvReporterOverview;
        private DataGridViewTextBoxColumn dgvReporterOverview_Reporter;
        private DataGridViewTextBoxColumn dgvReporterOverview_MessageCount;
        private ToolStripMenuItem mnuItemLogs;
        private ToolStripStatusLabel tsslblDomainCount;
    }
}
