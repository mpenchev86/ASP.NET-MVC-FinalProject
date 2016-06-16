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
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using ViewModels.Users;
    using ViewModels.Votes;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class VotesController : BaseGridController<Vote, VoteViewModel, IVotesService, int>
    {
        private readonly IVotesService votesService;
        private readonly IProductsService productsService;
        private readonly IUsersService usersService;

        public VotesController(
            IVotesService votesService,
            IProductsService productsService,
            IUsersService usersService)
            : base(votesService)
        {
            this.votesService = votesService;
            this.productsService = productsService;
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            var foreignKeys = new VoteViewModelForeignKeys
            {
                Users = this.usersService.GetAll().To<UserDetailsForVoteViewModel>().ToList(),
                Products = this.productsService.GetAll().To<ProductDetailsForVoteViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Vote { };
                this.PopulateEntity(entity, viewModel);
                this.votesService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Vote { Id = viewModel.Id };
                this.PopulateEntity(entity, viewModel);
                this.votesService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        protected override void PopulateEntity(Vote entity, VoteViewModel viewModel)
        {
            entity.VoteValue = viewModel.VoteValue;
            entity.ProductId = viewModel.ProductId;
            entity.UserId = viewModel.UserId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
#endregion
    }
}