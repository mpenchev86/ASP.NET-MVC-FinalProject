namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.EntityContracts;

    public class EfIntPKRepository<T> : GenericRepository<T, int>, IIntPKRepository<T>
        where T : class, IBaseEntityModel<int>
    {
        public EfIntPKRepository(DbContext context)
            : base(context)
        {
        }

        public override T GetById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
