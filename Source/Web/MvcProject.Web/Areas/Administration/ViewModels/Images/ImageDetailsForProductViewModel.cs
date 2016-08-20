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
    using Services.Data.ServiceModels;
    using Services.Logic.ServiceModels;

    public class ImageDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Image>/*, IMapTo<RawFile>*/, IHaveCustomMappings
    {
        //public static Func<ImageDetailsForProductViewModel, RawFile> ToRawFile
        //{
        //    get
        //    {
        //        return file => new RawFile
        //        {
        //            OriginalFileName = file.OriginalFileName,
        //            FileExtension = file.FileExtension,
        //            Content = file.ByteArrayContent
        //        };
        //    }
        //}

        [Required]
        [StringLength(ValidationConstants.ImageOriginalFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [StringLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        [DataType(DataType.ImageUrl)]
        [StringLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

        //[Required]
        //public string Base64Content { get; set; }

        //public byte[] ByteArrayContent
        //{
        //    get
        //    {
        //        return Convert.FromBase64String(this.Base64Content);
        //    }
        //}

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            // for IMapFrom<>
            configuration.CreateMap<Image, ImageDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            //// for IMapTo<>
            //configuration.CreateMap<ImageDetailsForProductViewModel, RawFile>()
            //    .ForMember(dest => dest.OriginalFileName, opt => opt.MapFrom(src => src.OriginalFileName))
            //    .ForMember(dest => dest.FileExtension, opt => opt.MapFrom(src => src.FileExtension))
            //    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.ByteArrayContent));
        }
    }
}