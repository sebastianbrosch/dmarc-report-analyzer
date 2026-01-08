namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the published policy from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record PolicyPublished
{
    /// <summary>
    /// Gets the unique identifier of the feedback this policy is associated with.
    /// </summary>
    public string FeedbackId { get; init; }

    /// <summary>
    /// Gets the domain at which the DMARC record was found.
    /// </summary>
    public string Domain { get; init; }

    /// <summary>
    /// Gets the DKIM alignment mode.
    /// </summary>
    public string? AlignmentDKIM { get; init; }

    /// <summary>
    /// Gets the SPF alignment mode.
    /// </summary>
    public string? AlignmentSPF { get; init; }

    /// <summary>
    /// Gets the policy published for messages from the domain.
    /// </summary>
    public string Policy { get; init; }

    /// <summary>
    /// Gets the policy published for messages from subdomains.
    /// </summary>
    public string? SubPolicy { get; init; }

    /// <summary>
    /// Gets the policy published for messages from non-existent subdomains.
    /// </summary>
    public string? NonExistentPolicy { get; init; }

    /// <summary>
    /// Gets the percent of messages to which policy applies.
    /// </summary>
    public int? Percentage { get; init; }

    /// <summary>
    /// Gets the method used to find / obtain the DMARC policy.
    /// </summary>
    public string? DiscoveryMethod { get; init; }

    /// <summary>
    /// Gets the failure reporting options in effect.
    /// </summary>
    public string? FailureOptions { get; init; }

    /// <summary>
    /// Gets the status whether testing mode was declared in the DMARC record.
    /// </summary>
    public string? Testing { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the feedback this policy is associated with.</param>
    /// <param name="domain">The domain at which the DMARC record was found.</param>
    /// <param name="alignmentDKIM">The DKIM alignment mode.</param>
    /// <param name="alignmentSPF">The SPF alignment mode.</param>
    /// <param name="policy">The policy published for messages from the domain.</param>
    /// <param name="subPolicy">The policy published for messages from subdomains.</param>
    /// <param name="nonExistentPolicy">The policy published for messages from non-existent subdomains.</param>
    /// <param name="percentage">The percent of messages to which policy applies.</param>
    /// <param name="dicoveryMethod">The method used to find / obtain the DMARC policy.</param>
    /// <param name="failureOptions">The failure reporting options in effect.</param>
    /// <param name="testing">The status whether testing mode was declared in the DMARC record.</param>
    public PolicyPublished(string feedbackId, string domain, string? alignmentDKIM, string? alignmentSPF, string policy, string? subPolicy, string? nonExistentPolicy, int? percentage, string? dicoveryMethod, string? failureOptions, string? testing)
    {
        this.FeedbackId = feedbackId;
        this.Domain = domain;
        this.AlignmentDKIM = alignmentDKIM;
        this.AlignmentSPF = alignmentSPF;
        this.Policy = policy;
        this.SubPolicy = subPolicy;
        this.NonExistentPolicy = nonExistentPolicy;
        this.Percentage = percentage;
        this.DiscoveryMethod = dicoveryMethod;
        this.FailureOptions = failureOptions;
        this.Testing = testing;
    }
}
