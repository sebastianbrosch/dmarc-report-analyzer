using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

/// <summary>
/// Alignment mode for DKIM and SPF.
/// </summary>
[XmlType("AlignmentType")]
public enum AlignmentType
{
    [XmlEnum("r")]
    Relaxed,
    [XmlEnum("s")]
    Strict
}
