namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcProject.Data.Models.EntityContracts;
    using MvcProject.GlobalConstants;

    public class Image : BaseEntityModel<int>, IAdministerable
    {
        [Required]
        [MaxLength(ValidationConstants.ImageOriginalFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}