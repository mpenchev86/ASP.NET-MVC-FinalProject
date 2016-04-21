namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.GlobalConstants;

    public class Comment : BaseEntityModel<int>, IAdministerable
    {
        [Required]
        [MinLength(GlobalConstants.ValidationConstants.MinProductCommentLength)]
        [MaxLength(GlobalConstants.ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}