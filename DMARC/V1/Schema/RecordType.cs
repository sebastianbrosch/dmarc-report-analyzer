using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema
{
    /// <summary>
    /// This element contains all the authentication results that were evaluated by the receiving system for the given set of messages.
    /// </summary>
    [XmlType("RecordType")]
    public class RecordType
    {
        [XmlElement("row")]
        public RowType? Row { get; set; }

        [XmlElement("identifiers")]
        public IdentifierType? Identifiers { get; set; }

        [XmlElement("auth_results")]
        public AuthResultType? AuthResults { get; set; }
    }
}
