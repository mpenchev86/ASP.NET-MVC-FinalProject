namespace MvcProject.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class CommentViewModel : BaseAdminViewModel<int>, IMapFrom<Comment>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        public string Content { get; set; }

        [Required]
        [UIHint("DropDown")]
        public string UserId { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int ProductId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}