namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;

    public class Tag : BaseEntityModel<string>
    {
        private ICollection<Product> products;

        public Tag()
        {
            this.products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
