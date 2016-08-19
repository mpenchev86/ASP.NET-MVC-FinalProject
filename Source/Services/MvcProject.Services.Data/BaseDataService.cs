namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models.Contracts;
    using Web;

    public abstract class BaseDataService<TEntity, TKey, TRepository> : IDeletableEntitiesBaseService<TEntity, TKey>
        where TEntity : class, IBaseEntityModel<TKey>, IDeletableEntity
        where TRepository : class, IRepository<TEntity, TKey>, IDeletableRepository<TEntity, TKey>
    {
        private readonly TRepository repository;
        private IIdentifierProvider identifierProvider;

        public BaseDataService(TRepository repository, IIdentifierProvider idProvider)
        {
            this.repository = repository;
            this.identifierProvider = idProvider;
        }

        public TRepository Repository
        {
            get { return this.repository; }
        }

        public IIdentifierProvider IdentifierProvider
        {
            get { return this.identifierProvider; }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            var result = this.repository.All();
            return result;
        }

        public virtual IQueryable<TEntity> GetAllNotDeleted()
        {
            var result = this.repository.AllNotDeleted();
            return result;
        }

        public virtual TEntity GetById(TKey id)
        {
            return this.repository.GetById(id);
        }

        public abstract TEntity GetByEncodedId(string id);

        public virtual TEntity GetByIdFromNotDeleted(TKey id)
        {
            return this.repository.GetByIdFromNotDeleted(id);
        }

        public abstract TEntity GetByEncodedIdFromNotDeleted(string id);

        public virtual void Insert(TEntity entity)
        {
            this.repository.Add(entity);
            this.repository.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            this.repository.Update(entity);
            this.repository.SaveChanges();
        }

        public virtual void UpdateMany(IEnumerable<TEntity> entities)
        {
            this.repository.UpdateMany(entities);
            this.repository.SaveChanges();
        }

        public virtual void MarkAsDeleted(TKey id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.repository.SaveChanges();
        }

        public virtual void MarkAsDeleted(TEntity entity)
        {
            entity.IsDeleted = true;
            this.repository.SaveChanges();
        }

        public virtual void DeletePermanent(TKey id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.repository.SaveChanges();
        }

        public virtual void DeletePermanent(TEntity entity)
        {
            this.repository.DeletePermanent(entity);
            this.repository.SaveChanges();
        }
    }
}
