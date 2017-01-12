namespace JustOrderIt.Web.Areas.Administration.ViewModels.Votes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Web.Infrastructure.Mapping;

    public class VoteViewModel : BaseAdminViewModel<int>, IMapFrom<Vote>
    {
        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int ProductId { get; set; }

        [Required]
        [UIHint("DropDown")]
        public string UserId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}