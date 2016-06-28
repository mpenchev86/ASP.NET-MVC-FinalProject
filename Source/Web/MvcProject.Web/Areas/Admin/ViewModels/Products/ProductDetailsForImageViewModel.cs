namespace MvcProject.Web.Areas.Admin.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductDetailsForImageViewModel : BaseAdminViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        //[Required]
        //[DataType(DataType.MultilineText)]
        //[MaxLength(ValidationConstants.ProductTitleMaxLength)]
        //public string Title { get; set; }

        //[DataType(DataType.MultilineText)]
        //[MaxLength(ValidationConstants.ShortDescriptionMaxLength)]
        //public string ShortDescription { get; set; }

        //[Required]
        //[Range(0, double.MaxValue)]
        //public decimal UnitPrice { get; set; }

        //[Index]
        //public bool IsDeleted { get; set; }

        //public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                ;
        }
    }
}