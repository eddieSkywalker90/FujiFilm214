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
                    FujiFilmController fujiFilm214 = new(Configuration.Root);
                    fujiFilm214.Start();

                    Log.CloseAndFlush();
                    Log.Information(Configuration.SuccessMessage);
                }
                catch (Exception e)
                {
                    EmailHandler.EmailManager.SendEmail(EmailHandler.AlertMessage);
                    Log.Information(e, Configuration.FailMessage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Configuration.LoggerFailure + " " + e.Message);
            }
        }
    }
}
