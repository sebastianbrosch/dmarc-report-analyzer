using System.IO.Compression;

namespace DMARCReportAnalyzer.Decompression;

/// <summary>
/// Strategy for decompressing a DMARC report archive using ZIP.
/// </summary>
public class DecompressReportZIP : IDecompressReportStrategy
{
    /// <summary>
    /// Decompress a DMARC report archive using ZIP.
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

        if (!fileInfo.Extension.Equals(".zip", StringComparison.CurrentCultureIgnoreCase))
        {
            return string.Empty;
        }

        using ZipArchive zipArchive = ZipFile.OpenRead(fileInfo.FullName);

        if (zipArchive.Entries.Count != 1)
        {
            return string.Empty;
        }

        string uncompressedPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".xml");
        zipArchive.Entries[0].ExtractToFile(uncompressedPath);
        return uncompressedPath;
    }
}
