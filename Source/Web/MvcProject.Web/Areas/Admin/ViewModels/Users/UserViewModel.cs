﻿namespace MvcProject.Web.Areas.Admin.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using Comments;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Roles;
    using Votes;

    public class UserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        private ICollection<RoleDetailsForUserViewModel> roles;
        private ICollection<CommentDetailsForUserViewModel> comments;
        private ICollection<VoteDetailsForUserViewModel> votes;

        public UserViewModel()
        {
            this.roles = new HashSet<RoleDetailsForUserViewModel>();
            this.comments = new HashSet<CommentDetailsForUserViewModel>();
            this.votes = new HashSet<VoteDetailsForUserViewModel>();
        }

        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the salted/hashed form of the user password.
        /// </summary>
        /// <value>
        /// The salted/hashed form of the user password.
        /// </value>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        /// <value>
        /// The user's email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email is confirmed, default is false
        /// </summary>
        /// <value>
        /// True if the email is confirmed, default is false
        /// </value>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the failures for the purposes of lockout
        /// </summary>
        /// <value>
        /// The failures for the purposes of lockout
        /// </value>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets a random value that should change whenever a users credentials have
        /// changed (password changed, login removed)
        /// </summary>
        /// <value>
        /// A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </value>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets phoneNumber for the user
        /// </summary>
        /// <value>
        /// PhoneNumber for the user
        /// </value>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the phone number is confirmed, default is false
        /// </summary>
        /// <value>
        /// True if the phone number is confirmed, default is false
        /// </value>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether two factor authentication is enabled for the user
        /// </summary>
        /// <value>
        /// True if two factor authentication is enabled for the user
        /// </value>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lockout is enabled for this user
        /// </summary>
        /// <value>
        /// Is lockout enabled for this user
        /// </value>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets dateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        /// <value>
        /// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </value>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        [UIHint("MultiSelect")]
        public ICollection<RoleDetailsForUserViewModel> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }

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
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(
                            src => src.Roles.Select(c => new RoleDetailsForUserViewModel
                            {
                                Id = c.RoleId,
                                Name = c.RoleName
                            })))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(
                            src => src.Comments.Select(c => new CommentDetailsForUserViewModel
                            {
                                Id = c.Id,
                                Content = c.Content,
                                ProductId = c.ProductId,
                                CreatedOn = c.CreatedOn,
                                ModifiedOn = c.ModifiedOn
                            })))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(
                            src => src.Votes.Select(c => new VoteDetailsForUserViewModel
                            {
                                Id = c.Id,
                                VoteValue = c.VoteValue,
                                ProductId = c.ProductId,
                                CreatedOn = c.CreatedOn,
                                ModifiedOn = c.ModifiedOn
                            })));
        }
    }
}