namespace JustOrderIt.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Contracts;

    /// <summary>
    /// Extends the generic Entity Framework repository for entities with string primary key and implements
    /// the repository functionality for entities implementing the IDeletableEntity interface.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    public class EfStringPKDeletableRepository<T> : GenericRepository<T, string>, IStringPKDeletableRepository<T>
        where T : class, IBaseEntityModel<string>, IDeletableEntity
    {
        public EfStringPKDeletableRepository(DbContext context)
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
