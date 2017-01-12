namespace MvcProject.Web.Areas.Public.ViewModel.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Mapping;

    public class StatisticsDetailsForIndexListViewProduct : BasePublicViewModel<int>, IMapFrom<Statistics>, IHaveCustomMappings
    {
        [Range(ValidationConstants.StatisticsAllTimesItemsSoldMin, ValidationConstants.StatisticsAllTimesItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int OverAllRating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Statistics, StatisticsDetailsForIndexListViewProduct>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}