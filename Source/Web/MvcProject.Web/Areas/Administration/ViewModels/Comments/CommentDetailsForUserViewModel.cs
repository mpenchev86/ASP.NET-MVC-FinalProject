namespace MvcProject.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class CommentDetailsForUserViewModel : BaseAdminViewModel<int>, IMapFrom<Comment>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}