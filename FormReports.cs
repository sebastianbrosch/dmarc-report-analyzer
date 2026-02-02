using Dapper;
using System.Data;
using System.Xml;

namespace DMARCReportAnalyzer;

public partial class FormReports : Form
{
    private readonly IDbConnection DbConnection;

    public FormReports(IDbConnection connection)
    {
        InitializeComponent();
        DbConnection = connection;
        LoadReports();
    }

    private void LoadReports()
    {
        IDataReader reader = DbConnection.ExecuteReader("SELECT f.id, m.report_begin, m.report_end, (SELECT SUM(count) FROM record WHERE feedback_id = f.id) message_count, m.organization FROM feedback f INNER JOIN metadata m ON f.id = m.feedback_id");
        DataSet dsReports = new();
        dsReports.EnforceConstraints = false;
        DataTable dtReports = dsReports.Tables.Add("reports");
        dtReports.Load(reader);

        dgvReportsOverview.AutoGenerateColumns = false;
        dgvReportsOverview.ReadOnly = true;
        dgvReportsOverview.DataSource = dtReports;
        dgvReportsOverview.Sort(dgvReportsOverview_ReportBegin, System.ComponentModel.ListSortDirection.Ascending);
        dgvReportsOverview.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvReportsOverview.ColumnHeadersDefaultCellStyle.BackColor;
    }

    private void LoadReportDetails(string id)
    {
        var policyPublished = DbConnection.QueryFirst("SELECT discovery_method, aspf, adkim, p, sp, np, pct FROM policy_published WHERE feedback_id = @id", new { id });
        txtDiscoveryMethod.Text = policyPublished.discovery_method;
        txtSPFAlignment.Text = policyPublished.aspf;
        txtDKIMAlignment.Text = policyPublished.adkim;
        txtPolicy.Text = policyPublished.p;
        txtSubDomainPolicy.Text = policyPublished.sp;
        txtNonExistentSubDomainsPolicy.Text = policyPublished.np;
        txtPercentage.Text = Convert.ToString(policyPublished.pct);

        var metadata = DbConnection.QueryFirst("SELECT report_begin, report_end, report_id, organization, email, extra_contact_info FROM metadata WHERE feedback_id = @id", new { id });
        txtReportBegin.Text = Convert.ToString(metadata.report_begin);
        txtReportEnd.Text = Convert.ToString(metadata.report_end);
        txtReportID.Text = metadata.report_id;
        txtOrganization.Text = metadata.organization;
        txtEmail.Text = metadata.email;
        txtExtraInfo.Text = metadata.extra_contact_info;
    }

    private void dgvReportsOverview_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvReportsOverview.SelectedRows.Count < 1)
        {
            return;
        }

        DataGridViewRow dgvrSelected = dgvReportsOverview.SelectedRows[0];
        LoadReportDetails(dgvrSelected!.Cells[dgvReportsOverview_ID!.Name!]!.Value!.ToString()!);
    }

    private void exportXMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DataGridViewSelectedRowCollection listSelectedRows = dgvReportsOverview.SelectedRows;

        if (listSelectedRows.Count == 1)
        {
            string? reportId = listSelectedRows[0].Cells[dgvReportsOverview_ID.Index]!.Value!.ToString();
            var data = DbConnection.QueryFirstOrDefault("SELECT f.id, m.report_begin, m.report_end, f.data, pp.domain FROM metadata m INNER JOIN feedback f ON m.feedback_id = f.id INNER JOIN policy_published pp ON pp.feedback_id = f.id");

            if (data is null)
            {
                return;
            }

            DateTimeOffset offsetBegin = new DateTimeOffset(data.report_begin, TimeSpan.Zero);
            DateTimeOffset offsetEnd = new DateTimeOffset(data.report_end, TimeSpan.Zero);
            
            string filename = data.domain + "!" + offsetBegin.ToUnixTimeSeconds() + "!" + offsetEnd.ToUnixTimeSeconds() + "!" + data.id + ".xml";
            
            string filePath = string.Empty;

            using (SaveFileDialog dlgSelectFile = new())
            {
                dlgSelectFile.DefaultExt = ".xml";
                dlgSelectFile.Filter = "XML-Dokument|*.xml";
                dlgSelectFile.FileName = filename;
                dlgSelectFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (dlgSelectFile.ShowDialog(this) == DialogResult.OK)
                {
                    filePath = dlgSelectFile.FileName;
                }
            }

            if (string.IsNullOrWhiteSpace(filePath) || File.Exists(filePath))
            {
                MessageBox.Show(this, "No storage location was specified, or the file already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(data.data);
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Document;

            using (XmlWriter writer = XmlWriter.Create(filePath, writerSettings))
            {
                document.Save(writer);
                writer.Close();
            }

            MessageBox.Show(this, "The XML document was created successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
