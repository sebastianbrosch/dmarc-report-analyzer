using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// Report generator metadata.
/// </summary>
[XmlType("ReportMetadataType")]
public class ReportMetadataType
{
    /// <summary>
    /// Reporting Organization
    /// </summary>
    [XmlElement("org_name")]
    public string? Organization { get; set; }

    /// <summary>
    /// Contact to use when contacting the Reporting Organization.
    /// </summary>
    [XmlElement("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Additional contact details
    /// </summary>
    [XmlElement("extra_contact_info")]
    public string? ExtraContactInfo { get; set; }

    /// <summary>
    /// Unique Report-ID
    /// </summary>
    [XmlElement("report_id")]
    public string? ReportId { get; set; }

    /// <summary>
    /// Timestamps used when forming report data.
    /// </summary>
    [XmlElement("date_range")]
    public DateRangeType? DateRange { get; set; }

    /// <summary>
    /// Optional error messages when processing DMARC policy.
    /// </summary>
    [XmlElement("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Optional information about the generating software.
    /// </summary>
    [XmlElement("generator")]
    public string? Generator { get; set; }
}
