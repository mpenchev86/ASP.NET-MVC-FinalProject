namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System.Linq;
    using Models.EntityContracts;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(IMvcProjectDbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => x.DeletedOn == null /*!x.IsDeleted*/);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}
