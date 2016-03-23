namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Models.EntityContracts;

    public class Property : BaseEntityModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string Value { get; set; }

        public int DescriptionId { get; set; }

        public virtual Description Description { get; set; }
    }
}