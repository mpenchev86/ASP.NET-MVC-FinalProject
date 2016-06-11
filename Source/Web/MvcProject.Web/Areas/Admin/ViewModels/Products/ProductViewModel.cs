namespace MvcProject.Web.Areas.Admin.ViewModels.Products
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
    using Data.Models.EntityContracts;
    using Descriptions;
    using Images;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;
    using Properties;
    using Tags;
    using Votes;

    public class ProductViewModel : BaseAdminViewModel, IMapFrom<Product>, IHaveCustomMappings
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

        //[Key]
        //public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxShortDescriptionLength)]
        public string ShortDescription { get; set; }

        [UIHint("DropDownForNull")]
        public int? DescriptionId { get; set; }

        //[UIHint("DescriptionDetailsForProductViewModel")]
        public DescriptionDetailsForProductViewModel Description { get; set; }

        [UIHint("DropDownForNonNull")]
        public int CategoryId { get; set; }

        public CategoryDetailsForProductViewModel Category { get; set; }

        [UIHint("DropDownForNull")]
        public int? MainImageId { get; set; }

        public ImageDetailsForProductViewModel MainImage { get; set; }

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

        public ICollection<CommentDetailsForProductViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public ICollection<TagDetailsForProductViewModel> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

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

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(
                           src => new CategoryDetailsForProductViewModel
                           {
                               Id = src.Category.Id,
                               Name = src.Category.Name,
                               CreatedOn = src.Category.CreatedOn,
                               ModifiedOn = src.Category.ModifiedOn
                           }))
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
                               CreatedOn = src.MainImage.CreatedOn,
                               ModifiedOn = src.MainImage.ModifiedOn
                           }))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(
                           src => src.Comments.Select(c => new CommentDetailsForProductViewModel
                           {
                               Id = c.Id,
                               Content = c.Content,
                               UserId = c.UserId,
                               CreatedOn = c.CreatedOn,
                               ModifiedOn = c.ModifiedOn
                           })))
                .ForMember(dest => dest.tags, opt => opt.MapFrom(
                           src => src.Tags.Select(t => new TagDetailsForProductViewModel
                           {
                               Id = t.Id,
                               Name = t.Name,
                               CreatedOn = t.CreatedOn,
                               ModifiedOn = t.ModifiedOn
                           })))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(
                           src => src.Votes.Select(v => new VoteDetailsForProductViewModel
                           {
                               Id = v.Id,
                               VoteValue = v.VoteValue,
                               UserId = v.UserId,
                               CreatedOn = v.CreatedOn,
                               ModifiedOn = v.ModifiedOn
                           })))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(
                           src => src.Images.Select(i => new ImageDetailsForProductViewModel
                           {
                               Id = i.Id,
                               OriginalFileName = i.OriginalFileName,
                               FileExtension = i.FileExtension,
                               UrlPath = i.UrlPath,
                               CreatedOn = i.CreatedOn,
                               ModifiedOn = i.ModifiedOn
                           })))
                .IncludeBase<BaseEntityModel<int>, BaseAdminViewModel>()
                ;
        }
    }
}