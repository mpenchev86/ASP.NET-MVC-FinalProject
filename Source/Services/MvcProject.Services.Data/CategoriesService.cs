namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Services.Web;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categories;
        private IIdentifierProvider idProvider;

        public CategoriesService(IRepository<Category> categories, IIdentifierProvider idProvider)
        {
            this.categories = categories;
            this.idProvider = idProvider;
        }

        public IQueryable<Category> GetAll()
        {
            var result = this.categories
                             .All()
                             .OrderBy(x => x.Name);
            return result;
        }

        public Category GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var category = this.categories.GetById(idAsInt);
            return category;
        }
    }
}
