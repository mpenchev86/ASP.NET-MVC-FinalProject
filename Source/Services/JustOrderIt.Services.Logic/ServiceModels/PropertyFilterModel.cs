namespace JustOrderIt.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PropertyFilterModel
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int? SearchFilterId { get; set; }
    }
}
