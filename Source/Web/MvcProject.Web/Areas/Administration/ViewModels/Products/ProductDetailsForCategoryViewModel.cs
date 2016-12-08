namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class ProductDetailsForCategoryViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }
    }
}