namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Catalog;
    using Images;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductSneakPeekViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public ProductSneakPeekViewModel()
        {
            this.Images = new HashSet<ImageForProductSneakPeekViewModel>();
        }

        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public ImageForProductSneakPeekViewModel MainImage { get; set; }

        public ICollection<ImageForProductSneakPeekViewModel> Images { get; set; }

        public string SellerName { get; set; }

        public decimal Price { get; set; }

        [UIHint("Rating")]
        public double? Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductSneakPeekViewModel>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(
                            src => src.Votes.Any() ? (double?)src.Votes.Average(v => v.VoteValue) : null
                            ))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(
                            src => src.MainImage ?? src.Images.FirstOrDefault()));
        }
    }
}