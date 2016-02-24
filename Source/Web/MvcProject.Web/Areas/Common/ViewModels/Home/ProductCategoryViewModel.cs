namespace MvcProject.Web.Areas.Common.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductCategoryViewModel : IMapFrom<ProductCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}