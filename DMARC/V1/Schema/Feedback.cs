using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    [XmlRoot("feedback")]
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
