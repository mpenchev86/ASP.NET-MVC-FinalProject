namespace MvcProject.Web.Areas.Common.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Mapping;

    [Bind(Include = "Name,Description,Category")]
    public class IndexProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [UIHint(GlobalConstants.Templates.CustomStringTemplate)]
        public string Description { get; set; }

        public string Category { get; set; }

        // public IEnumerable<Tag> Tags { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, IndexProductViewModel>()
                .ForMember(vm => vm.Category, opt => opt.MapFrom(e => e.Category.Name));
        }
    }
}