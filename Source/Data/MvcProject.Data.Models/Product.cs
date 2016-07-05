namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;
    using MvcProject.GlobalConstants;

    public class Product : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        private ICollection<Comment> comments;
        //private ICollection<ShippingInfo> shippingOptions;
        private ICollection<Vote> votes;

        public Product()
        {
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
            //this.shippingOptions = new HashSet<ShippingInfo>();
        }

        [Required]
        [MaxLength(ValidationConstants.ProductTitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(ValidationConstants.ShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        public int? DescriptionId { get; set; }

        public virtual Description Description { get; set; }

        public int? MainImageId { get; set; }

        public virtual Image MainImage { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
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

        [Range(0, double.MaxValue)]
        public double? Length { get; set; }

        [Range(0, double.MaxValue)]
        public double? Height { get; set; }

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

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        //public virtual ICollection<ShippingInfo> ShippingOptions
        //{
        //    get { return this.shippingOptions; }
        //    set { this.shippingOptions = value; }
        //}
    }
}
