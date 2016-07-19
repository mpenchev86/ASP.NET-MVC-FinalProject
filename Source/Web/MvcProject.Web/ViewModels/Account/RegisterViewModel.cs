namespace MvcProject.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/RegisterViewModel.cs
    using MvcProject.Web.Common.Constants;
=======
    using MvcProject.Common.GlobalConstants;
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/RegisterViewModel.cs

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/RegisterViewModel.cs
        [StringLength(100, ErrorMessage = ValidationConstants.MinPasswordLengthErrorMessage, MinimumLength = 4)]
=======
        [StringLength(20, ErrorMessage = ValidationConstants.ErrorMessageUsernameLength, MinimumLength = 6)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = 4)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/RegisterViewModel.cs
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Account/RegisterViewModel.cs
        [Compare("Password", ErrorMessage = ValidationConstants.ConfirmPasswordErrorMessage)]
=======
        [Compare("Password", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirm)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Account/RegisterViewModel.cs
        public string ConfirmPassword { get; set; }
    }
}