namespace MvcProject.Web.Areas.Admin.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;
    using Products;
    using Users;

    public class CommentViewModel : BaseAdminViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(GlobalConstants.ValidationConstants.MinProductCommentLength)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        [Required]
        [UIHint("DropDownUserId")]
        public string UserId { get; set; }

        //public UserDetailsForCommentViewModel User { get; set; }

        [Required]
        [UIHint("GridForeignKey")]
        public int ProductId { get; set; }

        //public ProductDetailsForCommentViewModel Product { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                ;
        }
    }
}