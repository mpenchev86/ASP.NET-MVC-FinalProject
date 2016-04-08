namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcProject.Data.Models.EntityContracts;

    public class Property : BaseEntityModel<string>
    {
        [Required]
        public string Name { get; set; }

        public string Value { get; set; }

        public int DescriptionId { get; set; }

        [InverseProperty("Properties")]
        public virtual Description Description { get; set; }
    }
}