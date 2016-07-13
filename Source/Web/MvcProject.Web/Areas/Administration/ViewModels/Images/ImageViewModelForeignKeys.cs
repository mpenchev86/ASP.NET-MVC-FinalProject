namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Products;

    public class ImageViewModelForeignKeys
    {
        public IEnumerable<ProductDetailsForImageViewModel> Products { get; set; }
    }
}