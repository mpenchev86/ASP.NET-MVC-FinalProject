namespace MvcProject.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using Products;

    public class ShoppingCartItem : IMapFrom<OrderItem>, IMapTo<OrderItem>, IHaveCustomMappings
    {
        [Range(1, int.MaxValue)]
        public int ProductQuantity { get; set; }

        [UIHint("CartProductDisplay")]
        public ProductForShoppingCart Product { get; set; }

        public bool ToDelete { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ShoppingCartItem, OrderItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
        }
    }
}