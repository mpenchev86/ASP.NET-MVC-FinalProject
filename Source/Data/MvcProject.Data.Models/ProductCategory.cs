namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;

    public class ProductCategory : BaseEntityModel<int>
    {
        public ProductCategory()
        {
            this.Products = new HashSet<SampleProduct>();
        }

        public string Name { get; set; }

        public virtual ICollection<SampleProduct> Products { get; set; }
    }
}
