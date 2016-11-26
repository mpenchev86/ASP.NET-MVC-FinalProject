namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common.GlobalConstants;
    using Data.Models;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Keywords;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class KeywordsController : BaseGridController<Keyword, KeywordViewModel, IKeywordsService, int>
    {
        private readonly IKeywordsService keywordsService;
        private readonly ICategoriesService categoriesService;

        public KeywordsController(
            IKeywordsService keywordsService,
            ICategoriesService categoriesService)
            : base(keywordsService)
        {
            this.keywordsService = keywordsService;
            this.categoriesService = categoriesService;
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, KeywordViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, KeywordViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, KeywordViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Keyword entity, KeywordViewModel viewModel)
        {
            var keywordCategoriessIds = viewModel.Categories.Select(t => t.Id);
            // Resolves conflict caused by the one-to-one relationship.
            entity.Categories.Clear();
            entity.Categories = this.categoriesService.GetAll().Where(c => keywordCategoriessIds.Contains(c.Id)).ToList();

            entity.SearchTerm = viewModel.SearchTerm;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            var categories = this.categoriesService.GetAll().To<CategoryDetailsForKeywordViewModel>();
            return this.Json(categories, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<KeywordViewModel> GetQueryableData()
        {
            return this.keywordsService.GetAll().To<KeywordViewModel>().OrderBy(x => x.SearchTerm);
        }
        #endregion
    }
}