namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductSneakPeakViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductSneakPeakViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}