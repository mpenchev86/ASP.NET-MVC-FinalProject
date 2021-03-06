﻿namespace JustOrderIt.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using JustOrderIt.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Products;
    using ViewModels.Users;
    using ViewModels.Votes;
    using Data.Models.Catalog;

    [Authorize(Roles = IdentityRoles.Admin)]
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

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new VoteViewModelForeignKeys
            {
                Users = this.usersService.GetAll().To<UserDetailsForVoteViewModel>(),
                Products = this.productsService.GetAll().To<ProductDetailsForVoteViewModel>()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, VoteViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
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