namespace MvcProject.Web.Areas.Administration.ViewModels.Descriptions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class DescriptionDetailsForPropertyViewModel : BaseAdminViewModel<int>, IMapFrom<Description>
    {
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}