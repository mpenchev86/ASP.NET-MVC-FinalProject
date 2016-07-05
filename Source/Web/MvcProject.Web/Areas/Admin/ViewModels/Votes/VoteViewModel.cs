﻿namespace MvcProject.Web.Areas.Admin.ViewModels.Votes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using MvcProject.GlobalConstants;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteViewModel : BaseAdminViewModel<int>, IMapFrom<Vote>, IHaveCustomMappings
    {
        [Required]
        [Range(ValidationConstants.VoteValueMin, ValidationConstants.VoteValueMax)]
        public int VoteValue { get; set; }

        [Required]
        [UIHint("DropDown")]
        public int ProductId { get; set; }

        [Required]
        [UIHint("DropDown")]
        public string UserId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}