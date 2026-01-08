using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

/// <summary>
/// Report generator metadata.
/// </summary>
[XmlType("ReportMetadataType")]
public class ReportMetadataType
{
    [XmlElement("org_name")]
    public string? Organization { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("extra_contact_info")]
    public string? ExtraContactInfo { get; set; }

    [XmlElement("report_id")]
    public string? ReportId { get; set; }

    [XmlElement("date_range")]
    public DateRangeType? DateRange { get; set; }

    [XmlElement("error")]
    public List<string> Errors { get; set; } = [];
}
