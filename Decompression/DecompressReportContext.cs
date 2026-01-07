namespace DMARCReportAnalyzer.Decompression
{
    /// <summary>
    /// The context to decompress a report file.
    /// </summary>
    public class DecompressReportContext
    {
        /// <summary>
        /// The strategy to decompress a report file.
        /// </summary>
        private IDecompressReportStrategy? Strategy;

        /// <summary>
        /// Setter for setting the strategy of the context.
        /// </summary>
        /// <param name="strategy">The strategy to be applied in the context.</param>
        public void SetStrategy(IDecompressReportStrategy strategy)
        {
            this.Strategy = strategy;
        }

        /// <summary>
        /// Decompress a report file using the specified strategy.
        /// </summary>
        /// <param name="path">The path to the archive.</param>
        /// <returns>The path to the decompressed report file.</returns>
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
}
