namespace MvcProject.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Products;
    using Users;

    public class CommentViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForCommentViewModel> Products { get; set; }

        public IEnumerable<UserDetailsForCommentViewModel> Users { get; set; }
    }
}