using System;
using System.Threading.Tasks;
using FujiFilm214.FujiFilm;
using Quartz;
using Quartz.Impl;
using Serilog;
using Serilog.Events;

namespace FujiFilm214
{
    public class Program : IJob
    {
        private static async Task Main(string[] args)
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
                    // Dev-only environment. Utilizes Console.ReadLine() to keep console running in order for project to run to completion.
                    if (Configuration.Environment == "Development")
                    {
                        Console.WriteLine("***** DEVELOPMENT ENVIRONMENT DETECTED *****\n" +
                                          "Returned amount of changed statuses in IdentifyPotentiallyChangedRecordIds()\n" +
                                          "has been reduced to 3 in order to finish execution sooner for debug purposes.\n");

                        await InitiateCronJob();
                        Console.ReadLine();
                    }
                    else
                    {
                        await InitiateCronJob();
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

        /// <summary>
        ///     Cron job that sets up job/trigger to execute rest of the code on hourly schedule, initially firing once immediately.
        /// </summary>
        private static async Task InitiateCronJob()
        {
            // construct a scheduler factory using defaults
            var factory = new StdSchedulerFactory();

            // get a scheduler
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            var job = JobBuilder.Create<Program>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        ///     Execute job.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            Log.Information("FujiFilm214 Service Starting..");

            FujiFilmController fujiFilm214 = new(Configuration.Root);
            fujiFilm214.Start();

            Log.Information(Configuration.SuccessMessage);

            return Task.CompletedTask;
        }
    }
}