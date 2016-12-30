namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Catalog;
    using MvcProject.Services.Web;

    public class CategoriesService : BaseDataService<Category, int, IIntPKDeletableRepository<Category>>, ICategoriesService
    {
        public CategoriesService(IIntPKDeletableRepository<Category> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override Category GetByEncodedId(string id)
        {
            var category = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return category;
        }

        public override Category GetByEncodedIdFromNotDeleted(string id)
        {
            var category = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return category;
        }

        //public Category GetByName(string categoryName)
        //{
        //    var category = this.Repository.All().Where(c => c.Name == categoryName).FirstOrDefault();
        //    return category;
        //}
    }
}
