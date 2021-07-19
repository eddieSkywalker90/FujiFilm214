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
        public static readonly string ProgramStatusDbConnection = Root["ConnectionStrings:SqlLiteDb"];
        public static readonly string ChemStarDbConnection = Root["ConnectionStrings:ChemStarDb"];

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