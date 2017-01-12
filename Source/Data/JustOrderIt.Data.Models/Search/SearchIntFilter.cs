namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class SearchIntFilter : BaseSearchFilter
    {
        private ICollection<int> options;

        public SearchIntFilter()
        {
            this.Options = new HashSet<int>();
        }

        [Required]
        public ICollection<int> Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public string OptionsMeasureUnit { get; set; }
    }
}
