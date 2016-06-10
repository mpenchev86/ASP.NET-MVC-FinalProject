namespace MvcProject.Web.Areas.Admin.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Descriptions;

    public class PropertyViewModelForeignKeys
    {
        public IEnumerable<DescriptionDetailsForPropertyViewModel> Descriptions { get; set; }

        //public IEnumerable<int> DescriptionIds { get; set; }
    }
}