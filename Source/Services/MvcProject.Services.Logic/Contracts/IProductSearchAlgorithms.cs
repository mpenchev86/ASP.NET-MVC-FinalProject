namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;

    public interface IProductSearchAlgorithms
    {
        List<string> SplitOptionsString(string optionsString);

        List<string> GetSearchOptionsLabels(string options, SearchFilterOptionsType type, string measureUnit);

        List<string> GetOptionsWithMeasureUnit(List<string> options, string measureUnit);
    }
}
