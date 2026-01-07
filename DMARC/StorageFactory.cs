namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// A factory to create a storage object based on the DMARC version.
/// </summary>
public static class StorageFactory
{
    /// <summary>
    /// Creates a storage object based on the DMARC version.
    /// </summary>
    /// <param name="feedback">A feedback object for retrieving the DMARC version.</param>
    /// <param name="connection">A database connection to be used as the target in storage.</param>
    /// <returns>A storage for saving a feedback object in the database.</returns>
    public static IStorage? Create(IFeedback feedback, System.Data.IDbConnection connection)
    {
        if (feedback is DMARC.V1.Schema.Feedback)
            return new DMARC.V1.Storage(connection);

        if (feedback is DMARC.V2.Schema.Feedback)
            return new DMARC.V2.Storage(connection);

        return null;
    }
}
