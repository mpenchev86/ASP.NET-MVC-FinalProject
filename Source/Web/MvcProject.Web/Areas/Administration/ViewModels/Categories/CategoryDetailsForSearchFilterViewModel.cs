using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using MvcProject.Common.GlobalConstants;
using MvcProject.Data.Models;
using MvcProject.Web.Infrastructure.Mapping;

namespace MvcProject.Web.Areas.Administration.ViewModels.Categories
{
    public class CategoryDetailsForSearchFilterViewModel : BaseAdminViewModel<int>, IMapFrom<Category>, IHaveCustomMappings
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.CategoryNameMaxLenght)]
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryDetailsForSearchFilterViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}