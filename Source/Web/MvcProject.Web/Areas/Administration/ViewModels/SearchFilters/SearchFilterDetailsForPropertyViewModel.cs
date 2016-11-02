namespace MvcProject.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Categories;
    using Data.Models;
    using Infrastructure.Mapping;

    public class SearchFilterDetailsForPropertyViewModel : BaseAdminViewModel<int>, IMapFrom<SearchFilter>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }

        [Required]
        [Range(1, 2)]
        [UIHint("DropDown")]
        public SearchFilterOptionsType OptionsType { get; set; }

        [Required]
        [Range(1, 2)]
        [UIHint("DropDown")]
        public SearchFilterSelectionType SelectionType { get; set; }

        public string KendoGridFieldText
        {
            get { return this.Name + "; " + "Category: " + this.Category.Name + "; " + "Id: " + this.Id.ToString(); }
        }

        [Required]
        [UIHint("DropDown")]
        public int CategoryId { get; set; }

        public virtual CategoryDetailsForSearchFilterViewModel Category { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}