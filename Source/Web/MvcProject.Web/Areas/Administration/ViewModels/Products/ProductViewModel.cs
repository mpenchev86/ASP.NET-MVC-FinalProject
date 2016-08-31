namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Categories;
    using Comments;
    using Data.Models;
    using Data.Models.Contracts;
    using Descriptions;
    using Images;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using MvcProject.Common.GlobalConstants;
    using Properties;
    using Services.Web;
    using Tags;
    using Users;
    using Votes;

    public class ProductViewModel : BaseAdminViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<TagDetailsForProductViewModel> tags;
        private ICollection<CommentDetailsForProductViewModel> comments;
        private ICollection<VoteDetailsForProductViewModel> votes;
        private ICollection<ImageDetailsForProductViewModel> images;

        public ProductViewModel()
        {
            this.tags = new HashSet<TagDetailsForProductViewModel>();
            this.comments = new HashSet<CommentDetailsForProductViewModel>();
            this.votes = new HashSet<VoteDetailsForProductViewModel>();
            this.images = new HashSet<ImageDetailsForProductViewModel>();
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.ProductShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int CategoryId { get; set; }

        [UIHint("DropDown")]
        public int? DescriptionId { get; set; }

        public DescriptionDetailsForProductViewModel Description { get; set; }

        public string MainImageIdEncoded
        {
            get
            {
                return this.MainImageId != null ? IdentifierProvider.EncodeIntIdStatic((int)this.MainImageId) : null;
            }
        }

        [UIHint("DropDownTemp")]
        public int? MainImageId { get; set; }
        
        public ImageDetailsForProductViewModel MainImage { get; set; }

        public bool IsInStock
        {
            get { return this.QuantityInStock != 0; }
        }

        [Required]
        [UIHint("Integer")]
        [Range(ValidationConstants.ProductQuantityInStockMin, ValidationConstants.ProductQuantityInStockMax)]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(typeof(decimal), ValidationConstants.ProductUnitPriceMinString, ValidationConstants.ProductUnitPriceMaxString)]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Range(typeof(decimal), ValidationConstants.ProductShippingPriceMinString, ValidationConstants.ProductShippingPriceMaxString)]
        [DataType(DataType.Currency)]
        public decimal? ShippingPrice { get; set; }

        [UIHint("Integer")]
        [Range(ValidationConstants.ProductAllTimeItemsSoldMin, ValidationConstants.ProductAllTimeItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [UIHint("Number")]
        [Range(ValidationConstants.ProductAllTimeAverageRatingMin, ValidationConstants.ProductAllTimeAverageRatingMax)]
        public double? AllTimeAverageRating
        {
            get { return this.Votes.Any() ? this.Votes.Average(v => v.VoteValue) : default(double?); }
        }

        [UIHint("DropDown")]
        public string SellerId { get; set; }

        public UserDetailsForProductViewModel Seller { get; set; }

        public ICollection<CommentDetailsForProductViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        [UIHint("FileUpload")]
        public ICollection<ImageDetailsForProductViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public ICollection<VoteDetailsForProductViewModel> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        [UIHint("MultiSelect")]
        public ICollection<TagDetailsForProductViewModel> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(
                            src => src.Description == null ? null : new DescriptionDetailsForProductViewModel
                            {
                                Id = src.Description.Id,
                                Content = src.Description.Content,
                                Properties = src.Description.Properties.Select(p => new PropertyDetailsForDescriptionViewModel
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    Value = p.Value,
                                    CreatedOn = p.CreatedOn,
                                    ModifiedOn = p.ModifiedOn
                                }).ToList(),
                                CreatedOn = src.Description.CreatedOn,
                                ModifiedOn = src.Description.ModifiedOn
                            }))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(
                            src => src.MainImage == null ? null : new ImageDetailsForProductViewModel
                            {
                                Id = src.MainImage.Id,
                                OriginalFileName = src.MainImage.OriginalFileName,
                                FileExtension = src.MainImage.FileExtension,
                                UrlPath = src.MainImage.UrlPath,
                                IsMainImage = src.MainImage.IsMainImage,
                                CreatedOn = src.MainImage.CreatedOn,
                                ModifiedOn = src.MainImage.ModifiedOn
                            }))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(
                            src => src.Images.Select(i => new ImageDetailsForProductViewModel
                            {
                                Id = i.Id,
                                OriginalFileName = i.OriginalFileName,
                                FileExtension = i.FileExtension,
                                UrlPath = i.UrlPath,
                                IsMainImage = i.IsMainImage,
                                CreatedOn = i.CreatedOn,
                                ModifiedOn = i.ModifiedOn
                            })))
                            ;
        }
    }
}