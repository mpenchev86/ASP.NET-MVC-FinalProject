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

    public class PublicUserProfile : BasePublicViewModel<string>, IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}