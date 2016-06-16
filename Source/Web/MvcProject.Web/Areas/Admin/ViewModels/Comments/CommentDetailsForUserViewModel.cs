namespace MvcProject.Web.Areas.Admin.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;

    public class CommentDetailsForUserViewModel : BaseAdminViewModel<int>, IMapFrom<Comment>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(GlobalConstants.ValidationConstants.MinProductCommentLength)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentDetailsForUserViewModel>()
                //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                ;
        }
    }
}