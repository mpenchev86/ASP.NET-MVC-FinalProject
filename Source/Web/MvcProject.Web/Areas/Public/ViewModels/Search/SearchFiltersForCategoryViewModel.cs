﻿namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Logic;

    public class SearchFiltersForCategoryViewModel : BasePublicViewModel<int>, IMapFrom<SearchFilter>, IHaveCustomMappings
    {
        private IProductSearchAlgorithms productSearchAlgorithms;

        public SearchFiltersForCategoryViewModel()
        {
        }

        public SearchFiltersForCategoryViewModel(IProductSearchAlgorithms productSearchAlgorithms)
        {
            this.productSearchAlgorithms = productSearchAlgorithms;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 3)]
        public SearchFilterOptionsType Type { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        public List<string> OptionsProcessed
        {
            get
            {
                return this.productSearchAlgorithms.ProcessSearchOptions(this.Options);
            }
        }

        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}