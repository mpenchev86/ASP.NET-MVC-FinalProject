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

    public class UserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        private ICollection<CommentDetailsForUserViewModel> comments;
        private ICollection<VoteDetailsForUserViewModel> votes;

        public UserViewModel()
        {
            this.comments = new HashSet<CommentDetailsForUserViewModel>();
            this.votes = new HashSet<VoteDetailsForUserViewModel>();
        }

        // The custom binding doesn't work if the key is named UserId
        //[Key]
        //public string Id { get; set; }

        // Maps from ApplicationUser.UserName
        public string UserName { get; set; }

        public string MainRole { get; set; }

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

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(
                            src => src.Comments.Select(c => new CommentDetailsForUserViewModel
                            {
                                Content = c.Content,
                                ProductId = c.ProductId,
                                CreatedOn = c.CreatedOn,
                                ModifiedOn = c.ModifiedOn
                            })))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(
                            src => src.Votes.Select(c => new VoteDetailsForUserViewModel
                            {
                                VoteValue = c.VoteValue,
                                ProductId = c.ProductId,
                                CreatedOn = c.CreatedOn,
                                ModifiedOn = c.ModifiedOn
                            })))
                ;
        }
    }
}