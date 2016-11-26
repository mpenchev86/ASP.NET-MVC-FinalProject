namespace MvcProject.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.DbAccessConfig.Contexts;
    using Hangfire;
    using Hangfire.Server;
    using Hangfire.SqlServer;
    using Hangfire.States;
    using Hangfire.Storage;
    using Hangfire.Storage.Monitoring;

    public static class HangfireExtensions
    {
        /// <summary>
        /// Executes sql command which deletes jobs that were interrupted on previous server shutdown.
        /// </summary>
        /// <param name="monitor">The Job Storage monitoring API.</param>
        public static void PurgeJobs(this IMonitoringApi monitor)
        {
            using (var db = new MvcProjectDbContext())
            {
                // Source for deleting last N entries: http://stackoverflow.com/a/8303440/4491770
                db.Database.ExecuteSqlCommand(string.Format(
                    @"
                    USE [Hangfire]
                    DELETE FROM [HangFire].[List];
                    DELETE FROM [HangFire].[Set];
                    DELETE FROM [HangFire].[JobQueue];
                    DELETE FROM [HangFire].[Job]
                    WHERE StateName IN ('{0}', '{1}');
                    DELETE FROM [HangFire].[Job]
                    WHERE Id <= (
                        SELECT TOP 1 Id
                        FROM(
                            SELECT Id
                            FROM [HangFire].[Job]
                            ORDER BY Id DESC
                            OFFSET {2} ROWS
                        ) foo
                    );
                    ",
                    "Processing",
                    "Scheduled",
                    1000));
            }
        }
    }
}
