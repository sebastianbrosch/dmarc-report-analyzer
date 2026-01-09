using Serilog;

namespace DMARCReportAnalyzer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // A logger to store log information in files.
            string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DMARC Report Analyzer");
            string logFile = Path.Combine(appDataFolder, "log.txt");
            Log.Logger = new LoggerConfiguration().WriteTo.File(logFile, rollingInterval: RollingInterval.Day).CreateLogger();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}