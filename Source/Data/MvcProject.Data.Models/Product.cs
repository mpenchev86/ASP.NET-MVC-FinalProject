namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;

    public class Product : BaseEntityModel<int>
    {
        public Product()
        {
            this.Tags = new HashSet<Tag>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}
