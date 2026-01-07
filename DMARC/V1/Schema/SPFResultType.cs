using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// SPF result.
    /// </summary>
    [XmlType("SPFResultType")]
    public enum SPFResultType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("neutral")]
        Neutral,
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail,
        [XmlEnum("softfail")]
        SoftFail,
        [XmlEnum("temperror")]
        TemporaryError,
        [XmlEnum("permerror")]
        PermanentError
    }
}
