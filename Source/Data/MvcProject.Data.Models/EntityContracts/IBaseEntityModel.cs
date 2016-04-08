namespace MvcProject.Data.Models.EntityContracts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseEntityModel<TKey> : IAuditInfo, IDeletableEntity
    {
        TKey Id { get; set; }
    }
}
