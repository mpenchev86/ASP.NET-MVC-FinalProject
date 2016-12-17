﻿namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryDetailsForKeywordViewModel : BaseAdminViewModel<int>, IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}