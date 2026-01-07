using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// The DMARC-aligned authentication result.
    /// </summary>
    [XmlType("DMARCResultType")]
    public enum DMARCResultType
    {
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail
    }
}
