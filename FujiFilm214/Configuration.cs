using System;
using Microsoft.Extensions.Configuration;

namespace FujiFilm214
{
    /// <summary>
    ///     This class sets up use of configuration settings.
    /// </summary>
    public class Configuration
    {
        public static IConfigurationRoot Root = Initialize();
        
        public static readonly string Environment = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        // Database.
        public static readonly string ProgramStatusDbConnection = Root["ConnectionStrings:SqlLiteDb"];
        public static readonly string ChemStarDbConnection = Root["ConnectionStrings:ChemStarDb"];
        // Logging.
        public static readonly string LogsFilePath = Root["Configuration:LogsFilePath"];
        public static readonly string DebugLogsFilePath = Root["Configuration:DebugLogsFilePath"];
        // Messages.
        public static readonly string FailMessage = Root["Application:FailMessage"];
        public static readonly string SuccessMessage = Root["Application:SuccessMessage"];
        public static readonly string NoChanges = Root["Application:NoChanges"];
        public static readonly string LoggerFailure = Root["Application:LoggerInitFailed"];

        private static IConfigurationRoot Initialize()
        {
            if (Root != null) return Root;

            var devEnvironment = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            Root = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{devEnvironment}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        
            return Root;
        }
    }
}