namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductDetailsForDescriptionViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
    }
}