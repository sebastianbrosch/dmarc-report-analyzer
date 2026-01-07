using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    [XmlType("PolicyOverrideReason")]
    public class PolicyOverrideReason
    {
        [XmlElement("type")]
        public PolicyOverrideType Type { get; set; }

        [XmlElement("comment")]
        public string? Comment { get; set; }
    }
}
