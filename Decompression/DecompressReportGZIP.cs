using System.IO.Compression;

namespace DMARCReportAnalyzer.Decompression
{
    /// <summary>
    /// Strategy for decompressing the report file from a GZIP archive.
    /// </summary>
    public class DecompressReportGZIP : IDecompressReportStrategy
    {
        /// <summary>
        /// Decompress the report file from a GZIP archive.
        /// </summary>
        /// <param name="path">The path to the compressed report file using GZIP.</param>
        /// <returns>The path to the decompressed report file.</returns>
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
            string uncompressedPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            using FileStream uncompressedFileStream = File.Create(uncompressedPath);
            using (GZipStream gzipStream = new(fileStream, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(uncompressedFileStream);
            }

            return uncompressedPath;
        }
    }
}
