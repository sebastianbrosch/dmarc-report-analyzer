using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// SPF domain scope.
/// </summary>
[XmlType("SPFDomainScope")]
public enum SPFDomainScope
{
    [XmlEnum("mfrom")]
    MailFrom
}
