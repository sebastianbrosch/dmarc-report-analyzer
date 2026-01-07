using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema
{
    /// <summary>
    /// The published DMARC policy.
    /// </summary>
    [XmlType("PolicyPublishedType")]
    public class PolicyPublishedType
    {
        /// <summary>
        /// The domain at which the DMARC record was found.
        /// </summary>
        [XmlElement("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// The policy published for messages from the domain.
        /// </summary>
        [XmlElement("p")]
        public DispositionType Policy { get; set; }

        /// <summary>
        /// The policy published for messages from subdomains.
        /// </summary>
        [XmlElement("sp")]
        public DispositionType SubPolicy { get; set; }

        /// <summary>
        /// The policy published for messages from non-existent subdomains.
        /// </summary>
        [XmlElement("np")]
        public DispositionType NonExistentSubPolicy { get; set; }

        /// <summary>
        /// The DKIM alignment mode.
        /// </summary>
        [XmlElement("adkim")]
        public AlignmentType AlignmentDKIM { get; set; }

        /// <summary>
        /// The SPF alignment mode.
        /// </summary>
        [XmlElement("aspf")]
        public AlignmentType AlignmentSPF { get; set; }

        /// <summary>
        /// Method used to find / obtain DMARC policy.
        /// </summary>
        [XmlElement("discovery_method")]
        public DiscoveryType DiscoveryMethod { get; set; }

        /// <summary>
        /// Failure reporting options in effect.
        /// </summary>
        [XmlElement("fo")]
        public string? FailureOptions { get; set; }

        /// <summary>
        /// Whether testing mode was declared in the DMARC record.
        /// </summary>
        [XmlElement("testing")]
        public TestingType Testing { get; set; }
    }
}
