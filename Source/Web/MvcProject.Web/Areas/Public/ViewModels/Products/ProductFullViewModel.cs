namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Comments;
    //using Votes;
    using Common.GlobalConstants;
    using Data.Models;
    using Descriptions;
    using Images;
    using Infrastructure.Mapping;
    using Services.Web;

    public class ProductFullViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<string> tags;
        private ICollection<ImageForProductFullViewModel> images;
        //private ICollection<CommentForProductFullViewModel> comments;
        //private ICollection<VoteForProductFullViewModel> votes;
        private ICollection<ProductCommentWithRatingViewModel> commentsWithRatings;

        public ProductFullViewModel()
        {
            this.tags = new HashSet<string>();
            this.images = new HashSet<ImageForProductFullViewModel>();
            //this.comments = new HashSet<CommentForProductFullViewModel>();
            //this.votes = new HashSet<VoteForProductFullViewModel>();
            this.commentsWithRatings = new HashSet<ProductCommentWithRatingViewModel>();
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

        //public ICollection<CommentForProductFullViewModel> Comments
        //{
        //    get { return this.comments; }
        //    set { this.comments = value; }
        //}

        public ICollection<ProductCommentWithRatingViewModel> CommentsWithRatings
        {
            get { return this.commentsWithRatings; }
            set { this.commentsWithRatings = value; }
        }

        //public ICollection<VoteForProductFullViewModel> Votes
        //{
        //    get { return this.votes; }
        //    set { this.votes = value; }
        //}

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductFullViewModel>()
            //Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductFullViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName))
            ;
        }
    }
}