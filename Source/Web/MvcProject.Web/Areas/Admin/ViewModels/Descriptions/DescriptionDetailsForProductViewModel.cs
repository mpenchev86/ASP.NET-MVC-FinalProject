namespace MvcProject.Web.Areas.Admin.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Properties;

    public class DescriptionDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Description>, IHaveCustomMappings
    {
        private ICollection<PropertyDetailsForDescriptionViewModel> properties;

        public DescriptionDetailsForProductViewModel()
        {
            this.properties = new HashSet<PropertyDetailsForDescriptionViewModel>();
        }

        //[Key]
        //public int Id { get; set; }

        [Required]
        //[MaxLength(GlobalConstants.ValidationConstants.MaxFullDescriptionLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [UIHint("PropertiesMultiSelectEditor")]
        public ICollection<PropertyDetailsForDescriptionViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Description, DescriptionDetailsForProductViewModel>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(
                           src => src.Properties.Select(p => new PropertyDetailsForDescriptionViewModel
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Value = p.Value,
                               CreatedOn = p.CreatedOn,
                               ModifiedOn = p.ModifiedOn
                           })))
                ;
        }
    }
}