namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Comments;
    using Data.Models;
    using Data.Models.Catalog;
    using Descriptions;
    using Images;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;
    using Properties;
    using Services.Web;
    using Tags;
    using Users;
    using Votes;

    public class ProductViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
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
    }
}