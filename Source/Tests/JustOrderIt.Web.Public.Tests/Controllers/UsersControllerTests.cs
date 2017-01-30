namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Areas.Public.Controllers;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class UsersControllerTests
    {
        //private AutoMapperConfig autoMapperConfig;
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

        #region Helpers
        public void PrepareController()
        {
            //this.autoMapperConfig = new AutoMapperConfig();
            //this.autoMapperConfig.Execute(typeof(UsersController).Assembly);
            this.usersServiceMock = new Mock<IUsersService>();
            this.mappingServiceMock = new Mock<IMappingService>();
        }
        #endregion
    }
}
