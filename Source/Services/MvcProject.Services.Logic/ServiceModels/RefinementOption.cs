namespace MvcProject.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;

    public class RefinementOption
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public bool Checked { get; set; }

        [Required]
        public int SearchFilterId { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterSelectionType SelectionType { get; set; }

        [Required]
        [Range(1, 2)]
        public SearchFilterOptionsType OptionsType { get; set; }
    }
}
