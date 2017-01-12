namespace JustOrderIt.Web.Areas.Administration.ViewModels.Properties
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.GlobalConstants;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class PropertyViewModel : BaseAdminViewModel<int>, IMapFrom<Property>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: ValidationConstants.PropertyNameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: ValidationConstants.PropertyValueMaxLength)]
        public string Value { get; set; }

        [UIHint("DropDown")]
        public int DescriptionId { get; set; }
        
        [UIHint("DropDown")]
        public int? SearchFilterId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }
    }
}