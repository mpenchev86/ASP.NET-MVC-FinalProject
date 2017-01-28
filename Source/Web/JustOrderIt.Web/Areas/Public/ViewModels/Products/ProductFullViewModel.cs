namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Comments;
    using Common.GlobalConstants;
    using Data.Models;
    using Data.Models.Catalog;
    using Data.Models.Media;
    using Descriptions;
    using Images;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductFullViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<string> tags;
        private ICollection<ImageForProductFullViewModel> images;
        private ICollection<CommentWithRatingViewModel> commentsWithRatings;

        public ProductFullViewModel()
        {
            this.tags = new HashSet<string>();
            this.images = new HashSet<ImageForProductFullViewModel>();
            this.commentsWithRatings = new HashSet<CommentWithRatingViewModel>();
        }

        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int? DescriptionId { get; set; }

        public DescriptionForProductFullViewModel Description { get; set; }

        [Range(ValidationConstants.ProductAllTimeItemsSoldMin, ValidationConstants.ProductAllTimeItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [UIHint("Rating")]
        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public double? AllTimeAverageRating { get; set; }

        public int? MainImageId { get; set; }

        public Image MainImage { get; set; }

        public bool IsInStock
        {
            get { return this.QuantityInStock != 0; }
        }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ShippingPrice { get; set; }

        public string SellerName { get; set; }

        public ICollection<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public ICollection<ImageForProductFullViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public ICollection<CommentWithRatingViewModel> CommentsWithRatings
        {
            get { return this.commentsWithRatings; }
            set { this.commentsWithRatings = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductFullViewModel>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName))
                .ForMember(dest => dest.AllTimeAverageRating, opt => opt.MapFrom(
                            src => src.Votes.Any() ? (double?)src.Votes.Average(v => v.VoteValue) : null
                            ));
        }
    }
}