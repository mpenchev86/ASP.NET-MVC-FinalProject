﻿namespace JustOrderIt.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Categories;
    using Data.Models;
    using Data.Models.Search;
    using Infrastructure.Mapping;

    public class SearchFilterDetailsForPropertyViewModel : BaseAdminViewModel<int>, IMapFrom<SearchFilter>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

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
    }
}