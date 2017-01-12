namespace JustOrderIt.Data.Models.Media
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.GlobalConstants;
    using Contracts;

    public abstract class FileInfo : BaseEntityModel<int>, IAdministerable
    {
        /// <summary>
        /// Gets or sets the original file name. Does not include any path or extension.
        /// </summary>
        /// <value>
        /// The original file name. Does not include any path or extension.
        /// </value>
        [Required]
        [StringLength(ValidationConstants.ImageFullyQaulifiedFileNameMaxLength)]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the image.
        /// </summary>
        /// <value>
        /// The file extension of the image.
        /// </value>
        [Required]
        [StringLength(ValidationConstants.ImageFileExtensionMaxLength)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the path to the image file on the server.
        /// </summary>
        /// <value>
        /// The path to the image file on the server.
        /// </value>
        [DataType(DataType.ImageUrl)]
        [StringLength(ValidationConstants.ImageUrlPathMaxLength)]
        public string UrlPath { get; set; }

        /// <summary>
        /// Gets or sets the content of an image file as byte array.
        /// </summary>
        /// <value>
        /// The content of an image file as byte array.
        /// </value>
        [DataType(DataType.Upload)]
        public byte[] Content { get; set; }
    }
}
