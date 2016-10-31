namespace MvcProject.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Categories;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class SearchFilterViewModel : BaseAdminViewModel<int>, IMapFrom<SearchFilter>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }

        [Required]
        [Range(1, 3)]
        [UIHint("DropDown")]
        public SearchFilterOptionsType Type { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int CategoryId { get; set; }

        public virtual CategoryDetailsForSearchFilterViewModel Category { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}