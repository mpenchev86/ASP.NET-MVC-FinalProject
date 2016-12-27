namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Logic.ServiceModels;

    public class SearchFilterOptionViewModel : IMapTo<RefinementOption>
    {
        [Required]
        [UIHint("SearchOptionValue")]
        public string Value { get; set; }

        [Required]
        public bool Checked { get; set; }

        [Required]
        public int SearchFilterId { get; set; }

        //[Required]
        [Range(1, 2)]
        public SearchFilterSelectionType SelectionType { get; set; }

        //[Required]
        [Range(1, 2)]
        public SearchFilterOptionsType OptionsType { get; set; }
    }
}