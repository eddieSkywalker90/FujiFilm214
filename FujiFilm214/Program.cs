using System;
using Serilog;
using Serilog.Events;

namespace FujiFilm214
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(Configuration.DebugLogsFilePath, retainedFileCountLimit: 5,
                        rollingInterval: RollingInterval.Day)
                    .WriteTo.File(Configuration.LogsFilePath, LogEventLevel.Information, retainedFileCountLimit: 5,
                        rollingInterval: RollingInterval.Day)
                    .WriteTo.Console(LogEventLevel.Debug)
                    .CreateLogger();

                FujiFilm214IntegrationManager integrationManager = new();
                integrationManager.InitializeTriggers(Configuration.ProgramStatusDbConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
    }
}
