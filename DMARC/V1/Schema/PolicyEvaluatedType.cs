using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// Taking into account everything else in the record, the results of applying DMARC.
    /// </summary>
    [XmlType("PolicyEvaluatedType")]
    public class PolicyEvaluatedType
    {
        [XmlElement("disposition")]
        public DispositionType Disposition { get; set; }

        [XmlElement("dkim")]
        public DMARCResultType DKIM { get; set; }

        [XmlElement("spf")]
        public DMARCResultType SPF { get; set; }

        [XmlElement("reason")]
        public List<PolicyOverrideReason> Reasons { get; set; } = [];
    }
}
