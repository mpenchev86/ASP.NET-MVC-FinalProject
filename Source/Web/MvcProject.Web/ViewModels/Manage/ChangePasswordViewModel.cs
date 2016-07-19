namespace MvcProject.Web.ViewModels.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/ChangePasswordViewModel.cs
    using MvcProject.Web.Common.Constants;
=======
    using MvcProject.Common.GlobalConstants;
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/ChangePasswordViewModel.cs

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/ChangePasswordViewModel.cs
        [StringLength(100, ErrorMessage = ValidationConstants.MinPasswordLengthErrorMessage, MinimumLength = 6)]
=======
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = 6)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/ChangePasswordViewModel.cs
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/ChangePasswordViewModel.cs
        [Compare("NewPassword", ErrorMessage = ValidationConstants.ConfirmNewPasswordErrorMessage)]
=======
        [Compare("NewPassword", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirmNew)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/ChangePasswordViewModel.cs
        public string ConfirmPassword { get; set; }
    }
}