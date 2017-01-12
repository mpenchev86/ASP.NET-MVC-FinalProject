namespace JustOrderIt.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using Categories;
    using Descriptions;
    using Users;

    public class ProductViewModelForeignKeys
    {
        public IEnumerable<DescriptionDetailsForProductViewModel> Descriptions { get; set; }

        public IEnumerable<CategoryDetailsForProductViewModel> Categories { get; set; }

        public IEnumerable<UserDetailsForProductViewModel> Sellers { get; set; }
    }
}