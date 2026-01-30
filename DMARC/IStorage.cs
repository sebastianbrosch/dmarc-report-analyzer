namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// A storage for saving a DMARC report in the database.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Checks whether a DMARC report exists in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <param name="detailed">Status whether a detailed check should be performed.</param>
    /// <returns>Status whether the DMARC report already exists in the database.</returns>
    public bool Exists(Report report, bool detailed = false);

    /// <summary>
    /// Saves a DMARC report in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <returns>Status indicating whether the DMARC report has been saved.</returns>
    public bool Save(Report report);
}
