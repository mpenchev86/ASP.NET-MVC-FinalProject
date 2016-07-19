namespace MvcProject.Web.Areas.Public.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ImageForProductFullViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Image, ImageForProductFullViewModel>()
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                ;
        }
    }
}