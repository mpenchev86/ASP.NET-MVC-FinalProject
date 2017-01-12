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

    public class SearchDoubleFiltersService : BaseDataService<SearchDoubleFilter, int, IIntPKDeletableRepository<SearchDoubleFilter>>, ISearchDoubleFiltersService
    {
        public SearchDoubleFiltersService(IIntPKDeletableRepository<SearchDoubleFilter> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override SearchDoubleFilter GetByEncodedId(string id)
        {
            var searchFilter = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }

        public override SearchDoubleFilter GetByEncodedIdFromNotDeleted(string id)
        {
            var searchFilter = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return searchFilter;
        }
    }
}
