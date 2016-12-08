namespace MvcProject.Web.Areas.Administration.ViewModels.Keywords
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Categories;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class KeywordViewModel : BaseAdminViewModel<int>, IMapFrom<Keyword>
    {
        private ICollection<CategoryDetailsForKeywordViewModel> categories;

        public KeywordViewModel()
        {
            this.categories = new HashSet<CategoryDetailsForKeywordViewModel>();
        }

        [Required]
        public string SearchTerm { get; set; }

        [UIHint("MultiSelect")]
        public virtual ICollection<CategoryDetailsForKeywordViewModel> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}