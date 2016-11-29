namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class Keyword : BaseEntityModel<int>, IAdministerable
    {
        private ICollection<Category> categories;

        public Keyword()
        {
            this.categories = new HashSet<Category>();
        }

        [Required]
        [StringLength(256)]
        [Index]
        public string SearchTerm { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }
    }
}
