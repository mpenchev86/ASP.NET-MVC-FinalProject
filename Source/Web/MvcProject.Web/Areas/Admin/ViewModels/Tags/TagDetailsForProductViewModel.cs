namespace MvcProject.Web.Areas.Admin.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Products;

    public class TagDetailsForProductViewModel : IMapFrom<Tag>, IHaveCustomMappings
    {
        //private ICollection<ProductDetailsForTagViewModel> products;

        public TagDetailsForProductViewModel()
        {
            //this.products = new HashSet<ProductDetailsForTagViewModel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        //public virtual ICollection<ProductDetailsForTagViewModel> Products
        //{
        //    get { return this.products; }
        //    set { this.products = value; }
        //}

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //configuration.CreateMap<Tag, TagDetailsForProductViewModel>()
            //    .ForMember(dest => dest.Products, opt => opt.MapFrom(
            //               src => src.Products.Select(p => new ProductDetailsForTagViewModel
            //               {
            //                   Id = p.Id,
            //                   Title = p.Title,
            //                   ShortDescription = p.ShortDescription,
            //                   UnitPrice = p.UnitPrice
            //               })))
            //    ;
        }
    }
}