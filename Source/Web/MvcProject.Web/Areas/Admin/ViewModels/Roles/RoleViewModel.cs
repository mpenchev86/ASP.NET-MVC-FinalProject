namespace MvcProject.Web.Areas.Admin.ViewModels.Roles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Users;
    using ViewModels;

    public class RoleViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationRole>, IHaveCustomMappings
    {
        private ICollection<UserDetailsForRoleViewModel> applicationUsers;

        public RoleViewModel()
        {
            this.applicationUsers = new HashSet<UserDetailsForRoleViewModel>();
        }

        public string Name { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<UserDetailsForRoleViewModel> ApplicationUsers
        {
            get { return this.applicationUsers; }
            set { this.applicationUsers = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationRole, RoleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.ApplicationUsers, opt => opt.MapFrom(
                //            src => src.ApplicationUsers.Select(user => new UserDetailsForRoleViewModel
                //            {
                //                Id = user.Id,
                //                UserName = user.UserName,
                //                CreatedOn = user.CreatedOn,
                //                ModifiedOn = user.ModifiedOn
                //            })))
                ;
        }
    }
}