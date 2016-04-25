namespace MvcProject.Web.Areas.Admin.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;
    using Products;

    public class CategoryDetailsViewModel : BaseAdminViewModel, IMapFrom<Category>, IHaveCustomMappings
    {
        private ICollection<ProductDetailsForCategoryViewModel> products;

        public CategoryDetailsViewModel()
        {
            this.products = new HashSet<ProductDetailsForCategoryViewModel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(50)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            // throw new NotImplementedException();
        }
    }
}