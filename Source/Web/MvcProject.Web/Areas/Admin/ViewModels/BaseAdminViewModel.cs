namespace MvcProject.Web.Areas.Admin.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Contracts;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;

    public class BaseAdminViewModel<TKey> : IMapFrom<BaseEntityModel<TKey>>
    {
        [Key]
        public TKey Id { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        [LongDateTimeFormat]
        public DateTime? ModifiedOn { get; set; }
    }
}