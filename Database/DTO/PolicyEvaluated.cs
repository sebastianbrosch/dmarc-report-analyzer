namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the evaluated policy from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record PolicyEvaluated
{
    /// <summary>
    /// Gets the unique identifier of the record this policy is associated with.
    /// </summary>
    public string RecordId { get; init; }

    /// <summary>
    /// Gets the disposition of the policy.
    /// </summary>
    public string Disposition { get; init; }

    /// <summary>
    /// Gets the DKIM result of the policy.
    /// </summary>
    public string DKIM { get; init; }

    /// <summary>
    /// Gets the SPF result of the policy.
    /// </summary>
    public string SPF { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="recordId">The unique identifier of the record this policy is associated with.</param>
    /// <param name="disposition">The disposition of the policy.</param>
    /// <param name="dkim">The DKIM result of the policy.</param>
    /// <param name="spf">The SPF result of the policy.</param>
    public PolicyEvaluated(string recordId, string disposition, string dkim, string spf)
    {
        this.RecordId = recordId;
        this.Disposition = disposition;
        this.DKIM = dkim;
        this.SPF = spf;
    }
}
