namespace MvcProject.Web.Areas.Admin.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Products;
    using Users;

    public class VoteViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForVoteViewModel> Products { get; set; }

        public IEnumerable<UserDetailsForVoteViewModel> Users { get; set; }
    }
}