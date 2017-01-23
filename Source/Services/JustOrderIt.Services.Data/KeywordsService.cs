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
    using JustOrderIt.Services.Web;

    public class KeywordsService : BaseDataService<Keyword, int, IIntPKDeletableRepository<Keyword>>, IKeywordsService
    {
        public KeywordsService(IIntPKDeletableRepository<Keyword> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override Keyword GetByEncodedId(string id)
        {
            var keyword = this.Repository.GetById((int)this.IdentifierProvider.DecodeToIntId(id));
            return keyword;
        }

        public override Keyword GetByEncodedIdFromNotDeleted(string id)
        {
            var keyword = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeToIntId(id));
            return keyword;
        }
    }
}
