﻿namespace MvcProject.Web.Areas.Admin.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Image>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ImageOriginalFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Image, ImageDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}