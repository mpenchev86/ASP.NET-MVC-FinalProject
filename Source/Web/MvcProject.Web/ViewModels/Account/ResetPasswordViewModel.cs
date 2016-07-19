namespace MvcProject.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/ResetPasswordViewModel.cs
    using MvcProject.Web.Common.Constants;
=======
    using MvcProject.Common.GlobalConstants;
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/ResetPasswordViewModel.cs

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/ResetPasswordViewModel.cs
        [StringLength(100, ErrorMessage = ValidationConstants.MinPasswordLengthErrorMessage, MinimumLength = 6)]
=======
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = 6)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/ResetPasswordViewModel.cs
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/ResetPasswordViewModel.cs
        [Compare("Password", ErrorMessage = ValidationConstants.ConfirmPasswordErrorMessage)]
=======
        [Compare("Password", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirm)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/ResetPasswordViewModel.cs
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}