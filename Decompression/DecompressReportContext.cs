namespace DMARCReportAnalyzer.Decompression;

/// <summary>
/// The context to decompress a DMARC report archive.
/// </summary>
public class DecompressReportContext
{
    /// <summary>
    /// The strategy to decompress a DMARC report archive.
    /// </summary>
    private IDecompressReportStrategy? Strategy;

    /// <summary>
    /// Sets the strategy of the context.
    /// </summary>
    /// <param name="strategy">The strategy to be applied in the context.</param>
    public void SetStrategy(IDecompressReportStrategy strategy)
    {
        this.Strategy = strategy;
    }

    /// <summary>
    /// Decompress a DMARC report archive using the specified strategy.
    /// </summary>
    /// <param name="path">The path to the DMARC report archive.</param>
    /// <returns>The path to the decompressed DMARC report file.</returns>
    public string Decompress(string path)
    {
        if (!File.Exists(path))
        {
            return string.Empty;
        }

        if (this.Strategy is null)
        {
            return string.Empty;
        }

        return this.Strategy.Decompress(path);
    }
}
