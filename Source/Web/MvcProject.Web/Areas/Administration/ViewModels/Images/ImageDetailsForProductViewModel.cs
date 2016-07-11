namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Image>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(ValidationConstants.ImageOriginalFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [MaxLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Image, ImageDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}