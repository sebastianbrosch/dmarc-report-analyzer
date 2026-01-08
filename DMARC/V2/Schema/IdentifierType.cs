using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema;

[XmlType("IdentifierType")]
public class IdentifierType
{
    /// <summary>
    /// The RFC5322.From domain.
    /// </summary>
    [XmlElement("header_from")]
    public string? HeaderFrom { get; set; }

    /// <summary>
    /// The RFC5321.MailFrom domain.
    /// </summary>
    [XmlElement("envelope_from")]
    public string? EnvelopeFrom { get; set; }

    /// <summary>
    /// The envelope recipient domain.
    /// </summary>
    [XmlElement("envelope_to")]
    public string? EnvelopeTo { get; set; }
}
