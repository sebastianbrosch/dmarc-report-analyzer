using System.IO.Compression;

namespace DMARCReportAnalyzer.Decompression
{
    /// <summary>
    /// Strategy for decompressing the report file from a ZIP archive.
    /// </summary>
    public class DecompressReportZIP : IDecompressReportStrategy
    {
        /// <summary>
        /// Decompress the report file from a ZIP archive.
        /// </summary>
        /// <param name="path">The path to the compressed report file using ZIP.</param>
        /// <returns>The path to the decompressed report file.</returns>
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

            string uncompressedPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            zipArchive.Entries[0].ExtractToFile(uncompressedPath);
            return uncompressedPath;
        }
    }
}
