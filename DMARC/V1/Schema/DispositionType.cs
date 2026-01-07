using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// The policy actions specified by policy and sub-policy in the DMARC record.
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
}
