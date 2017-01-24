namespace JustOrderIt.Web.Areas.Administration.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using Comments;
    using Data.Models;
    using Data.Models.Identity;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Roles;
    using Votes;

    public class UserViewModel : BaseAdminViewModel<string>, IMapFrom<ApplicationUser>
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

        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the salted/hashed form of the user password.
        /// </summary>
        /// <value>
        /// The salted/hashed form of the user password.
        /// </value>
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        /// <value>
        /// The user's email.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email is confirmed, default is false
        /// </summary>
        /// <value>
        /// True if the email is confirmed, default is false
        /// </value>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the failures for the purposes of lockout
        /// </summary>
        /// <value>
        /// The failures for the purposes of lockout
        /// </value>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets a random value that should change whenever a users credentials have
        /// changed (password changed, login removed)
        /// </summary>
        /// <value>
        /// A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </value>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets phoneNumber for the user
        /// </summary>
        /// <value>
        /// PhoneNumber for the user
        /// </value>
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the phone number is confirmed, default is false
        /// </summary>
        /// <value>
        /// True if the phone number is confirmed, default is false
        /// </value>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether two factor authentication is enabled for the user
        /// </summary>
        /// <value>
        /// True if two factor authentication is enabled for the user
        /// </value>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lockout is enabled for this user
        /// </summary>
        /// <value>
        /// Is lockout enabled for this user
        /// </value>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets dateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        /// <value>
        /// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </value>
        public DateTime? LockoutEndDateUtc { get; set; }

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
    }
}