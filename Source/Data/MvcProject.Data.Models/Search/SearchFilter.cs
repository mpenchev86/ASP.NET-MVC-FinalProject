namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using Contracts;

    public class SearchFilter : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the search filter name.
        /// </summary>
        /// <value>
        /// The search filter name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the search filter type specifiying the refinement procedure.
        /// </summary>
        /// <value>
        /// The search filter type specifiying the refinement procedure.
        /// </value>
        [Required]
        [Range(1, 3)]
        public SearchFilterOptionsType Type { get; set; }

        /// <summary>
        /// Gets or sets the search filter category entity Id.
        /// </summary>
        /// <value>
        /// The search filter category entity Id.
        /// </value>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the search filter category entity.
        /// </summary>
        /// <value>
        /// The search filter category entity.
        /// </value>
        [InverseProperty("SearchFilters")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the search filter options that could be selected by the user.
        /// </summary>
        /// <value>
        /// The search filter options that could be selected by the user.
        /// </value>
        [Required]
        [DataType(DataType.MultilineText)]
        public string Options { get; set; }

        /// <summary>
        /// Gets or sets the measure unit for the options values, if any.
        /// </summary>
        /// <value>
        /// The measure unit for the options values, if any.
        /// </value>
        [DataType(DataType.Text)]
        public string MeasureUnit { get; set; }
    }
}
