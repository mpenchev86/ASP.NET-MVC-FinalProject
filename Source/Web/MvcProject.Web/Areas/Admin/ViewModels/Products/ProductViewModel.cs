namespace MvcProject.Web.Areas.Admin.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Data.Common.Constants;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        //private ICollection<Tag> tags;
        private ICollection<Comment> comments;
        private ICollection<Image> images;
        private ICollection<Vote> votes;
        ////private ICollection<ShippingInfo> shippingOptions;

        public ProductViewModel()
        {
            //this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
            this.images = new HashSet<Image>();
            this.votes = new HashSet<Vote>();
            //this.shippingOptions = new HashSet<ShippingInfo>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Data.Common.Constants.ValidationConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [MaxLength(Data.Common.Constants.ValidationConstants.MaxShortDescriptionLength)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int? MainImage { get; set; }

        public string Category { get; set; }

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

        //public string JoinedTags
        //{
        //    get
        //    {
        //        if (this.Tags == null)
        //        {
        //            return string.Empty;
        //        }

        //        return string.Join(", ", this.Tags.ToArray());
        //    }
        //}

        //public virtual ICollection<Tag> Tags
        //{
        //    get { return this.tags; }
        //    set { this.tags = value; }
        //}

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title/*.Substring(0, 30) + "..."*/))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription/*.Substring(0, 30) + "..."*/))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Content/*.Substring(0, 30) + "..."*/))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImageId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                ;
        }

        //private string StringShortener(string src, int length)
        //{
        //    var result = src.Substring(0, length) + "...";
        //    return result;
        //}
    }
}