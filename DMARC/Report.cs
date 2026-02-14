namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// A structure to organize all information of a DMARC report.
/// </summary>
public struct Report
{
    /// <summary>
    /// The DMARC report as a XML document.
    /// </summary>
    public System.Xml.XmlDocument Document;

    /// <summary>
    /// The feedback object of the DMARC report.
    /// </summary>
    public IFeedback Feedback;

    /// <summary>
    /// The filename of the DMARC report.
    /// </summary>
    public FileName FileName;

    /// <summary>
    /// The email message with which the DMARC report was received.
    /// </summary>
    public MimeKit.MimeMessage? Message;
}
