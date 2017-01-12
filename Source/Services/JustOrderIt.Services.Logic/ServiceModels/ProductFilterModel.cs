namespace JustOrderIt.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductFilterModel
    {
        private ICollection<TagFilterModel> tags;

        public ProductFilterModel()
        {
            this.tags = new HashSet<TagFilterModel>();
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public int DescriptionId { get; set; }

        public DescriptionFilterModel Description { get; set; }

        public ICollection<TagFilterModel> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public decimal UnitPrice { get; set; }

        public decimal? ShippingPrice { get; set; }

        public double? AllTimeAverageRating { get; /*set; */}
    }
}
