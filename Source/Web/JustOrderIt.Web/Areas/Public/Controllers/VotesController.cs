namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using Services.Web;
    using ViewModels.Votes;

    public class VotesController : BasePublicController
    {
        private readonly IVotesService votesService;
        private readonly IIdentifierProvider identifierProvider;
        private readonly IMappingService mappingService;
        private readonly IProductsService productsService;
        private readonly IUsersService usersService;

        public VotesController(
            IVotesService votesService,
            IIdentifierProvider identifierProvider,
            IMappingService mappingService,
            IProductsService productsService,
            IUsersService usersService)
        {
            this.votesService = votesService;
            this.identifierProvider = identifierProvider;
            this.mappingService = mappingService;
            this.productsService = productsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult EditableRating(VoteEditorModel viewModel)
        {
            var user = this.usersService.GetById(viewModel.UserId);

            // Check if current user has purchased the product or has already voted for it
            if (!user.Orders.SelectMany(o => o.OrderItems).Any(oi => oi.ProductId == viewModel.ProductId)
                //|| user.Votes.Any(v => v.ProductId == viewModel.ProductId)
                )
            {
                return this.PartialView("Rating", viewModel.VoteValue);
            }

            return this.PartialView("CastVote", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult CastVote(/*string encodedId, double rating*/VoteEditorModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                var vote = this.mappingService.Map<Vote>(model);
                this.votesService.Insert(vote);
                model.VoteValue = this.productsService.GetById(model.ProductId).Votes.Average(v => v.VoteValue);
            }

            return this.PartialView(model);
        }
    }
}