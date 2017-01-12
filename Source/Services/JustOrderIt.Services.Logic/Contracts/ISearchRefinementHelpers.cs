namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Web.Infrastructure.Mapping;

    public interface ISearchRefinementHelpers<TProduct, TFilter>
            where TProduct : class, IMapFrom<Product>
            where TFilter : class, IMapFrom<SearchFilter>
    {
        IQueryable<TProduct> FilterProducts(this IQueryable<TProduct> products, IEnumerable<TFilter> filters);
    }
}
