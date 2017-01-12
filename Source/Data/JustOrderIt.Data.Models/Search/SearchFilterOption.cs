namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class SearchFilterOption : BaseEntityModel<int>, IAdministerable
    {
        public string Value { get; set; }

        public int SearchFilterId { get; set; }

        [InverseProperty("SearchFilterOptions")]
        public virtual SearchFilter SearchFilter { get; set; }
    }
}
