using Dapper;
using System.Data;

namespace DMARCReportAnalyzer.Database;

/// <summary>
/// Provides methods for managing the database of the DMARC Report Analyzer.
/// </summary>
/// <remarks>
/// This class requires an existing database connection, which must be managed by the caller.
/// The class does not manage connection state or transactions; callers are responsible for 
/// ensuring the connection is open and for handling transaction boundaries if needed.
/// </remarks>
public class Database
{
    /// <summary>
    /// The database connection used for all SQL operations.
    /// </summary>
    private readonly IDbConnection DbConnection;

    /// <summary>
    /// Initializes a new instance of this class using the specified database connection.
    /// </summary>
    /// <param name="dbConnection">The database connection to be used by the new instance of this class.</param>
    public Database(IDbConnection dbConnection)
    {
        this.DbConnection = dbConnection;
    }

    /// <summary>
    /// Inserts a feedback record into the database.
    /// </summary>
    /// <param name="feedback">An object containing the feedback data to be inserted.</param>
    public void CreateFeedback(DTO.Feedback feedback)
    {
        this.DbConnection.Execute("INSERT INTO feedback (id, data, created, sender, received, message_id, version) VALUES (@Id, @Data, @Created, @Sender, @Received, @MessageId, @Version)", feedback);
    }

    /// <summary>
    /// Inserts a metadata record into the database.
    /// </summary>
    /// <param name="metadata">An object containing the metadata data to be inserted.</param>
    public void CreateMetadata(DTO.Metadata metadata)
    {
        this.DbConnection.Execute("INSERT INTO metadata (feedback_id, report_id, organization, email, extra_contact_info, report_begin, report_end, errors, generator) VALUES (@FeedbackId, @ReportId, @Organization, @Email, @ExtraContactInfo, @ReportBegin, @ReportEnd, @Errors, @Generator)", metadata);
    }

    /// <summary>
    /// Inserts a SPF authentication result record into the database.
    /// </summary>
    /// <param name="authResultSPF">An object containing the authentication result data to be inserted.</param>
    public void CreateAuthResultSPF(DTO.AuthResultSPF authResultSPF)
    {
        this.DbConnection.Execute("INSERT INTO auth_result_spf (record_id, domain, scope, result, human_result) VALUES (@RecordId, @Domain, @Scope, @Result, @HumanResult)", authResultSPF);
    }

    /// <summary>
    /// Inserts a DKIM authentication result record into the database.
    /// </summary>
    /// <param name="authResultDKIM">An object containing the authentication result data to be inserted.</param>
    public void CreateAuthResultDKIM(DTO.AuthResultDKIM authResultDKIM)
    {
        this.DbConnection.Execute("INSERT INTO auth_result_dkim (record_id, domain, selector, result, human_result) VALUES (@RecordId, @Domain, @Selector, @Result, @HumanResult)", authResultDKIM);
    }

    /// <summary>
    /// Inserts a policy record (evaluated policy) into the database.
    /// </summary>
    /// <param name="policy">An object containing the policy data to be inserted.</param>
    public void CreatePolicyEvaluated(DTO.PolicyEvaluated policy)
    {
        this.DbConnection.Execute("INSERT INTO policy_evaluated (record_id, disposition, dkim, spf) VALUES (@RecordId, @Disposition, @DKIM, @SPF)", policy);
    }

    /// <summary>
    /// Inserts a policy record (published policy) into the database.
    /// </summary>
    /// <param name="policy">An object containing the policy data to be inserted.</param>
    public void CreatePolicyPublished(DTO.PolicyPublished policy)
    {
        this.DbConnection.Execute("INSERT INTO policy_published (feedback_id, domain, adkim, aspf, p, sp, np, pct, discovery_method, fo, testing) VALUES (@FeedbackId, @Domain, @AlignmentDKIM, @AlignmentSPF, @Policy, @SubPolicy, @NonExistentPolicy, @Percentage, @DiscoveryMethod, @FailureOptions, @Testing)", policy);
    }

    /// <summary>
    /// Inserts a record (from feedback) into the database.
    /// </summary>
    /// <param name="record">An object containing the record data to be inserted.</param>
    public void CreateRecord(DTO.Record record)
    {
        this.DbConnection.Execute("INSERT INTO record (id, feedback_id, source_ip, count, envelope_to, envelope_from, header_from) VALUES (@Id, @FeedbackId, @SourceIp, @Count, @EnvelopeTo, @EnvelopeFrom, @HeaderFrom)", record);
    }

    /// <summary>
    /// Inserts a reason record into the database.
    /// </summary>
    /// <param name="reason">An object containing the reason data to be inserted.</param>
    public void CreateReason(DTO.Reason reason)
    {
        this.DbConnection.Execute("INSERT INTO reason (record_id, type, comment) VALUES (@RecordId, @Type, @Comment)", reason);
    }

    /// <summary>
    /// Initializes the database with all required tables.
    /// </summary>
    public void InitializeDatabase()
    {
        this.DbConnection.Execute(
          @"CREATE TABLE IF NOT EXISTS feedback (
              id VARCHAR(32) NOT NULL,
              data TEXT NOT NULL,
              created DATETIME NOT NULL,
              sender VARCHAR(255) NULL,
              received DATETIME NULL,
              message_id VARCHAR(255) NULL,
              version VARCHAR(10) NULL,
              PRIMARY KEY (id)
            );
            CREATE TABLE IF NOT EXISTS metadata (
              feedback_id VARCHAR(32) NOT NULL,
              report_id VARCHAR(255) NOT NULL,
              organization VARCHAR(255) NOT NULL,
              email VARCHAR(255) NOT NULL,
              extra_contact_info VARCHAR(255) NULL,
              report_begin DATETIME NOT NULL,
              report_end DATETIME NOT NULL,
              errors VARCHAR(255) NULL,
              generator VARCHAR(255) NULL,
              PRIMARY KEY (feedback_id),
              FOREIGN KEY (feedback_id) REFERENCES feedback(id)
            );
            CREATE TABLE IF NOT EXISTS policy_published (
              feedback_id VARCHAR(32) NOT NULL,
              domain VARCHAR(255) NOT NULL,
              adkim VARCHAR(1) NULL,
              aspf VARCHAR(1) NULL,
              p VARCHAR(10) NOT NULL,
              sp VARCHAR(10) NULL,
              np VARCHAR(10) NULL,
              pct INTEGER NULL,
              discovery_method VARCHAR(8) NULL,
              fo VARCHAR(255) NULL,
              testing VARCHAR(1) NULL,
              PRIMARY KEY (feedback_id),
              FOREIGN KEY (feedback_id) REFERENCES feedback(id)
            );
            CREATE TABLE IF NOT EXISTS record (
              id VARCHAR(32) NOT NULL,
              feedback_id VARCHAR(32) NOT NULL,
              source_ip VARCHAR(39) NOT NULL,
              count INTEGER NOT NULL,
              envelope_to VARCHAR(255) NULL,
              envelope_from VARCHAR(255) NULL,
              header_from VARCHAR(255) NOT NULL,
              PRIMARY KEY (id),
              FOREIGN KEY (feedback_id) REFERENCES feedback(id)
            );
            CREATE TABLE IF NOT EXISTS policy_evaluated (
              record_id VARCHAR(32) NOT NULL,
              disposition VARCHAR(10) NOT NULL,
              dkim VARCHAR(4) NOT NULL,
              spf VARCHAR(4) NOT NULL,
              FOREIGN KEY (record_id) REFERENCES record(id)
            );
            CREATE TABLE IF NOT EXISTS reason (
              record_id VARCHAR(32) NOT NULL,
              type VARCHAR(17) NOT NULL,
              comment VARCHAR(255) NULL,
              FOREIGN KEY (record_id) REFERENCES record(id)
            );
            CREATE TABLE IF NOT EXISTS auth_result_dkim (
              record_id VARCHAR(32) NOT NULL,
              domain VARCHAR(255) NOT NULL,
              selector VARCHAR(255) NULL,
              result VARCHAR(9) NOT NULL,
              human_result VARCHAR(255) NULL,
              FOREIGN KEY (record_id) REFERENCES record(id)
            );
            CREATE TABLE IF NOT EXISTS auth_result_spf (
              record_id VARCHAR(32) NOT NULL,
              domain VARCHAR(255) NOT NULL,
              scope VARCHAR(5) NULL,
              result VARCHAR(9) NOT NULL,
              human_result VARCHAR(255) NULL,
              FOREIGN KEY (record_id) REFERENCES record(id)
            );
            CREATE VIEW metadata_expansion
            AS
            SELECT feedback_id,
              CASE
                WHEN DATE(report_begin) = DATE(report_end) THEN
                  DATE(report_begin)
                WHEN CAST(strftime('%H', report_begin) AS INTEGER) = 0 AND CAST(strftime('%H', report_end) AS INTEGER) = 0 THEN
                  DATE(report_end)
                WHEN CAST(strftime('%H', report_begin) AS INTEGER) >= 19 THEN
                  DATE(report_end)
                WHEN CAST(strftime('%H', report_begin) AS INTEGER) < 19 THEN
                  DATE(report_begin)
                ELSE
                  NULL
              END report_date
            FROM metadata;"
        );
    }
}
