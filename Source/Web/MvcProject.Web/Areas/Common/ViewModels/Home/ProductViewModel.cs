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
    using Services.Web;

    [Bind(Include = "Name,Description,Category")]
    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Home.Index), ErrorMessageResourceName = nameof(Resources.Home.Index.RequiredField))]
        public string Name { get; set; }

        [UIHint(GlobalConstants.Templates.CustomStringTemplate)]
        public string Description { get; set; }

        public string Category { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider provider = new IdentifierProvider();
                return $"/Product/{provider.EncodeId(this.Id)}";
            }
        }

        // public IEnumerable<Tag> Tags { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(vm => vm.Category, opt => opt.MapFrom(m => m.Category.Name));
        }
    }
}