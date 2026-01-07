namespace DMARCReportAnalyzer.Decompression
{
    /// <summary>
    /// The strategy interface for decompressing a report file.
    /// </summary>
    public interface IDecompressReportStrategy
    {
        /// <summary>
        /// Decompressing the report file.
        /// </summary>
        /// <param name="path">The path to the compressed report file.</param>
        /// <returns>The path to the decompressed report file.</returns>
        public string Decompress(string path);
    }
}
