namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Votes;
    using Data.Models.Identity;
    using Data.Models.Orders;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class VotesControllerTests
    {
        private Mock<IVotesService> votesServiceMock;
        private Mock<IMappingService> mappingServiceMock;
        private Mock<IUsersService> usersServiceMock;

        public VotesControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void DisplayProductRatingShouldReturnRatingDisplayPartialIfViewModelIsNullOrUserIdIsNullOrWhiteSpace()
        {
            // Arrange
            var requestModel = new VoteEditorModel()
            {
                VoteValue = 3.2,
                //ProductId = 3123,
                //UserId = Guid.NewGuid().ToString()
            };

            // Act
            var controller = new VotesController(this.votesServiceMock.Object, this.mappingServiceMock.Object, this.usersServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.DisplayProductRating(requestModel))
                .ShouldRenderPartialView("RatingDisplay");
        }

        [Test]
        public void DisplayProductRatingShouldReturnVoteForProductPartialIfUserIsNotNullAndHasPurchasedRequestModelProduct()
        {
            // Arrange
            var requestModel = new VoteEditorModel()
            {
                VoteValue = 3.2,
                ProductId = 3123,
                UserId = Guid.NewGuid().ToString()
            };

            var user = new ApplicationUser()
            {
                Id = requestModel.UserId,
                Orders = new List<Order>
                {
                    new Order { OrderItems = new List<OrderItem> { new OrderItem { ProductId = requestModel.ProductId } } },
                },
            };

            this.usersServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(user);

            // Act
            var controller = new VotesController(this.votesServiceMock.Object, this.mappingServiceMock.Object, this.usersServiceMock.Object);

            // Assert
            controller.WithCallTo(x => x.DisplayProductRating(requestModel))
                .ShouldRenderPartialView("VoteForProduct")
                .WithModel<VoteEditorModel>();
        }

        #region Helpers
        public void PrepareController()
        {
            this.votesServiceMock = new Mock<IVotesService>();
            this.mappingServiceMock = new Mock<IMappingService>();
            this.usersServiceMock = new Mock<IUsersService>();
        }
        #endregion
    }
}
