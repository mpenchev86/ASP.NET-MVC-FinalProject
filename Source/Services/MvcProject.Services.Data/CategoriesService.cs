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

    public class CategoriesService : BaseDataService<Category, int, IIntPKDeletableRepository<Category>>, ICategoriesService
    {
        private readonly IIntPKDeletableRepository<Category> categoriesRepository;
        private IIdentifierProvider identifierProvider;

        public CategoriesService(IIntPKDeletableRepository<Category> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
            this.categoriesRepository = repository;
            this.identifierProvider = idProvider;
        }

        public override Category GetByEncodedId(string id)
        {
            var category = this.categoriesRepository.GetById(this.identifierProvider.DecodeIdToInt(id));
            return category;
        }

        public override Category GetByEncodedIdFromNotDeleted(string id)
        {
            var category = this.categoriesRepository.GetByIdFromNotDeleted(this.identifierProvider.DecodeIdToInt(id));
            return category;
        }
    }
}
