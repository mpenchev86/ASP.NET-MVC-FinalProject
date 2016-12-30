namespace MvcProject.Web.Areas.Administration.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;
    using Products;

    public class TagViewModel : BaseAdminViewModel<int>, IMapFrom<Tag>
    {
        private ICollection<ProductDetailsForTagViewModel> products;

        public TagViewModel()
        {
            this.products = new HashSet<ProductDetailsForTagViewModel>();
        }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public ICollection<ProductDetailsForTagViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}