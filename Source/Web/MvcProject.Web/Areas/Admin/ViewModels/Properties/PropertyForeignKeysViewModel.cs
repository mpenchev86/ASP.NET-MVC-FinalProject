namespace MvcProject.Web.Areas.Admin.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Descriptions;

    public class PropertyForeignKeysViewModel
    {
        public IEnumerable<DescriptionDetailsForPropertyViewModel> Descriptions { get; set; }
    }
}