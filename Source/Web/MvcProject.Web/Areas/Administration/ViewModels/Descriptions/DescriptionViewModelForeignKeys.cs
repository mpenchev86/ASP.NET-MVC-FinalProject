namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using Products;

    public class DescriptionViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForDescriptionViewModel> Products { get; set; }
    }
}