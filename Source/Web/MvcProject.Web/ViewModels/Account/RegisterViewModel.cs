namespace MvcProject.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using MvcProject.Common.GlobalConstants;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.ApplicationUserNameMaxLength, ErrorMessage = ValidationConstants.ErrorMessageUsernameLength, MinimumLength = ValidationConstants.ApplicationUserNameMinLength)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(ValidationConstants.ApplicationUserPasswordMaxLength, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = ValidationConstants.ApplicationUserPasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirm)]
        public string ConfirmPassword { get; set; }
    }
}