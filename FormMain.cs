using Dapper;
using DMARCReportAnalyzer.DMARC;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
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
        public DateTime report_begin;
        public DateTime report_end;
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
        DateTimePickerStart.Value = dateRange.report_begin;
        DateTimePickerEnd.Value = dateRange.report_end;

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
        } else
        {
            reader = DatabaseConnection.ExecuteReader("SELECT source_ip, SUM(r.count) message_count FROM record r INNER JOIN metadata m ON r.feedback_id = m.feedback_id WHERE DATE(m.report_begin) >= @begin AND DATE(m.report_end) <= @end GROUP BY source_ip", new { begin = begin, end = end });
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
            sqlMessagesOverTime = """
            SELECT SUM(r.count) message_count,
            	CASE WHEN DATE(report_begin) = DATE(report_end) THEN 
            		DATE(report_begin) 
            	ELSE
            		CASE WHEN CAST(strftime('%H', report_begin) AS INT) = 0 AND CAST(strftime('%H', report_end) AS INT) = 0 THEN 
            			DATE(report_begin) 
            		ELSE 
            			CASE WHEN CAST(strftime('%H', report_end) AS INT) >= 12 THEN 
            				DATE(report_end) 
            			ELSE 
            				CASE WHEN CAST(strftime('%H', report_end) AS INT) <= 11 THEN 
            					DATE(report_begin) 
            				ELSE 
            					NULL 
            				END 
            			END 
            		END 
            	END report_date
            FROM metadata m INNER JOIN record r ON m.feedback_id = r.feedback_id
            GROUP BY report_date
            ORDER BY report_date ASC
            """;
            messages = DatabaseConnection.Query<MessageOverTime>(sqlMessagesOverTime);
        }
        else
        {
            sqlMessagesOverTime = """
            SELECT SUM(r.count) message_count,
            	CASE WHEN DATE(report_begin) = DATE(report_end) THEN 
            		DATE(report_begin) 
            	ELSE
            		CASE WHEN CAST(strftime('%H', report_begin) AS INT) = 0 AND CAST(strftime('%H', report_end) AS INT) = 0 THEN 
            			DATE(report_begin) 
            		ELSE 
            			CASE WHEN CAST(strftime('%H', report_end) AS INT) >= 12 THEN 
            				DATE(report_end) 
            			ELSE 
            				CASE WHEN CAST(strftime('%H', report_end) AS INT) <= 11 THEN 
            					DATE(report_begin) 
            				ELSE 
            					NULL 
            				END 
            			END 
            		END 
            	END report_date
            FROM metadata m INNER JOIN record r ON m.feedback_id = r.feedback_id
            WHERE DATE(report_date) >= @begin AND DATE(report_date) <= @end
            GROUP BY report_date
            ORDER BY report_date ASC
            """;
            messages = DatabaseConnection.Query<MessageOverTime>(sqlMessagesOverTime, new { begin = begin, end = end });
        }

        DateTime[] dataX = messages!.ToList<MessageOverTime>().Select(x => x.report_date).ToArray<DateTime>();
        int[] dataY = messages!.ToList<MessageOverTime>().Select(x => x.message_count).ToArray<int>();
        PlotMessagesOverTime.Plot.Clear();
        PlotMessagesOverTime.Plot.Add.Scatter(dataX, dataY);
        var axis = PlotMessagesOverTime.Plot.Axes.DateTimeTicksBottom();
        PlotMessagesOverTime.Refresh();
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
        LoadSenderOverview(DateTimePickerStart.Value, DateTimePickerEnd.Value);
        LoadPlotMessagesOverTime(DateTimePickerStart.Value, DateTimePickerEnd.Value);
    }
}
