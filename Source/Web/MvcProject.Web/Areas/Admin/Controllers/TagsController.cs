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
    using ViewModels.Tags;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class TagsController : BaseGridController<Tag, TagViewModel, ITagsService>
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
            : base(tagsService)
        {
            this.tagsService = tagsService;
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
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            //if (viewModel != null)
            //{
            //    // Destroy record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Destroy(request, viewModel);
        }

        #region ClientDetailsHelpers
        //[HttpPost]
        //public JsonResult ReadByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        //{
        //    var result = this.tagsService
        //        .GetAll()
        //        .Where(t => t.Products.Any(p => p.Id == productId))
        //        .To<TagDetailsForProductViewModel>();
        //    return this.Json(result.ToDataSourceResult(request, this.ModelState));
        //}
        #endregion
    }
}