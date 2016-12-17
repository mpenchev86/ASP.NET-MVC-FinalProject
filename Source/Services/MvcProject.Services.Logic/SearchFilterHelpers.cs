namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public class SearchFilterHelpers : ISearchFilterHelpers
    {
        public static List<string> SplitOptionsString(string optionsString)
        {
            var optionsSplit = optionsString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return optionsSplit;
        }

        public static List<string> GetSearchOptionsLabels(string options, SearchFilterOptionsType /*int */optionsType, string measureUnit)
        {
            var splitOptions = /*this.*/SplitOptionsString(options);
            var splitOptionsWithLabels = /*this.*/GetOptionsWithMeasureUnit(splitOptions, measureUnit);

            if (optionsType == SearchFilterOptionsType.ValueRange/*.GetHashCode()*/)
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

        public IQueryable<TProduct> FilterProducts<TProduct, TFilter>(IQueryable<TProduct> products, IEnumerable<TFilter> filters)
            where TProduct : class, IMapFrom<Product>
            where TFilter : class, IMapFrom<SearchFilter>
        {
            return products;
        }
    }
}
