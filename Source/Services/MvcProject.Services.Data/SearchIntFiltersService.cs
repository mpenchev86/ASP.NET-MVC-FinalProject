namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class SearchIntFiltersService : BaseDataService<SearchIntFilter, int, IIntPKDeletableRepository<SearchIntFilter>>, ISearchIntFiltersService
    {
        public SearchIntFiltersService(IIntPKDeletableRepository<SearchIntFilter> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override SearchIntFilter GetByEncodedId(string id)
        {
            var searchFilter = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }

        public override SearchIntFilter GetByEncodedIdFromNotDeleted(string id)
        {
            var searchFilter = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }
    }
}
