namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Comments;
    using Areas.Public.ViewModels.Products;
    using Data.Models.Catalog;
    using Data.Models.Identity;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class CommentsControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<ICommentsService> commentsService;
        private Mock<IUsersService> usersService;

        public CommentsControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void CreateCommentWorksCorrectlyWhenModelAndModelStateAreValid()
        {
            // Arrange
            this.commentsService.Setup(x => x.Insert(It.IsAny<Comment>()));
            var user = this.GetUserInstance();
            this.usersService.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(user);
            //var mockRequest = new Mock<HttpRequestBase>();
            var controllerContext = new Mock<ControllerContext>();
            this.SetupControllerContext(controllerContext, user.UserName);

            var commentPostViewModel = new CommentPostViewModel()
            {
                Content = "jhgjkzsgdfjkshdkfhksfhksjfksdfkshd",
                ProductId = 42,
                UserName = "jfsjkhfsd",
            };

            // Act
            var controller = new CommentsController(this.commentsService.Object, this.usersService.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            Assert.IsTrue(controller.ModelState.IsValid);
            controller.WithCallTo(x => x.CreateComment(commentPostViewModel))
                .ShouldRenderPartialView("_ProductCommentWithRating")
                .WithModel<ProductCommentWithRatingViewModel>(vm =>
                {
                    Assert.IsNotNull(vm);
                    Assert.Multiple(() => 
                    {
                        Assert.AreEqual(commentPostViewModel.UserName, vm.UserName);
                        Assert.AreEqual(commentPostViewModel.Content, vm.CommentContent);
                    });
                    Assert.IsInstanceOf<ProductCommentWithRatingViewModel>(vm);
                });
        }

        [Test]
        public void CreateCommentThrowsWhenModelIsNull()
        {
            // Arrange
            this.commentsService.Setup(x => x.Insert(It.IsAny<Comment>()));
            var user = this.GetUserInstance();
            this.usersService.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(user);
            var controllerContext = new Mock<ControllerContext>();
            this.SetupControllerContext(controllerContext, user.UserName);

            // Act
            var controller = new CommentsController(this.commentsService.Object, this.usersService.Object);
            controller.ControllerContext = controllerContext.Object;

            // Assert
            Assert.Throws<HttpException>(() => controller.CreateComment(null));
        }

        [Test]
        public void CreateCommentThrowsWhenModelStateIsInvalid()
        {
            // Arrange
            this.commentsService.Setup(x => x.Insert(It.IsAny<Comment>()));
            var user = this.GetUserInstance();
            this.usersService.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(user);
            var controllerContext = new Mock<ControllerContext>();
            this.SetupControllerContext(controllerContext, user.UserName);

            var commentPostViewModel = new CommentPostViewModel()
            {
                Content = "jhgjkzsgdfjkshdkfhksfhksjfksdfkshd",
                ProductId = 42,
                UserName = "jfsjkhfsd",
            };

            // Act
            var controller = new CommentsController(this.commentsService.Object, this.usersService.Object);
            controller.ControllerContext = controllerContext.Object;
            controller.ModelState.AddModelError(string.Empty, It.IsAny<string>());

            // Assert
            Assert.Throws<HttpException>(() => controller.CreateComment(commentPostViewModel));
        }

        #region Helpers
        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(CommentsController).Assembly);
            this.commentsService = new Mock<ICommentsService>();
            this.usersService = new Mock<IUsersService>();
        }

        private void ArrangeCreateCommentTest()
        {
            
        }

        private void SetupControllerContext(Mock<ControllerContext> controllerContext, string userName)
        {
            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns(userName);
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(p => p.Identity).Returns(identity.Object);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
        }

        private ApplicationUser GetUserInstance()
        {
            return new ApplicationUser()
            {
                Id = "ahkshashjkjsd",
                UserName = "jfsjkhfsd",
                Votes = new List<Vote>
                {
                    new Vote { ProductId = 1, VoteValue = 1, },
                    new Vote { ProductId = 32, VoteValue = 4, },
                    new Vote { ProductId = 7111, VoteValue = 3, },
                },
            };
        }
        #endregion
    }
}
