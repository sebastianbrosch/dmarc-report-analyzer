using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    [XmlType("RowType")]
    public class RowType
    {
        /// <summary>
        /// The connecting IP.
        /// </summary>
        [XmlElement("source_ip")]
        public string? SourceIP { get; set; }

        /// <summary>
        /// The number of matching messages.
        /// </summary>
        [XmlElement("count")]
        public int? Count { get; set; }

        /// <summary>
        /// The DMARC disposition applying to matching messages.
        /// </summary>
        [XmlElement("policy_evaluated")]
        public PolicyEvaluatedType? PolicyEvaluated { get; set; }
    }
}
