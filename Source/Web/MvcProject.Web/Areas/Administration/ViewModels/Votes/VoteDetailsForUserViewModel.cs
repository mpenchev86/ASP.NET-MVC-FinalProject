namespace MvcProject.Web.Areas.Administration.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Catalog;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteDetailsForUserViewModel : BaseAdminViewModel<int>, IMapFrom<Vote>
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }
    }
}