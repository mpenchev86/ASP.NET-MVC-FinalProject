﻿namespace JustOrderIt.Web.Areas.Public.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Search;

    public class PropertyCacheViewModel : BasePublicViewModel<int>, IMapFrom<Property>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int DescriptionId { get; set; }

        public int? SearchFilterId { get; set; }

        public SearchFilterCacheViewModel SearchFilter { get; set; }
    }
}