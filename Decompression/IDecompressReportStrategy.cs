namespace DMARCReportAnalyzer.Decompression;

/// <summary>
/// The strategy interface for decompressing a DMARC report archive.
/// </summary>
public interface IDecompressReportStrategy
{
    /// <summary>
    /// Decompressing the DMARC report archive.
    /// </summary>
    /// <param name="path">The path to the DMARC report archive.</param>
    /// <returns>The path to the decompressed DMARC report file.</returns>
    public string Decompress(string path);
}
