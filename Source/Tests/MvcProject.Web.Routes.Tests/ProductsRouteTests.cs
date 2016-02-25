namespace MvcProject.Web.Routes.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Routing;

    using Areas.Common.Controllers;
    using MvcRouteTester;
    using NUnit.Framework;

    [TestFixture]
    public class ProductsRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            var routeCollection = new RouteCollection();
            const string Url = "/Product/M2toaGR3NldEbW4tc2sha2o4bQ==";
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection
                .ShouldMap(Url)
                .To<ProductsController>(ctr => ctr.ById("M2toaGR3NldEbW4tc2sha2o4bQ=="));
            
            // Structure of the above test
            // => ProductsController
            // => ById
            // => Id = M2toaGR3NldEbW4tc2sha2o4bQ==
        }
    }
}
