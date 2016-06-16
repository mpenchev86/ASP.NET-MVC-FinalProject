namespace MvcProject.Web.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Data.Models.EntityContracts;
    using Infrastructure.DataAnnotations;
    using Infrastructure.Mapping;
    using Tags;

    public class BaseAdminViewModel<TKey> : IMapFrom<BaseEntityModel<int>>/*, IHaveCustomMappings*/
    {
        [Key]
        public TKey Id { get; set; }

        [LongDateTimeFormat]
        public DateTime CreatedOn { get; set; }

        [LongDateTimeFormat]
        public DateTime? ModifiedOn { get; set; }

        //public void CreateMappings(IMapperConfiguration configuration)
        //{
        //    configuration.CreateMap<BaseEntityModel<int>, BaseAdminViewModel<TKey>>()
        //        //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //        //.ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
        //        //.ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn))
        //        ;
        //}
    }
}