namespace MvcProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcProject.Data.Common.Constants;
    using MvcProject.Data.Models.EntityContracts;

    public class Image : BaseEntityModel<int>, IAdministerable
    {
        [Required]
        [MaxLength(Common.Constants.ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(Common.Constants.ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}