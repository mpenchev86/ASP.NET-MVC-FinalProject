namespace JustOrderIt.Web.Areas.Public.ViewModels.Keywords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Search;
    using JustOrderIt.Web.Infrastructure.Mapping;

    public class KeywordCacheViewModel : BasePublicViewModel<int>, IMapFrom<Keyword>, IHaveCustomMappings
    {
        public string SearchTerm { get; set; }

        public IDictionary<int, string> Categories { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Keyword, KeywordCacheViewModel>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(
                            src => src.Categories.Any() ? src.Categories.ToDictionary(c => c.Id, c => c.Name) : new Dictionary<int, string>()));
        }
    }
}
