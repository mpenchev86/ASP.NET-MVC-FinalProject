namespace MvcProject.Web.Areas.Administration.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class TagDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Tag>
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }
    }
}