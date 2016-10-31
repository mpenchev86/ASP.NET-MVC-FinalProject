namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;

    public class SearchDoubleFilter : BaseSearchFilter
    {
        private ICollection<double> options;

        public SearchDoubleFilter()
        {
            this.Options = new HashSet<double>();
        }

        [Required]
        public ICollection<double> Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public string OptionsMeasureUnit { get; set; }
    }
}
