namespace DMARCReportAnalyzer.Database.DTO;

/// <summary>
/// Represents the metadata from a DMARC report in the database.
/// </summary>
/// <remarks>
/// This is a DTO to transfer the data of a DMARC schema to the database.
/// So this record is a exact representation of the table in database.
/// </remarks>
public record Metadata
{
    /// <summary>
    /// Gets the unique identifier of the feedback the metadata is associated with.
    /// </summary>
    public string FeedbackId { get; init; }

    /// <summary>
    /// Gets the unique identifier of the report.
    /// </summary>
    public string ReportId { get; init; }

    /// <summary>
    /// Gets the reporting organization.
    /// </summary>
    public string Organization { get; init; }

    /// <summary>
    /// Gets the contact to use when contacting the reporting organization.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Gets the additional contact details.
    /// </summary>
    public string? ExtraContactInfo { get; init; }

    /// <summary>
    /// Gets the beginning of the DMARC report.
    /// </summary>
    public DateTime ReportBegin { get; init; }

    /// <summary>
    /// Gets the ending of the DMARC report.
    /// </summary>
    public DateTime ReportEnd { get; init; }

    /// <summary>
    /// Gets the error messages when processing DMARC policy.
    /// </summary>
    public string? Errors { get; init; }

    /// <summary>
    /// Gets the information about the generating software.
    /// </summary>
    public string? Generator { get; init; }

    /// <summary>
    /// Initializes a new instance of this DTO.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the feedback the metadata is associated with.</param>
    /// <param name="reportId">The unique identifier of the report.</param>
    /// <param name="organization">The reporting organization.</param>
    /// <param name="email">The contact to use when contacting the reporting organization.</param>
    /// <param name="extraContactInfo">The additional contact details.</param>
    /// <param name="reportBegin">The beginning of the DMARC report.</param>
    /// <param name="reportEnd">The ending of the DMARC report.</param>
    /// <param name="errors">The error messages when processing DMARC policy.</param>
    /// <param name="generator">The information about the generating software.</param>
    public Metadata(string feedbackId, string reportId, string organization, string email, string? extraContactInfo, DateTime reportBegin, DateTime reportEnd, string? errors, string? generator)
    {
        this.FeedbackId = feedbackId;
        this.ReportId = reportId;
        this.Organization = organization;
        this.Email = email;
        this.ExtraContactInfo = extraContactInfo;
        this.ReportBegin = reportBegin;
        this.ReportEnd = reportEnd;
        this.Errors = errors;
        this.Generator = generator;
    }
}
