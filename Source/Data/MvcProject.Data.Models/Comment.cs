namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Common;
    using MvcProject.Data.Models.EntityContracts;

    public class Comment : BaseEntityModel<int>
    {
        [Required]
        [MinLength(ValidationConstants.MinProductCommentLength)]
        [MaxLength(ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}