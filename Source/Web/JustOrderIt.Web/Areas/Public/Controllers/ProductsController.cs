namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Models.Catalog;
    using Infragistics.Web.Mvc;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using Services.Web;
    using ViewModels.Products;

    public class ProductsController : BasePublicController
    {
        private readonly IProductsService productsService;
        private readonly IVotesService votesService;
        private readonly IIdentifierProvider identifierProvider;
        private readonly IMappingService mappingService;

        public ProductsController(
            IProductsService productsService,
            IIdentifierProvider identifierProvider,
            IVotesService votesService,
            IMappingService mappingService)
        {
            this.productsService = productsService;
            this.votesService = votesService;
            this.identifierProvider = identifierProvider;
            this.mappingService = mappingService;
        }
        
        [HttpGet]
        public ActionResult Index(string id)
        {
            var decodedId = this.identifierProvider.DecodeToIntId(id);
            var product = this.productsService.GetById(/*id*/(int)decodedId);
            var viewModel = this.mappingService.Map<Product, ProductFullViewModel>(product);
            viewModel.CommentsWithRatings = this.PopulateCommentAndVote(product.Comments, product.Votes);
            // Prevents populating tags with system.data.entity.dynamicproxies...(eager loading)
            viewModel.Tags = product.Tags.Select(t => t.Name).ToList();
            return this.View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult SneakPeek(string id)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action is accessible only through AJAX calls" + this.Request.AppRelativeCurrentExecutionFilePath);
            }

            var product = this.productsService.GetById((int)this.identifierProvider.DecodeToIntId(id));
            if (product == null)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("Product not found");
            }

            var result = this.mappingService.Map<Product, ProductSneakPeekViewModel>(product);
            return this.PartialView(result);
        }

    #region Workers
        private ICollection<ProductCommentWithRatingViewModel> PopulateCommentAndVote(
            ICollection<Comment> comments,
            ICollection<Vote> votes)
        {
            var result = new List<ProductCommentWithRatingViewModel>();

            foreach (var comment in comments)
            {
                var commentAndVote = new ProductCommentWithRatingViewModel();
                commentAndVote.Id = comment.Id;
                commentAndVote.CommentContent = comment.Content;
                commentAndVote.CommentCreatedOn = comment.CreatedOn;
                commentAndVote.UserName = comment.User.UserName;
                
                foreach (var vote in votes)
                {
                    if (commentAndVote.UserName == vote.User.UserName)
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