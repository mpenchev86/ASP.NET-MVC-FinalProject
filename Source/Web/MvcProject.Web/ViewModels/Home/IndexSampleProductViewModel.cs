namespace MvcProject.Web.ViewModels.Home
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class IndexSampleProductViewModel : IMapFrom<SampleProduct>
    {
        public string Name { get; set; }
    }
}