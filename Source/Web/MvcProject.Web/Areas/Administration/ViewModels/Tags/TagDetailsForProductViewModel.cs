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
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}