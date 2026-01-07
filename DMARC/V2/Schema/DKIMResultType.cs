using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema
{
    /// <summary>
    /// DKIM verification result, see RFC 8601 Section 2.7.1.
    /// </summary>
    [XmlType("DKIMResultType")]
    public enum DKIMResultType
    {
        [XmlEnum("none")]
        None,
        [XmlEnum("pass")]
        Pass,
        [XmlEnum("fail")]
        Fail,
        [XmlEnum("policy")]
        Policy,
        [XmlEnum("neutral")]
        Neutral,
        [XmlEnum("temperror")]
        TemporaryError,
        [XmlEnum("permerror")]
        PermanentError
    }
}
