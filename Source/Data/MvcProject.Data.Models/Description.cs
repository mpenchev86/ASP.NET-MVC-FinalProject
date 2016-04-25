namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MvcProject.GlobalConstants;
    using EntityContracts;

    public class Description : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Property> properties;

        public Description()
        {
            this.properties = new HashSet<Property>();
        }

        [Required]
        //[MaxLength(GlobalConstants.ValidationConstants.MaxFullDescriptionLength)]
        public string Content { get; set; }

        //public int? ProductId { get; set; }

        //public virtual Product Product { get; set; }

        public virtual ICollection<Property> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}