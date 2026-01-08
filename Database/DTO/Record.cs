namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the record of a feedback in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record Record
{
    /// <summary>
    /// Gets the unique identifier of the record.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the unique identifier of the feedback this record is associated with.
    /// </summary>
    public string FeedbackId { get; init; }

    /// <summary>
    /// Gets the the connecting IP.
    /// </summary>
    public string SourceIp { get; init; }

    /// <summary>
    /// Gets the number of messages for which the evaluated policy was applied.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Gets the envelope recipient domain.
    /// </summary>
    public string? EnvelopeTo { get; init; }

    /// <summary>
    /// Gets the mail from domain (RFC5321).
    /// </summary>
    public string? EnvelopeFrom { get; init; }

    /// <summary>
    /// Gets the mail from domain (RFC5322).
    /// </summary>
    public string HeaderFrom { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="id">The unique identifier of the record.</param>
    /// <param name="feedbackId">The unique identifier of the feedback this record is associated with.</param>
    /// <param name="sourceIp">The the connecting IP.</param>
    /// <param name="count">The number of messages for which the evaluated policy was applied.</param>
    /// <param name="envelopeTo">The envelope recipient domain.</param>
    /// <param name="envelopeFrom">The mail from domain (RFC5321).</param>
    /// <param name="headerFrom">The mail from domain (RFC5322).</param>
    public Record(string id, string feedbackId, string sourceIp, int count, string? envelopeTo, string? envelopeFrom, string headerFrom)
    {
        this.Id = id;
        this.FeedbackId = feedbackId;
        this.SourceIp = sourceIp;
        this.Count = count;
        this.EnvelopeTo = envelopeTo;
        this.EnvelopeFrom = envelopeFrom;
        this.HeaderFrom = headerFrom;
    }
}
