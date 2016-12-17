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
        private List<SearchFilterOptionViewModel> refinementOptions;

        public SearchFilterForCategoryViewModel()
            : this(new SearchFilterHelpers())
        {
            //this.refinementOptions = new List<SearchFilterOptionViewModel>();
        }

        public SearchFilterForCategoryViewModel(ISearchFilterHelpers productSearchAlgorithms)
        {
            this.productSearchAlgorithms = productSearchAlgorithms;
            this.refinementOptions = new List<SearchFilterOptionViewModel>(
                //this.OptionsSplit
                //SearchFilterHelpers.GetSearchOptionsLabels(this.Options, this.OptionsType, this.MeasureUnit)
                //.Select(option => new SearchFilterOptionViewModel
                //{
                //    Value = option,
                //    Checked = false,
                //    SearchFilterId = this.Id,
                //    OptionsType = this.OptionsType,
                //    SelectionType = this.SelectionType
                //})
            );
        }

        //[Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterOptionsType OptionsType { get; set; }

        //[Required]
        //[Range(1, 2)]
        //public int OptionsTypeValue { get; set; }

        //public string OptionsTypeString { get; set; }
        //{
        //    get
        //    {
        //        return this.OptionsType.ToString().ToLower()/* == SearchFilterOptionsType.ConcreteValue ? SearchFilterOptionsType.ConcreteValue.ToString().ToLower() : SearchFilterOptionsType.ValueRange.ToString().ToLower()*/;
        //    }
        //}

        [Required]
        [Range(1, 2)]
        public SearchFilterSelectionType SelectionType { get; set; }

        //[Required]
        //[Range(1, 2)]
        //public int SelectionTypeValue { get; set; }

        //public string SelectionTypeString { get; set; }
        //{
        //    get
        //    {
        //        return this.SelectionType.ToString().ToLower()/* == SearchFilterSelectionType.Single ? SearchFilterSelectionType.Single.ToString().ToLower() : SearchFilterSelectionType.Multiple.ToString().ToLower()*/;
        //    }
        //}

        //[Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        public List<string> OptionsSplit
        {
            get
            {
                var values = /*this.productSearchAlgorithms.*/SearchFilterHelpers.GetSearchOptionsLabels(this.Options, /*this.OptionsTypeValue */this.OptionsType, this.MeasureUnit);
                return values;
            }
        }

        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }

        [UIHint("SearchFilterOptions")]
        public List<SearchFilterOptionViewModel> RefinementOptions
        {
            get { return this.refinementOptions; }
            set { this.refinementOptions = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SearchFilter, SearchFilterForCategoryViewModel>()
                //.ForMember(dest => dest.OptionsTypeValue, opt => opt.MapFrom(src => src.OptionsType.GetHashCode()))
                //.ForMember(dest => dest.OptionsTypeString, opt => opt.MapFrom(src => src.OptionsType.ToString()))
                //.ForMember(dest => dest.SelectionTypeValue, opt => opt.MapFrom(src => src.SelectionType.GetHashCode()))
                //.ForMember(dest => dest.SelectionTypeString, opt => opt.MapFrom(src => src.SelectionType.ToString()))
                .ForMember(dest => dest.RefinementOptions, opt => opt.MapFrom(
                            src =>
                                      //src.Options
                                      //.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                                      //this.OptionsSplit
                                      SearchFilterHelpers.GetSearchOptionsLabels(src.Options, src.OptionsType/*.GetHashCode()*/, src.MeasureUnit)
                                      .Select(option => new SearchFilterOptionViewModel
                                      {
                                          Value = option,
                                          Checked = false,
                                          SearchFilterId = src.Id,
                                          //OptionsType = src.OptionsType,
                                          //SelectionType = src.SelectionType
                                      })
                                      .ToList()
                                      ));
        }
    }
}