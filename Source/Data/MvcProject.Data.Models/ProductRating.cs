namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EntityContracts;

    public class ProductRating : BaseEntityModel<int>
    {
        public int ProductId { get; set; }

        public int TotalRating { get; set; }

        public int VotesCount { get; set; }

        public int AverageRating { get; set; }
    }
}
