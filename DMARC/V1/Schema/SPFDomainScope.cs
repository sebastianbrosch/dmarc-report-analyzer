using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// SPF domain scope.
    /// </summary>
    [XmlType("SPFDomainScope")]
    public enum SPFDomainScope
    {
        [XmlEnum("helo")]
        HELO,
        [XmlEnum("mfrom")]
        MailFrom
    }
}
