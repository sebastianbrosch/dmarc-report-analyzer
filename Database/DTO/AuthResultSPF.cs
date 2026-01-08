namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the SPF authentication result from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record AuthResultSPF
{
    /// <summary>
    /// Gets the unique identifier of the record this authentication result is associated with.
    /// </summary>
    public string RecordId { get; init; }

    /// <summary>
    /// Gets the domain of the SPF authentication result.
    /// </summary>
    public string Domain { get; init; }

    /// <summary>
    /// Gets the scope of the SPF authentication result.
    /// </summary>
    public string? Scope { get; init; }

    /// <summary>
    /// Gets the result of the SPF authentication result.
    /// </summary>
    public string Result { get; init; }

    /// <summary>
    /// Gets the human result of the SPF authentication result.
    /// </summary>
    public string? HumanResult { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="recordId">The unique identifier of the associated record in the DMARC report.</param>
    /// <param name="domain">The domain of the SPF authentication result.</param>
    /// <param name="scope">The scope of the SPF authentication result.</param>
    /// <param name="result">The result of the SPF authentication result.</param>
    /// <param name="humanResult">The human result of the SPF authentication result.</param>
    public AuthResultSPF(string recordId, string domain, string? scope, string result, string? humanResult)
    {
        this.RecordId = recordId;
        this.Domain = domain;
        this.Scope = scope;
        this.Result = result;
        this.HumanResult = humanResult;
    }
}
