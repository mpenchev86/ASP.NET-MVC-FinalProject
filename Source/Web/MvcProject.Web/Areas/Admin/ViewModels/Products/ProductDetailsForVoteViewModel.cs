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

    public class ProductDetailsForVoteViewModel : BaseAdminViewModel<int>, IMapFrom<Product>, IHaveCustomMappings
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
            //throw new NotImplementedException();
        }
    }
}