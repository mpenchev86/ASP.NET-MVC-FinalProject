namespace JustOrderIt.Web.Areas.Public.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.Models;
    using Properties;
    using Infrastructure.Mapping;
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Catalog;

    public class DescriptionForProductFullViewModel : BasePublicViewModel<int>, IMapFrom<Description>, IHaveCustomMappings
    {
        private ICollection<PropertyForProductFullViewModel> properties;

        public DescriptionForProductFullViewModel()
        {
            this.properties = new HashSet<PropertyForProductFullViewModel>();
        }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public ICollection<PropertyForProductFullViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Description, DescriptionForProductFullViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                            src => src.Properties.Select(p => new PropertyForProductFullViewModel
                            {
                                Name = p.Name,
                                Value = p.Value
                            })));
        }
    }
}