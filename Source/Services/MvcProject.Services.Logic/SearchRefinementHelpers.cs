namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Data.Models;
    using Data.Models.Search;
    using ServiceModels;
    using Web.Infrastructure.Mapping;

    public static class SearchRefinementHelpers/* : ISearchRefinementHelpers<TProduct, TFilter>*/
    {
        public static /*IQueryable*/IEnumerable<TProduct> FilterProductsByRefinementOptions<TProduct>(
            this /*IQueryable*/IEnumerable<TProduct> products,
            IEnumerable<SearchFilterRefinementModel> searchFilters
            )
            where TProduct : class, IProductFilteringModel
        {
            var result = products;

            foreach (var searchFilter in searchFilters)
            {
                result = result.Where(p => PassesFilter(p, searchFilter));
            }

            return result;
        }

        private static bool PassesFilter<TProduct>(
            TProduct product,
            SearchFilterRefinementModel searchFilter
            )
            where TProduct : class, IProductFilteringModel
        {
            var refinementOptions = searchFilter.RefinementOptions;
            var chechedOptions = refinementOptions.Where(ro => ro.Checked);
            // If all options are checked or all are unchecked, then the product matches.
            if ((chechedOptions.Count() == 0) || (refinementOptions.Count() == chechedOptions.Count()))
            {
                return true;
            }
            else
            {
                var productProperties = typeof(IProductFilteringModel).GetProperties()
                    .Where(pr => !typeof(IEnumerable).IsAssignableFrom(pr.PropertyType) || (pr.PropertyType == typeof(string)))
                    .ToDictionary(pr => pr.Name, pr =>
                    {
                        var val = Convert.ToString(pr.GetValue(product));
                        return val;
                    })
                    ;

                var descriptionProperties = product.DescriptionPropertiesNamesValues;
                var allPropertiesToFilterBy = productProperties.Concat(descriptionProperties);

                foreach (var propertyInfo in allPropertiesToFilterBy)
                {
                    if (FilterAppliesToProperty(searchFilter.Name.ToLower(), propertyInfo.Key.ToLower()))
                    {
                        if (searchFilter.OptionsType == SearchFilterOptionsType.ConcreteValue)
                        {
                            foreach (var checkedOption in chechedOptions)
                            {
                                var optionValueToLower = checkedOption.Value.ToLower();
                                var propertyValueToLower = propertyInfo.Value.ToLower();
                                if (string.Equals(optionValueToLower, propertyValueToLower)
                                    //|| optionValueToLower.Contains(propertyValueToLower)
                                    //|| propertyValueToLower.Contains(optionValueToLower)
                                    )
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var checkedOption in chechedOptions)
                            {
                                decimal? lowerBound;
                                decimal? upperBound;
                                ExtractBoundaries(checkedOption.Value, out lowerBound, out upperBound);
                                decimal propertyValue;
                                var decimalPartOfPropertyValue = Regex.Split(propertyInfo.Value, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "")/*.ToList()*/.FirstOrDefault();
                                decimal.TryParse(decimalPartOfPropertyValue, out propertyValue);
                                if ((lowerBound == null && propertyValue <= upperBound) ||
                                    (lowerBound <= propertyValue && propertyValue <= upperBound) ||
                                    (lowerBound <= propertyValue && upperBound == null))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void ExtractBoundaries(string value, out decimal? lowerBound, out decimal? upperBound)
        {
            var valueSubstrings = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower());
            var wordBoundary = valueSubstrings.FirstOrDefault(x => x == "under" || x == "above");
            var boundaries = Regex.Split(value, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "").ToList();

            if (boundaries.Count() < 1 || boundaries.Count() > 2)
            {
                throw new ApplicationException("Boundaries not extracted correctly from the range refinement option value.");
            }

            if (boundaries.Count() == 1)
            {
                if (wordBoundary == "under")
                {
                    decimal temp;
                    decimal.TryParse(boundaries[0], out temp);
                    lowerBound = null;
                    upperBound = temp;
                    return;
                }
                else
                {
                    decimal temp;
                    decimal.TryParse(boundaries[0], out temp);
                    lowerBound = temp;
                    upperBound = null;
                    return;
                }
            }

            if (boundaries.Count() == 2)
            {
                decimal tempLowerBound;
                decimal.TryParse(boundaries[0], out tempLowerBound);
                lowerBound = tempLowerBound;
                decimal tempUpperBound;
                decimal.TryParse(boundaries[1], out tempUpperBound);
                upperBound = tempUpperBound;
                return;
            }

            lowerBound = null;
            upperBound = null;
        }

        private static bool FilterAppliesToProperty(string filterName, string propertyName)
        {
            //var filterNameNoWhiteSpace = filterName.SkipWhile(ch => char.IsWhiteSpace(ch) || char.IsSeparator(ch));
            var filterNameNoWhiteSpace = new string(filterName.SkipWhile(ch => char.IsWhiteSpace(ch) || char.IsSeparator(ch)).ToArray());

            //var builder = new StringBuilder();
            //builder.Append(filterNameNoWhiteSpace);

            //var propertyNameNoWhiteSpace = propertyName.SkipWhile(ch => char.IsWhiteSpace(ch) || char.IsSeparator(ch)).ToString();
            var propertyNameNoWhiteSpace = new string(propertyName.SkipWhile(ch => char.IsWhiteSpace(ch) || char.IsSeparator(ch)).ToArray());

            if (string.Equals(filterNameNoWhiteSpace, propertyNameNoWhiteSpace) ||
                filterNameNoWhiteSpace.Contains(propertyNameNoWhiteSpace) ||
                propertyNameNoWhiteSpace.Contains(filterNameNoWhiteSpace))
            {
                return true;
            }

            return false;
        }

        //private static bool CoversRequirement<TProduct>(TProduct product, RefinementOption refinementOption)
        //    where TProduct : class, IProductFilteringModel
        //{
        //    return true;
        //}

        public static /*IQueryable*/IEnumerable<TProduct> FilterProductsBySearchQuery<TProduct/*, TFilter*/>(this /*IQueryable*/IEnumerable<TProduct> products, string searchQuery)
            where TProduct : class, IProductFilteringModel
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return products;
            }

            var result = products;
            result = result.Where(p => IsAssociatedWithSearchQuery(p, searchQuery));

            return result;
        }

        private static bool IsAssociatedWithSearchQuery<TProduct>(TProduct product, string searchQuery)
            where TProduct : class, IProductFilteringModel
        {
            var queryTerms = searchQuery.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryTerm in queryTerms)
            {
                if (!MatchesTerm(product, queryTerm.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool MatchesTerm<TProduct>(TProduct product, string queryTerm) where TProduct : class, IProductFilteringModel
        {
            bool result = false;
            if (product.Title.ToLower().Contains(queryTerm))
            {
                result = true;
            }

            if (product.ShortDescription.ToLower().Contains(queryTerm))
            {
                result = true;
            }

            if (product.DescriptionContent.ToLower().Contains(queryTerm))
            {
                result = true;
            }

            if (product.SellerName.ToLower().Contains(queryTerm))
            {
                result = true;
            }

            if (product.TagNames.Select(t => t.ToLower()).Contains(queryTerm))
            {
                result = true;
            }

            if (product.DescriptionPropertiesNamesValues.Any(pr => pr.Value.ToLower().Contains(queryTerm)))
            {
                result = true;
            }

            return result;
        }
    }
}
