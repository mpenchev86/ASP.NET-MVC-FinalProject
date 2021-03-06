﻿namespace JustOrderIt.Web.Areas.Public.Controllers
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
        private readonly IUsersService usersService;

        public CommentsController(
            ICommentsService commentsService,
            IUsersService usersService)
        {
            this.commentsService = commentsService;
            this.usersService = usersService;
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CommentPostViewModel commentPostViewModel)
        {
            if (commentPostViewModel != null && ModelState.IsValid)
            {
                var userId = this.usersService.GetByUserName(this.User.Identity.Name).Id;
                commentPostViewModel.CreatedOn = DateTime.Now;

                var newComment = new Comment();
                this.PopulateEntity(newComment, commentPostViewModel);
                newComment.UserId = userId;
                this.commentsService.Insert(newComment);

                var voteEntity = this.usersService.GetByUserName(commentPostViewModel.UserName).Votes.FirstOrDefault(v => v.ProductId == commentPostViewModel.ProductId);
                int? rating = voteEntity != null ? (int?)voteEntity.VoteValue : null;

                var commentWithRating = new CommentWithRatingViewModel();
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

        private void PopulateViewModel(CommentWithRatingViewModel commentWithRating, CommentPostViewModel commentPostViewModel)
        {
            commentWithRating.CommentContent = commentPostViewModel.Content;
            commentWithRating.UserName = commentPostViewModel.UserName;
            commentWithRating.CommentCreatedOn = commentPostViewModel.CreatedOn;
        }
    }
}