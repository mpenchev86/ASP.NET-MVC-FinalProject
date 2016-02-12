namespace MvcProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EntityContracts;

    public class SampleProduct : BaseEntityModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
