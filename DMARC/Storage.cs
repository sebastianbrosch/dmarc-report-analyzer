using System.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace DMARCReportAnalyzer.DMARC;

/// <summary>
/// Class to store a DMARC report in database.
/// </summary>
/// <param name="connection">A database connection that is to be used as the storage destination.</param>
public abstract class Storage : IStorage
{
    /// <summary>
    /// A database connection as the storage destination.
    /// </summary>
    protected IDbConnection Connection;

    /// <summary>
    /// A database connection to store the DMARC report.
    /// </summary>
    protected Database.Database Database;

    /// <summary>
    /// Constructor to initialize the storage.
    /// </summary>
    /// <param name="connection">A database connection to store the DMARC report.</param>
    public Storage(IDbConnection connection)
    {
        this.Connection = connection;
        this.Database = new Database.Database(connection);
    }

    /// <summary>
    /// Checks whether a DMARC report exists in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <param name="detailed">Status whether a detailed check should be performed.</param>
    /// <returns>Status whether the DMARC report already exists in the database.</returns>
    public abstract bool Exists(Report report, bool detailed = false);

    /// <summary>
    /// Saves a DMARC report in the database.
    /// </summary>
    /// <param name="report">All information from the DMARC report.</param>
    /// <returns>Status indicating whether the DMARC report has been saved.</returns>
    public abstract bool Save(Report report);

    /// <summary>
    /// Gets a GUID that can be used as an ID.
    /// </summary>
    /// <returns>A GUID that can be used as an ID.</returns>
    protected string GetGUID()
    {
        return Guid.NewGuid().ToString("N").ToUpper();
    }

    /// <summary>
    /// Gets the XML string value of an enumeration.
    /// </summary>
    /// <param name="value">The enumeration value to get the XML string value from.</param>
    /// <returns>The XML string value of the enumeration value.</returns>
    protected string GetEnumStringValue(Enum value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo is null)
        {
            return value.ToString();
        }

        List<XmlEnumAttribute> enumAttributes = [.. fieldInfo.GetCustomAttributes<XmlEnumAttribute>(true)];

        if (enumAttributes.Count < 1)
        {
            return value.ToString();
        } else
        {
            return enumAttributes.First<XmlEnumAttribute>().Name ?? string.Empty;
        }
    }
}
