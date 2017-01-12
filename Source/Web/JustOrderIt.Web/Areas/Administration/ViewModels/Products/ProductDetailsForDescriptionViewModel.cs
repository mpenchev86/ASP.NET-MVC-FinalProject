namespace JustOrderIt.Web.Areas.Administration.ViewModels.Products
{
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class ProductDetailsForDescriptionViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
    }
}