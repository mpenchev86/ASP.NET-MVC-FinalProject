namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class File : FileInfo
    {
        /// <summary>
        /// Gets or sets the foreign key pointing to the product.
        /// </summary>
        /// <value>
        /// The foreign key pointing to the product.
        /// </value>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product to which the file belongs.
        /// </summary>
        /// <value>
        /// The product to which the file belongs.
        /// </value>
        public virtual Product Product { get; set; }

        [NotMapped]
        public byte[] Content { get; set; }
    }
}
