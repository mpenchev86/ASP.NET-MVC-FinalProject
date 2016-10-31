namespace MvcProject.Web.Areas.Public.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Properties;

    public class DescriptionForProductOfCategoryViewModel : BasePublicViewModel<int>, IMapFrom<Description>, IHaveCustomMappings
    {
        private ICollection<PropertyForProductOfCategoryViewModel> properties;

        public DescriptionForProductOfCategoryViewModel()
        {
            this.properties = new HashSet<PropertyForProductOfCategoryViewModel>();
        }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public ICollection<PropertyForProductOfCategoryViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Description, DescriptionForProductOfCategoryViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                            src => src.Properties.Select(p => new PropertyForProductOfCategoryViewModel
                            {
                                Name = p.Name,
                                Value = p.Value
                            })));
        }
    }
}