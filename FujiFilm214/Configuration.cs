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
        public static readonly string ChemStarDbConnection = Root["ConnectionStrings:ChemStarDb"];

        // Logging.
        public static readonly string LogsFilePath = Root["Configuration:LogsFilePath"];
        public static readonly string DebugLogsFilePath = Root["Configuration:DebugLogsFilePath"];

        // Messages.
        public static readonly string FailMessage = Root["Application:FailMessage"];
        public static readonly string SuccessMessage = Root["Application:SuccessMessage"];
        public static readonly string LoggerFailure = Root["Application:LoggerInitFailed"];

        // Ftp.
        public static readonly string Host = Root["Ftp:Host"];
        public static readonly string Port = Root["Ftp:Port"];
        public static readonly string Username = Root["Ftp:Username"];
        public static readonly string Password = Root["Ftp:Password"];
        public static readonly string Filename = Root["Ftp:FilenameWithExtension"];
        public static readonly string FtpDirectory = Root["Ftp:FtpDirectory"];
        public static readonly string AlternateFtpDirectory = Root["Ftp:AlternateFtpDirectory"];

        // Email.
        public static readonly string ToEmail = Root["Email:ToEmail"];
        public static readonly string EmailUsername = Root["Email:Username"];
        public static readonly string EmailPassword = Root["Email:Password"];
        public static readonly string FromEmail = Root["Email:FromEmail"];
        public static readonly string Subject = Root["Email:Subject"];
        public static readonly string Message = Root["Email:Message"];

        // Web Service.
        public static readonly string XmlToEdiServiceAddress = Root["WebService:XmlToEdiServiceAddress"];
        public static readonly string EdiToXmlServiceAddress = Root["WebService:EdiToXmlServiceAddress"];
        public static readonly string X12Document = Root["WebService:X12Document"];
        public static readonly string X12Version = Root["WebService:X12Version"];

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