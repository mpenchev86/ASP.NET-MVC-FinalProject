namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Models;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class CategoryDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }
    }
}