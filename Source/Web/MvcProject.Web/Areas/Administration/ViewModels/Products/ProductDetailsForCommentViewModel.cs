namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class ProductDetailsForCommentViewModel : BaseAdminViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductDetailsForCommentViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}