namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Products;

    public class DescriptionViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForDescriptionViewModel> Products { get; set; }
    }
}