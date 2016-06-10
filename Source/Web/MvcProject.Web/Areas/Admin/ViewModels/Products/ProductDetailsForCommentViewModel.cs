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
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class ProductDetailsForCommentViewModel : BaseAdminViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //configuration.CreateMap<Product, ProductViewModel>()
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(
            //               src => src.Description == null ? null : new DescriptionDetailsForProductViewModel
            //               {
            //                   Id = src.Description.Id,
            //                   Content = src.Description.Content,
            //                   Properties = src.Description.Properties.Select(p => new PropertyDetailsForDescriptionViewModel
            //                   {
            //                       Id = p.Id,
            //                       Name = p.Name,
            //                       Value = p.Value,
            //                       CreatedOn = p.CreatedOn,
            //                       ModifiedOn = p.ModifiedOn
            //                   }).ToList(),
            //                   CreatedOn = src.Description.CreatedOn,
            //                   ModifiedOn = src.Description.ModifiedOn
            //               }));
        }
    }
}