namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;
    using Properties;

    public class DescriptionDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Description>
    {
        private ICollection<PropertyDetailsForDescriptionViewModel> properties;

        public DescriptionDetailsForProductViewModel()
        {
            this.properties = new HashSet<PropertyDetailsForDescriptionViewModel>();
        }

        [Required]
        [StringLength(ValidationConstants.DescriptionContentMaxLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [UIHint("PropertiesMultiSelectEditor")]
        public ICollection<PropertyDetailsForDescriptionViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}