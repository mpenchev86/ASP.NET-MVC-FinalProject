﻿namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using EntityContracts;

    public class Description : BaseEntityModel<int>
    {
        private ICollection<Property> properties;

        public Description()
        {
            this.properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(ValidationConstants.MaxFullDescriptionLength)]
        public string Summary { get; set; }

        public virtual ICollection<Property> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}