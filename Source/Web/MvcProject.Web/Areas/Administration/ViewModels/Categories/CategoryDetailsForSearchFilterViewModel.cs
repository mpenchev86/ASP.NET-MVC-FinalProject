namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Catalog;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class CategoryDetailsForSearchFilterViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }
    }
}