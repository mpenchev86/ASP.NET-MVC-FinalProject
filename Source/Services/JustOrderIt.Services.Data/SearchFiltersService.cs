namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.DbAccessConfig.Repositories;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Search;
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
