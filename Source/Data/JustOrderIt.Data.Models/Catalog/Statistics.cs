namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using GlobalConstants;

    /// <summary>
    /// Represents the entity for statistics of a product.
    /// </summary>
    public class Statistics : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the overall number of bought product units.
        /// </summary>
        /// <value>
        /// The overall number of bought product units.
        /// </value>
        [Range(ValidationConstants.StatisticsAllTimesItemsSoldMin, ValidationConstants.StatisticsAllTimesItemsSoldMax)]
        public int AllTimeItemsSold { get; set; }

        /// <summary>
        /// Gets or sets the overall rating of a product.
        /// </summary>
        /// <value>
        /// The overall rating of a product.
        /// </value>
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int OverAllRating { get; set; }
    }
}
