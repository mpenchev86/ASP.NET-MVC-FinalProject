namespace MvcProject.Services.Data
{
    using System.Linq;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public interface IProductsService : IDeletableEntitiesBaseService<Product, int>
    {
    }
}