namespace MvcProject.Web.Areas.Public.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Infrastructure.Mapping;
    using Properties;

    public class DescriptionCacheViewModel : BasePublicViewModel<int>, IMapFrom<Description>
    {
        private ICollection<PropertyCacheViewModel> properties;

        public DescriptionCacheViewModel()
        {
            this.properties = new HashSet<PropertyCacheViewModel>();
        }

        public string Content { get; set; }

        public ICollection<PropertyCacheViewModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}