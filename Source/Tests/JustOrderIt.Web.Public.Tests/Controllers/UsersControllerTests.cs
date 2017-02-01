namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Orders;
    using Areas.Public.ViewModels.Users;
    using Data.Models.Identity;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class UsersControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IUsersService> usersServiceMock;
        private Mock<IMappingService> mappingServiceMock;

        public UsersControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void UserHomePageShouldReturnDefaultView()
        {
            // Act
            var controller = new UsersController(this.usersServiceMock.Object, this.mappingServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.UserHomePage())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void OrderHistoryShouldReturnDefaultViewWithViewModel()
        {
            // Arrange
            var user = new ApplicationUser() { UserName = "nsjknf", };
            var principalMock = this.GetPrincipalMock(user.UserName);
            this.usersServiceMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(user);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);

            // Act
            var controller = new UsersController(this.usersServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.OrderHistory())
                .ShouldRenderDefaultView()
                .WithModel<OrderHistoryViewModel>();
        }

        public void PublicUserProfileShouldReturnDefaultViewWithViewModel()
        {
            // Arrange
            var user = new ApplicationUser() { UserName = "nsjknf", };
            var principalMock = this.GetPrincipalMock(user.UserName);
            this.usersServiceMock.Setup(x => x.GetByUserName(It.IsAny<string>())).Returns(user);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext.User).Returns(principalMock.Object);

            // Act
            var controller = new UsersController(this.usersServiceMock.Object, this.mappingServiceMock.Object);
            controller.ControllerContext = controllerContextMock.Object;

            // Assert
            controller.WithCallTo(x => x.PublicUserProfile(It.IsAny<string>()))
                .ShouldRenderDefaultView()
                .WithModel<PublicUserProfile>();
        }

        #region Helpers
        public void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(typeof(UsersController).Assembly);
            this.usersServiceMock = new Mock<IUsersService>();
            this.mappingServiceMock = new Mock<IMappingService>();
        }

        private Mock<IPrincipal> GetPrincipalMock(string userName)
        {
            var principalMock = new Mock<IPrincipal>();
            var identityMock = new Mock<IIdentity>();
            identityMock.Setup(x => x.Name).Returns(userName);
            principalMock.SetupGet(p => p.Identity).Returns(identityMock.Object);
            return principalMock;
        }
        #endregion
    }
}
