using System.Text.RegularExpressions;

namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// Class for managing the file name of a DMARC report.
/// </summary>
/// <remarks>
/// See RFC 7489 (V1) Chapter 7.2.1.1
/// See DRAFT-IETF-DMARC-AGGREGATE-REPORTING-32 (V2) Chapter 3.5.2
/// </remarks>
public class FileName
{
    /// <summary>
    /// Domain of the receiving provider.
    /// </summary>
    public readonly string Receiver;

    /// <summary>
    /// Domain of the policy/sender.
    /// </summary>
    public readonly string Domain;

    /// <summary>
    /// Begin of the DMARC report.
    /// </summary>
    public readonly DateTime Begin;

    /// <summary>
    /// End of the DMARC report.
    /// </summary>
    public readonly DateTime End;

    /// <summary>
    /// Unique ID of the DMARC report.
    /// </summary>
    public readonly string? UniqueId;

    /// <summary>
    /// Creates a new instance of this class.
    /// </summary>
    /// <param name="receiver">Domain of the receiving provider.</param>
    /// <param name="domain">Domain of the policy/sender.</param>
    /// <param name="begin">Begin of the DMARC report.</param>
    /// <param name="end">End of the DMARC report.</param>
    /// <param name="uniqueId">Unique ID of the DMARC report.</param>
    private FileName(string receiver, string domain, DateTime begin, DateTime end, string? uniqueId)
    {
        this.Receiver = receiver;
        this.Domain = domain;
        this.Begin = begin;
        this.End = end;
        this.UniqueId = uniqueId;
    }

    /// <summary>
    /// Creates a new instance of this class using a file name or file path.
    /// </summary>
    /// <param name="path">A file name or file path.</param>
    /// <returns>A new instance of this class or null if the instance could not be created.</returns>
    public static FileName? Create(string path)
    {
        Regex rgxFileName = new(@"(?<provider>[^!]+)!(?<domain>[^!]+)!(?<begin>\d+)!(?<end>\d+)(?:!(?<uniqueId>[^!\.]+))?", RegexOptions.CultureInvariant);
        Match matchFileName = rgxFileName.Match(Path.GetFileName(path));

        if (!matchFileName.Success)
        {
            return null;
        }

        return Create(
            matchFileName.Groups["provider"].Value,
            matchFileName.Groups["domain"].Value,
            DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(matchFileName.Groups["begin"].Value)).UtcDateTime,
            DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(matchFileName.Groups["end"].Value)).UtcDateTime,
            matchFileName.Groups["uniqueId"].Success ? matchFileName.Groups["uniqueId"].Value : null
        );
    }

    /// <summary>
    /// Creates a new instance of this class using the needed information.
    /// </summary>
    /// <param name="provider">Domain of the receiving provider./param>
    /// <param name="domain">Domain of the policy/sender.</param>
    /// <param name="begin">Begin of the DMARC report.</param>
    /// <param name="end">End of the DMARC report.</param>
    /// <param name="uniqueId">Unique ID of the DMARC report.</param>
    /// <returns>A new instance of this class or null if the instance could not be created.</returns>
    public static FileName? Create(string provider, string domain, DateTime begin, DateTime end, string? uniqueId)
    {
        if (string.IsNullOrWhiteSpace(provider) || Uri.CheckHostName(provider) != UriHostNameType.Dns)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(domain) || Uri.CheckHostName(domain) != UriHostNameType.Dns)
        {
            return null;
        }

        if (end < begin)
        {
            return null;
        }

        return new FileName(
            provider.Trim(),
            domain.Trim(),
            begin,
            end,
            string.IsNullOrWhiteSpace(uniqueId) ? null : uniqueId.Trim()
        );
    }

    /// <summary>
    /// Gets the complete file name from the existing properties.
    /// </summary>
    /// <returns>The complete file name from the existing properties.</returns>
    public override string ToString()
    {
        string[] information = [
            this.Receiver,
            this.Domain,
            (new DateTimeOffset(this.Begin)).ToUnixTimeSeconds().ToString(),
            (new DateTimeOffset(this.End)).ToUnixTimeSeconds().ToString(),
            string.IsNullOrWhiteSpace(this.UniqueId) ? string.Empty : this.UniqueId
        ];

        return string.Join("!", information.Where<string>(s => !string.IsNullOrWhiteSpace(s)));
    }
}
