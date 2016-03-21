namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using EntityContracts;

    public class Product : BaseEntityModel<int>
    {
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        private ICollection<Comment> comments;

        public Product()
        {
            this.Tags = new HashSet<Tag>();
            this.Images = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(ValidationConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [MaxLength(ValidationConstants.MaxShortDescriptionLength)]
        public string ShortDescription { get; set; }

        [MinLength(ValidationConstants.MinFullDescriptionLength)]
        [MaxLength(ValidationConstants.MaxFullDescriptionLength)]
        public string FullDescription { get; set; }

        public int? MainImageId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ProductRating Rating { get; set; }

        public bool IsInStock { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DisplayFormat(ApplyFormatInEditMode = false, ConvertEmptyStringToNull = false, DataFormatString = "{0:C2}", HtmlEncode = false, NullDisplayText = "")]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ShippingPrice { get; set; }

        public virtual ShippingInfo ShippingInfo { get; set; }

        [Range(0, double.MaxValue)]
        public double? Length { get; set; }

        [Range(0, double.MaxValue)]
        public double? Hight { get; set; }

        [Range(0, double.MaxValue)]
        public double? Width { get; set; }

        [Range(0, double.MaxValue)]
        public double? Weight { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
