using Dapper;
using DMARCReportAnalyzer.DMARC;
using ScottPlot;
using ScottPlot.WinForms;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Xml;

namespace DMARCReportAnalyzer;

public partial class FormMain : Form
{
    private DbConnection? DatabaseConnection;

    public FormMain()
    {
        InitializeComponent();
        ToolStripStatusLabelReportCount.Visible = false;
        ToolStripStatusLabelDatabaseName.Visible = false;
        ToolStripStatusLabelDatabaseName.Text = string.Empty;
        ToolStripStatusLabelReportCount.Text = string.Empty;
    }

    struct MessageOverTime
    {
        public DateTime report_date;
        public int message_count;
    }

    struct ReportsTimeSpan
    {
        public DateTime? report_begin;
        public DateTime? report_end;
    }

    struct DKIMSPF
    {
        public DateTime report_date;
        public int dkim_pass_count;
        public int dkim_fail_count;
        public int spf_pass_count;
        public int spf_fail_count;
    }

    private void OpenDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog dlgOpenDatabase = new OpenFileDialog())
        {
            dlgOpenDatabase.DefaultExt = "*.sqlite";
            dlgOpenDatabase.Filter = "SQLite Datenbank|*.sqlite";
            dlgOpenDatabase.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlgOpenDatabase.Multiselect = false;
            dlgOpenDatabase.ReadOnlyChecked = false;
            dlgOpenDatabase.SelectReadOnly = false;
            dlgOpenDatabase.ShowHelp = false;
            dlgOpenDatabase.ShowHiddenFiles = false;
            dlgOpenDatabase.ShowPinnedPlaces = true;
            dlgOpenDatabase.ShowPreview = false;
            dlgOpenDatabase.ShowReadOnly = false;
            dlgOpenDatabase.SupportMultiDottedExtensions = false;
            dlgOpenDatabase.Title = "Datenbank auswählen";

            if (dlgOpenDatabase.ShowDialog() == DialogResult.OK && File.Exists(dlgOpenDatabase.FileName))
            {
                DatabaseConnection = new SQLiteConnection("Data Source=" + dlgOpenDatabase.FileName + "; Version=3;FailIfMissing=True;");
                DatabaseConnection.Open();
            }
        }

        LoadDatabase();
    }

    private void NewDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (SaveFileDialog dlgNewDatabase = new SaveFileDialog())
        {
            dlgNewDatabase.CheckWriteAccess = true;
            dlgNewDatabase.DefaultExt = "*.sqlite";
            dlgNewDatabase.DereferenceLinks = true;
            dlgNewDatabase.Filter = "SQLite Datenbank|*.sqlite";
            dlgNewDatabase.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlgNewDatabase.OverwritePrompt = true;
            dlgNewDatabase.RestoreDirectory = true;
            dlgNewDatabase.ShowHelp = false;
            dlgNewDatabase.ShowHiddenFiles = false;
            dlgNewDatabase.ShowPinnedPlaces = true;
            dlgNewDatabase.SupportMultiDottedExtensions = false;
            dlgNewDatabase.Title = "Datenbank erstellen";

            if (dlgNewDatabase.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlgNewDatabase.FileName))
                {
                    MessageBox.Show("Die Datenbank " + Path.GetFileName(dlgNewDatabase.FileName) + " existiert bereits!", "Datenbank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DatabaseConnection = new SQLiteConnection("Data Source=" + dlgNewDatabase.FileName + "; Version=3;");
                DatabaseConnection.Open();

                DMARCReportAnalyzer.Database.Database database = new DMARCReportAnalyzer.Database.Database(DatabaseConnection);
                database.InitializeDatabase();
            }
        }

        LoadDatabase();
    }

    private void LoadDatabase()
    {
        if (DatabaseConnection is null)
        {
            return;
        }

        ReportsTimeSpan dateRange = DatabaseConnection.QuerySingle<ReportsTimeSpan>("SELECT DATE(MIN(report_begin)) report_begin, DATE(MAX(report_end)) report_end FROM metadata");

        if (dateRange.report_begin is null || dateRange.report_end is null)
        {
            return;
        }

        DateTime begin = dateRange.report_end.Value.AddDays(-7);
        DateTimePickerStart.Value = begin;
        DateTimePickerEnd.Value = dateRange.report_end.Value;

        int reportCount = DatabaseConnection.QuerySingleOrDefault<int>("SELECT COUNT(id) FROM feedback");
        ToolStripStatusLabelReportCount.Text = reportCount + " Reports";

        StatusStripMain.ShowItemToolTips = true;
        string databaseFilePath = ((SQLiteConnection)DatabaseConnection).FileName;
        ToolStripStatusLabelDatabaseName.Text = Path.GetFileName(databaseFilePath);
        ToolStripStatusLabelDatabaseName.ToolTipText = databaseFilePath;

        ToolStripStatusLabelReportCount.Visible = true;
        ToolStripStatusLabelDatabaseName.Visible = true;

        LoadSenderOverview(DateTimePickerStart.Value, DateTimePickerEnd.Value);
        LoadPlotMessagesOverTime(DateTimePickerStart.Value, DateTimePickerEnd.Value);
    }

    private void LoadSenderOverview(DateTime? begin, DateTime? end)
    {
        if (DatabaseConnection is null)
        {
            return;
        }

        IDataReader reader;

        if (begin is null || end is null)
        {
            reader = DatabaseConnection.ExecuteReader("SELECT source_ip, SUM(r.count) message_count FROM record r GROUP BY source_ip");
        }
        else
        {
            reader = DatabaseConnection.ExecuteReader("SELECT source_ip, SUM(r.count) message_count FROM record r INNER JOIN metadata_expansion me ON r.feedback_id = me.feedback_id WHERE me.report_date BETWEEN @begin AND @end GROUP BY source_ip", new { begin = begin, end = end });
        }

        DataSet dsSenderOverview = new DataSet();
        dsSenderOverview.EnforceConstraints = false;
        DataTable dtSenderOverview = dsSenderOverview.Tables.Add("senders");

        dtSenderOverview.Load(reader);
        DataGridViewSenderOverview.AutoGenerateColumns = false;
        DataGridViewSenderOverview.ReadOnly = true;
        DataGridViewSenderOverview.DataSource = dtSenderOverview;
        DataGridViewSenderOverview.Sort(dgvSenderOverview_MessageCount, System.ComponentModel.ListSortDirection.Descending);
    }

    private void LoadPlotMessagesOverTime(DateTime? begin, DateTime? end)
    {
        if (DatabaseConnection is null)
        {
            return;
        }

        string sqlMessagesOverTime = string.Empty;
        IEnumerable<MessageOverTime>? messages;

        if (begin is null || end is null)
        {
            sqlMessagesOverTime = @"
              SELECT report_date, SUM(r.count) message_count
              FROM metadata m 
                INNER JOIN metadata_expansion me ON me.feedback_id = m.feedback_id 
                INNER JOIN record r ON m.feedback_id = r.feedback_id
              GROUP BY report_date
              ORDER BY report_date ASC";
            messages = DatabaseConnection.Query<MessageOverTime>(sqlMessagesOverTime);
        }
        else
        {
            sqlMessagesOverTime = @"
              SELECT report_date, SUM(r.count) message_count
              FROM metadata m 
                INNER JOIN metadata_expansion me ON me.feedback_id = m.feedback_id 
                INNER JOIN record r ON m.feedback_id = r.feedback_id
              WHERE report_date BETWEEN @begin AND @end
              GROUP BY report_date
              ORDER BY report_date ASC";
            messages = DatabaseConnection.Query<MessageOverTime>(sqlMessagesOverTime, new { begin = begin, end = end });
        }

        DateTime[] dataX = messages!.ToList<MessageOverTime>().Select(x => x.report_date).ToArray<DateTime>();
        int[] dataY = messages!.ToList<MessageOverTime>().Select(x => x.message_count).ToArray<int>();

        double[] xs = dataX.Select(d => d.ToOADate()).ToArray();
        double[] ys = dataY.Select(v => (double)v).ToArray();

        PlotMessagesOverTime.Plot.Clear();
        PlotMessagesOverTime.Plot.Add.Bars(xs, ys);
        PlotMessagesOverTime.Plot.Axes.Margins(bottom: 0);

        var dtAx = PlotMessagesOverTime.Plot.Axes.DateTimeTicksBottom();

        dtAx.TickGenerator = new ScottPlot.TickGenerators.DateTimeFixedInterval(
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,   // major: 1 Tag
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,   // minor: 1 Tag (kannst du leer-labeln)
            dt => new DateTime(dt.Year, dt.Month, dt.Day));

        PlotMessagesOverTime.Plot.Axes.Bottom.TickLabelStyle.Rotation = 90;
        PlotMessagesOverTime.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
        PlotMessagesOverTime.Plot.Axes.Bottom.MinimumSize = 120;

        PlotMessagesOverTime.Refresh();

        IEnumerable<DKIMSPF> datas = Enumerable.Empty<DKIMSPF>();

        if (begin is null || end is null)
        {
            string sqlDKIMSPF = @"
              SELECT me.report_date,
                SUM(CASE WHEN pe.dkim = 'pass' THEN r.count ELSE 0 END) dkim_pass_count, 
                SUM(CASE WHEN pe.dkim = 'fail' THEN r.count ELSE 0 END) dkim_fail_count,
                SUM(CASE WHEN pe.spf = 'pass' THEN r.count ELSE 0 END) spf_pass_count,
                SUM(CASE WHEN pe.spf = 'fail' THEN r.count ELSE 0 END) spf_fail_count
              FROM policy_evaluated pe 
                INNER JOIN record r ON pe.record_id = r.id 
                INNER JOIN metadata_expansion me ON r.feedback_id = me.feedback_id 
              GROUP BY me.report_date
              ORDER BY me.report_date ASC";
            datas = DatabaseConnection.Query<DKIMSPF>(sqlDKIMSPF);
        } else
        {
            string sqlDKIMSPF = @"
              SELECT me.report_date,
                SUM(CASE WHEN pe.dkim = 'pass' THEN r.count ELSE 0 END) dkim_pass_count, 
                SUM(CASE WHEN pe.dkim = 'fail' THEN r.count ELSE 0 END) dkim_fail_count,
                SUM(CASE WHEN pe.spf = 'pass' THEN r.count ELSE 0 END) spf_pass_count,
                SUM(CASE WHEN pe.spf = 'fail' THEN r.count ELSE 0 END) spf_fail_count
              FROM policy_evaluated pe 
                INNER JOIN record r ON pe.record_id = r.id 
                INNER JOIN metadata_expansion me ON r.feedback_id = me.feedback_id 
              WHERE report_date BETWEEN @begin AND @end
              GROUP BY me.report_date
              ORDER BY me.report_date ASC";
            datas = DatabaseConnection.Query<DKIMSPF>(sqlDKIMSPF, new { begin = begin, end = end });
        }

        LoadPlotDKIM(datas, PlotDKIM);
        LoadPlotSPF(datas, PlotSPF);
    }

    private void LoadPlotDKIM(IEnumerable<DKIMSPF> datas, FormsPlot plot)
    {
        ScottPlot.Palettes.Category10 palette = new();
        List<Bar> bars = new();

        foreach (DKIMSPF data in datas)
        {
            bars.Add(new() { Position = data.report_date.ToOADate(), ValueBase = 0, Value = data.dkim_pass_count, FillColor = palette.GetColor(0) });
            bars.Add(new() { Position = data.report_date.ToOADate(), ValueBase = data.dkim_pass_count, Value = data.dkim_fail_count + data.dkim_pass_count, FillColor = palette.GetColor(1) });
        }

        plot.Plot.Add.Bars(bars);

        plot.Plot.Axes.Margins(bottom: 0);

        var dtAxx = plot.Plot.Axes.DateTimeTicksBottom();

        dtAxx.TickGenerator = new ScottPlot.TickGenerators.DateTimeFixedInterval(
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,
            dt => new DateTime(dt.Year, dt.Month, dt.Day));

        plot.Plot.Axes.Bottom.TickLabelStyle.Rotation = 90;
        plot.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
        plot.Plot.Axes.Bottom.MinimumSize = 120;

        plot.Plot.Legend.IsVisible = true;
        plot.Plot.Legend.Alignment = Alignment.UpperLeft;
        plot.Plot.Legend.ManualItems.Clear();
        plot.Plot.Legend.ManualItems.Add(new() { LabelText = "DKIM Pass", FillColor = palette.GetColor(0) });
        plot.Plot.Legend.ManualItems.Add(new() { LabelText = "DKIM Fail", FillColor = palette.GetColor(1) });

        plot.Refresh();
    }

    private void LoadPlotSPF(IEnumerable<DKIMSPF> datas, FormsPlot plot)
    {
        ScottPlot.Palettes.Category10 palette = new();
        List<Bar> bars = new();

        foreach (DKIMSPF data in datas)
        {
            bars.Add(new() { Position = data.report_date.ToOADate(), ValueBase = 0, Value = data.spf_pass_count, FillColor = palette.GetColor(0) });
            bars.Add(new() { Position = data.report_date.ToOADate(), ValueBase = data.spf_pass_count, Value = data.spf_fail_count + data.spf_pass_count, FillColor = palette.GetColor(1) });
        }

        plot.Plot.Add.Bars(bars);

        plot.Plot.Axes.Margins(bottom: 0);

        var dtAxx = plot.Plot.Axes.DateTimeTicksBottom();

        dtAxx.TickGenerator = new ScottPlot.TickGenerators.DateTimeFixedInterval(
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,
            new ScottPlot.TickGenerators.TimeUnits.Day(), 1,
            dt => new DateTime(dt.Year, dt.Month, dt.Day));

        plot.Plot.Axes.Bottom.TickLabelStyle.Rotation = 90;
        plot.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
        plot.Plot.Axes.Bottom.MinimumSize = 120;

        plot.Plot.Legend.IsVisible = true;
        plot.Plot.Legend.Alignment = Alignment.UpperLeft;
        plot.Plot.Legend.ManualItems.Clear();
        plot.Plot.Legend.ManualItems.Add(new() { LabelText = "SPF Pass", FillColor = palette.GetColor(0) });
        plot.Plot.Legend.ManualItems.Add(new() { LabelText = "SPF Fail", FillColor = palette.GetColor(1) });

        plot.Refresh();
    }

    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (DatabaseConnection is null)
        {
            MessageBox.Show(this, "Es muss eine Datenbank geöffnet sein!", "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using (FormImport frmImport = new FormImport(DatabaseConnection))
        {
            frmImport.StartPosition = FormStartPosition.CenterParent;
            frmImport.ShowDialog(this);
        }
    }

    private void ButtonExportXML_Click(object sender, EventArgs e)
    {
        if (DatabaseConnection is null)
        {
            MessageBox.Show(this, "Es muss eine Datenbank geöffnet sein!", "Export XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string exportFolder = string.Empty;

        using (FolderBrowserDialog dlgSelectFolder = new FolderBrowserDialog())
        {
            dlgSelectFolder.Description = "Speicherort für XML-Dokumente";
            dlgSelectFolder.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlgSelectFolder.Multiselect = false;
            dlgSelectFolder.ShowHiddenFiles = false;
            dlgSelectFolder.ShowNewFolderButton = true;
            dlgSelectFolder.ShowPinnedPlaces = false;
            dlgSelectFolder.UseDescriptionForTitle = true;

            if (dlgSelectFolder.ShowDialog(this) == DialogResult.OK)
            {
                exportFolder = dlgSelectFolder.SelectedPath;
            }
        }

        if (!Directory.Exists(exportFolder))
        {
            MessageBox.Show(this, "Das Exportverzeichnis ist nicht vorhanden!", "Export XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var documents = DatabaseConnection.Query("SELECT id, data FROM feedback");

        foreach (var document in documents)
        {
            string exportFilePath = Path.Combine(exportFolder, document.id + ".xml");
            XmlDocument documentXml = new XmlDocument();
            documentXml.LoadXml(document.data);
            documentXml.Save(exportFilePath);
        }
    }

    private void ButtonImportXML_Click(object sender, EventArgs e)
    {
        if (DatabaseConnection is null)
        {
            MessageBox.Show(this, "Es muss eine Datenbank geöffnet sein!", "Import XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string importFolder = string.Empty;

        using (FolderBrowserDialog dlgSelectFolder = new FolderBrowserDialog())
        {
            dlgSelectFolder.Description = "Speicherort für XML-Dokumente";
            dlgSelectFolder.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlgSelectFolder.Multiselect = false;
            dlgSelectFolder.ShowHiddenFiles = false;
            dlgSelectFolder.ShowNewFolderButton = true;
            dlgSelectFolder.ShowPinnedPlaces = false;
            dlgSelectFolder.UseDescriptionForTitle = true;

            if (dlgSelectFolder.ShowDialog(this) == DialogResult.OK)
            {
                importFolder = dlgSelectFolder.SelectedPath;
            }
        }

        if (!Directory.Exists(importFolder))
        {
            MessageBox.Show(this, "Das Importverzeichnis ist nicht vorhanden!", "Import XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        List<string> txtFiles = [.. Directory.EnumerateFiles(importFolder, "*.xml", SearchOption.TopDirectoryOnly)];

        foreach (string currentFile in txtFiles)
        {
            XmlDocument document = new XmlDocument();
            document.Load(currentFile);

            IFeedback? feedback = FeedbackFactory.Create(document);

            if (feedback is null)
            {
                continue;
            }

            IStorage? storage = StorageFactory.Create(feedback, DatabaseConnection);

            if (storage is null)
            {
                continue;
            }

            storage.Save(new Report
            {
                Document = document,
                Message = null,
                Feedback = feedback
            });
        }
    }

    private void DateTimePickerStart_ValueChanged(object sender, EventArgs e)
    {
        LoadSenderOverview(DateTimePickerStart.Value, DateTimePickerEnd.Value);
        LoadPlotMessagesOverTime(DateTimePickerStart.Value, DateTimePickerEnd.Value);
    }

    private void DateTimePickerEnd_ValueChanged(object sender, EventArgs e)
    {
        if (DatabaseConnection is not null)
        {
            LoadSenderOverview(DateTimePickerStart.Value, DateTimePickerEnd.Value);
            LoadPlotMessagesOverTime(DateTimePickerStart.Value, DateTimePickerEnd.Value);
        }
    }

    private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (DatabaseConnection is null)
        {
            MessageBox.Show("Es wird eine Datenbankverbindung benötigt.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using (FormReports frmReports = new FormReports(DatabaseConnection))
        {
            frmReports.StartPosition = FormStartPosition.CenterParent;
            frmReports.ShowDialog(this);
        }
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        DatabaseConnection?.Close();
    }
}
