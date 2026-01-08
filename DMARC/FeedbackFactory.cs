namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// A factory to create a feedback object of a DMARC report.
/// </summary>
public static class FeedbackFactory
{
    /// <summary>
    /// Creates a feedback object of a DMARC report.
    /// </summary>
    /// <param name="document">The DMARC report as XML document.</param>
    /// <returns>A feedback object containing the information of the DMARC report.</returns>
    public static IFeedback? Create(System.Xml.XmlDocument document)
    {
        if (document.DocumentElement is null)
        {
            return null;
        }

        if (document.DocumentElement.NamespaceURI.Equals("urn:ietf:params:xml:ns:dmarc-2.0"))
        {
            System.Xml.Serialization.XmlSerializer serializerXml = new(typeof(DMARC.V2.Schema.Feedback));
            System.Xml.XmlNodeReader nodeStreamXml = new(document.DocumentElement);
            return (DMARC.V2.Schema.Feedback?)serializerXml.Deserialize(nodeStreamXml);
        } else
        {
            System.Xml.Serialization.XmlSerializer serializerXml = new(typeof(DMARC.V1.Schema.Feedback));
            System.Xml.XmlNodeReader nodeStreamXml = new(document.DocumentElement);
            return (DMARC.V1.Schema.Feedback?)serializerXml.Deserialize(nodeStreamXml);
        }
    }
}
