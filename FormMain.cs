using Dapper;
using System.Data.Common;
using System.Data.SQLite;

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
    }
}
