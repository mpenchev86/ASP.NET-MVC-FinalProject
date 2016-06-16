namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Common.Controllers;
    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using MvcProject.Services.Data;
    using ViewModels;
    using ViewModels.Comments;
    using ViewModels.Users;
    using ViewModels.Votes;
    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class UsersController : /*BaseController*/BaseGridController<ApplicationUser, UserViewModel, IUsersService, string>
    {
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IVotesService votesService;

        public UsersController(
            IUsersService usersService,
            ICommentsService commentsService,
            IVotesService votesService)
            : base(usersService)
        {
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.votesService = votesService;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        // Implemented through ASP.NET Identity user manager
        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new ApplicationUser { Id = viewModel.Id };
                this.PopulateEntity(entity, viewModel);
                this.usersService.Update(entity);
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        [HttpPost]
        public JsonResult GetCommentsByUserId([DataSourceRequest]DataSourceRequest request, string userId)
        {
            var result = this.usersService
                .GetById(userId)
                .Comments
                .AsQueryable()
                .To<CommentDetailsForUserViewModel>();
            return this.Json(result.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public JsonResult GetVotesByUserId([DataSourceRequest]DataSourceRequest request, string userId)
        {
            var result = this.usersService
                .GetById(userId)
                .Votes
                .AsQueryable()
                .To<VoteDetailsForUserViewModel>();
            return this.Json(result.ToDataSourceResult(request, this.ModelState));
        }

        protected override void PopulateEntity(ApplicationUser entity, UserViewModel viewModel)
        {
            if (viewModel.Comments != null)
            {
                entity.Comments = new List<Comment>();
                foreach (var comment in viewModel.Comments)
                {
                    entity.Comments.Add(this.commentsService.GetById(comment.Id));
                }
            }

            if (viewModel.Votes != null)
            {
                entity.Votes = new List<Vote>();
                foreach (var vote in viewModel.Votes)
                {
                    entity.Votes.Add(this.votesService.GetById(vote.Id));
                }
            }

            entity.UserName = viewModel.UserName;
            entity.MainRole = viewModel.MainRole;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
#endregion
    }
}