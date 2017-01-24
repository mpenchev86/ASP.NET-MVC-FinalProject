namespace JustOrderIt.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Search;

    public class SearchFilterRefinementModel : ISearchFilterRefinementModel
    {
        private List<RefinementOption> refinementOptions;

        public SearchFilterRefinementModel()
        {
            this.refinementOptions = new List<RefinementOption>();
        }
        
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public SearchFilterOptionsType OptionsType { get; set; }

        public string MeasureUnit { get; set; }

        public List<RefinementOption> RefinementOptions
        {
            get { return this.refinementOptions; }
            set { this.refinementOptions = value; }
        }
    }
}
