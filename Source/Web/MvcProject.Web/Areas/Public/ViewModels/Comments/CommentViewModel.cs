namespace MvcProject.Web.Areas.Public.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Common.GlobalConstants;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CommentContentMaxLength, MinimumLength = ValidationConstants.CommentContentMinLength)]
        public string Content { get; set; }

        [Required]
        //[UIHint("DropDown")]
        public string UserId { get; set; }

        [Required]
        //[UIHint("DropDown")]
        public int ProductId { get; set; }
        
        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        [LongDateTimeFormat]
        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}