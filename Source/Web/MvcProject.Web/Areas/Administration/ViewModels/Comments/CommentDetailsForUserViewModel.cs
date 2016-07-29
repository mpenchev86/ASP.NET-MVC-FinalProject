namespace MvcProject.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class CommentDetailsForUserViewModel : BaseAdminViewModel<int>, IMapFrom<Comment>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentDetailsForUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}