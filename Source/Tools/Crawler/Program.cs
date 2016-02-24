namespace Crawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AngleSharp;
    using MvcProject.Data.DbAccessConfig;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Services.Data;
    public class Program
    {
        public static void Main()
        {
            var db = new MvcProjectDbContext();
            var repo = new GenericRepository<Product>(db);
            var productsService = new ProductsService(repo);

            var configuration = Configuration.Default;
            var browsingContext = BrowsingContext.New(configuration);

            //for (int i = 1; i <= 10000; i++)
            //{
            //    var url = $"http://vicove.com/vic-{i}";
            //    var document = browsingContext.OpenAsync(url).Result;
            //    var jokeContent = document.QuerySelector("#content_box .post-content").TextContent.Trim();
            //    if (!string.IsNullOrWhiteSpace(jokeContent))
            //    {
            //        var categoryName = document.QuerySelector("#content_box .thecategory a").TextContent.Trim();
            //        var category = categoriesService.EnsureCategory(categoryName);
            //        var joke = new Joke { Category = category, Content = jokeContent };
            //        db.Jokes.Add(joke);
            //        db.SaveChanges();
            //        Console.WriteLine(i);
            //    }
            //}
        }
    }
}
