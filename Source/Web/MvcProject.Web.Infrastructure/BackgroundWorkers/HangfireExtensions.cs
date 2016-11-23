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
        public static void PurgeJobs(this IMonitoringApi monitor)
        {
            using (var db = new MvcProjectDbContext())
            {
                // Source for deleting last N entries: http://stackoverflow.com/a/8303440/4491770
                db.Database.ExecuteSqlCommand(string.Format(
                    @"
                    USE [Final-Project]
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
                            OFFSET 10 ROWS
                        ) foo
                    );
                    "
                    , "Processing"
                    , "Scheduled"
                    //, 1
                    //, 10
                    ));

                //// Source for deleting last N entries: http://stackoverflow.com/a/8303440/4491770
                // WHERE Id <= (
                //    SELECT Id
                //    FROM(
                //        SELECT Id
                //        FROM[HangFire].[Job]
                //        ORDER BY Id DESC
                //        LIMIT 1 OFFSET 10
                //    ) foo
                //    )
            }
        }
    }
}
