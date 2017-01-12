namespace JustOrderIt.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Media;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Web.Infrastructure.Mapping;
    using Services.Data.ServiceModels;
    using Services.Web;

    public class ImageDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Image>, IMapFrom<ProcessedImage>
    {
        public string IdEncoded
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        [Required]
        [StringLength(ValidationConstants.ImageFullyQaulifiedFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [StringLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        [DataType(DataType.ImageUrl)]
        [StringLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

        public bool IsMainImage { get; set; }
    }
}