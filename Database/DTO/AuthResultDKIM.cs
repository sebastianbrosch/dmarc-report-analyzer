namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the DKIM authentication result from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record AuthResultDKIM
{
    /// <summary>
    /// Gets the unique identifier of the record this authentication result is associated with.
    /// </summary>
    public string RecordId { get; init; }

    /// <summary>
    /// Gets the domain of the DKIM authentication result.
    /// </summary>
    public string Domain { get; init; }

    /// <summary>
    /// Gets the selector of the DKIM authentication result.
    /// </summary>
    public string? Selector { get; init; }

    /// <summary>
    /// Gets the result of the DKIM authentication result.
    /// </summary>
    public string Result { get; init; }

    /// <summary>
    /// Gets the human result of the DKIM authentication result.
    /// </summary>
    public string? HumanResult { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="recordId">The unique identifier of the associated record in the DMARC report.</param>
    /// <param name="domain">The domain of the DKIM authentication result.</param>
    /// <param name="selector">The selector of the DKIM authentication result.</param>
    /// <param name="result">The result of the DKIM authentication result.</param>
    /// <param name="humanResult">The human result of the DKIM authentication result.</param>
    public AuthResultDKIM(string recordId, string domain, string? selector, string result, string? humanResult)
    {
        this.RecordId = recordId;
        this.Domain = domain;
        this.Selector = selector;
        this.Result = result;
        this.HumanResult = humanResult;
    }
}
