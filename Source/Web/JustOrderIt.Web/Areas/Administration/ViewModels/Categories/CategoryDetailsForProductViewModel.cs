namespace JustOrderIt.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Web.Infrastructure.Mapping;
    using Data.Models.Catalog;

    public class CategoryDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }
    }
}