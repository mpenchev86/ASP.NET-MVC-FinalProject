namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Search;
    using Web.Infrastructure.Mapping;

    public class SearchFilterHelpers : ISearchFilterHelpers
    {
        public static List<string> SplitOptionsString(string optionsString)
        {
            var optionsSplit = optionsString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return optionsSplit;
        }

        public static List<string> GetSearchOptionsLabels(string options, SearchFilterOptionsType optionsType, string measureUnit)
        {
            var splitOptions = SplitOptionsString(options);
            var splitOptionsWithLabels = GetOptionsWithMeasureUnit(splitOptions, measureUnit);

            if (optionsType == SearchFilterOptionsType.ValueRange)
            {
                var optionLabels = new List<string>();
                optionLabels.Add("Under " + splitOptionsWithLabels.FirstOrDefault());
                for (int i = 0; i < splitOptionsWithLabels.Count() - 1; i++)
                {
                    optionLabels.Add(splitOptionsWithLabels[i] + " to " + splitOptionsWithLabels[i + 1]);
                }

                optionLabels.Add(splitOptionsWithLabels.LastOrDefault() + " & Above");
                return optionLabels;
            }

            return splitOptionsWithLabels;
        }

        public static List<string> GetOptionsWithMeasureUnit(List<string> options, string measureUnit)
        {
            if (string.IsNullOrWhiteSpace(measureUnit))
            {
                return options;
            }
            else if (measureUnit == "$")
            {
                var result = new List<string>();
                for (int i = 0; i < options.Count(); i++)
                {
                    result.Add(measureUnit + options[i]);
                }

                return result;
            }
            else
            {
                var result = new List<string>();
                for (int i = 0; i < options.Count(); i++)
                {
                    result.Add(options[i] /*+ " "*/ + measureUnit);
                }

                return result;
            }
        }
    }
}
