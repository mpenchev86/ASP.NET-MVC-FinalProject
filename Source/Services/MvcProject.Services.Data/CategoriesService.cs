namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categories;

        public CategoriesService(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public IQueryable<Category> GetAll()
        {
            var result = this.categories
                             .All()
                             .OrderBy(x => x.Name);
            return result;
        }
    }
}
