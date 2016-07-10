namespace MvcProject.Web.Areas.Admin.ViewModels.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class StatisticsDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Statistics>, IHaveCustomMappings
    {
        /// <summary>
        /// Gets or sets the overall number of bought product units.
        /// </summary>
        /// <value>
        /// The overall number of bought product units.
        /// </value>
        [Range(ValidationConstants.StatisticsAllTimesItemsSoldMin, ValidationConstants.StatisticsAllTimesItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        /// <summary>
        /// Gets or sets the overall rating of a product.
        /// </summary>
        /// <value>
        /// The overall rating of a product.
        /// </value>
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int OverAllRating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Statistics, StatisticsDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}