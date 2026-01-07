using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// The DMARC policy that applied to the messages in this report.
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
        /// The policy to apply to messages from the domain.
        /// </summary>
        [XmlElement("p")]
        public DispositionType Policy { get; set; }

        /// <summary>
        /// The policy to apply to messages from subdomains.
        /// </summary>
        [XmlElement("sp")]
        public DispositionType SubPolicy { get; set; }

        /// <summary>
        /// The percent of messages to which policy applies.
        /// </summary>
        [XmlElement("pct")]
        public int? Percentage { get; set; }

        /// <summary>
        /// Failure reporting options in effect.
        /// </summary>
        [XmlElement("fo")]
        public string? FailureOptions { get; set; }
    }
}
