namespace MvcProject.Web.Areas.Admin.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Data.Models.EntityContracts;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.GlobalConstants;
    using Products;

    public class TagViewModel : BaseAdminViewModel<int>, IMapFrom<Tag>, IHaveCustomMappings
    {
        private ICollection<ProductDetailsForTagViewModel> products;

        public TagViewModel()
        {
            this.products = new HashSet<ProductDetailsForTagViewModel>();
        }

        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public ICollection<ProductDetailsForTagViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(
                            src => src.Products.Select(p => new ProductDetailsForTagViewModel
                            {
                                Id = p.Id,
                                Title = p.Title,
                                CreatedOn = p.CreatedOn,
                                ModifiedOn = p.ModifiedOn
                            })))
                ;
        }
    }
}