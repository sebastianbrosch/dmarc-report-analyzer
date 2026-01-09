using System.IO.Compression;

namespace DMARCReportAnalyzer.Decompression;

/// <summary>
/// Strategy for decompressing a DMARC report archive using GZIP.
/// </summary>
public class DecompressReportGZIP : IDecompressReportStrategy
{
    /// <summary>
    /// Decompress the DMARC report archive using GZIP.
    /// </summary>
    /// <param name="path">The path to the DMARC report archive.</param>
    /// <returns>The path to the decompressed DMARC report file.</returns>
    string IDecompressReportStrategy.Decompress(string path)
    {
        FileInfo fileInfo = new(path);

        if (!fileInfo.Exists)
        {
            return string.Empty;
        }

        if (!fileInfo.Extension.Equals(".gz", StringComparison.CurrentCultureIgnoreCase))
        {
            return string.Empty;
        }

        using FileStream fileStream = fileInfo.OpenRead();
        string uncompressedPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".xml");

        using FileStream uncompressedFileStream = File.Create(uncompressedPath);
        using (GZipStream gzipStream = new(fileStream, CompressionMode.Decompress))
        {
            gzipStream.CopyTo(uncompressedFileStream);
        }

        return uncompressedPath;
    }
}
