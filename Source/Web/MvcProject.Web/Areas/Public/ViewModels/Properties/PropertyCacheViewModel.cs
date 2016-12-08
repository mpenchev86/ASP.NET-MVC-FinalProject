namespace MvcProject.Web.Areas.Public.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Infrastructure.Mapping;
    using Search;

    public class PropertyCacheViewModel : BasePublicViewModel<int>, IMapFrom<Property>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int DescriptionId { get; set; }

        //public virtual Description Description { get; set; }

        public int? SearchFilterId { get; set; }

        public SearchFilterCacheViewModel SearchFilter { get; set; }
    }
}