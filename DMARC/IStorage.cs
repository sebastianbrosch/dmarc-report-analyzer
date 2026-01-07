namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// A storage for saving a DMARC report in the database.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Saves a DMARC report in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <returns>Status indicating whether the DMARC report has been saved.</returns>
    public bool Save(Report report);
}
