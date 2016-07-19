namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class ProductCommentWithRatingViewModel
    {
        public string CommentContent { get; set; }

        public int? Rating { get; set; }

        public string UserName { get; set; }

        public DateTime CommentCreatedOn { get; set; }
    }
}