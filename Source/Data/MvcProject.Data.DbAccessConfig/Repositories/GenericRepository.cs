namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GlobalConstants;
    using Models.EntityContracts;

    /// <summary>
    /// A generic implementation of an Entity Framework repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity which the repository manages.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public abstract class GenericRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IBaseEntityModel<TKey>
    {
        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(GlobalConstants.ExceptionMessages.DbContextArgumentException, nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet;
        }

        public abstract T GetById(TKey id);

        public virtual void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void DeletePermanent(TKey id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.DeletePermanent(entity);
            }
        }

        public virtual void DeletePermanent(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public virtual void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
