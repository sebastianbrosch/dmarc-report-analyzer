using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema
{
    /// <summary>
    /// The policy actions utilized on messages for this record.
    /// </summary>
    [XmlType("ActionDispositionType")]
    public enum ActionDispositionType
    {
        /// <summary>
        /// No action taken.
        /// </summary>
        [XmlEnum("none")]
        None,

        /// <summary>
        /// No action, passing DMARC with enforcing policy.
        /// </summary>
        [XmlEnum("pass")]
        Pass,

        /// <summary>
        /// Failed DMARC, message marked for quarantine.
        /// </summary>
        [XmlEnum("quarantine")]
        Quarantine,

        /// <summary>
        /// Failed DMARC, marked as reject.
        /// </summary>
        [XmlEnum("reject")]
        Reject
    }
}
