using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

[XmlType("DKIMAuthResultType")]
public class DKIMAuthResultType
{
    /// <summary>
    /// The domain parameter in the signature.
    /// </summary>
    [XmlElement("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// The selector parameter in the signature.
    /// </summary>
    [XmlElement("selector")]
    public string? Selector { get; set; }

    /// <summary>
    /// The DKIM verification result.
    /// </summary>
    [XmlElement("result")]
    public DKIMResultType Result { get; set; }

    /// <summary>
    /// Any extra information (e.g. from Authentication-Results).
    /// </summary>
    [XmlElement("human_result")]
    public string? HumanResult { get; set; }
}
