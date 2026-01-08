using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// Values for Testing mode attached to policy.
/// </summary>
[XmlType("TestingType")]
public enum TestingType
{
    [XmlEnum("n")]
    No,
    [XmlEnum("y")]
    Yes
}
