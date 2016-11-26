namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryDetailsForKeywordViewModel : BaseAdminViewModel<int>, IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //throw new NotImplementedException();
        }
    }
}