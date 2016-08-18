namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infragistics.Web.Mvc;
    using Infrastructure.Extensions;
    using Infrastructure.Mapping;
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
        private IMappingService mappingService;

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
        public ActionResult Index(int id)
        {
            var product = this.productsService.GetById(id);
            var viewModel = this.mappingService.IMapper.Map<Product, ProductFullViewModel>(product);
            viewModel.CommentsWithRatings = this.PopulateCommentAndVote(viewModel.Comments, viewModel.Votes);
            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult SneakPeek(string id)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action is accessible only through AJAX calls" + this.Request.AppRelativeCurrentExecutionFilePath);
            }

            var product = this.productsService.GetById(this.identifierProvider.DecodeIdToInt(id));
            if (product == null)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("Product not found");
            }

            var result = this.mappingService.IMapper.Map<Product, ProductSneakPeekViewModel>(product);
            return this.PartialView("_SneakPeek", result);
        }

        [HttpGet]
        public ActionResult AllProductsOfCategory(string category)
        {
            var products = this.productsService.GetAll().Where(p => p.Category.Name == category);
            var result = products.To<ProductOfCategoryViewModel>();
            return this.View(result);
        }

        [HttpGet]
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