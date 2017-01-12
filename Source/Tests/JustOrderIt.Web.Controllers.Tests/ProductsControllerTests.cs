namespace MvcProject.Web.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Areas.Common.ViewModels.Home;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class ProductsControllerTests : ProductsControllerTestsBase
    {
        [Test]
        public void ByIdShouldRetrieveCorrectProductData()
        {
            this.Controller
                .WithCallTo(x => x.ById("someId"))
                .ShouldRenderView("ById")
                .WithModel<ProductViewModel>(
                    vm =>
                    {
                        Assert.AreEqual(this.ProductDescription, vm.Description);
                    })
                .AndNoModelErrors();
        }
    }
}
