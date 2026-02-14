namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the filename of a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record FileName
{
    /// <summary>
    /// Gets the unique identifier of the feedback the filename is associated with.
    /// </summary>
    public string FeedbackId { get; init; }

    /// <summary>
    /// Gets the receiver of the mail messages.
    /// </summary>
    public string Receiver { get; init; }

    /// <summary>
    /// Gets the domain of the mail messages.
    /// </summary>
    public string Domain { get; init; }

    /// <summary>
    /// Gets the beginning of the DMARC report.
    /// </summary>
    public DateTime ReportBegin { get; init; }

    /// <summary>
    /// Gets the ending of the DMARC report.
    /// </summary>
    public DateTime ReportEnd { get; init; }

    /// <summary>
    /// Gets the unique identifier of the DMARC report.
    /// </summary>
    public string? UniqueId { get; init; }

    /// <summary>
    /// Gets the filename of the DMARC report.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the feedback the filename is associated with.</param>
    /// <param name="receiver">The receiver of the mail messages.</param>
    /// <param name="domain">The domain of the mail messages.</param>
    /// <param name="reportBegin">The beginning of the DMARC report.</param>
    /// <param name="reportEnd">The ending of the DMARC report.</param>
    /// <param name="uniqueId">The unique identifier of the DMARC report.</param>
    /// <param name="fileName">The filename of the DMARC report.</param>
    public FileName(string feedbackId, string receiver, string domain, DateTime reportBegin, DateTime reportEnd, string? uniqueId, string fileName)
    {
        this.FeedbackId = feedbackId;
        this.Receiver = receiver;
        this.Domain = domain;
        this.ReportBegin = reportBegin;
        this.ReportEnd = reportEnd;
        this.UniqueId = uniqueId;
        this.Name = fileName;
    }
}
