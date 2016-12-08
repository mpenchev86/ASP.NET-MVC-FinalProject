namespace MvcProject.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ProductDetailsForVoteViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
    }
}