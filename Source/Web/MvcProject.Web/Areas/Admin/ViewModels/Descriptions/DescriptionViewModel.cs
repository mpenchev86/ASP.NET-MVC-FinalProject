namespace MvcProject.Web.Areas.Admin.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class DescriptionViewModel : IMapFrom<Description>, IHaveCustomMappings
    {
        private ICollection<Property> properties;

        public DescriptionViewModel()
        {
            this.properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        //[MaxLength(ValidationConstants.MaxFullDescriptionLength)]
        public string Content { get; set; }

        public virtual ICollection<Property> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //throw new NotImplementedException();
        }
    }
}