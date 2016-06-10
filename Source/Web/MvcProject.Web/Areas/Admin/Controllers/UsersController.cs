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

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            //var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var model = this.usersService.GetAll().To<UserViewModel>().ToList();
            return this.View(model);
        }

        public ActionResult GetUser(string id)
        {
            var model = this.Mapper.Map<UserViewModel>(this.usersService.GetById(id));
            model.MainRole = this.usersService.GetUserRoles(model.UserId).FirstOrDefault();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var viewModel = this.usersService.GetAll().ToList();
            var dataSourceResult = viewModel.ToDataSourceResult(request, this.ModelState);
            return this.Json(dataSourceResult, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public override ActionResult Create([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        //{
        //    if (viewModel != null && this.ModelState.IsValid)
        //    {
        //    }
        //
        //    return base.Create(request, viewModel);
        //}

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new ApplicationUser { Id = viewModel.UserId };
                this.MapEntity(entity, viewModel);
                this.usersService.Update(entity);
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                this.usersService.DeleteUser(viewModel.UserId);
            }

            return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public void MapEntity(ApplicationUser entity, UserViewModel viewModel)
        {
            entity.UserName = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        //[HttpPost]
        //public ActionResult EditUser(UserPostModel model)
        //{
        //    return null;
        //}

        //[HttpPost]
        //public ActionResult DeleteUser(UserPostModel model)
        //{
        //    return null;
        //}

        #region DataProviders
        public virtual IEnumerable<UserViewModel> GetDataAsEnumerable()
        {
            return this.usersService.GetAll().To<UserViewModel>();
        }

        public virtual JsonResult GetDataAsJson()
        {
            return this.Json(this.GetDataAsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        //public virtual JsonResult GetEntityAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, T data, ModelStateDictionary modelState)
        //{
        //    return this.Json(new[] { data }.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        //}

        //public virtual JsonResult GetCollectionAsDataSourceResult<T>([DataSourceRequest]DataSourceRequest request, IEnumerable<T> data, ModelStateDictionary modelState)
        //{
        //    return this.Json(data.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        //}

        //public virtual JsonResult GetAsSelectList(string text, string value)
        //{
        //    var data = this.GetDataAsEnumerable().Select(x => new SelectListItem { Text = text, Value = value });
        //    return this.Json(data, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region UserDetailsHelpers
        [HttpPost]
        public JsonResult GetCommentsByUserId([DataSourceRequest]DataSourceRequest request, string userId)
        {
            var result = this.usersService
                .GetById(userId)
                .Comments
                .AsQueryable()
                .To<CommentDetailsForProductViewModel>();
            return this.Json(result.ToDataSourceResult(request, this.ModelState));
        }
        #endregion
    }
}