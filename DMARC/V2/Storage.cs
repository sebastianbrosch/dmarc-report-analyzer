using Dapper;
using MimeKit;
using System.Data;
using System.Xml;

namespace DMARCReportAnalyzer.DMARC.V2;

/// <summary>
/// Class to store a DMARC report in database.
/// </summary>
/// <param name="connection">A database connection to store a DMARC report.</param>
public class Storage(IDbConnection connection) : DMARC.Storage(connection)
{
    /// <summary>
    /// Saves the Authentication-Results of the DMARC report.
    /// </summary>
    /// <param name="recordId">The ID of the record.</param>
    /// <param name="authResults">The Authentication-Results of the DMARC report.</param>
    /// <returns>Status whether the Authentication-Results were stored successfully to the database.</returns>
    private bool SaveAuthResults(string recordId, Schema.AuthResultType? authResults)
    {
        if (string.IsNullOrWhiteSpace(recordId) || authResults is null)
        {
            return false;
        }

        foreach (Schema.SPFAuthResultType spfAuthResult in authResults.SPF)
        {
            Database.CreateAuthResultSPF(new DMARCReportAnalyzer.Database.DTO.AuthResultSPF(
                recordId,
                spfAuthResult.Domain ?? string.Empty,
                GetEnumStringValue(spfAuthResult.Scope),
                GetEnumStringValue(spfAuthResult.Result),
                spfAuthResult.HumanResult
            ));
        }

        foreach (Schema.DKIMAuthResultType dkimAuthResult in authResults.DKIM)
        {
            Database.CreateAuthResultDKIM(new DMARCReportAnalyzer.Database.DTO.AuthResultDKIM(
                recordId,
                dkimAuthResult.Domain ?? string.Empty,
                dkimAuthResult.Selector,
                GetEnumStringValue(dkimAuthResult.Result),
                dkimAuthResult.HumanResult
            ));
        }

        return true;
    }

    /// <summary>
    /// Saves a DMARC feedback to the database.
    /// </summary>
    /// <param name="feedbackId">The ID of the feedback.</param>
    /// <param name="feedback">The DMARC feedback to be stored.</param>
    /// <param name="documentXml">The XML document of the DMARC report.</param>
    /// <param name="message">The message with which the DMARC report was sent.</param>
    /// <returns>Status whether the DMARC feedback object was successfully saved to the database.</returns>
    private bool SaveFeedback(string feedbackId, Schema.Feedback feedback, XmlDocument documentXml, MimeMessage? message)
    {
        if (string.IsNullOrWhiteSpace(feedbackId) || feedback is null || documentXml is null)
        {
            return false;
        }

        string? address = null;
        DateTime? received = null;
        string? messageId = null;

        if (message is not null)
        {
            address = ((MailboxAddress)message.From.First()).Address;
            received = message.Date.DateTime;
            messageId = message.MessageId;
        }

        string version = feedback.Version is null ? string.Empty : feedback.Version.ToString()!.Replace(",", ".");

        Database.CreateFeedback(new DMARCReportAnalyzer.Database.DTO.Feedback(
            feedbackId,
            documentXml.InnerXml,
            DateTime.Now,
            address,
            received,
            messageId,
            version
        ));

        return true;
    }

    /// <summary>
    /// Saves the metadata of the DMARC report to the database.
    /// </summary>
    /// <param name="feedbackId">The ID of the feedback.</param>
    /// <param name="metadata">The metadata of the DMARC report to be stored.</param>
    /// <returns>Status whether the metadata was successfully saved to the database.</returns>
    private bool SaveMetadata(string feedbackId, Schema.ReportMetadataType? metadata)
    {
        if (metadata is null)
        {
            return false;
        }

        Database.CreateMetadata(new DMARCReportAnalyzer.Database.DTO.Metadata(
            feedbackId,
            metadata.ReportId ?? string.Empty,
            metadata.Organization ?? string.Empty,
            metadata.Email ?? string.Empty,
            metadata.ExtraContactInfo,
            metadata.DateRange?.Begin ?? DateTime.MinValue,
            metadata.DateRange?.End ?? DateTime.MaxValue,
            metadata.Error,
            metadata.Generator
        ));

        return true;
    }

    /// <summary>
    /// Saves the evaluated policy of the DMARC report to the database.
    /// </summary>
    /// <param name="recordId">The ID of the record.</param>
    /// <param name="policyEvaluated">The evaluated policy of the DMARC report.</param>
    /// <returns>Status whether the evaluated policy was successfully saved to the database.</returns>
    private bool SavePolicyEvaluated(string recordId, Schema.PolicyEvaluatedType? policyEvaluated)
    {
        if (string.IsNullOrWhiteSpace(recordId) || policyEvaluated is null)
        {
            return false;
        }

        Database.CreatePolicyEvaluated(new DMARCReportAnalyzer.Database.DTO.PolicyEvaluated(
            recordId,
            GetEnumStringValue(policyEvaluated.Disposition),
            GetEnumStringValue(policyEvaluated.DKIM),
            GetEnumStringValue(policyEvaluated.SPF)
        ));

        foreach (Schema.PolicyOverrideReason reason in policyEvaluated.Reasons)
        {
            Database.CreateReason(new DMARCReportAnalyzer.Database.DTO.Reason(
                recordId,
                GetEnumStringValue(reason.Type),
                reason.Comment
            ));
        }

        return true;
    }

    /// <summary>
    /// Saves the published policy of the DMARC report to the database.
    /// </summary>
    /// <param name="feedbackId">The ID of the feedback.</param>
    /// <param name="policyPublished">The published policy of the DMARC report to be stored.</param>
    /// <returns>Status whether the published policy was successfully saved to the database.</returns>
    private bool SavePolicyPublished(string feedbackId, Schema.PolicyPublishedType? policyPublished)
    {
        if (policyPublished is null)
        {
            return false;
        }

        Database.CreatePolicyPublished(new DMARCReportAnalyzer.Database.DTO.PolicyPublished(
            feedbackId,
            policyPublished.Domain ?? string.Empty,
            GetEnumStringValue(policyPublished.AlignmentDKIM),
            GetEnumStringValue(policyPublished.AlignmentSPF),
            GetEnumStringValue(policyPublished.Policy),
            GetEnumStringValue(policyPublished.SubPolicy),
            GetEnumStringValue(policyPublished.NonExistentSubPolicy),
            null,
            GetEnumStringValue(policyPublished.DiscoveryMethod),
            policyPublished.FailureOptions,
            GetEnumStringValue(policyPublished.Testing)
        ));

        return true;
    }

    /// <summary>
    /// Saves a record of the DMARC report to the database.
    /// </summary>
    /// <param name="feedbackId">The ID of the feedback.</param>
    /// <param name="records">A list of all records of the DMARC report.</param>
    /// <returns>Status whether all records of the DMARC report were successfully saved to the database.</returns>
    private bool SaveRecords(string feedbackId, List<Schema.RecordType> records)
    {
        if (records.Count == 0 || string.IsNullOrWhiteSpace(feedbackId))
        {
            return false;
        }

        foreach (Schema.RecordType record in records)
        {
            if (record.Row is null || record.Identifiers is null)
            {
                return false;
            }

            string recordId = this.GetGUID();

            Database.CreateRecord(new DMARCReportAnalyzer.Database.DTO.Record(
                recordId,
                feedbackId,
                record.Row.SourceIP ?? string.Empty,
                record.Row.Count ?? 0,
                record.Identifiers.EnvelopeTo,
                record.Identifiers.EnvelopeFrom,
                record.Identifiers.HeaderFrom ?? string.Empty
            ));

            if (!SavePolicyEvaluated(recordId, record.Row.PolicyEvaluated))
            {
                return false;
            }

            if (!SaveAuthResults(recordId, record.AuthResults))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Exists(Report report, bool detailed = false)
    {
        Schema.Feedback feedback = (Schema.Feedback)report.Feedback;
        Schema.ReportMetadataType metadata = feedback.ReportMetadata!;

        if (detailed)
        {
            var listMetadata = Connection.Query("SELECT report_begin, report_end FROM metadata WHERE report_id = @ReportId", new { metadata.ReportId });
            return listMetadata.Any(item => item.report_begin == metadata.DateRange!.Begin && item.report_end == metadata.DateRange!.End);
        }
        else
        {
            int cntReports = Connection.ExecuteScalar<int>("SELECT COUNT(feedback_id) FROM metadata WHERE report_id = @ReportId", new { metadata.ReportId });
            return (cntReports > 0);
        }
    }

    /// <summary>
    /// Saves a DMARC report in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <returns>Status indicating whether the DMARC report has been saved.</returns>
    public override bool Save(Report report)
    {
        string feedbackId = this.GetGUID();
        Schema.Feedback feedback = (Schema.Feedback)report.Feedback;
        XmlDocument documentXml = report.Document;
        MimeKit.MimeMessage? message = report.Message;

        using IDbTransaction transaction = Connection.BeginTransaction();

        if (!SaveFeedback(feedbackId, feedback, documentXml, message))
        {
            transaction.Rollback();
            return false;
        }

        if (!SaveMetadata(feedbackId, feedback.ReportMetadata))
        {
            transaction.Rollback();
            return false;
        }

        if (!SavePolicyPublished(feedbackId, feedback.PolicyPublished))
        {
            transaction.Rollback();
            return false;
        }

        if (!SaveRecords(feedbackId, feedback.Records))
        {
            transaction.Rollback();
            return false;
        }

        transaction.Commit();
        return true;
    }
}
