using DMARCReportAnalyzer.Decompression;
using DMARCReportAnalyzer.DMARC;
using MailKit.Net.Imap;
using MimeKit;
using Serilog;
using System.Data;
using System.Xml;

namespace DMARCReportAnalyzer;

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
    }

    /// <summary>
    /// Button to start the import of DMARC reports.
    /// </summary>
    private void ButtonImport_Click(object sender, EventArgs e)
    {
        ImportFromMailServer();
    }

    /// <summary>
    /// Imports DMARC reports from a mail server.
    /// </summary>
    private void ImportFromMailServer()
    {
        Log.Information("Datenbank: {database}", Connection.ConnectionString);
        Log.Information("Import Beginn: {date}", DateTime.Now);
        
        bool isNumericPort = int.TryParse(TextBoxPort.Text, out int port);

        if (!isNumericPort)
        {
            return;
        }

        string server = TextBoxServer.Text;
        string username = TextBoxUsername.Text;
        string password = TextBoxPassword.Text;

        if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        using ImapClient client = new ImapClient();

        // connect to the mail server to get all messages from inbox.
        try
        {
            client.Connect(server, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);
            Log.Information("Anmeldung am Postfach erfolgreich.");
        } catch (Exception e)
        {
            MessageBox.Show(this, "Anmeldung am Postfach fehlgeschlagen: " + e.Message, "Anmeldung am Postfach", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log.Error("Anmeldung am Postfach fehlgeschlagen: {error}", e.Message);
            return;
        }

        MailKit.IMailFolder sourceFolder;
        MailKit.IMailFolder? archiveFolder;

        if (string.IsNullOrWhiteSpace(TextBoxSourceFolder.Text) || TextBoxSourceFolder.Text.Equals("INBOX", StringComparison.InvariantCultureIgnoreCase))
        {
            sourceFolder = client.Inbox;
        } else
        {
            sourceFolder = client.Inbox.GetSubfolder(TextBoxSourceFolder.Text);
        }

        if (string.IsNullOrWhiteSpace(TextBoxArchiveFolder.Text))
        {
            archiveFolder = null;
            sourceFolder.Open(MailKit.FolderAccess.ReadOnly);
        } else
        {
            if (client.Capabilities.HasFlag(ImapCapabilities.Move))
            {
                archiveFolder = client.GetFolder(TextBoxArchiveFolder.Text);
                sourceFolder.Open(MailKit.FolderAccess.ReadWrite);
            } else
            {
                archiveFolder = null;
                sourceFolder.Open(MailKit.FolderAccess.ReadOnly);
            }
        }

        // get all the mails.
        IList<MailKit.UniqueId> inboxUIds = client.Inbox.Search(MailKit.Search.SearchQuery.All);
        Log.Information("Anzahl der E-Mails: {count}", inboxUIds.Count);

        // process every mail of the inbox.
        foreach (MailKit.UniqueId inboxUId in inboxUIds)
        {
            MimeMessage message = client.Inbox.GetMessage(inboxUId);
            bool hasFailure = false;

            if (message is null)
            {
                Log.Error("Nachricht konnte nicht ermittelt werden.");
                continue;
            }

            List<string> reportPaths = GetReportsFromMessage(message);
            Log.Information("Aktuelle Nachricht: {@message}", new { message.Subject, message.Date, message.MessageId });
            Log.Information("Anzahl der Archivdateien für DMARC-Reports: {count}", reportPaths.Count);

            if (reportPaths.Count == 0)
            {
                Log.Warning("Keine Archivdateien mit DMARC-Reports gefunden.");
                continue;
            }

            foreach (string reportPath in reportPaths)
            {
                if (!File.Exists(reportPath))
                {
                    Log.Error("Archivdatei nicht gefunden: {path}", reportPath);
                    continue;
                }
            
                Log.Information("Archivdatei: {path}", reportPath);
                
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
                        Log.Warning("Unbekannte Dateiendung: {extension}", Path.GetExtension(reportPath).ToLower());
                        continue;
                }

                string filePathXml = decompressContext.Decompress(reportPath);
                Log.Information("DMARC-Report: {path}", filePathXml);

                using (FileStream fileStreamXml = new FileStream(filePathXml, FileMode.Open))
                {
                    XmlDocument documentXml = new XmlDocument();
                    documentXml.Load(fileStreamXml);

                    IFeedback? feedback = FeedbackFactory.Create(documentXml);

                    if (feedback is null)
                    {
                        Log.Error("Feedback des DMARC-Reports konnte nicht ermittelt werden.");
                        hasFailure = true;
                        continue;
                    }

                    IStorage? storage = StorageFactory.Create(feedback, Connection);

                    if (storage is null)
                    {
                        Log.Error("Speicher für das Feedback des DMARC-Reports konnte nicht ermittelt werden.");
                        hasFailure = true;
                        continue;
                    }

                    bool isSaved = storage.Save(new Report
                    {
                        Document = documentXml,
                        Message = message,
                        Feedback = feedback
                    });

                    if (isSaved)
                    {
                        Log.Information("DMARC-Report wurde in der Datenbank gespeichert.");
                    }
                    else
                    {
                        Log.Error("DMARC-Report konnte nicht in der Datenbank gespeichert werden.");
                        hasFailure = true;
                    }
                }

                Log.Information("Lösche temporäre Dateien.");
                File.Delete(filePathXml);
                File.Delete(reportPath);
            }

            if (archiveFolder is not null && !hasFailure)
            {
                sourceFolder.MoveTo(inboxUId, archiveFolder);
            }
        }

        Log.Information("Import Ende: {date}", DateTime.Now);
        MessageBox.Show(this, "Import abgeschlossen.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            } else if (attachment.ContentType.IsMimeType("application", "zip"))
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
