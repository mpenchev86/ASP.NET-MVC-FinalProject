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
    using Web.Public;

    public class VotesController : BasePublicController
    {
        private readonly IVotesService votesService;
        private readonly IMappingService mappingService;
        private readonly IUsersService usersService;

        public VotesController(
            IVotesService votesService,
            IMappingService mappingService,
            IUsersService usersService)
        {
            this.votesService = votesService;
            this.mappingService = mappingService;
            this.usersService = usersService;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DisplayProductRating(VoteEditorModel viewModel)
        {
            if (viewModel != null && !string.IsNullOrWhiteSpace(viewModel.UserId))
            {
                var user = this.usersService.GetById(viewModel.UserId);
                // Check if current user has purchased the product to be allowed to rate it.
                if (user != null && user.Orders.SelectMany(o => o.OrderItems).Any(oi => oi.ProductId == viewModel.ProductId))
                {
                    return this.PartialView("VoteForProduct", viewModel);
                }
            }

            return this.PartialView("RatingDisplay", viewModel.VoteValue);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult VoteForProduct(VoteEditorModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
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

                return this.PartialView(model);
            }

            throw new HttpException(400, "Invalid vote editor model");
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult VoteForOrderItem([ModelBinder(typeof(VoteEditorModelBinder))]VoteEditorModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
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

                return this.PartialView(model);
            }

            throw new HttpException(400, "Invalid vote editor model");
        }
    }
}