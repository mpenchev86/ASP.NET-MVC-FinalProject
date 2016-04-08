namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MvcProject.Data.Common.Constants;
    using MvcProject.Data.Models.EntityContracts;

    public class Comment : BaseEntityModel<string>
    {
        [Required]
        [MinLength(Common.Constants.ValidationConstants.MinProductCommentLength)]
        [MaxLength(Common.Constants.ValidationConstants.MaxProductCommentLength)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}