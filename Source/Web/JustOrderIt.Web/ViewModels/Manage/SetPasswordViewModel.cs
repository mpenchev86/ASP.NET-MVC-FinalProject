namespace JustOrderIt.Web.ViewModels.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using JustOrderIt.Common.GlobalConstants;

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessagePasswordMinLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = ValidationConstants.ErrorMessagePasswordConfirmNew)]
        public string ConfirmPassword { get; set; }
    }
}