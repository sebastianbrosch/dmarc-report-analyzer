using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC.V1.Schema;

/// <summary>
/// The time range in UTC covered by messages in this report, specified in seconds since epoch.
/// </summary>
[XmlType("DateRangeType")]
public class DateRangeType
{
    /// <summary>
    /// The begin of the time range in UTC covered by messages in this report, specified in seconds since epoch.
    /// </summary>
    [XmlElement("begin")]
    public long BeginRaw { get; set; }

    /// <summary>
    /// The end of the time range in UTC covered by messages in this report, specified in seconds since epoch.
    /// </summary>
    [XmlElement("end")]
    public long EndRaw { get; set; }

    /// <summary>
    /// The begin of the time range in UTC covered by messages in this report.
    /// </summary>
    [XmlIgnore]
    public DateTime Begin
    {
        get => DateTimeOffset.FromUnixTimeSeconds(BeginRaw).DateTime;
        set => BeginRaw = ((DateTimeOffset) value).ToUnixTimeSeconds();
    }

    /// <summary>
    /// The end of the time range in UTC covered by messages in this report.
    /// </summary>
    [XmlIgnore]
    public DateTime End
    {
        get => DateTimeOffset.FromUnixTimeSeconds(EndRaw).DateTime;
        set => EndRaw = ((DateTimeOffset) value).ToUnixTimeSeconds();
    }

    /// <summary>
    /// The timespan covered by messages in this report.
    /// </summary>
    [XmlIgnore]
    public TimeSpan TimeSpan
    {
        get => End - Begin;
    }
}
