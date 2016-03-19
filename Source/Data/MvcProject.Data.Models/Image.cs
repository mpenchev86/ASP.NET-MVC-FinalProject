namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;
    using MvcProject.Data.Models.EntityContracts;

    public class Image : BaseEntityModel<int>
    {
        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}