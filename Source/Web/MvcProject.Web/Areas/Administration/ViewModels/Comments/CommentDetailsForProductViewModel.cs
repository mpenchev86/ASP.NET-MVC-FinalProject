namespace MvcProject.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class CommentDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Comment>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        public string Content { get; set; }

        [Required]
        public string UserName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentDetailsForProductViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}