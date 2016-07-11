namespace MvcProject.Web.Areas.Administration.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteDetailsForProductViewModel : BaseAdminViewModel<int>, IMapFrom<Vote>, IHaveCustomMappings
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteDetailsForProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}