namespace MvcProject.Web.Areas.Admin.ViewModels.Votes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.DataAnnotations;
    using MvcProject.Web.Infrastructure.Mapping;

    public class VoteViewModel : BaseAdminViewModel, IMapFrom<Vote>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public int VoteValue { get; set; }

        [Required]
        [UIHint("GridForeignKey")]
        public int ProductId { get; set; }

        [Required]
        [UIHint("DropDownUserId")]
        public string UserId { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [LongDateTimeFormat]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Vote, VoteViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                ;
        }
    }
}