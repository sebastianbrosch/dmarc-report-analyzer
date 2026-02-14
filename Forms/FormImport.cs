using Dapper;
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

struct MailboxList
{
    public string Id { get; set; }
    public string Name { get; set; }
}

struct MailboxItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public int Encryption { get; set; }
    public string Source { get; set; }
    public string? Archive { get; set; }
    public bool? MarkAsRead { get; set; }
    public bool? DeleteMessage { get; set; }
}

/// <summary>
/// Form to import DMARC reports to the database.
/// </summary>
public partial class FormImport : Form
{
    bool IsInitialization = false;

    string MailboxId = string.Empty;

    /// <summary>
    /// The database connection.
    /// </summary>
    private readonly IDbConnection Connection;


    const string NEW_ENTRY = "[New Entry]";





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
            if (folder.ToUpper().Trim() == "[INBOX]")
            {
                return client.Inbox;
            }

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
    /// Removes a list of files.
    /// </summary>
    /// <param name="filePaths">A list with file paths to be deleted.</param>
    private void DeleteFiles(List<string> filePaths)
    {
        foreach (string filePath in filePaths)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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

        pbImport.Maximum = messageUIds.Count;
        pbImport.Step = 1;
        pbImport.Value = 0;
        pbImport.Visible = true;

        foreach (MailKit.UniqueId messageUId in messageUIds)
        {
            pbImport.PerformStep();
            bool hasErrors = false;

            MimeMessage message = sourceFolder.GetMessage(messageUId);

            if (message is null)
            {
                Log.Error("Message could not be loaded: {uid}", messageUId.Id);
                continue;
            }

            List<string> reportPaths = GetReportsFromMessage(message);

            Log.Information("Current message: {@message}", new { message.Subject, message.Date, message.MessageId });
            Log.Information("Count of archive files with DMARC report: {count}", reportPaths.Count);

            if (reportPaths.Count == 0)
            {
                Log.Information("No archive file with DMARC report found.");
                continue;
            }
            else if (reportPaths.Count > 1)
            {
                DeleteFiles(reportPaths);
                Log.Information("There is more than one DMARC report available.");
                continue;
            }

            string reportPath = reportPaths.First<string>();
            cleanupFiles.AddRange(reportPath);

            if (string.IsNullOrWhiteSpace(reportPath) || !File.Exists(reportPath))
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
                FileName? fileName = FileName.Create(reportPath);
                IFeedback? feedback = FeedbackFactory.Create(documentXml);

                if (fileName is null)
                {
                    Log.Error("File name could not be parsed.");
                    hasErrors = true;
                    continue;
                }

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

                Report report = new() { Document = documentXml, Message = message, Feedback = feedback, FileName = fileName };

                if (storage.Exists(report))
                {
                    if (storage.Exists(report, true))
                    {
                        Log.Information("DMARC report already exists in database.");
                        continue;
                    }
                    else
                    {
                        Log.Warning("DMARC report already exists in database, but with different information.");
                        hasErrors = true;
                        continue;
                    }
                }

                if (storage.Save(report))
                {
                    cntNewReports++;
                    Log.Information("DMARC report has been successfully saved to the database.");
                }
                else
                {
                    Log.Error("DMARC report could not be saved to the database.");
                    hasErrors = true;
                    continue;
                }
            }
            
            Log.Information("Delete temporary files.");
            DeleteFiles(cleanupFiles);

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
        pbImport.Visible = false;
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
            if (attachment.ContentType.IsMimeType("application", "gzip") || attachment.ContentType.IsMimeType("application", "zip"))
            {
                string reportFilePath = Path.Combine(Path.GetTempPath(), attachment.FileName);
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

    private void FillComboBoxMailbox()
    {
        List<MailboxList> mailboxes = Connection.Query<MailboxList>("SELECT id, name FROM mailbox").ToList<MailboxList>();
        mailboxes.Add(new MailboxList { Id = string.Empty, Name = NEW_ENTRY });

        IsInitialization = true;
        cmbName.DataSource = mailboxes;
        cmbName.DisplayMember = "Name";
        cmbName.ValueMember = "Id";
        IsInitialization = false;

        if (mailboxes.Count > 0)
        {
            cmbName.SelectedValue = mailboxes.First<MailboxList>().Id;
        }

        LoadMailbox();
    }

    private void FormImport_Load(object sender, EventArgs e)
    {
        FillComboBoxMailbox();
    }

    private void LoadMailbox()
    {
        MailboxId = (string)cmbName.SelectedValue!;

        if (!string.IsNullOrWhiteSpace(MailboxId))
        {
            MailboxItem mailbox = Connection.QueryFirst<MailboxItem>("SELECT id, name, server, port, username, encryption, source, archive, mark_as_read AS MarkAsRead, delete_message AS DeleteMessage FROM mailbox WHERE id = @id", new { id = MailboxId });

            txtIncomingServer.Text = mailbox.Server;
            cmbServerPort.Text = mailbox.Port.ToString();
            txtUsername.Text = mailbox.Username;
            cmbEncryption.SelectedValue = Enum.Parse(typeof(MailKit.Security.SecureSocketOptions), mailbox.Encryption.ToString());
            txtSourceFolder.Text = mailbox.Source;
            txtArchiveFolder.Text = mailbox.Archive;
            chkMarkAsRead.CheckState = mailbox.MarkAsRead is null ? CheckState.Unchecked : (mailbox.MarkAsRead == true ? CheckState.Checked : CheckState.Unchecked);
            chkDeleteMessage.CheckState = mailbox.DeleteMessage is null ? CheckState.Unchecked : (mailbox.DeleteMessage == true ? CheckState.Checked : CheckState.Unchecked);
        }
        else
        {
            txtIncomingServer.Text = string.Empty;
            cmbServerPort.SelectedValue = 993;
            txtUsername.Text = string.Empty;
            cmbEncryption.SelectedValue = MailKit.Security.SecureSocketOptions.SslOnConnect;
            txtSourceFolder.Text = string.Empty;
            txtArchiveFolder.Text = string.Empty;
            chkMarkAsRead.CheckState = CheckState.Unchecked;
            chkDeleteMessage.CheckState = CheckState.Unchecked;
        }
    }

    private void SaveMailbox()
    {
        MailboxItem mailbox;

        if (cmbName.Text == string.Empty)
        {
            MessageBox.Show(this, "No name was specified for the mailbox.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(MailboxId))
        {
            mailbox = new MailboxItem();
            mailbox.Id = Guid.NewGuid().ToString("N").ToUpper();
            mailbox.Name = cmbName.Text;
            mailbox.Server = txtIncomingServer.Text.Trim();
            mailbox.Port = int.Parse(cmbServerPort.Text);
            mailbox.Username = txtUsername.Text.Trim();
            mailbox.Encryption = Convert.ToInt32(cmbEncryption.SelectedValue!);
            mailbox.Source = txtSourceFolder.Text.Trim();
            mailbox.Archive = txtArchiveFolder.Text.Trim();
            mailbox.MarkAsRead = chkMarkAsRead.CheckState == CheckState.Checked;
            mailbox.DeleteMessage = chkDeleteMessage.CheckState == CheckState.Checked;
            Connection.Execute("INSERT INTO mailbox (id, name, server, port, username, encryption, source, archive, mark_as_read, delete_message) VALUES (@Id, @Name, @Server, @Port, @Username, @Encryption, @Source, @Archive, @MarkAsRead, @DeleteMessage)", mailbox);
        }
        else
        {
            mailbox = new MailboxItem();
            mailbox.Id = MailboxId;
            mailbox.Name = cmbName.Text;
            mailbox.Server = txtIncomingServer.Text.Trim();
            mailbox.Port = int.Parse(cmbServerPort.Text);
            mailbox.Username = txtUsername.Text.Trim();
            mailbox.Encryption = Convert.ToInt32(cmbEncryption.SelectedValue!);
            mailbox.Source = txtSourceFolder.Text.Trim();
            mailbox.Archive = txtArchiveFolder.Text.Trim();
            mailbox.MarkAsRead = chkMarkAsRead.CheckState == CheckState.Checked;
            mailbox.DeleteMessage = chkDeleteMessage.CheckState == CheckState.Checked;
            Connection.Execute("UPDATE mailbox SET name = @Name, server = @Server, port = @Port, username = @Username, encryption = @Encryption, source = @Source, archive = @Archive, mark_as_read = @MarkAsRead, delete_message = @DeleteMessage WHERE id = @Id", mailbox);
        }

        FillComboBoxMailbox();
        cmbName.SelectedValue = mailbox.Id;
        LoadMailbox();

    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        SaveMailbox();
    }

    private void cmbName_SelectionChangeCommitted(object sender, EventArgs e)
    {
        LoadMailbox();
    }
}
