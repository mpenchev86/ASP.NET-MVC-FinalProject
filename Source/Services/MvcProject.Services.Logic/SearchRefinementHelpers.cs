namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using ServiceModels;
    using Web.Infrastructure.Mapping;

    public static class SearchRefinementHelpers/* : ISearchRefinementHelpers<TProduct, TFilter>*/
    {
        public static IQueryable<Product> FilterProducts<TProduct, TFilter>(this IQueryable<Product> products, IEnumerable<RefinementFilter> filters)
            where TProduct : class, IMapFrom<Product>
            where TFilter : class, IMapFrom<SearchFilter>
        {
            foreach (var product in products)
            {
                if (product.DescriptionId != null)
                {
                    foreach (var property in product.Description.Properties)
                    {

                    }
                }
            }

            return products;
        }
    }
}
