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
        private readonly IMappingService mappingService;
        private readonly IProductsService productsService;
        private readonly IUsersService usersService;

        public VotesController(
            IVotesService votesService,
            IMappingService mappingService,
            IProductsService productsService,
            IUsersService usersService)
        {
            this.votesService = votesService;
            this.mappingService = mappingService;
            this.productsService = productsService;
            this.usersService = usersService;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult InteractiveProductRating(VoteEditorModel viewModel)
        {
            var user = this.usersService.GetById(viewModel.UserId);

            // Check if current user has purchased the product to rate it.
            if (!user.Orders.SelectMany(o => o.OrderItems).Any(oi => oi.ProductId == viewModel.ProductId))
            {
                return this.PartialView("RatingDisplay", viewModel.VoteValue);
            }

            return this.PartialView("VoteForProduct", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult VoteForProduct(VoteEditorModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                //var product = this.productsService.GetById(model.ProductId);
                var vote = this.votesService.GetAll().FirstOrDefault(v => v.UserId == model.UserId && v.ProductId == model.ProductId);
                if (vote != null)
                {
                    vote.VoteValue = Convert.ToInt32(model.VoteValue);
                    this.votesService.Update(vote);
                }
                else
                {
                    vote = this.mappingService.Map<Vote>(model);
                    this.votesService.Insert(vote);
                }

                model.VoteValue = this.votesService.GetAll().Where(v => v.ProductId == model.ProductId).Average(v => v.VoteValue);
            }

            return this.PartialView(model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult VoteForOrderItem(VoteEditorModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                //foreach (var vote in model)
                //{
                //}
                    var product = this.productsService.GetById(model.ProductId);
                    var vote = product.Votes.FirstOrDefault(v => v.UserId == model.UserId);
                    if (vote != null)
                    {
                        vote.VoteValue = Convert.ToInt32(model.VoteValue);
                        this.votesService.Update(vote);
                    }
                    else
                    {
                        vote = this.mappingService.Map<Vote>(model);
                        this.votesService.Insert(vote);
                    }

            }

            return this.PartialView(model);
        }
    }
}