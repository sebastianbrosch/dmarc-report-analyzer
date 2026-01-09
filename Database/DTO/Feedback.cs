namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the feedback from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record Feedback
{
    /// <summary>
    /// Gets the unique identifier of the feedback.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the raw data (XML) of the DMARC report.
    /// </summary>
    public string Data { get; init; }

    /// <summary>
    /// Gets the date and time when the feedback was created in database.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Gets the sender of the email that sent the DMARC report.
    /// </summary>
    public string? Sender { get; init; }

    /// <summary>
    /// Gets the date and time at which the email was received.
    /// </summary>
    public DateTime? Received { get; init; }

    /// <summary>
    /// Gets the unique identifier of the message.
    /// </summary>
    public string? MessageId { get; init; }

    /// <summary>
    /// Gets the version of the feedback.
    /// </summary>
    public string? Version { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="id">The unique identifier of the feedback.</param>
    /// <param name="data">The raw data (XML) of the DMARC report.</param>
    /// <param name="created">The date and time when the feedback was created in database.</param>
    /// <param name="sender">The sender of the email that sent the DMARC report.</param>
    /// <param name="received">The date and time at which the email was received.</param>
    /// <param name="messageId">The unique identifier of the message.</param>
    /// <param name="version">The version of the feedback.</param>
    public Feedback(string id, string data, DateTime created, string? sender, DateTime? received, string? messageId, string? version)
    {
        this.Id = id;
        this.Data = data;
        this.Created = created;
        this.Sender = sender;
        this.Received = received;
        this.MessageId = messageId;
        this.Version = version;
    }
}
