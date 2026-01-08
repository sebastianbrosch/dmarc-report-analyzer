using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

/// <summary>
/// This element contains DKIM and SPF results, uninterpreted with respect to DMARC.
/// </summary>
[XmlType("AuthResultType")]
public class AuthResultType
{
    /// <summary>
    /// There may be no DKIM signatures, or multiple DKIM signatures.
    /// </summary>
    [XmlElement("dkim")]
    public List<DKIMAuthResultType> DKIM { get; set; } = [];

    /// <summary>
    /// There will always be at least one SPF result.
    /// </summary>
    [XmlElement("spf")]
    public List<SPFAuthResultType> SPF { get; set; } = [];
}
