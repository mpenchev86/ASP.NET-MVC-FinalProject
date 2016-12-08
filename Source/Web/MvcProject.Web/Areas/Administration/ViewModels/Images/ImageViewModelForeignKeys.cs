namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using Products;

    public class ImageViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForImageViewModel> Products { get; set; }
    }
}