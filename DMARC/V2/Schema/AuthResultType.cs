using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

/// <summary>
/// This element contains DKIM and SPF results, uninterpreted with respect to DMARC.
/// </summary>
[XmlType("AuthResultType")]
public class AuthResultType
{
    /// <summary>
    /// There may be zero or more DKIM signatures.
    /// </summary>
    [XmlElement("dkim")]
    public List<DKIMAuthResultType> DKIM { get; set; } = [];

    /// <summary>
    /// There may be zero or one SPF result.
    /// </summary>
    [XmlElement("spf")]
    public List<SPFAuthResultType> SPF { get; set; } = [];
}
