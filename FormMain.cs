using Dapper;
using DMARCReportAnalyzer.DMARC;
using System.Data.Common;
using System.Data.SQLite;
using System.Xml;

namespace DMARCReportAnalyzer
{
    public partial class FormMain : Form
    {
        private DbConnection? DatabaseConnection;

        public FormMain()
        {
            InitializeComponent();
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
    }
}
