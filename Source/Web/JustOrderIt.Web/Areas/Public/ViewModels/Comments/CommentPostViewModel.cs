namespace JustOrderIt.Web.Areas.Public.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Common.GlobalConstants;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class CommentPostViewModel : BasePublicViewModel<int>, IMapFrom<Comment>, IHaveCustomMappings
    {
        public CommentPostViewModel()
        {
        }

        public CommentPostViewModel(int productId, string userName)
        {
            this.ProductId = productId;
            this.UserName = userName;
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        [LongDateTimeFormat]
        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentPostViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}