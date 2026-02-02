namespace DMARCReportAnalyzer
{
    partial class FormReports
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReports));
            dgvReportsOverview = new DataGridView();
            dgvReportsOverview_ID = new DataGridViewTextBoxColumn();
            dgvReportsOverview_ReportBegin = new DataGridViewTextBoxColumn();
            dgvReportsOverview_ReportEnd = new DataGridViewTextBoxColumn();
            dgvReportsOverview_Organization = new DataGridViewTextBoxColumn();
            dgvReportsOverview_MessageCount = new DataGridViewTextBoxColumn();
            grpReportDetails = new GroupBox();
            lblExtraInfo = new Label();
            txtExtraInfo = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblReportID = new Label();
            txtReportID = new TextBox();
            lblReportDateRange = new Label();
            txtReportEnd = new TextBox();
            txtReportBegin = new TextBox();
            lblOrganization = new Label();
            txtOrganization = new TextBox();
            grpPublishedPolicy = new GroupBox();
            lblDiscoveryMethod = new Label();
            txtDiscoveryMethod = new TextBox();
            lblPercentage = new Label();
            txtPercentage = new TextBox();
            lblNonExistentSubDomainsPolicy = new Label();
            txtNonExistentSubDomainsPolicy = new TextBox();
            lblSubDomainPolicy = new Label();
            txtSubDomainPolicy = new TextBox();
            lblPolicy = new Label();
            txtPolicy = new TextBox();
            txtSPFAlignment = new TextBox();
            lblSPFAlignment = new Label();
            lblDKIMAlignment = new Label();
            txtDKIMAlignment = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvReportsOverview).BeginInit();
            grpReportDetails.SuspendLayout();
            grpPublishedPolicy.SuspendLayout();
            SuspendLayout();
            // 
            // dgvReportsOverview
            // 
            dgvReportsOverview.AllowUserToAddRows = false;
            dgvReportsOverview.AllowUserToDeleteRows = false;
            dgvReportsOverview.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvReportsOverview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvReportsOverview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportsOverview.Columns.AddRange(new DataGridViewColumn[] { dgvReportsOverview_ID, dgvReportsOverview_ReportBegin, dgvReportsOverview_ReportEnd, dgvReportsOverview_Organization, dgvReportsOverview_MessageCount });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvReportsOverview.DefaultCellStyle = dataGridViewCellStyle3;
            dgvReportsOverview.EnableHeadersVisualStyles = false;
            dgvReportsOverview.Location = new Point(12, 12);
            dgvReportsOverview.MultiSelect = false;
            dgvReportsOverview.Name = "dgvReportsOverview";
            dgvReportsOverview.ReadOnly = true;
            dgvReportsOverview.RowHeadersVisible = false;
            dgvReportsOverview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportsOverview.Size = new Size(711, 150);
            dgvReportsOverview.TabIndex = 0;
            dgvReportsOverview.SelectionChanged += dgvReportsOverview_SelectionChanged;
            // 
            // dgvReportsOverview_ID
            // 
            dgvReportsOverview_ID.DataPropertyName = "id";
            dgvReportsOverview_ID.HeaderText = "ID";
            dgvReportsOverview_ID.Name = "dgvReportsOverview_ID";
            dgvReportsOverview_ID.ReadOnly = true;
            dgvReportsOverview_ID.Visible = false;
            // 
            // dgvReportsOverview_ReportBegin
            // 
            dgvReportsOverview_ReportBegin.DataPropertyName = "report_begin";
            dgvReportsOverview_ReportBegin.HeaderText = "Begin";
            dgvReportsOverview_ReportBegin.Name = "dgvReportsOverview_ReportBegin";
            dgvReportsOverview_ReportBegin.ReadOnly = true;
            // 
            // dgvReportsOverview_ReportEnd
            // 
            dgvReportsOverview_ReportEnd.DataPropertyName = "report_end";
            dgvReportsOverview_ReportEnd.HeaderText = "End";
            dgvReportsOverview_ReportEnd.Name = "dgvReportsOverview_ReportEnd";
            dgvReportsOverview_ReportEnd.ReadOnly = true;
            // 
            // dgvReportsOverview_Organization
            // 
            dgvReportsOverview_Organization.DataPropertyName = "organization";
            dgvReportsOverview_Organization.HeaderText = "Organization";
            dgvReportsOverview_Organization.Name = "dgvReportsOverview_Organization";
            dgvReportsOverview_Organization.ReadOnly = true;
            dgvReportsOverview_Organization.Width = 200;
            // 
            // dgvReportsOverview_MessageCount
            // 
            dgvReportsOverview_MessageCount.DataPropertyName = "message_count";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReportsOverview_MessageCount.DefaultCellStyle = dataGridViewCellStyle2;
            dgvReportsOverview_MessageCount.HeaderText = "Message Count";
            dgvReportsOverview_MessageCount.Name = "dgvReportsOverview_MessageCount";
            dgvReportsOverview_MessageCount.ReadOnly = true;
            dgvReportsOverview_MessageCount.Width = 120;
            // 
            // grpReportDetails
            // 
            grpReportDetails.Controls.Add(lblExtraInfo);
            grpReportDetails.Controls.Add(txtExtraInfo);
            grpReportDetails.Controls.Add(lblEmail);
            grpReportDetails.Controls.Add(txtEmail);
            grpReportDetails.Controls.Add(lblReportID);
            grpReportDetails.Controls.Add(txtReportID);
            grpReportDetails.Controls.Add(lblReportDateRange);
            grpReportDetails.Controls.Add(txtReportEnd);
            grpReportDetails.Controls.Add(txtReportBegin);
            grpReportDetails.Controls.Add(lblOrganization);
            grpReportDetails.Controls.Add(txtOrganization);
            grpReportDetails.Location = new Point(12, 168);
            grpReportDetails.Name = "grpReportDetails";
            grpReportDetails.Size = new Size(322, 176);
            grpReportDetails.TabIndex = 1;
            grpReportDetails.TabStop = false;
            grpReportDetails.Text = "Report Details";
            // 
            // lblExtraInfo
            // 
            lblExtraInfo.AutoSize = true;
            lblExtraInfo.Location = new Point(25, 141);
            lblExtraInfo.Name = "lblExtraInfo";
            lblExtraInfo.Size = new Size(56, 15);
            lblExtraInfo.TabIndex = 10;
            lblExtraInfo.Text = "Extra Info";
            // 
            // txtExtraInfo
            // 
            txtExtraInfo.Location = new Point(87, 138);
            txtExtraInfo.Name = "txtExtraInfo";
            txtExtraInfo.Size = new Size(226, 23);
            txtExtraInfo.TabIndex = 9;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(40, 112);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 15);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "E-Mail";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(87, 109);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(226, 23);
            txtEmail.TabIndex = 7;
            // 
            // lblReportID
            // 
            lblReportID.AutoSize = true;
            lblReportID.Location = new Point(25, 83);
            lblReportID.Name = "lblReportID";
            lblReportID.Size = new Size(56, 15);
            lblReportID.TabIndex = 6;
            lblReportID.Text = "Report ID";
            // 
            // txtReportID
            // 
            txtReportID.Location = new Point(87, 80);
            txtReportID.Name = "txtReportID";
            txtReportID.Size = new Size(226, 23);
            txtReportID.TabIndex = 5;
            // 
            // lblReportDateRange
            // 
            lblReportDateRange.AutoSize = true;
            lblReportDateRange.Location = new Point(14, 54);
            lblReportDateRange.Name = "lblReportDateRange";
            lblReportDateRange.Size = new Size(67, 15);
            lblReportDateRange.TabIndex = 4;
            lblReportDateRange.Text = "Date Range";
            // 
            // txtReportEnd
            // 
            txtReportEnd.Location = new Point(203, 51);
            txtReportEnd.Name = "txtReportEnd";
            txtReportEnd.Size = new Size(110, 23);
            txtReportEnd.TabIndex = 3;
            // 
            // txtReportBegin
            // 
            txtReportBegin.Location = new Point(87, 51);
            txtReportBegin.Name = "txtReportBegin";
            txtReportBegin.Size = new Size(110, 23);
            txtReportBegin.TabIndex = 2;
            // 
            // lblOrganization
            // 
            lblOrganization.AutoSize = true;
            lblOrganization.Location = new Point(6, 25);
            lblOrganization.Name = "lblOrganization";
            lblOrganization.Size = new Size(75, 15);
            lblOrganization.TabIndex = 1;
            lblOrganization.Text = "Organization";
            // 
            // txtOrganization
            // 
            txtOrganization.Location = new Point(87, 22);
            txtOrganization.Name = "txtOrganization";
            txtOrganization.Size = new Size(226, 23);
            txtOrganization.TabIndex = 0;
            // 
            // grpPublishedPolicy
            // 
            grpPublishedPolicy.Controls.Add(lblDiscoveryMethod);
            grpPublishedPolicy.Controls.Add(txtDiscoveryMethod);
            grpPublishedPolicy.Controls.Add(lblPercentage);
            grpPublishedPolicy.Controls.Add(txtPercentage);
            grpPublishedPolicy.Controls.Add(lblNonExistentSubDomainsPolicy);
            grpPublishedPolicy.Controls.Add(txtNonExistentSubDomainsPolicy);
            grpPublishedPolicy.Controls.Add(lblSubDomainPolicy);
            grpPublishedPolicy.Controls.Add(txtSubDomainPolicy);
            grpPublishedPolicy.Controls.Add(lblPolicy);
            grpPublishedPolicy.Controls.Add(txtPolicy);
            grpPublishedPolicy.Controls.Add(txtSPFAlignment);
            grpPublishedPolicy.Controls.Add(lblSPFAlignment);
            grpPublishedPolicy.Controls.Add(lblDKIMAlignment);
            grpPublishedPolicy.Controls.Add(txtDKIMAlignment);
            grpPublishedPolicy.Location = new Point(340, 168);
            grpPublishedPolicy.Name = "grpPublishedPolicy";
            grpPublishedPolicy.Size = new Size(383, 233);
            grpPublishedPolicy.TabIndex = 2;
            grpPublishedPolicy.TabStop = false;
            grpPublishedPolicy.Text = "Published Policy";
            // 
            // lblDiscoveryMethod
            // 
            lblDiscoveryMethod.AutoSize = true;
            lblDiscoveryMethod.Location = new Point(88, 199);
            lblDiscoveryMethod.Name = "lblDiscoveryMethod";
            lblDiscoveryMethod.Size = new Size(103, 15);
            lblDiscoveryMethod.TabIndex = 13;
            lblDiscoveryMethod.Text = "Discovery Method";
            // 
            // txtDiscoveryMethod
            // 
            txtDiscoveryMethod.Location = new Point(197, 196);
            txtDiscoveryMethod.Name = "txtDiscoveryMethod";
            txtDiscoveryMethod.Size = new Size(173, 23);
            txtDiscoveryMethod.TabIndex = 12;
            // 
            // lblPercentage
            // 
            lblPercentage.AutoSize = true;
            lblPercentage.Location = new Point(125, 170);
            lblPercentage.Name = "lblPercentage";
            lblPercentage.Size = new Size(66, 15);
            lblPercentage.TabIndex = 11;
            lblPercentage.Text = "Percentage";
            // 
            // txtPercentage
            // 
            txtPercentage.Location = new Point(197, 167);
            txtPercentage.Name = "txtPercentage";
            txtPercentage.Size = new Size(173, 23);
            txtPercentage.TabIndex = 10;
            // 
            // lblNonExistentSubDomainsPolicy
            // 
            lblNonExistentSubDomainsPolicy.AutoSize = true;
            lblNonExistentSubDomainsPolicy.Location = new Point(6, 141);
            lblNonExistentSubDomainsPolicy.Name = "lblNonExistentSubDomainsPolicy";
            lblNonExistentSubDomainsPolicy.Size = new Size(185, 15);
            lblNonExistentSubDomainsPolicy.TabIndex = 9;
            lblNonExistentSubDomainsPolicy.Text = "Non-Existent Sub-Domains Policy";
            // 
            // txtNonExistentSubDomainsPolicy
            // 
            txtNonExistentSubDomainsPolicy.Location = new Point(197, 138);
            txtNonExistentSubDomainsPolicy.Name = "txtNonExistentSubDomainsPolicy";
            txtNonExistentSubDomainsPolicy.Size = new Size(173, 23);
            txtNonExistentSubDomainsPolicy.TabIndex = 8;
            // 
            // lblSubDomainPolicy
            // 
            lblSubDomainPolicy.AutoSize = true;
            lblSubDomainPolicy.Location = new Point(82, 112);
            lblSubDomainPolicy.Name = "lblSubDomainPolicy";
            lblSubDomainPolicy.Size = new Size(109, 15);
            lblSubDomainPolicy.TabIndex = 7;
            lblSubDomainPolicy.Text = "Sub-Domain Policy";
            // 
            // txtSubDomainPolicy
            // 
            txtSubDomainPolicy.Location = new Point(197, 109);
            txtSubDomainPolicy.Name = "txtSubDomainPolicy";
            txtSubDomainPolicy.Size = new Size(173, 23);
            txtSubDomainPolicy.TabIndex = 6;
            // 
            // lblPolicy
            // 
            lblPolicy.AutoSize = true;
            lblPolicy.Location = new Point(152, 83);
            lblPolicy.Name = "lblPolicy";
            lblPolicy.Size = new Size(39, 15);
            lblPolicy.TabIndex = 5;
            lblPolicy.Text = "Policy";
            // 
            // txtPolicy
            // 
            txtPolicy.Location = new Point(197, 80);
            txtPolicy.Name = "txtPolicy";
            txtPolicy.Size = new Size(173, 23);
            txtPolicy.TabIndex = 4;
            // 
            // txtSPFAlignment
            // 
            txtSPFAlignment.Location = new Point(197, 51);
            txtSPFAlignment.Name = "txtSPFAlignment";
            txtSPFAlignment.Size = new Size(173, 23);
            txtSPFAlignment.TabIndex = 3;
            // 
            // lblSPFAlignment
            // 
            lblSPFAlignment.AutoSize = true;
            lblSPFAlignment.Location = new Point(106, 54);
            lblSPFAlignment.Name = "lblSPFAlignment";
            lblSPFAlignment.Size = new Size(85, 15);
            lblSPFAlignment.TabIndex = 2;
            lblSPFAlignment.Text = "SPF Alignment";
            // 
            // lblDKIMAlignment
            // 
            lblDKIMAlignment.AutoSize = true;
            lblDKIMAlignment.Location = new Point(96, 25);
            lblDKIMAlignment.Name = "lblDKIMAlignment";
            lblDKIMAlignment.Size = new Size(95, 15);
            lblDKIMAlignment.TabIndex = 1;
            lblDKIMAlignment.Text = "DKIM Alignment";
            // 
            // txtDKIMAlignment
            // 
            txtDKIMAlignment.Location = new Point(197, 22);
            txtDKIMAlignment.Name = "txtDKIMAlignment";
            txtDKIMAlignment.Size = new Size(173, 23);
            txtDKIMAlignment.TabIndex = 0;
            // 
            // FormReports
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 411);
            Controls.Add(grpPublishedPolicy);
            Controls.Add(grpReportDetails);
            Controls.Add(dgvReportsOverview);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormReports";
            Text = "DMARC Reports";
            ((System.ComponentModel.ISupportInitialize)dgvReportsOverview).EndInit();
            grpReportDetails.ResumeLayout(false);
            grpReportDetails.PerformLayout();
            grpPublishedPolicy.ResumeLayout(false);
            grpPublishedPolicy.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvReportsOverview;
        private DataGridViewTextBoxColumn dgvReportsOverview_ID;
        private DataGridViewTextBoxColumn dgvReportsOverview_ReportBegin;
        private DataGridViewTextBoxColumn dgvReportsOverview_ReportEnd;
        private DataGridViewTextBoxColumn dgvReportsOverview_Organization;
        private DataGridViewTextBoxColumn dgvReportsOverview_MessageCount;
        private GroupBox grpReportDetails;
        private TextBox txtExtraInfo;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblReportID;
        private TextBox txtReportID;
        private Label lblReportDateRange;
        private TextBox txtReportEnd;
        private TextBox txtReportBegin;
        private Label lblOrganization;
        private TextBox txtOrganization;
        private Label lblExtraInfo;
        private GroupBox grpPublishedPolicy;
        private TextBox txtSPFAlignment;
        private Label lblSPFAlignment;
        private Label lblDKIMAlignment;
        private TextBox txtDKIMAlignment;
        private Label lblSubDomainPolicy;
        private TextBox txtSubDomainPolicy;
        private Label lblPolicy;
        private TextBox txtPolicy;
        private Label lblPercentage;
        private TextBox txtPercentage;
        private Label lblNonExistentSubDomainsPolicy;
        private TextBox txtNonExistentSubDomainsPolicy;
        private Label lblDiscoveryMethod;
        private TextBox txtDiscoveryMethod;
    }
}