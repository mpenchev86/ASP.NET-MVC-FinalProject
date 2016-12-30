namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Search;
    using Web;

    public class SearchFiltersService : BaseDataService<SearchFilter, int, IIntPKDeletableRepository<SearchFilter>>, ISearchFiltersService
    {
        public SearchFiltersService(IIntPKDeletableRepository<SearchFilter> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override SearchFilter GetByEncodedId(string id)
        {
            var searchFilter = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }

        public override SearchFilter GetByEncodedIdFromNotDeleted(string id)
        {
            var searchFilter = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }
    }
}
