namespace JustOrderIt.Web.Areas.Public.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Common.GlobalConstants;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class VoteEditorModel : BasePublicViewModel<int>, IMapFrom<Vote>, IMapTo<Vote>, IHaveCustomMappings
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        [UIHint("Rating")]
        public double VoteValue { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<VoteEditorModel, Vote>()
                .ForMember(dest => dest.VoteValue, opt => opt.MapFrom(src => Convert.ToInt32(src.VoteValue)));
        }
    }
}