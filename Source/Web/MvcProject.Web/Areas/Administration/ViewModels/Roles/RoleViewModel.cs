namespace MvcProject.Web.Areas.Administration.ViewModels.Roles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Users;
    using ViewModels;

    public class RoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>, IHaveCustomMappings
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

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationRole, RoleViewModel>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(
                            src => src.Users.Select(user => new UserDetailsForRoleViewModel
                            {
                                Id = user.UserId,
                                UserName = user.UserName
                            })));
        }
    }
}