namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Search;
    using Infrastructure.Mapping;

    public class SearchFilterCacheViewModel : BasePublicViewModel<int>, IMapFrom<SearchFilter>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public SearchFilterOptionsType OptionsType { get; set; }

        public SearchFilterSelectionType SelectionType { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Options { get; set; }

        public string MeasureUnit { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SearchFilter, SearchFilterCacheViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}