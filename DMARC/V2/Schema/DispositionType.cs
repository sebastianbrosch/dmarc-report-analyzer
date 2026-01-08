using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// The policy actions specified by p, sp and np in the DMARC Policy Record.
/// </summary>
[XmlType("DispositionType")]
public enum DispositionType
{
    [XmlEnum("none")]
    None,
    [XmlEnum("quarantine")]
    Quarantine,
    [XmlEnum("reject")]
    Reject
}
