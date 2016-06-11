namespace MvcProject.Web.Areas.Admin.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Data.Models.EntityContracts;
    using Infrastructure.Mapping;
    using Products;

    public class TagDetailsForProductViewModel : BaseAdminViewModel, IMapFrom<Tag>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagDetailsForProductViewModel>()
                //.InheritMappingFromBaseType(WithBaseFor.Destination)
                //.IncludeBase<Tag, BaseAdminViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .IncludeBase<BaseEntityModel<int>, BaseAdminViewModel>()
                ;

            //configuration.CreateMap<Tag, BaseAdminViewModel<Tag>>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Mapper.Map<Tag, TagDetailsForProductViewModel>(src)))
            //    .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => Mapper.Map<Tag, TagDetailsForProductViewModel>(src)))
            //    .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => Mapper.Map<Tag, TagDetailsForProductViewModel>(src)))
            //    ;

            //base.CreateMappings(configuration);
        }
    }
}