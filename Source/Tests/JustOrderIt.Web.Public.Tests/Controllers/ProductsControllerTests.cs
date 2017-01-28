namespace JustOrderIt.Web.Public.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Areas.Public.Controllers;
    using Areas.Public.ViewModels.Comments;
    using Areas.Public.ViewModels.Products;
    using Data.Models.Catalog;
    using Data.Models.Identity;
    using Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data;
    using Services.Web;
    using TestStack.FluentMVCTesting;
    [TestFixture]
    public class ProductsControllerTests
    {
        private AutoMapperConfig autoMapperConfig;
        private Mock<IProductsService> productsService;
        private Mock<IVotesService> votesService;
        private Mock<IIdentifierProvider> identifierProvider;
        private Mock<IMappingService> mappingService;

        public ProductsControllerTests()
        {
            this.PrepareController();
        }

        [Test]
        public void IndexShouldWorkCorrectly()
        {
            // Arrange
            var productId = 43;
            var product = /*this.GetProduct()*/new Product();
            this.identifierProvider.Setup(x => x.DecodeToIntId(It.IsAny<string>())).Returns(productId);
            this.productsService.Setup(x => x.GetById(It.IsAny<int>())).Returns(product);
            var viewModel = new ProductFullViewModel() /*this.GetProductViewModel()*/ /*this.mappingService.Object.Map<ProductFullViewModel>(product)*/;
            this.mappingService.Setup(x => x.Map<Product, ProductFullViewModel>(It.IsAny<Product>())).Returns(/*new ProductFullViewModel()*/viewModel);

            // Act
            var controller = new ProductsController(this.productsService.Object, this.identifierProvider.Object, this.votesService.Object, this.mappingService.Object);

            // Assert
            controller.WithCallTo(x => x.Index("jkfgksdf"))
                .ShouldRenderDefaultView()
                .WithModel<ProductFullViewModel>(
                vm =>
                {
                    Assert.IsNotNull(vm);
                    Assert.AreEqual(vm, viewModel);
                }
                );
        }

        private Product GetProduct()
        {
            return new Product()
            {
                Id = 43,
                
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 2,
                        CreatedOn = DateTime.Now,
                        Content = "ioqtrwytejhdgavshdklioqwuuehd",
                        User = new ApplicationUser() { UserName = "gvjshdgkjsd" }
                    },
                },
                Votes = new List<Vote>()
                {
                    new Vote()
                    {
                        VoteValue = 3,
                        User = new ApplicationUser() { UserName = "qtyqrwtvdb" }
                    },
                },
            };
        }

        #region Helpers
        private void PrepareController()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(ProductsController).Assembly);
            this.productsService = new Mock<IProductsService>();
            this.votesService = new Mock<IVotesService>();
            this.identifierProvider = new Mock<IIdentifierProvider>();
            this.mappingService = new Mock<IMappingService>();
        }

        public ProductFullViewModel GetProductViewModel()
        {
            return new ProductFullViewModel()
            {
                CommentsWithRatings = new List<CommentWithRatingViewModel>()
                {
                    new CommentWithRatingViewModel() { Id = 1, CommentCreatedOn = DateTime.Now, CommentContent = "jkkuiqhjvcjhbmqkwhkjkukgausu", UserName = "bcvqiboqq", Rating = 2 },
                    new CommentWithRatingViewModel() { Id = 3, CommentCreatedOn = DateTime.Now, CommentContent = "jkkuiqhjvcjhbmqkwhkjkukgausu", UserName = "bcvqiboqq", Rating = 4},
                    new CommentWithRatingViewModel() { Id = 2, CommentCreatedOn = DateTime.Now, CommentContent = "jkkuiqhjvcjhbmqkwhkjkukgausu", UserName = "bcvqiboqq", },
                    new CommentWithRatingViewModel() { Id = 5, CommentCreatedOn = DateTime.Now, CommentContent = "jkkuiqhjvcjhbmqkwhkjkukgausu", UserName = "bcvqiboqq", },
                },
            };
        }
        #endregion
    }
}
