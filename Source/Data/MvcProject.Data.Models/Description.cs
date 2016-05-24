namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EntityContracts;
    using MvcProject.GlobalConstants;

    public class Description : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Property> properties;

        public Description()
        {
            this.properties = new HashSet<Property>();
        }

        [Required]
        //[MaxLength(GlobalConstants.ValidationConstants.MaxFullDescriptionLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public virtual ICollection<Property> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}