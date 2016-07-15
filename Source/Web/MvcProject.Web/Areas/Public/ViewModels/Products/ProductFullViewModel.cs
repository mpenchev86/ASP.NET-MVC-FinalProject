namespace MvcProject.Web.Areas.Public.ViewModels.Products
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
    using Descriptions;
    using Infrastructure.Mapping;
    using Services.Web;
    public class ProductFullViewModel : BasePublicViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<string> tags;
        private ICollection<Image> images;
        private ICollection<CommentForProductFullViewModel> comments;
        private ICollection<Vote> votes;
        private IIdentifierProvider identifierProvider;

        public ProductFullViewModel()
        {
            this.tags = new HashSet<string>();
            this.images = new HashSet<Image>();
            this.comments = new HashSet<CommentForProductFullViewModel>();
            this.votes = new HashSet<Vote>();
            this.identifierProvider = new IdentifierProvider();
        }

        public string EncodedId
        {
            get { return this.identifierProvider.EncodeIntId(this.Id); }
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(ValidationConstants.ProductShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int? DescriptionId { get; set; }

        public DescriptionForProductFullViewModel Description { get; set; }

        [Range(ValidationConstants.ProductAllTimeItemsSoldMin, ValidationConstants.ProductAllTimeItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public int AllTimeAverageRating { get; set; }

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

        public ICollection<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public ICollection<CommentForProductFullViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductFullViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}