namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using ViewModels.Tags;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class TagsController : BaseGridController<Tag, TagViewModel, ITagsService, int>
    {
        private readonly ITagsService tagsService;
        private readonly IProductsService productsService;

        public TagsController(ITagsService tagsService, IProductsService productsService)
            : base(tagsService)
        {
            this.tagsService = tagsService;
            this.productsService = productsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        protected override void PopulateEntity(Tag entity, TagViewModel viewModel)
        {
            if (viewModel.Products != null)
            {
                foreach (var product in viewModel.Products)
                {
                    entity.Products.Add(this.productsService.GetById(product.Id));
                }
            }

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
#endregion
    }
}