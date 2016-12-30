namespace MvcProject.Web.Areas.Public.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class PropertyForProductFullViewModel : BasePublicViewModel<int>, IMapFrom<Property>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int? SearchFilterId { get; set; }
    }
}