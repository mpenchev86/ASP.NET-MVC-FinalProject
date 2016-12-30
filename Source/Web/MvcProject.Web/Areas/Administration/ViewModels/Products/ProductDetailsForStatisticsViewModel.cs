namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Mapping;
    using Data.Models.Catalog;

    public class ProductDetailsForStatisticsViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }
    }
}