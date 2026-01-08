namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the policy override reason of evaluated policy in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record Reason
{
    /// <summary>
    /// Gets the unique identifier of the record this reason is associated with.
    /// </summary>
    public string RecordId { get; init; }

    /// <summary>
    /// Gets the type of the policy override reason.
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Gets the comment of the policy override reason.
    /// </summary>
    public string? Comment { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="recordId">The unique identifier of the record this reason is associated with.</param>
    /// <param name="type">The type of the policy override reason.</param>
    /// <param name="comment">The comment of the policy override reason.</param>
    public Reason(string recordId, string type, string? comment)
    {
        this.RecordId = recordId;
        this.Type = type;
        this.Comment = comment;
    }
}
