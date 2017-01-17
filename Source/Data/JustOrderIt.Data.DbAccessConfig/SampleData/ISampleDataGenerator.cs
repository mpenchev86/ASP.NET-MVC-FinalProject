namespace JustOrderIt.Data.DbAccessConfig.SampleDataGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contexts;
    using HelperModels;

    public interface ISampleDataGenerator
    {
        void GenerateApplicationRoles(JustOrderItDbContext context);

        void GenerateApplicationUsers(JustOrderItDbContext context);

        void GenerateApplicationUserRoles(JustOrderItDbContext context);

        void GenerateCategories(JustOrderItDbContext context);

        void GenerateKeywords(JustOrderItDbContext context);

        void GenerateSearchFilters(JustOrderItDbContext context);

        void GenerateTags(JustOrderItDbContext context);

        void GenerateProducts(JustOrderItDbContext context);

        void GenerateComments(JustOrderItDbContext context);

        void GenerateVotes(JustOrderItDbContext context);
    }
}
