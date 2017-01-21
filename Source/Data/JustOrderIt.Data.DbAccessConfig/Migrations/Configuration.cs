namespace JustOrderIt.Data.DbAccessConfig.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Contexts;
    using JustOrderIt.Common.GlobalConstants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Catalog;
    using Models.Identity;
    using Models.Media;
    using Models.Orders;
    using Models.Search;
    using SampleDataGenerators;
    using Web.Infrastructure.StringHelpers;

    public sealed class Configuration : DbMigrationsConfiguration<JustOrderItDbContext>
    {
        private readonly ISampleDataGenerator sampleDataGenerator;

        public Configuration()
            : this(new SampleDataGenerator())
        {
        }

        public Configuration(ISampleDataGenerator sampleDataGenerator)
        {
            // TODO: Remove in production
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            this.ContextKey = DbAccess.DbMigrationsConfigurationContextKey;

            this.sampleDataGenerator = sampleDataGenerator;
        }

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <remarks>
        /// The order of the generator method calls up to the one for Product entities is important. If changed, invalid object references might occur.
        /// </remarks>
        /// <param name="context">Context to be used for updating seed data.</param>
        protected override void Seed(JustOrderItDbContext context)
        {
            this.sampleDataGenerator.GenerateApplicationRoles(context);
            this.sampleDataGenerator.GenerateApplicationUsers(context);
            this.sampleDataGenerator.GenerateApplicationUserRoles(context);
            this.sampleDataGenerator.GenerateCategories(context);
            this.sampleDataGenerator.GenerateKeywords(context);
            this.sampleDataGenerator.GenerateSearchFilters(context);
            this.sampleDataGenerator.GenerateTags(context);
            this.sampleDataGenerator.GenerateProducts(context);
            this.sampleDataGenerator.GenerateComments(context);
            this.sampleDataGenerator.GenerateVotes(context);
        }
    }
}
