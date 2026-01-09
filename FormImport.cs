using DMARCReportAnalyzer.Decompression;
using DMARCReportAnalyzer.DMARC;
using MailKit.Net.Imap;
using MimeKit;
using Serilog;
using System.Data;
using System.Xml;

namespace DMARCReportAnalyzer
{
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
            Log.Information("Import in Datenbank '{database}' wurde gestartet.", Connection.ConnectionString);
            
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
                client.Inbox.Open(MailKit.FolderAccess.ReadOnly);
                Log.Information("Anmeldung am E-Mail-Postfach erfolgreich.");
            } catch (Exception e)
            {
                Log.Error("Anmeldung am E-Mail-Postfach fehlgeschlagen: {error}", e.Message);
            }

            // get all the mails.
            IList<MailKit.UniqueId> inboxUIds = client.Inbox.Search(MailKit.Search.SearchQuery.All);
            Log.Information("Es wurden {count} Nachrichten im E-Mail-Postfach gefunden.", inboxUIds.Count);

            // process every mail of the inbox.
            foreach (MailKit.UniqueId inboxUId in inboxUIds)
            {
                MimeMessage message = client.Inbox.GetMessage(inboxUId);
                List<string> reportPaths = GetReportsFromMessage(message);
                Log.Information("Nachricht mit Betreff '{subject}' vom {datetime} wird verarbeitet.", message.Subject, message.Date);
                
                if (reportPaths.Count == 0)
                {
                    Log.Warning("Keine Archivdateien für DMARC-Reports gefunden.");
                    continue;
                } else
                {
                    Log.Information("Es wurden {count} Archivdateien für DMARC-Reports gefunden.", reportPaths.Count);
                }

                foreach (string reportPath in reportPaths)
                {
                    if (!File.Exists(reportPath))
                    {
                        Log.Error("Archivdatei konnte nicht gefunden werden: {filepath}", reportPath);
                        continue;
                    } else
                    {
                        Log.Information("Verarbeite Archivdatei: {filepath}", reportPath);
                    }

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
                    Log.Information("DMARC-Report: {filepath}", filePathXml);

                    using (FileStream fileStreamXml = new FileStream(filePathXml, FileMode.Open))
                    {
                        XmlDocument documentXml = new XmlDocument();
                        documentXml.Load(fileStreamXml);

                        IFeedback? feedback = FeedbackFactory.Create(documentXml);

                        if (feedback is null)
                        {
                            Log.Error("Feedback für DMARC-Report konnte nicht ermittelt werden.");
                            continue;
                        }

                        IStorage? storage = StorageFactory.Create(feedback, Connection);

                        if (storage is null)
                        {
                            Log.Error("Speicher für DMARC-Report konnte nicht ermittelt werden.");
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
                        }
                    }

                    File.Delete(filePathXml);
                    File.Delete(reportPath);
                }
            }
        }

        private List<string> GetReportsFromMessage(MimeMessage message)
        {
            List<string> reports = new List<string>();
            
            if (!(message.TextBody.Contains("Submitter") && message.TextBody.Contains("Report-ID")))
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
}
