namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class StatisticsService : BaseDataService<Statistics, int, IIntPKDeletableRepository<Statistics>>, IStatisticsService
    {
        private readonly IIntPKDeletableRepository<Statistics> repository;
        private IIdentifierProvider idProvider;

        public StatisticsService(IIntPKDeletableRepository<Statistics> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
            this.repository = repository;
            this.idProvider = idProvider;
        }

        public override IQueryable<Statistics> GetAll()
        {
            return base.GetAll();
        }

        public override Statistics GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var statistics = this.repository.GetById(idAsInt);
            return statistics;
        }

        public override Statistics GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var statistics = this.repository.GetByIdFromNotDeleted(idAsInt);
            return statistics;
        }
    }
}
