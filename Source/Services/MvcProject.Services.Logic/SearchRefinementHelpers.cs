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
        public static IQueryable<Product> FilterProductsByRefinementOptions/*<TProduct, TFilter>*/(this IQueryable<Product> products, IEnumerable<RefinementOption> refinementOptions)
            //where TProduct : class, IMapFrom<Product>
            //where TFilter : class, IMapFrom<SearchFilter>
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

        public static IQueryable<Product> FilterProductsBySearchTerm/*<TProduct, TFilter>*/(this IQueryable<Product> products, string searchTerm)
        {
            return products;
        }

        //public static IEnumerable<Product> FilterProductsBySearchTerm/*<TProduct, TFilter>*/(this List<Product> products, string searchTerm)
        //{
        //    return products;
        //}
    }
}
