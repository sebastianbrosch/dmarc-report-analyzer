using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    [XmlType("SPFAuthResultType")]
    public class SPFAuthResultType
    {
        /// <summary>
        /// The checked domain.
        /// </summary>
        [XmlElement("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// The scope of the checked domain.
        /// </summary>
        [XmlElement("scope")]
        public SPFDomainScope Scope { get; set; }

        /// <summary>
        /// The SPF verification result.
        /// </summary>
        [XmlElement("result")]
        public SPFResultType Result { get; set; }
    }
}
