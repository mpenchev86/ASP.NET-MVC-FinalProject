namespace MvcProject.Web.Areas.Administration.ViewModels.SearchFilters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class SearchDoubleFilterViewModel : BaseAdminViewModel<int>, IMapFrom<SearchDoubleFilter>, IHaveCustomMappings
    {
        private ICollection<double> options;

        public SearchDoubleFilterViewModel()
        {
            this.Options = new HashSet<double>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<double> Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public string OptionsMeasureUnit { get; set; }

        [Required]
        [Range(0, 2)]
        public SearchFilterOptionsType Type { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}