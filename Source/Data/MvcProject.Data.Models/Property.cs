namespace MvcProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MvcProject.Data.Models.EntityContracts;

    public class Property : BaseEntityModel<int>, IAdministerable
    {
        [Required]
        public string Name { get; set; }

        public string Value { get; set; }

        //[Required]
        public int DescriptionId { get; set; }

        [InverseProperty("Properties")]
        public virtual Description Description { get; set; }
    }
}