namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using ViewModels.Tags;
    using Data.Models.Catalog;

    [Authorize(Roles = IdentityRoles.Admin)]
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

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Tag entity, TagViewModel viewModel)
        {
            var tagProductsIds = viewModel.Products.Select(t => t.Id);
            // Resolves conflict caused by the one-to-one relationship.
            entity.Products.Clear();
            entity.Products = this.productsService.GetAll().Where(p => tagProductsIds.Contains(p.Id)).ToList();

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        [HttpGet]
        protected override IQueryable<TagViewModel> GetQueryableData()
        {
            return this.tagsService.GetAll().To<TagViewModel>().OrderBy(t => t.Name);
        }
        #endregion
    }
}