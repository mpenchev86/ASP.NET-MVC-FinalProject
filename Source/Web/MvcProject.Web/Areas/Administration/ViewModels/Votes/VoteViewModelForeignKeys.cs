namespace MvcProject.Web.Areas.Administration.ViewModels.Votes
{
    using System.Collections.Generic;
    using Products;
    using Users;

    public class VoteViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForVoteViewModel> Products { get; set; }

        public IEnumerable<UserDetailsForVoteViewModel> Users { get; set; }
    }
}