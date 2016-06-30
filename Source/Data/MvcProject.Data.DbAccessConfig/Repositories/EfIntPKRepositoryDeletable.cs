namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.EntityContracts;

    public class EfIntPKRepositoryDeletable<T> : GenericRepository<T, int>, IIntPKRepositoryDeletable<T>
        where T : class, IBaseEntityModel<int>, IDeletableEntity
    {
        public EfIntPKRepositoryDeletable(DbContext context)
            : base(context)
        {
        }

        public override T GetById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public T GetByIdFromNotDeleted(int id)
        {
            return this.AllNotDeleted().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> AllNotDeleted()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public virtual void DeleteMark(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }
    }
}
