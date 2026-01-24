using DMARCReportAnalyzer.Decompression;
using DMARCReportAnalyzer.DMARC;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using Serilog;
using System.Data;
using System.Xml;

namespace DMARCReportAnalyzer;

struct ComboBoxItemSecureSocketOption
{
    public string Text { get; set; }
    public MailKit.Security.SecureSocketOptions Value { get; set; }
}

struct ComboBoxItemPort
{
    public string Text { get; set; }
    public int Value { get; set; }
}

/// <summary>
/// Form to import DMARC reports to the database.
/// </summary>
public partial class FormImport : Form
{
    /// <summary>
    /// The database connection.
    /// </summary>
    private readonly IDbConnection Connection;

    




    /// <summary>
    /// Constructor to initialize this form to import DMARC reports.
    /// </summary>
    /// <param name="connection">The database connection.</param>
    public FormImport(IDbConnection connection)
    {
        InitializeComponent();
        Connection = connection;
        FillComboBoxPorts(cmbServerPort, 993);
        FillComboBoxEncryption(cmbEncryption, SecureSocketOptions.SslOnConnect);
    }

    /// <summary>
    /// Button to start the import of DMARC reports.
    /// </summary>
    private void BtnImport_Click(object sender, EventArgs e)
    {
        try
        {
            this.UseWaitCursor = true;
            ImportFromIMAP();
        }
        finally
        {
            this.UseWaitCursor = false;
        }
    }

    /// <summary>
    /// Fills a <see cref="ComboBox"/> with some default ports.
    /// </summary>
    /// <param name="control">The <see cref="ComboBox"/> to be filled.</param>
    /// <param name="defaultValue">The default value to be set on the <see cref="ComboBox"/>.</param>
    private void FillComboBoxPorts(ComboBox control, int defaultValue)
    {
        List<ComboBoxItemPort> listPort =
        [
            new ComboBoxItemPort { Text = "143", Value = 143 },
            new ComboBoxItemPort { Text = "993", Value = 993 },
        ];

        control.DataSource = listPort;
        control.ValueMember = "Value";
        control.DisplayMember = "Text";
        
        if (listPort.Exists(port => port.Value == defaultValue))
        {
            control.SelectedValue = defaultValue;
        }
    }

    /// <summary>
    /// Fills a <see cref="ComboBox"/> with all encryption methods.
    /// </summary>
    /// <param name="control">The <see cref="ComboBox"/> to be filled.</param>
    /// <param name="defaultValue">The default value to be set on the <see cref="ComboBox"/>.</param>
    private void FillComboBoxEncryption(ComboBox control, MailKit.Security.SecureSocketOptions defaultValue)
    {
        List<ComboBoxItemSecureSocketOption> listEncryption =
        [
            new ComboBoxItemSecureSocketOption { Text = "None", Value = MailKit.Security.SecureSocketOptions.None },
            new ComboBoxItemSecureSocketOption { Text = "STARTTLS", Value = MailKit.Security.SecureSocketOptions.StartTls },
            new ComboBoxItemSecureSocketOption { Text = "SSL/TLS", Value = MailKit.Security.SecureSocketOptions.SslOnConnect },
        ];

        control.DataSource = listEncryption;
        control.ValueMember = "Value";
        control.DisplayMember = "Text";

        if (listEncryption.Exists(encryption => encryption.Value == defaultValue))
        {
            control.SelectedValue = defaultValue;
        }
    }

    /// <summary>
    /// Determine the necessary folder access for the mail account based on the actions after import.
    /// </summary>
    /// <param name="source">The source folder from which the messages are imported.</param>
    /// <param name="archive">The archive folder to which the imported messages are to be moved after import.</param>
    /// <returns>The necessary folder access for the mail account based on the actions after import.</returns>
    private MailKit.FolderAccess GetFolderAccess(MailKit.IMailFolder source, MailKit.IMailFolder? archive)
    {
        if (chkDeleteMessage.CheckState == CheckState.Checked)
        {
            return MailKit.FolderAccess.ReadWrite;
        }
        else
        {
            if (chkMarkAsRead.CheckState == CheckState.Checked)
            {
                return MailKit.FolderAccess.ReadWrite;
            }

            if (archive is not null && archive.FullName != source.FullName)
            {
                return MailKit.FolderAccess.ReadWrite;
            }
        }

        return MailKit.FolderAccess.ReadOnly;
    }

    /// <summary>
    /// Gets a folder (<see cref="MailKit.IMailFolder"/>) of the mail account.
    /// </summary>
    /// <param name="folder">The path to the folder in the mail account.</param>
    /// <param name="client">The client to access the mail account.</param>
    /// <returns>The folder in the mail account or null if the folder could not be found.</returns>
    private MailKit.IMailFolder? GetFolder(string folder, ImapClient client)
    {
        try
        {
            MailKit.FolderNamespace? firstPersonalNamespace = client.PersonalNamespaces.FirstOrDefault<MailKit.FolderNamespace>();

            if (firstPersonalNamespace is null)
            {
                return null;
            }

            foreach (char replaceChar in new char[] { '.', '/', '\\' })
            {
                folder = folder.Replace(replaceChar, firstPersonalNamespace.DirectorySeparator);
            }

            return client.GetFolder(folder);
        }
        catch (FolderNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Imports all DMARC reports from IMAP mailbox.
    /// </summary>
    /// <returns>The status whether the import of DMARC reports was successful.</returns>
    private bool ImportFromIMAP()
    {
        Log.Information("Connection: {connection}", Connection.ConnectionString);
        Log.Information("Start of Import: {date}", DateTime.Now);

        string server = txtIncomingServer.Text.Trim();
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;
        string serverPort = cmbServerPort.Text;
        
        if (string.IsNullOrWhiteSpace(server))
        {
            MessageBox.Show(this, "Server was not specified.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Server was not specified.");
            txtIncomingServer.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            MessageBox.Show(this, "Username was not specified.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Username was not specified.");
            txtUsername.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(this, "Password was not specified.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Password was not specified.");
            txtPassword.Focus();
            return false;
        }

        if (!int.TryParse(serverPort, out int port))
        {
            MessageBox.Show(this, "No valid port was entered.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Port is not valid: {port}", serverPort);
            cmbServerPort.Focus();
            return false;
        }

        string pathSourceFolder = txtSourceFolder.Text.Trim();
        string pathArchiveFolder = txtArchiveFolder.Text.Trim();

        if (string.IsNullOrWhiteSpace(pathSourceFolder))
        {
            MessageBox.Show("Source Folder was not specified.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Source Folder was not specified.");
            txtSourceFolder.Focus();
            return false;
        }

        using ImapClient client = new();

        try
        {
            client.Connect(server, port, (MailKit.Security.SecureSocketOptions)cmbEncryption.SelectedValue!);
            client.Authenticate(username, password);
            Log.Information("Login to mailbox successful.");
        }
        catch (Exception e)
        {
            MessageBox.Show(this, "Login to mailbox failed: " + e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log.Error("Login to mailbox failed: {error}", e.Message);
            return false;
        }

        MailKit.IMailFolder? sourceFolder = null;
        MailKit.IMailFolder? archiveFolder = null;

        sourceFolder = GetFolder(pathSourceFolder, client);

        if (sourceFolder is null)
        {
            MessageBox.Show(this, "The source folder could not be found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log.Warning("Source folder not found: {folder}", pathSourceFolder);
            txtSourceFolder.Focus();
            return false;
        }

        if (chkDeleteMessage.CheckState == CheckState.Unchecked && !string.IsNullOrWhiteSpace(pathArchiveFolder))
        {
            if (!client.Capabilities.HasFlag(ImapCapabilities.Move))
            {
                DialogResult dlgResult = MessageBox.Show("Messages cannot be moved on this server.\n\nContinue?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dlgResult != DialogResult.Yes)
                {
                    Log.Information("Import was canceled by the user because messages cannot be moved on the server.");
                    return false;
                }
            }

            archiveFolder = GetFolder(pathArchiveFolder, client);

            if (archiveFolder is null)
            {
                MessageBox.Show(this, "The archive folder could not be found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.Warning("Archive folder not found: {folder}", pathSourceFolder);
                txtArchiveFolder.Focus();
                return false;
            }
        }

        sourceFolder.Open(GetFolderAccess(sourceFolder, archiveFolder));
       
        IList<MailKit.UniqueId> messageUIds = sourceFolder.Search(MailKit.Search.SearchQuery.All);
        Log.Information("Count of messages: {count}", messageUIds.Count);

        List<string> cleanupFiles = [];
        int cntNewReports = 0;

        foreach (MailKit.UniqueId messageUId in messageUIds)
        {
            bool hasErrors = false;

            MimeMessage message = sourceFolder.GetMessage(messageUId);

            if (message is null)
            {
                Log.Error("Message could not be loaded: {uid}", messageUId.Id);
                continue;
            }

            List<string> reportPaths = GetReportsFromMessage(message);

            if (reportPaths.Count == 0)
            {
                Log.Information("No archive file with DMARC report found.");
                continue;
            }

            Log.Information("Current message: {@message}", new { message.Subject, message.Date, message.MessageId });
            Log.Information("Count of archive files with DMARC report: {count}", reportPaths.Count);
            cleanupFiles.AddRange(reportPaths);
        
            foreach (string reportPath in reportPaths)
            {
                if (!File.Exists(reportPath))
                {
                    Log.Error("Archive file not found: {path}", reportPath);
                    continue;
                }

                Log.Information("Current archive file: {path}", reportPath);

                DecompressReportContext decompressContext = new();

                switch (Path.GetExtension(reportPath).ToLower())
                {
                    case ".gz":
                        decompressContext.SetStrategy(new DecompressReportGZIP());
                        break;
                    case ".zip":
                        decompressContext.SetStrategy(new DecompressReportZIP());
                        break;
                    default:
                        Log.Warning("Unknown file extension: {extension}", Path.GetExtension(reportPath).ToLower());
                        continue;
                }

                string filePathXml = decompressContext.Decompress(reportPath);
                Log.Information("Current DMARC report: {path}", filePathXml);
                cleanupFiles.Add(filePathXml);

                using (FileStream fileStreamXml = new(filePathXml, FileMode.Open))
                {
                    XmlDocument documentXml = new();
                    documentXml.Load(fileStreamXml);

                    IFeedback? feedback = FeedbackFactory.Create(documentXml);

                    if (feedback is null)
                    {
                        Log.Error("Feedback from the DMARC report could not be found.");
                        hasErrors = true;
                        continue;
                    }

                    IStorage? storage = StorageFactory.Create(feedback, Connection);

                    if (storage is null)
                    {
                        Log.Error("The storage location for the DMARC report feedback could not be determined.");
                        hasErrors = true;
                        continue;
                    }

                    if (storage.Save(new Report { Document = documentXml, Message = message, Feedback = feedback }))
                    {
                        cntNewReports++;
                        Log.Information("DMARC report has been successfully saved to the database.");
                    }
                    else
                    {
                        Log.Error("DMARC report could not be saved to the database.");
                        hasErrors = true;
                    }
                }
            }

            Log.Information("Delete temporary files.");

            foreach (string cleanupFile in cleanupFiles)
            {
                if (File.Exists(cleanupFile))
                {
                    File.Delete(cleanupFile);
                }
            }

            if (!hasErrors)
            {
                if (chkDeleteMessage.CheckState == CheckState.Checked)
                {
                    sourceFolder.AddFlags(messageUId, MessageFlags.Deleted, true);
                }
                else
                {
                    if (chkMarkAsRead.CheckState == CheckState.Checked)
                    {
                        sourceFolder.AddFlags(messageUId, MailKit.MessageFlags.Seen, true);
                    }

                    if (archiveFolder is not null && archiveFolder.FullName != sourceFolder.FullName)
                    {
                        sourceFolder.MoveTo(messageUId, archiveFolder);
                    }
                }
            }
        }

        if (chkDeleteMessage.CheckState == CheckState.Checked)
        {
            sourceFolder.Expunge();
        }

        Log.Information("End of Import: {date}", DateTime.Now);
        MessageBox.Show(this, "Import completed. " + cntNewReports + " new DMARC reports.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        return true;
    }





    private bool IsDMARCSubject(string subject)
    {
        return subject.Contains("Submitter", StringComparison.InvariantCultureIgnoreCase) && subject.Contains("Report-ID", StringComparison.InvariantCultureIgnoreCase);
    }

    private List<string> GetReportsFromMessage(MimeMessage message)
    {
        List<string> reports = new List<string>();

        if (!IsDMARCSubject(message.Subject))
        {
            return [];
        }

        foreach (MimePart attachment in message.Attachments)
        {
            if (attachment.ContentType.IsMimeType("application", "gzip"))
            {
                string reportFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".gz");
                Directory.CreateDirectory(Path.GetDirectoryName(reportFilePath)!);

                using (FileStream reportFileStream = File.Create(reportFilePath))
                {
                    attachment.Content.DecodeTo(reportFileStream);
                }

                reports.Add(reportFilePath);
            }
            else if (attachment.ContentType.IsMimeType("application", "zip"))
            {
                string reportFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zip");
                Directory.CreateDirectory(Path.GetDirectoryName(reportFilePath)!);

                using (FileStream reportFileStream = File.Create(reportFilePath))
                {
                    attachment.Content.DecodeTo(reportFileStream);
                }

                reports.Add(reportFilePath);
            }
        }

        return reports;
    }
}
