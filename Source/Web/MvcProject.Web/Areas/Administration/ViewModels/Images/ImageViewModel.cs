namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Data.Models.Media;
    using Infrastructure.DataAnnotations;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageViewModel : BaseAdminViewModel<int>, IMapFrom<Image>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ImageFullyQaulifiedFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [StringLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

        [UIHint("DropDown")]
        public int? ProductId { get; set; }

        public bool IsMainImage { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}