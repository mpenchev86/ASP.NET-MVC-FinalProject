﻿namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EntityContracts;
    using MvcProject.GlobalConstants;

    public class Vote : BaseEntityModel<int>, IAdministerable
    {
        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
