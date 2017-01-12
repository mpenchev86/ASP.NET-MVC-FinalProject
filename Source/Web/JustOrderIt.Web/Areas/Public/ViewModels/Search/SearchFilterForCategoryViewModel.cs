namespace JustOrderIt.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Search;
    using Infrastructure.Mapping;
    using Services.Logic;
    using Services.Logic.ServiceModels;

    public class SearchFilterForCategoryViewModel : BasePublicViewModel<int>, IMapFrom<SearchFilter>, IMapTo<SearchFilterRefinementModel>/*, ISearchFilter*/, IHaveCustomMappings
    {
        //private ISearchFilterHelpers productSearchAlgorithms;
        private List<SearchFilterOptionViewModel> refinementOptions;

        //public SearchFilterForCategoryViewModel()
        //    : this(new SearchFilterHelpers())
        //{
        //}

        public SearchFilterForCategoryViewModel(/*ISearchFilterHelpers productSearchAlgorithms*/)
        {
            //this.productSearchAlgorithms = productSearchAlgorithms;
            this.refinementOptions = new List<SearchFilterOptionViewModel>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterOptionsType OptionsType { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterSelectionType SelectionType { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        public List<string> OptionsSplit
        {
            get
            {
                var values = SearchFilterHelpers.GetSearchOptionsLabels(this.Options, this.OptionsType, this.MeasureUnit);
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
                .ForMember(dest => dest.RefinementOptions, opt => opt.MapFrom(
                            src => SearchFilterHelpers.GetSearchOptionsLabels(src.Options, src.OptionsType, src.MeasureUnit)
                                      .Select(option => new SearchFilterOptionViewModel
                                      {
                                          Value = option,
                                          Checked = false,
                                          SearchFilterId = src.Id,
                                          OptionsType = src.OptionsType,
                                          SelectionType = src.SelectionType
                                      })
                                      .ToList()
                                      ));
        }
    }
}