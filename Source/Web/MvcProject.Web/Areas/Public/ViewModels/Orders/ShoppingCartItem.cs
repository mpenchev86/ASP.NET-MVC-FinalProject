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

    public class ShoppingCartItem : /*BasePublicViewModel<int>,*/ IMapFrom<OrderItem>, IMapTo<OrderItem>, IHaveCustomMappings
    {
        //[ProductQuantityRange(0, maximum: Product.QuantityInStock)]
        public int ProductQuantity { get; set; }

        //public int OrderId { get; set; }

        //public ShoppingCartViewModel Order { get; set; }

        //public int ProductId { get; set; }

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