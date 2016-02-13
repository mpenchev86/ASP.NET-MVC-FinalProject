namespace MvcProject.Web.Areas.Common.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexSampleProductViewModel : IMapFrom<SampleProduct>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        // public IEnumerable<Tag> Tags { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<SampleProduct, IndexSampleProductViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Name));
        }
    }
}