namespace MvcProject.Web.Areas.Administration.ViewModels.Keywords
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class KeywordDetailsForCategoryViewModel : BaseAdminViewModel<int>, IMapFrom<Keyword>, IHaveCustomMappings
    {
        [Required]
        public string SearchTerm { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //throw new NotImplementedException();
        }
    }
}