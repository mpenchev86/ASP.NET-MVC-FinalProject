﻿namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Infrastructure.DataAnnotations;
    using MvcProject.Data.Models;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;
    using Products;
    using SearchFilters;
    using Keywords;
    public class CategoryViewModel : BaseAdminViewModel<int>, IMapFrom<Category>, IHaveCustomMappings
    {
        private ICollection<ProductDetailsForCategoryViewModel> products;
        private ICollection<SearchFilterDetailsForCategoryViewModel> searchFilters;
        private ICollection<KeywordDetailsForCategoryViewModel> keywords;


        public CategoryViewModel()
        {
            this.products = new HashSet<ProductDetailsForCategoryViewModel>();
            this.searchFilters = new HashSet<SearchFilterDetailsForCategoryViewModel>();
            this.keywords = new HashSet<KeywordDetailsForCategoryViewModel>();
        }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }

        public ICollection<ProductDetailsForCategoryViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public ICollection<SearchFilterDetailsForCategoryViewModel> SearchFilters
        {
            get { return this.searchFilters; }
            set { this.searchFilters = value; }
        }

        //[DataType(DataType.MultilineText)]
        //[StringLength(ValidationConstants.CategoryKeyWordsMaxLenght)]
        //public string Keywords { get; set; }

        [UIHint("MultiSelect")]
        public virtual ICollection<KeywordDetailsForCategoryViewModel> Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                ;
        }
    }
}