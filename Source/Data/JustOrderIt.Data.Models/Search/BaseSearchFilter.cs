namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public abstract class BaseSearchFilter : BaseEntityModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public SearchFilterOptionsType Type { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [InverseProperty("SearchFilters")]
        public virtual Category Category { get; set; }
    }
}
