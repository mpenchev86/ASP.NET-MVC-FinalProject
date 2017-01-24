namespace JustOrderIt.Web.Areas.Administration.ViewModels.Roles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Users;
    using ViewModels;

    public class RoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>
    {
        private ICollection<UserDetailsForRoleViewModel> users;

        public RoleViewModel()
        {
            this.users = new HashSet<UserDetailsForRoleViewModel>();
        }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<UserDetailsForRoleViewModel> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}