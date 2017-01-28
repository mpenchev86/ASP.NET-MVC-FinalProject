namespace JustOrderIt.Web.Areas.Public.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentWithRatingViewModel : BasePublicViewModel<int>
    {
        public string CommentContent { get; set; }
        
        public int? Rating { get; set; }

        public string UserName { get; set; }

        public DateTime CommentCreatedOn { get; set; }
    }
}