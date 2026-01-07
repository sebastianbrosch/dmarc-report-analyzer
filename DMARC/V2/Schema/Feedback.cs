using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema
{
    [XmlRoot(ElementName = "feedback", Namespace = "urn:ietf:params:xml:ns:dmarc-2.0")]
    public class Feedback : IFeedback
    {
        [XmlElement("version")]
        public decimal? Version { get; set; }

        [XmlElement("report_metadata")]
        public ReportMetadataType? ReportMetadata { get; set; }

        [XmlElement("policy_published")]
        public PolicyPublishedType? PolicyPublished { get; set; }

        [XmlElement("record")]
        public List<RecordType> Records { get; set; } = [];
    }
}
