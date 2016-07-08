namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using GlobalConstants;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Statistics;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class StatisticsController : BaseGridController<Statistics, StatisticsViewModel, IStatisticsService, int>
    {
        private readonly IStatisticsService statisticsService;
        private readonly IProductsService productsService;

        public StatisticsController(IStatisticsService statisticsService, IProductsService productsService)
            : base(statisticsService)
        {
            this.statisticsService = statisticsService;
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, StatisticsViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, StatisticsViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, StatisticsViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        protected override void PopulateEntity(Statistics entity, StatisticsViewModel viewModel)
        {
            entity.AllTimeItemsBought = viewModel.AllTimeItemsBought;
            entity.OverAllRating = viewModel.OverAllRating;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
#endregion
    }
}