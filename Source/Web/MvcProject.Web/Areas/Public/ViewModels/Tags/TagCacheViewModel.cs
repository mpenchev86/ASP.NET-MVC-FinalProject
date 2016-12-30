namespace MvcProject.Web.Areas.Public.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class TagCacheViewModel : BasePublicViewModel<int>, IMapFrom<Tag>
    {
        //private ICollection<Product> products;

        //public TagCacheViewModel()
        //{
        //    this.products = new HashSet<Product>();
        //}

        public string Name { get; set; }

        //public virtual ICollection<Product> Products
        //{
        //    get { return this.products; }
        //    set { this.products = value; }
        //}
    }
}