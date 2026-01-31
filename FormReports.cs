using Dapper;
using System.Data;

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
}
