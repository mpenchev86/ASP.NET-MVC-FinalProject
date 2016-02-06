namespace MvcProject.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexSampleProductViewModel : IMapFrom<SampleProduct>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}