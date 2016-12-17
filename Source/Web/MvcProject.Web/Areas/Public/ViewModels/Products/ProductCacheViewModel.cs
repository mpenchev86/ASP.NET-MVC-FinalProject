namespace MvcProject.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Comments;
    using Data.Models;
    using Descriptions;
    using Images;
    using MvcProject.Web.Infrastructure.Mapping;
    using Services.Logic.ServiceModels;
    using Tags;
    using Votes;

    public class ProductCacheViewModel : BasePublicViewModel<int>, IProductFilteringModel, IMapFrom<Product>, IHaveCustomMappings
    {
        private ICollection<TagCacheViewModel> tags;
        private ICollection<ImageCacheViewModel> images;
        private ICollection<CommentCacheViewModel> comments;
        private ICollection<VoteCacheViewModel> votes;

        public ProductCacheViewModel()
        {
            this.tags = new HashSet<TagCacheViewModel>();
            this.images = new HashSet<ImageCacheViewModel>();
            this.comments = new HashSet<CommentCacheViewModel>();
            this.votes = new HashSet<VoteCacheViewModel>();
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? DescriptionId { get; set; }

        public DescriptionCacheViewModel Description { get; set; }

        public int AllTimeItemsSold { get; set; }

        public double? AllTimeAverageRating
        {
            get { return ProductCacheViewModel.GetAverageRating.Compile().Invoke(this); }
        }

        public static Expression<Func<ProductCacheViewModel, double?>> GetAverageRating
        {
            get
            {
                return p => p.Votes.Any() ? (double?)p.Votes.Average(v => v.VoteValue) : null;
            }
        }

        public int? MainImageId { get; set; }

        public ImageCacheViewModel MainImage { get; set; }

        public bool IsInStock
        {
            get { return this.QuantityInStock != 0; }
        }

        public int QuantityInStock { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? ShippingPrice { get; set; }

        public string SellerId { get; set; }

        public string SellerName { get; set; }

        public ICollection<TagCacheViewModel> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public ICollection<ImageCacheViewModel> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public ICollection<CommentCacheViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public ICollection<VoteCacheViewModel> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public string DescriptionContent
        {
            get
            {
                return this.DescriptionId != null ? this.Description.Content : string.Empty;
            }

            //set
            //{
            //    throw new NotImplementedException();
            //}
        }

        public IDictionary<string, string> DescriptionPropertiesNamesValues
        {
            get
            {
                return this.DescriptionId != null ? (this.Description.Properties != null ? this.Description.Properties.Select(p => new KeyValuePair<string, string>(p.Name, p.Value)).ToDictionary(x => x.Key, x => x.Value) : new Dictionary<string, string>()) : new Dictionary<string, string>();
            }

            //set
            //{
            //    throw new NotImplementedException();
            //}
        }

        public IEnumerable<string> TagNames
        {
            get
            {
                return this.Tags.Any() ? this.Tags.Select(t => t.Name).ToList() : new List<string>();
            }

            //set
            //{
            //    throw new NotImplementedException();
            //}
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductCacheViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(
                            src => src.Seller != null ? src.Seller.UserName : string.Empty));
        }
    }
}
