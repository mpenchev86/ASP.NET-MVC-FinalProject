namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;

    public class ProductSearchAlgorithms : IProductSearchAlgorithms
    {
        public List<string> SplitOptionsString(string optionsString)
        {
            var optionsSplit = optionsString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return optionsSplit;
        }

        public List<string> GetSearchOptionsLabels(string options, SearchFilterOptionsType type, string measureUnit)
        {
            var splitOptions = this.SplitOptionsString(options);
            var splitOptionsWithLabels = this.GetOptionsWithMeasureUnit(splitOptions, measureUnit);

            //switch (type)
            //{
            //    case SearchFilterOptionsType.RadioButton:
            //    case SearchFilterOptionsType.MultiSelect:
            //        return splitOptionsWithLabels;
            //    case SearchFilterOptionsType.Range:
            //        var optionLabels = new List<string>();
            //        optionLabels.Add("Under " + splitOptionsWithLabels.FirstOrDefault());
            //        for (int i = 0; i < splitOptionsWithLabels.Count() - 1; i++ )
            //        {
            //            optionLabels.Add(splitOptionsWithLabels[i] + " to "+ splitOptionsWithLabels[i + 1]);
            //        }

            //        optionLabels.Add(splitOptionsWithLabels.LastOrDefault() + " & Above");
            //        return optionLabels;
            //    default:
            //        break;
            //}

            if (type == SearchFilterOptionsType.Range)
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

        public List<string> GetOptionsWithMeasureUnit(List<string> options, string measureUnit)
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
                    result.Add(options[i] + " " + measureUnit);
                }

                return result;
            }
        }
    }
}
