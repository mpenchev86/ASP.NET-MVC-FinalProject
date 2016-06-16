namespace MvcProject.Web.Areas.Admin.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.DataAnnotations;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageViewModel : BaseAdminViewModel<int>, IMapFrom<Image>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Image, ImageViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                ;
        }
    }
}