using System;
using FujiFilm214.FujiFilm;
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
                    .WriteTo.File(Configuration.DebugLogsFilePath, retainedFileCountLimit: 5, rollingInterval: RollingInterval.Day)
                    .WriteTo.File(Configuration.LogsFilePath, LogEventLevel.Information, retainedFileCountLimit: 5, rollingInterval: RollingInterval.Day)
                    .WriteTo.Console(LogEventLevel.Debug)
                    .CreateLogger();

                try
                {
                    // Dev-only environment.
                    if (Configuration.Environment == "Development")
                    {
                        Console.WriteLine("***** DEVELOPMENT ENVIRONMENT DETECTED *****\n" +
                                          "Returned amount of changed statuses in IdentifyPotentiallyChangedRecordIds()\n" +
                                          "has been reduced to 1-3 in order to finish execution sooner for debug purposes.\n");

                        Log.Information("FujiFilm214 Service Starting..");

                        FujiFilmController fujiFilm214 = new(Configuration.Root);
                        fujiFilm214.Start();

                        Log.Information(Configuration.SuccessMessage);
                    }
                }
                catch (Exception)
                {
                    EmailHandler.EmailManager.SendEmail(EmailHandler.AlertMessage);
                    Log.Information(Configuration.FailMessage);
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Configuration.LoggerFailure + " " + e.Message + " " + e.StackTrace);
            }
        }
    }
}