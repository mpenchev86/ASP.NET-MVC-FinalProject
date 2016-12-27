namespace MvcProject.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class SearchFilterDetailsForCategoryViewModel : BaseAdminViewModel<int>, IMapFrom<SearchFilter>
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

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}