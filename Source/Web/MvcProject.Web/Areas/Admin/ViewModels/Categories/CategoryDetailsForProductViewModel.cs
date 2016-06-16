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

    public class CategoryDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Category>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

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