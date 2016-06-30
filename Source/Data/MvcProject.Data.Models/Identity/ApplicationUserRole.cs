namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EntityContracts;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUserRole : IdentityUserRole, IBaseEntityModel<int>
    {
        public ApplicationUserRole()
            : base()
        {
        }

        [NotMapped]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}
