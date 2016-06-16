namespace MvcProject.Web.Areas.Common.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using MvcProject.Web.Common.Constants;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = ValidationConstants.UsernameLengthErrorMessage, MinimumLength = 6)]
        //[Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = ValidationConstants.MinPasswordLengthErrorMessage, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ValidationConstants.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}