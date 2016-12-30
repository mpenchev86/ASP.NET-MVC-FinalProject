namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;
    using Properties;

    public class DescriptionViewModel : BaseAdminViewModel<int>, IMapFrom<Description>
    {
        private ICollection<PropertyDetailsForDescriptionViewModel> properties;

        public DescriptionViewModel()
        {
            this.properties = new HashSet<PropertyDetailsForDescriptionViewModel>();
        }

        [Required]
        [StringLength(ValidationConstants.DescriptionContentMaxLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public ICollection<PropertyDetailsForDescriptionViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}