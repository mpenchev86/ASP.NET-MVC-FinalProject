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
    using MvcProject.GlobalConstants;

    public class ProductViewModel : BaseAdminViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        //private ICollection<Tag> tags;
        //private ICollection<Comment> comments;
        //private ICollection<Image> images;
        //private ICollection<Vote> votes;
        //private ICollection<int> comments;
        //private ICollection<int> images;
        //private ICollection<int> votes;
        //private ICollection<ShippingInfo> shippingOptions;

        public ProductViewModel()
        {
            //this.tags = new HashSet<Tag>();
            //this.comments = new HashSet<Comment>();
            //this.images = new HashSet<Image>();
            //this.votes = new HashSet<Vote>();
            //this.comments = new HashSet<int>();
            //this.images = new HashSet<int>();
            //this.votes = new HashSet<int>();
            //this.shippingOptions = new HashSet<ShippingInfo>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxShortDescriptionLength)]
        public string ShortDescription { get; set; }

        public int? DescriptionId { get; set; }

        public int? MainImageId { get; set; }

        public int? CategoryId { get; set; }

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

        public virtual ICollection<int> Comments { get; set; }
        
        //public virtual ICollection<Comment> Comments
        //{
        //    get { return this.comments; }
        //    set { this.comments = value; }
        //}

        public virtual ICollection<int> Images { get; set; }

        //public virtual ICollection<Image> Images
        //{
        //    get { return this.images; }
        //    set { this.images = value; }
        //}

        public virtual ICollection<int> Votes { get; set; }

        //public virtual ICollection<Vote> Votes
        //{
        //    get { return this.votes; }
        //    set { this.votes = value; }
        //}

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title/*.Substring(0, 30) + "..."*/))
                //.ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription/*.Substring(0, 30) + "..."*/))
                //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Content/*.Substring(0, 30) + "..."*/))
                //.ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImageId))
                //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.Id)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Id)))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes.Select(v => v.Id)))
                ;
        }
    }
}