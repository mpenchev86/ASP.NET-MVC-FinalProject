namespace MvcProject.Web.Areas.Administration.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteDetailsForUserViewModel : BaseAdminViewModel<int>, IMapFrom<Vote>, IHaveCustomMappings
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteDetailsForUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}