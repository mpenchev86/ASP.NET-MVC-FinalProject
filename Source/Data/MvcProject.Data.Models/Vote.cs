namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EntityContracts;

    public class Vote : BaseEntityModel<int>, IAdministerable
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int VoteValue { get; set; }
    }
}
