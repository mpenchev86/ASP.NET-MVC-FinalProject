namespace JustOrderIt.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Catalog;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Web.Infrastructure.Mapping;

    public class CategoryDetailsForSearchFilterViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }
    }
}