namespace JustOrderIt.Web.Areas.Public.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class CategoryForLayoutDropDown : BasePublicViewModel<int>, IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}