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
    using MvcProject.GlobalConstants;

    public class Tag : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Product> products;

        public Tag()
        {
            this.products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
