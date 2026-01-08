using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

/// <summary>
/// DKIM verification result, according to RFC 7001 Section 2.6.1.
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
