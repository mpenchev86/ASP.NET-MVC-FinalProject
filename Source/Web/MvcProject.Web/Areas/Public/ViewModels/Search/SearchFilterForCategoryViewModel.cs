namespace MvcProject.Web.Areas.Public.ViewModels.Search
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

    public class SearchFilterForCategoryViewModel : BasePublicViewModel<int>, IMapFrom<SearchFilter>, IHaveCustomMappings
    {
        private ISearchFilterHelpers productSearchAlgorithms;

        public SearchFilterForCategoryViewModel()
            : this(new SearchFilterHelpers())
        {
        }

        public SearchFilterForCategoryViewModel(ISearchFilterHelpers productSearchAlgorithms)
        {
            this.productSearchAlgorithms = productSearchAlgorithms;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterOptionsType OptionsType { get; set; }

        public string OptionsTypeString
        {
            get
            {
                return this.OptionsType == SearchFilterOptionsType.ConcreteValue ? SearchFilterOptionsType.ConcreteValue.ToString().ToLower() : SearchFilterOptionsType.ValueRange.ToString().ToLower();
            }
        }

        [Required]
        [Range(1, 2)]
        public SearchFilterSelectionType SelectionType { get; set; }

        public string SelectionTypeString
        {
            get
            {
                return this.SelectionType == SearchFilterSelectionType.Single ? SearchFilterSelectionType.Single.ToString().ToLower() : SearchFilterSelectionType.Multiple.ToString().ToLower();
            }
        }

        //[Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        public List</*SearchFilterOptionViewModel*/string> OptionsSplit
        {
            get
            {
                var values = this.productSearchAlgorithms.GetSearchOptionsLabels(this.Options, this.OptionsType, this.MeasureUnit);
                //var result = new List<SearchFilterOptionViewModel>();
                //foreach (var value in values)
                //{
                //    result.Add(new SearchFilterOptionViewModel() { Value = value });
                //}

                //return result;

                return values;
            }
        }

        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SearchFilter, SearchFilterForCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            ;
        }
    }
}