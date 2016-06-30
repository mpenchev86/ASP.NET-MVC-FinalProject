namespace MvcProject.Data.DbAccessConfig.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.EntityContracts;

    public class EfStringPKRepository<T> : GenericRepository<T, string>, IStringPKRepository<T>
        where T : class, IBaseEntityModel<string>
    {
        public EfStringPKRepository(DbContext context)
            : base(context)
        {
        }

        public override T GetById(string id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
