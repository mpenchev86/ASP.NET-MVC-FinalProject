namespace MvcProject.Web.ViewModels.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/SetPasswordViewModel.cs
    using MvcProject.Web.Common.Constants;
=======
    using MvcProject.Common.GlobalConstants;
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/SetPasswordViewModel.cs

    public class SetPasswordViewModel
    {
        [Required]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/SetPasswordViewModel.cs
        [StringLength(100, ErrorMessage = ValidationConstants.MinPasswordLengthErrorMessage, MinimumLength = 6)]
=======
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = 6)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/SetPasswordViewModel.cs
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
<<<<<<< HEAD:Source/Web/MvcProject.Web/Areas/Common/ViewModels/Manage/SetPasswordViewModel.cs
        [Compare("NewPassword", ErrorMessage = ValidationConstants.ConfirmNewPasswordErrorMessage)]
=======
        [Compare("NewPassword", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirmNew)]
>>>>>>> master:Source/Web/MvcProject.Web/ViewModels/Manage/SetPasswordViewModel.cs
        public string ConfirmPassword { get; set; }
    }
}