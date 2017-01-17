namespace JustOrderIt.Web.Areas.Public.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.Mapping;
    using Orders;

    public class UserProfileViewModel : BasePublicViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        private ICollection<OrderForUserProfile> orders;

        public UserProfileViewModel()
        {
            orders = new HashSet<OrderForUserProfile>();
        }

        public string UserName { get; set; }

        public virtual ICollection<OrderForUserProfile> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserProfileViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}