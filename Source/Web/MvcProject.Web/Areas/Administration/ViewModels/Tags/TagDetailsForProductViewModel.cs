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
            //configuration.CreateMap<Tag, TagDetailsForProductViewModel>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            //    .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn))
            //    ;
        }
    }
}