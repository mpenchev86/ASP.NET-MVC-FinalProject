namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Infragistics.Web.Mvc;
    using Infrastructure.Extensions;
    using Services.Data;
    using Services.Web;
    using ViewModels.Comments;
    using ViewModels.Products;
    using ViewModels.Votes;

    public class ProductsController : BasePublicController
    {
        private IProductsService productsService;
        private IVotesService votesService;
        private IIdentifierProvider identifierProvider;

        public ProductsController(
            IProductsService productsService,
            IIdentifierProvider identifierProvider,
            IVotesService votesService)
        {
            this.productsService = productsService;
            this.votesService = votesService;
            this.identifierProvider = identifierProvider;
        }

        // GET
        [HttpGet]
        public ActionResult Index(int Id)
        {
            var product = this.productsService.GetById(Id);
            var viewModel = this.Mapper.Map<Product, ProductFullViewModel>(product);
            viewModel.CommentsWithRatings = this.PopulateCommentAndVote(viewModel.Comments, viewModel.Votes);

            return this.View(viewModel);
        }

        // GET
        [HttpGet]
        public PartialViewResult SneakPeak(string id)
        {
            var product = this.productsService.GetById(this.identifierProvider.DecodeIdToInt(id));
            var result = this.Mapper.Map<Product, ProductSneakPeakViewModel>(product);
            return this.PartialView("_SneakPeak", result);
        }

        // GET
        public ActionResult AllProductsOfCategory(string category)
        {
            var products = this.productsService.GetAll().Where(p => p.Category.Name == category);
            var result = products.To<ProductOfCategoryViewModel>();
            return this.View(result);
        }

        // GET
        public ActionResult AllProductsWithTag(string tag)
        {
            var products = this.productsService.GetAll().Where(p => p.Tags.Select(t => t.Name).Contains(tag));
            var result = products.To<ProductWithTagViewModel>();
            return this.View(result);
        }

#region Workers
        private ICollection<ProductCommentWithRatingViewModel> PopulateCommentAndVote(
            ICollection<CommentForProductFullViewModel> comments,
            ICollection<VoteForProductFullViewModel> votes)
        {
            var result = new List<ProductCommentWithRatingViewModel>();

            foreach (var comment in comments)
            {
                var commentAndVote = new ProductCommentWithRatingViewModel();
                commentAndVote.CommentContent = comment.Content;
                commentAndVote.CommentCreatedOn = comment.CreatedOn;
                commentAndVote.UserName = comment.UserName;
                
                foreach (var vote in votes)
                {
                    if (commentAndVote.UserName == vote.UserName)
                    {
                        commentAndVote.Rating = vote.VoteValue;
                        break;
                    }
                }

                result.Add(commentAndVote);
            }

            return result;
        }
#endregion
    }
}