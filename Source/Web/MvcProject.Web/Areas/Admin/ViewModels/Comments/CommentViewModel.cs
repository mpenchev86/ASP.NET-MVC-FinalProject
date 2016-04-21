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

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(GlobalConstants.ValidationConstants.MinProductCommentLength)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //throw new NotImplementedException();
        }
    }
}