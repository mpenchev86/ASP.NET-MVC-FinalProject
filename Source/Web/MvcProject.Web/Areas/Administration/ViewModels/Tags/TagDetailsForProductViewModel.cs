namespace MvcProject.Web.Areas.Administration.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MvcProject.Common.GlobalConstants;

    public class TagDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Tag>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}