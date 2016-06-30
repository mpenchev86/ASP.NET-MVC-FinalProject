namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.EntityContracts;

    public class EfStringPKRepositoryDeletable<T> : GenericRepository<T, string>, IStringPKRepositoryDeletable<T>
        where T : class, IBaseEntityModel<string>, IDeletableEntity
    {
        public EfStringPKRepositoryDeletable(DbContext context)
            : base(context)
        {
        }

        public override T GetById(string id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public T GetByIdFromNotDeleted(string id)
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
