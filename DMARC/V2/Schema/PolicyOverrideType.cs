using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V2.Schema
{
    /// <summary>
    /// Reasons that may affect DMARC disposition or execution.
    /// </summary>
    [XmlType("PolicyOverrideType")]
    public enum PolicyOverrideType
    {
        [XmlEnum("local_policy")]
        LocalPolicy,
        [XmlEnum("mailing_list")]
        MailingList,
        [XmlEnum("other")]
        Other,
        [XmlEnum("policy_test_mode")]
        PolicyTestMode,
        [XmlEnum("trusted_forwarder")]
        TrustedForwarder
    }
}
