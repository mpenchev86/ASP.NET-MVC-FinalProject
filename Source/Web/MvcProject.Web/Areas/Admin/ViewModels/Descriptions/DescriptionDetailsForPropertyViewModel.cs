namespace MvcProject.Web.Areas.Admin.ViewModels.Descriptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class DescriptionDetailsForPropertyViewModel : BaseAdminViewModel, IMapFrom<Description>, IHaveCustomMappings
    {
        //[Key]
        //public int Id { get; set; }

        //[Required]
        ////[MaxLength(GlobalConstants.ValidationConstants.MaxFullDescriptionLength)]
        //[DataType(DataType.MultilineText)]
        //public string Content { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //throw new NotImplementedException();
        }
    }
}