using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// The method used to discover the DMARC Policy Record used during evaluation.
/// </summary>
[XmlType("DiscoveryType")]
public enum DiscoveryType
{
    [XmlEnum("psl")]
    PSL,
    [XmlEnum("treewalk")]
    Treewalk
}
