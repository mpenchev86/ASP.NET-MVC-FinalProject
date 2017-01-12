namespace JustOrderIt.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using JustOrderIt.Common.GlobalConstants;

    public class ProductDetailsForTagViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }
    }
}