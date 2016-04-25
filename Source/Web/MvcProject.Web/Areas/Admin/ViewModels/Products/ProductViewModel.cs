namespace MvcProject.Web.Areas.Admin.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Comments;
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;
    using Tags;
    public class ProductViewModel : BaseAdminViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<TagDetailsForProductViewModel> tags;
        private IEnumerable<string> tagsNames;
        private ICollection<CommentDetailsForProductViewModel> comments;
        //private IEnumerable<int> commentsIds;
        //private ICollection<Image> images;
        //private ICollection<Vote> votes;
        private IEnumerable<int> imagesIds;
        private IEnumerable<int> votesIds;
        //private ICollection<ShippingInfo> shippingOptions;

        public ProductViewModel()
        {
            this.tags = new HashSet<TagDetailsForProductViewModel>();
            this.tagsNames = new HashSet<string>();
            this.comments = new HashSet<CommentDetailsForProductViewModel>();
            //this.commentsIds = new HashSet<int>();
            //this.images = new HashSet<Image>();
            //this.votes = new HashSet<Vote>();
            this.imagesIds = new HashSet<int>();
            this.votesIds = new HashSet<int>();
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

        //public virtual IEnumerable<int> CommentsIds
        //{
        //    get { return this.commentsIds; }
        //    set { this.commentsIds = value; }
        //}

        public virtual ICollection<CommentDetailsForProductViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        //public virtual IEnumerable<string> TagsNames
        //{
        //    get { return this.tagsNames; }
        //    set { this.tagsNames = value; }
        //}

        public virtual ICollection<TagDetailsForProductViewModel> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual IEnumerable<int> ImagesIds
        {
            get { return this.imagesIds; }
            set { this.imagesIds = value; }
        }

        //public virtual ICollection<Image> Images
        //{
        //    get { return this.images; }
        //    set { this.images = value; }
        //}

        public virtual IEnumerable<int> VotesIds
        {
            get { return this.votesIds; }
            set { this.votesIds = value; }
        }

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
                //.ForMember(dest => dest.TagsNames, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.tags, opt => opt.MapFrom(
                           src => src.Tags.Select(t => new TagDetailsForProductViewModel
                           {
                               Id = t.Id,
                               Name = t.Name
                           })))
                //.ForMember(dest => dest.CommentsIds, opt => opt.MapFrom(src => src.Comments.Select(c => c.Id)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => new CommentDetailsForProductViewModel { Id = c.Id, Content = c.Content, UserId = c.UserId })))
                .ForMember(dest => dest.ImagesIds, opt => opt.MapFrom(src => src.Images.Select(i => i.Id)))
                .ForMember(dest => dest.VotesIds, opt => opt.MapFrom(src => src.Votes.Select(v => v.Id)))
                ;
        }
    }
}