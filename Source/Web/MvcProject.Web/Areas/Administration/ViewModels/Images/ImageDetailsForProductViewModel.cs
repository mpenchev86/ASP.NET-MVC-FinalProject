namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Extensions;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;
    using Services.Data.ServiceModels;
    using Services.Logic.ServiceModels;
    using Services.Web;

    public class ImageDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Image>, IMapFrom<ProcessedImage>, IHaveCustomMappings
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

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Image, ImageDetailsForProductViewModel>();
        }
    }
}