﻿namespace JustOrderIt.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Identity;
    using Data.Models.Orders;
    using Infrastructure.Mapping;

    public class ShoppingCartViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        private ICollection<ShoppingCartItem> cartItems;

        public ShoppingCartViewModel()
        {
            this.cartItems = new List<ShoppingCartItem>();
        }

        public decimal TotalCost { get; set; }

        public string UserName { get; set; }

        [UIHint("CartItems")]
        public virtual ICollection<ShoppingCartItem> CartItems
        {
            get { return this.cartItems; }
            set { this.cartItems = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Order, ShoppingCartViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}