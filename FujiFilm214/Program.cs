using FujiFilm214.FujiFilm;
using Serilog;
using Serilog.Events;

namespace FujiFilm214
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // try
            // {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(Configuration.DebugLogsFilePath, retainedFileCountLimit: 5, rollingInterval: RollingInterval.Day)
                    .WriteTo.File(Configuration.LogsFilePath, LogEventLevel.Information, retainedFileCountLimit: 5, rollingInterval: RollingInterval.Day)
                    .WriteTo.Console(LogEventLevel.Debug)
                    .CreateLogger();

                FujiFilmController fujiFilm214 = new();
                fujiFilm214.InitializeTriggers(Configuration.ProgramStatusDbConnection, Log.Logger);

                Log.Debug(Configuration.SuccessMessage);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            //     throw;
            // }
        }
    }
}
