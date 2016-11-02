namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public interface ISearchFilterHelpers
    {
        List<string> SplitOptionsString(string optionsString);

        List<string> GetSearchOptionsLabels(string options, SearchFilterOptionsType optionsType, string measureUnit);

        List<string> GetOptionsWithMeasureUnit(List<string> options, string measureUnit);
    }
}
