namespace JustOrderIt.Web.Areas.Administration.ViewModels.Products
{
    using System;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;

    public class ProductDetailsForVoteViewModel : BaseAdminViewModel<int>, IMapFrom<Product>
    {
    }
}