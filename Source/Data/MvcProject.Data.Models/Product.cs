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
        private ICollection<Tag> tags;
        private ICollection<Image> images;

        public Product()
        {
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual Category Category { get; set; }

        public virtual ProductRating Rating { get; set; }
    }
}
