using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// SPF verification result, see RFC 8601 Section 2.7.2.
/// </summary>
[XmlType("SPFResultType")]
public enum SPFResultType
{
    [XmlEnum("none")]
    None,
    [XmlEnum("pass")]
    Pass,
    [XmlEnum("fail")]
    Fail,
    [XmlEnum("softfail")]
    SoftFail,
    [XmlEnum("policy")]
    Policy,
    [XmlEnum("neutral")]
    Neutral,
    [XmlEnum("temperror")]
    TemporaryError,
    [XmlEnum("permerror")]
    PermanentError
}
