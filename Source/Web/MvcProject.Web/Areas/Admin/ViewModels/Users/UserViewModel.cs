namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using AutoMapper;
    using Comments;
    using Data.DbAccessConfig;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data;
    using Votes;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        private ICollection<CommentDetailsForUserViewModel> comments;
        private ICollection<VoteDetailsForUserViewModel> votes;

        public UserViewModel()
        {
            this.comments = new HashSet<CommentDetailsForUserViewModel>();
            this.votes = new HashSet<VoteDetailsForUserViewModel>();
        }

        [Key]
        public string UserId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the user as displayed in the user interface,
        /// depending on whether the user has been deleted from the system or not.
        /// </summary>
        public string DisplayName { get; set; }

        public string MainRole { get; set; }

        //public IList<string> Roles
        //{
        //    get
        //    {
        //        var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        var list = userManager.GetRoles(this.Id);
        //        return list;
        //    }
        //}

        public ICollection<CommentDetailsForUserViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public ICollection<VoteDetailsForUserViewModel> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        [LongDateTimeFormat]
        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(
                           src => src.IsDeleted ? GlobalConstants.ApplicationSpecialStrings.UserNameDeletedUser : src.UserName))
                ;
        }
    }
}