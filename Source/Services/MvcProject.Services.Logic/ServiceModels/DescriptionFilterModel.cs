namespace MvcProject.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DescriptionFilterModel
    {
        private ICollection<PropertyFilterModel> properties;

        public DescriptionFilterModel()
        {
            this.properties = new HashSet<PropertyFilterModel>();
        }

        public string Content { get; set; }

        public ICollection<PropertyFilterModel> Properties
        {
            get { return this.properties; }
            set { this.properties = value; }
        }
    }
}
