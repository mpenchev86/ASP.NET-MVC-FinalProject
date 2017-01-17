namespace JustOrderIt.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Models.Catalog;
    using Infrastructure.Mapping;
    using Infrastructure.Validators;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels.Comments;
    using ViewModels.Products;

    public class CommentsController : BasePublicController
    {
        private readonly ICommentsService commentsService;
        //private IMappingService mappingService;
        private readonly IUsersService usersService;

        public CommentsController(
            ICommentsService commentsService,
            //IMappingService mappingService,
            IUsersService usersService)
        {
            this.commentsService = commentsService;
            //this.mappingService = mappingService;
            this.usersService = usersService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CommentPostViewModel commentPostViewModel)
        {
            if (commentPostViewModel != null && ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                commentPostViewModel.CreatedOn = DateTime.Now;

                var newComment = new Comment();
                this.PopulateEntity(newComment, commentPostViewModel);
                newComment.UserId = userId;
                this.commentsService.Insert(newComment);

                var voteEntity = this.usersService.GetByUserName(commentPostViewModel.UserName).Votes.FirstOrDefault(v => v.ProductId == commentPostViewModel.ProductId);
                int? rating = voteEntity != null ? (int?)voteEntity.VoteValue : null;

                var commentWithRating = new ProductCommentWithRatingViewModel();
                this.PopulateViewModel(commentWithRating, commentPostViewModel);
                commentWithRating.Rating = rating;
                this.ViewData["rating-id"] = newComment.Id;

                return this.PartialView("_ProductCommentWithRating", commentWithRating);
            }

            throw new HttpException(400, "Invalid comment");
        }

        private void PopulateEntity(Comment newComment, CommentPostViewModel commentPostViewModel)
        {
            newComment.Content = commentPostViewModel.Content;
            newComment.ProductId = commentPostViewModel.ProductId;
            newComment.CreatedOn = commentPostViewModel.CreatedOn;
            newComment.ModifiedOn = commentPostViewModel.ModifiedOn;
        }

        private void PopulateViewModel(ProductCommentWithRatingViewModel commentWithRating, CommentPostViewModel commentPostViewModel)
        {
            commentWithRating.CommentContent = commentPostViewModel.Content;
            commentWithRating.UserName = commentPostViewModel.UserName;
            commentWithRating.CommentCreatedOn = commentPostViewModel.CreatedOn;
        }
    }
}