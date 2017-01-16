namespace JustOrderIt.Data.DbAccessConfig.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Contexts;
    using JustOrderIt.Common.GlobalConstants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Catalog;
    using Models.Identity;
    using Models.Media;
    using Models.Orders;
    using Models.Search;
    using Web.Infrastructure.SampleDataGenerators;
    using Web.Infrastructure.SampleDataGenerators.HelperModels;
    using Web.Infrastructure.StringHelpers;

    public sealed class Configuration : DbMigrationsConfiguration<JustOrderItDbContext>
    {
        private readonly ISampleDataGenerator sampleDataGenerator;

        public Configuration()
            : this(new SampleDataGenerator())
        {
        }

        public Configuration(ISampleDataGenerator sampleDataGenerator)
        {
            // TODO: Remove in production
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            this.ContextKey = DbAccess.DbMigrationsConfigurationContextKey;

            this.sampleDataGenerator = sampleDataGenerator;
        }

        protected override void Seed(JustOrderItDbContext context)
        {
            #region Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                    r => r.Name,
                    new ApplicationRole
                    {
                        Name = IdentityRoles.Admin,
                    },
                    new ApplicationRole
                    {
                        Name = IdentityRoles.Customer,
                    },
                    new ApplicationRole
                    {
                        Name = IdentityRoles.Seller,
                    });

                context.SaveChanges();
            }
            #endregion

            #region Users
            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher();
                context.Users.AddOrUpdate(
                    u => u.UserName,
                    new ApplicationUser
                    {
                        Email = "admin@mail.com",
                        UserName = "admin",
                        PasswordHash = hasher.HashPassword("123456"),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                    });

                for (int i = 1; i < 100; i++)
                {
                    context.Users.AddOrUpdate(
                        u => u.Id,
                        new ApplicationUser
                        {
                            Email = "user" + i.ToString() + "@mail.com",
                            UserName = "user" + i.ToString(),
                            PasswordHash = hasher.HashPassword("000000".Substring(0, 6 - i.ToString().Length) + i.ToString()),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            IsDeleted = false,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                        });
                }

                context.SaveChanges();
            }
            #endregion

            #region UserRoles
            if (!context.UserRoles.Any())
            {
                context.UserRoles.AddOrUpdate(
                    r => new { r.UserName, r.RoleName },
                    new ApplicationUserRole
                    {
                        UserId = context.Users.FirstOrDefault(u => u.UserName == "admin").Id,
                        UserName = "admin",
                        RoleId = context.Roles.FirstOrDefault(r => r.Name == IdentityRoles.Admin).Id,
                        RoleName = IdentityRoles.Admin
                    });

                context.SaveChanges();

                var roles = new Dictionary<int, ApplicationRole>();
                var roleIndex = 0;
                foreach (var role in context.Roles.Where(r => r.Name != IdentityRoles.Admin))
                {
                    roles.Add(roleIndex, role);
                    roleIndex++;
                }

                var users = new Dictionary<string, string>();
                var userEntities = context.Users.Where(u => u.UserName != "admin");
                foreach (var user in userEntities)
                {
                    users.Add(user.Id, user.UserName);
                }

                foreach (var user in users)
                {
                    var random = new Random();
                    var roleKey = random.Next(roles.Count);
                    context.UserRoles.AddOrUpdate(
                        r => r.UserId,
                        new ApplicationUserRole
                        {
                            UserId = user.Key,
                            UserName = user.Value,
                            RoleId = roles[roleKey].Id,
                            RoleName = roles[roleKey].Name
                        });
                }

                context.SaveChanges();

            }
            #endregion

            #region Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(
                    c => c.Name,
                    new Category { Name = "Appliances" },
                    new Category { Name = "Books" },
                    new Category { Name = "Cameras" },
                    new Category { Name = "Furniture" },
                    new Category { Name = "Health & Beauty" },
                    new Category { Name = "Notebooks" },
                    new Category { Name = "Sports Equipment" });

                context.SaveChanges();
            }
            #endregion

            #region Keywords
            if (!context.Keywords.Any())
            {
                //var categories = new Dictionary<int, string>();
                //foreach (var item in context.Categories)
                //{
                //    categories.Add(item.Id, item.Name);
                //}

                context.Keywords.AddOrUpdate(
                    k => k.Id,
                #region Appliances
                    new Keyword { SearchTerm = "appliance", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "air conditioner", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "refrigerator", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "fridge", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "dishwasher", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "fan", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "freezer", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "iron", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "heater", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "dryer", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "vacuum cleaner", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "mixer", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "toaster", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "oven", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "microwave", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "food processor", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "blender", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "coffee machine", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "juicer", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "pan", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "knife", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "cup", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "mug", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "pot", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    new Keyword { SearchTerm = "kettle", Categories = new HashSet<Category>(context.Categories.Where(c => c.Name == "Appliances")) },
                    #endregion
                #region Books
                    new Keyword { SearchTerm = "SAMPLE" },
                #endregion
                    #region Cameras
                    new Keyword { SearchTerm = "SAMPLE" },
                #endregion
                #region Furniture
                    new Keyword { SearchTerm = "SAMPLE" },
                #endregion
                #region Health & Beauty
                    new Keyword { SearchTerm = "SAMPLE" },
                #endregion
                #region Notebooks
                    new Keyword { SearchTerm = "SAMPLE" },
                #endregion
                #region Sports Equipment
                    new Keyword { SearchTerm = "SAMPLE" }
                #endregion
                    );

                context.SaveChanges();
            }
            #endregion

            #region SearchFilters
            if (!context.SearchFilters.Any())
            {
                context.SearchFilters.AddOrUpdate(
                    c => c.Id,
                #region Appliances
                    new SearchFilter
                    {
                        Name = "Motor Power",
                        DisplayName = "Motor Power",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        Options = "200, 300, 400, 600, 800, 1000",
                        MeasureUnit = "Watts",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Color",
                        DisplayName = "Color",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        Options = "black, grey, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        DisplayName = "Condition",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Books
                    new SearchFilter
                    {
                        Name = "Format",
                        DisplayName = "Format",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Options = "Paperback, Hardcover, Kindle Edition, Large Print, Audible Audio Edition, Audio CD",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Language",
                        DisplayName = "Language",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Options = "English, German, French, Spanish, Italian, Japanese",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        DisplayName = "Condition",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Options = "New, Used, Collectible",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Cameras
                    new SearchFilter
                    {
                        Name = "Resolution",
                        DisplayName = "Resolution",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "12, 24, 36",
                        MeasureUnit = "MP",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Optical Zoom",
                        DisplayName = "Optical Zoom",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "4, 10, 20, 50",
                        MeasureUnit = "x",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Maximum ISO",
                        DisplayName = "Maximum ISO",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "800, 1600, 3200, 6400, 12800, 25600, 51200, 102400, 204800",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        DisplayName = "Condition",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Furniture
                    new SearchFilter
                    {
                        Name = "Material",
                        DisplayName = "Material",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Options = "Wood, Fabric, Leather, Metal, Rattan, Glass, Plastic",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Color",
                        DisplayName = "Color",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Options = "black, grey, gray, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        DisplayName = "Condition",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Health & Beauty
                    new SearchFilter
                    {
                        Name = "Sex/Gender",
                        DisplayName = "Sex/Gender",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Health & Beauty").Id,
                        Options = "For Her, For Him, Unisex",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Specific properties",
                        DisplayName = "Specific properties",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Health & Beauty").Id,
                        Options = "Natural, Cruelty Free, Organic, Hypoallergenic, Unscented, Paraben Free, Alcohol Free, Ammonia Free, Oil Free",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Health & Beauty").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Health & Beauty").Id,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                #endregion
                #region Notebooks
                    new SearchFilter
                    {
                        Name = "Display Size",
                        DisplayName = "Display Size",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options = "11, 12, 13, 14, 15, 16, 17",
                        MeasureUnit = "inches",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "RAM",
                        DisplayName = "RAM",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options = "2, 3, 4, 6, 8, 12, 16, 24, 32, 64",
                        MeasureUnit = "GB",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Processor",
                        DisplayName = "Processor",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options =
                            "Intel Core i7, " +
                            "Intel Core i5, " +
                            "Intel Core i3, " +
                            "Intel Core m7, " +
                            "Intel Core m5, " +
                            "Intel Core m3, " +
                            "Intel Core 2, " +
                            "Intel Atom, " +
                            "AMD A-Series, " +
                            "AMD E-Series",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options = "500, 600, 700, 800, 1000",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        DisplayName = "Condition",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Sports Equipment
                    new SearchFilter
                    {
                        Name = "Sex/Gender",
                        DisplayName = "Sex/Gender",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        Options = "For Her, For Him, Unisex",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Size",
                        DisplayName = "Size",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        Options = "XS, S, M, L, XL, 2XL, 3XL, 4XL, 5XL",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Color",
                        DisplayName = "Color",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        Options = "black, grey, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "AllTimeAverageRating",
                        DisplayName = "Average Customer Review",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        Options = "1, 2, 3, 4, 5",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        DisplayName = "Price",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    }
                    #endregion
                );

                context.SaveChanges();
            }
            #endregion

            #region Tags
            if (!context.Tags.Any())
            {
                context.Tags.AddOrUpdate(
                    t => t.Id,
                    new Tag { Name = "black" },
                    new Tag { Name = "chrome" },
                    new Tag { Name = "red" },
                    new Tag { Name = "brown" },
                    new Tag { Name = "green" },
                    new Tag { Name = "blue" },
                    new Tag { Name = "white" },
                    new Tag { Name = "steel" },
                    new Tag { Name = "silver" },
                    new Tag { Name = "DSLR Camera" },
                    new Tag { Name = "cheap camera" },
                    new Tag { Name = "Canon camera" },
                    new Tag { Name = "large digital photo frame" },
                    new Tag { Name = "Nikon camera" },
                    new Tag { Name = "CMOS sensor" },
                    new Tag { Name = "3D tracking" },
                    new Tag { Name = "Mirrorless Digital Camera" },
                    new Tag { Name = "wide angle lens" },
                    new Tag { Name = "point-and-shoot camera" },
                    new Tag { Name = "Panasonic camera" },
                    new Tag { Name = "Sony camera" },
                    new Tag { Name = "Olympus camera" },
                    new Tag { Name = "Cuisinart food processor" },
                    new Tag { Name = "energy star dishwasher" },
                    new Tag { Name = "breville food processor" },
                    new Tag { Name = "KitchenAid food processor" },
                    new Tag { Name = "KitchenAid hand blender" },
                    new Tag { Name = "breville hand mixer" },
                    new Tag { Name = "braun food processor" },
                    new Tag { Name = "vitamix blender" },
                    new Tag { Name = "breville blender" },
                    new Tag { Name = "ninja blender" },
                    new Tag { Name = "python programming" },
                    new Tag { Name = "crash course" },
                    new Tag { Name = "performing arts" },
                    new Tag { Name = "misty copeland" },
                    new Tag { Name = "Eric Matthes" },
                    new Tag { Name = "Assimil" },
                    new Tag { Name = "japanese language" },
                    new Tag { Name = "Alexandra Dannenmann" },
                    new Tag { Name = "children's books" },
                    new Tag { Name = "Ruy Xoconostle Waye" },
                    new Tag { Name = "Eduardo Scarpetta" },
                    new Tag { Name = "comedy" },
                    new Tag { Name = "Margot Lee Shetterly" },
                    new Tag { Name = "Cédric H.Roserens" },
                    new Tag { Name = "Yuval Harari" },
                    new Tag { Name = "comfortable chair" },
                    new Tag { Name = "leather chair" },
                    new Tag { Name = "living room chair" },
                    new Tag { Name = "blue chair" },
                    new Tag { Name = "cherry wood" },
                    new Tag { Name = "Rolando wardrobe" },
                    new Tag { Name = "L-Shaped Desk" },
                    new Tag { Name = "Z-Line Nero Desk" },
                    new Tag { Name = "Orlando Dining Table" },
                    new Tag { Name = "Handi-craft" },
                    new Tag { Name = "Sectional Sofa" },
                    new Tag { Name = "Adjustable sofa" },
                    new Tag { Name = "cruelty free" },
                    new Tag { Name = "brown mascara" },
                    new Tag { Name = "Alcohol Free Shaving Cream" },
                    new Tag { Name = "Cruelty Free Fragrance" },
                    new Tag { Name = "Cruelty Free Eau De Toilette" },
                    new Tag { Name = "Rene Furterer shampoo" },
                    new Tag { Name = "Ducray Shampoo" },
                    new Tag { Name = "AmLactin Body Lotion" },
                    new Tag { Name = "maternity cosmetics" },
                    new Tag { Name = "MacBook Air" },
                    new Tag { Name = "Samsung Tab Pro tablet" },
                    new Tag { Name = "ASUS Transformer Book" },
                    new Tag { Name = "Samsung Chromebook" },
                    new Tag { Name = "Lenovo Yoga laptop" },
                    new Tag { Name = "ASUS ZenBook" },
                    new Tag { Name = "Dell Inspiron" },
                    new Tag { Name = "hp laptop" },
                    new Tag { Name = "HP ProBook" },
                    new Tag { Name = "adidas Jacket" },
                    new Tag { Name = "Adriana Arango Gym Outfit" },
                    new Tag { Name = "TrailHeads Headband" },
                    new Tag { Name = "ASICS Tank Top" },
                    new Tag { Name = "MÜV365 Armband" },
                    new Tag { Name = "Under Armour T-Shirt" },
                    new Tag { Name = "New Balance Long-Sleeve Shirt" },
                    new Tag { Name = "PUMA Training Pant" },
                    new Tag { Name = "Lemu Jacket" },
                    new Tag { Name = "Thai Fisherman Yoga Pants" }
                    );

                context.SaveChanges();
            }
            #endregion

            #region Products
            if (!context.Products.Any())
            {
                var sellerIds = new Dictionary<int, string>();
                var sellers = context.Users.Where(u => u.Roles.Any(r => r.RoleName == IdentityRoles.Seller)).OrderBy(u => u.UserName);
                var sellerIndex = 0;
                foreach (var seller in sellers)
                {
                    sellerIds.Add(sellerIndex, seller.Id);
                    sellerIndex++;
                }

                var random = new Random();
                var appliancesPath = "~/App_Data/SampleProductImages/Appliances/";
                var booksPath = "~/App_Data/SampleProductImages/Books/";
                var camerasPath = "~/App_Data/SampleProductImages/Cameras/";
                var furniturePath = "~/App_Data/SampleProductImages/Furniture/";
                var healthBeautyPath = "~/App_Data/SampleProductImages/Health_Beauty/";
                var notebooksPath = "~/App_Data/SampleProductImages/Notebooks/";
                var sportsPath = "~/App_Data/SampleProductImages/Sports_Equipment/";

                context.Products.AddOrUpdate(
                    p => p.Id,
                #region Appliances
                    //new Product
                    //{
                    //    Title = "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel",
                    //    ShortDescription = "Why Is This The Perfect Mini Processor For You? The Cuisinart Mini-Prep Plus Processor handles a variety of food preparation tasks including chopping, grinding, puréeing, emulsifying and blending. The patented auto-reversing SmartPower blade provides a super-sharp edge for the delicate chopping of herbs and for blending and puréeing other soft foods.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "Why Is This The Perfect Mini Processor For You? The Cuisinart Mini-Prep Plus Processor handles a variety of food preparation tasks including chopping, grinding, puréeing, emulsifying and blending. The patented auto-reversing SmartPower blade provides a super-sharp edge for the delicate chopping of herbs and for blending and puréeing other soft foods. The blunt edge offers a powerful cutting surface to grind through spices and other hard foods. Pulse activation gives maximum control for precision processing, whether chopping or grinding. Spatula, product manual and recipe booklet included. Using Your Cuisinart Mini-Prep Plus Processor The powerful high-speed 250-Watt motor works hard and fast to accomplish any small job with ease. Chop herbs, onions, garlic; grind spices, hard cheese, purée baby foods; blend mayonnaise and flavored butters, all with the same compact appliance. The Mini-Prep Plus Processor takes up minimum counter space and stores neatly on the countertop or in a cabinet.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor Power", Value = "250W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "chrome", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Size", Value = "3 cup" },
                    //            new Property { Name = "Body material", Value = "plastic" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 50,
                    //    UnitPrice = 37.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cuisinart food processor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "SPT SD-9252SS Energy Star 18\" Built - In Dishwasher, Stainless Steel",
                    //    ShortDescription = "18\" wide, but spacious cavity.Up to 8 standard place settings.It is a great addition to any home.It takes up minimal space and is a great replacement for older appliances. Meets federal guidelines for energy efficiency for year-round energy and money savings. Bring more cleaning power into a smaller space.",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "This built-in 8 place setting dishwasher is a great addition to any home. At 18 inches wide, this unit takes up minimal space and is a great replacement for older appliances. Features 2 pull out dish racks, 6 wash programs and time delay feature. Time Delay Feature: allows you to program operation at a later start time (1-24 hours). Error Alarm: displays fault codes. Rinse Aid Warning Indicator: refill reminder on rinse aid. Stainless Steel Interior. Quite Operation: at 55 dBA. Capacity: up to 8 standard place settings. 6 Wash Programs: All-in-1, Heavy, Normal, Light , Rinse and Speed. Two Racks: with adjustable upper rack to accommodate larger plates/pots. 2 Spray Arms: for complete cleaning coverage. Silverware Basket: holds silverware and utensils for easy cleaning. Automatic Dispensers: detergent and rinse agent. Energy Star: meets or exceeds federal guidelines for energy efficiency for year-round energy and money savings.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Power Consumption", Value = "1104W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Installation type", Value = "built-in" },
                    //            new Property { Name = "Color", Value = "steel", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "color".ToLower()) },
                    //            new Property { Name = "Voltage", Value = "120V" },
                    //            new Property { Name = "Material type", Value = "stainless steel" },
                    //            new Property { Name = "Noise level", Value = "55db" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 37,
                    //    UnitPrice = 329.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "steel".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "energy star dishwasher".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Breville BFP800XL Sous Chef Food Processor, Stainless Steel",
                    //    ShortDescription = "Every Breville product begins with a simple moment of brilliance. The Breville Sous Chef began with the observation that the food comes in many different shapes and sizes, making it difficult for one machine to consistently cut all ingredients into the optimal size pieces. So how do you make sure that you get the perfect size for what you’re cooking?...",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "Every Breville product begins with a simple moment of brilliance. The Breville Sous Chef began with the observation that the food comes in many different shapes and sizes, making it difficult for one machine to consistently cut all ingredients into the optimal size pieces. So how do you make sure that you get the perfect size for what you’re cooking? The Breville Sous Chef solves this problem with its unique design.Its wide feed chute makes it possible to slice vegetables of all shapes and sizes, while numerous disc and blade options makes it easy to get perfect results, any way you slice it. Food processors are supposed to make food prep easier, not more frustrating. The Breville Sous Chef has a 5.5” Super Wide Feed Chute that reduces the need to pre-cut most fruits & veggies, saving you time. The Breville Sous Chef comes with a set of 8 discs and blades for numerous prep options, all housed in a convenient accessory storage.The discs include a variable slicing disc that can be set to 24 different slicing settings so you can customize the thickness of your slices from a paper thin 0.3mm all the way up to a thick 8.0mm.Other discs in the set include a julienne disc, a French fry cutting disc, a whisking disc, and a reversible shredding disc, while the blades include a micro-serrated universal S blade, a dough blade for kneading and combining ingredients, and a mini blade for use with mini - bowl.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor Power", Value = "1200W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "steel", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Food pusher", Value = "yes" },
                    //            new Property { Name = "Display", Value = "LCD" },
                    //            new Property { Name = "Body material", Value = "metal" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 6,
                    //    UnitPrice = 373.44M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "red".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville food processor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red",
                    //    ShortDescription = "This model has a 9-cup work bowl with 2-in-1 Feed Tube and pusher for continuous processing. The 9-cup capacity is ideal for many home cooking needs, allowing you to chop, mix, slice and shred with ease, offering multiple tools in one appliance.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "The first ever externally adjustable slicing, KitchenAid ExactSlice System gives you precise slicing and accuracy for all kinds of food—hard or soft, large or small. And it does it all using less energy than previous model. Accommodates tomatoes, cucumbers, and potatoes with minimal prep work required. The UltraTight Seal Features a specially designed locking system with leak-resistant ring that allows you to fill the work bowl to capacity with ingredients without worrying about making a mess. High, Low & Pulse speed options allow you to precisely and properly handle soft or hard ingredients with the touch of a button.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor power", Value = "1000W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "red", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Body material", Value = "Polycarbonate" },
                    //            new Property { Name = "Size", Value = "9 cup" },
                    //            new Property { Name = "Condition", Value = "refurbished", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 27,
                    //    UnitPrice = 199.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "red".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "KitchenAid food processor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple",
                    //    ShortDescription = "The 2-Speed Hand Blender let's you blend, puree, and crush with ease. Two speeds provide control for food, such as, smoothies, soups, or baby food. The blending arm twists off for quick and easy cleanup.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "The 2-Speed Hand Blender let's you blend, puree, and crush with ease. Two speeds provide control for food, such as, smoothies, soups, or baby food. The blending arm twists off for quick and easy cleanup. The Removable 8-inch Blending Arm locks into the motor body for easy operation when blending in deeper pots. The soft grip handle offers a non-slip and comfortable grip when continuously blending ingredients. The 3-Cup BPA-Free Blending Jar with Lid is convenient for individual blending jobs, to serve or store for later. Top-rack dishwasher safe. Lid not included with Model KHB2352. Model KHB2571 comes with 4-Cup Pitcher.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor power", Value = "250W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "green", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Speed Settings", Value = "2" },
                    //            new Property { Name = "Material type", Value = "plastic" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 76,
                    //    UnitPrice = 39.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "green".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "KitchenAid hand blender".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver",
                    //    ShortDescription = "The Breville BHM800SIL Handy Mix Scraper with intuitive ergonomic control and Beater IQ automatically adjusts power to suit what you're mixing. Features an intuitive 9 speed selector...",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "How do you maximize speed for whisking and power for kneading from the same handy mixer? The Breville BHM800SIL Handy Mix Scraper with intuitive ergonomic control and Beater IQ automatically adjusts power to suit what you're mixing. Features an intuitive 9 speed selector, plus boost, with an easy to use scroll wheel is electronically controlled to spin at a precise speed no matter what the load. Accessories include 2 scraper beaters, 2 dough hooks, and 2 balloon whisks which are housed in a storage case which clips under the unit so nothing gets lost. Pause function holds your setting while you prepare or add ingredients. Quick release trigger and swivel cord also featured.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor power", Value = "240W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "silver", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Speed Settings", Value = "9" },
                    //            new Property { Name = "Material type", Value = "plastic" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 37,
                    //    UnitPrice = 129.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "silver".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville hand mixer".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering",
                    //    ShortDescription = "Every cook – professional or not – knows the frequent slicing, dicing, shredding and mixing that happens in the kitchen. By purchasing a food processor you can save your time doing all these much quicker than do it with a knife. The well - known Braun brand designed the most functional food processor with a wide range of tools for your convenience.",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "Every cook – professional or not – knows the frequent slicing, dicing, shredding and mixing that happens in the kitchen. By purchasing a food processor you can save your time doing all these much quicker than do it with a knife. The well - known Braun brand designed the most functional food processor with a wide range of tools for your convenience. Here’s what they have for you. Made in Europe with German Engineering for the best performance. Rated 600W and can Deliver Up to 900W peak Power. 12 cup for dry ingredients, 9 cups for liquid(wet ingredients). Quick to put together. Silent strength Ultra - Quiet. Energy efficient with low power consumption. Pre - set speed function. Compact design Easy to store. Easy to clean: Every part(except the base with the motor) is dishwasher safe.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor power", Value = "600W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Color", Value = "white", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Body material", Value = "plastic" },
                    //            new Property { Name = "Speed Settings", Value = "11" },
                    //            new Property { Name = "Size", Value = "12 cup" },
                    //            new Property { Name = "Voltage", Value = "110V" },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 18,
                    //    UnitPrice = 159.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "white".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "braun food processor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Vitamix 5200 Series Blender, Black",
                    //    ShortDescription = "Create every course of your home-cooked meal-from frozen drinks to creamy desserts-in minutes. The Vitamix 5200 is the universal tool for family meals and entertaining.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "Create every course of your home-cooked meal-from frozen drinks to creamy desserts-in minutes. The Vitamix 5200 is the universal tool for family meals and entertaining. The size and shape of the 64-ounce container is ideal for blending medium to large batches. Easily adjust speed to achieve a variety of textures. The dial can be rotated at any point during the blend, so you're in complete control. The power and precision of our patented designs are able to pulverize every recipe ingredient, including the tiniest seeds. The blades in the Vitamix container reach speeds fast enough to create friction heat, bringing cold ingredients to steaming hot in about six minutes.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor Power", Value = "1380W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Speed Settings", Value = "10" },
                    //            new Property { Name = "Body material", Value = "Tritan Copolyester" },
                    //            new Property { Name = "Color", Value = "Black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 112,
                    //    UnitPrice = 429.98M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "vitamix blender".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Breville BBL605XL Hemisphere Control Blender",
                    //    ShortDescription = "The Hemisphere Control combines the functionality of a powerful blender with some food processing tasks. It crushes and chops to turn ice into snow for velvety cocktails and also folds and aerates for creamy smoothies and soups. In addition, thanks to its innovative blade design...",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "Declared one of the best blenders of 2012 by a leading rater of kitchen products, the Breville Hemisphere Control uses an innovative design: extra wide stainless steel blades are contoured to sweep along the base of the jug and push ingredients up while central blades pull ingredients down. The Hemisphere Control combines the functionality of a powerful blender with some food processing tasks. It crushes and chops to turn ice into snow for velvety cocktails and also folds and aerates for creamy smoothies and soups. In addition, thanks to its innovative blade design and high torque motor, the Hemisphere Control can handle blending tasks more efficiently and with less noise. The Hemisphere Control blender is both easy to use and easy to clean. An LCD timer, 5 different speeds and three additional task functions make creating the perfect blend as easy as pushing one button. The permanent blade system is a snap to clean: just put in a drop of detergent and blend with water to remove food remnants, and then put the entire jug in the dishwasher.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor Power", Value = "750W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Speed Settings", Value = "5" },
                    //            new Property { Name = "Body material", Value = "Tritan" },
                    //            new Property { Name = "Color", Value = "silver", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 64,
                    //    UnitPrice = 180.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "silver".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville blender".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Ninja Professional Blender (NJ600)",
                    //    ShortDescription = "The Ninja Professional Blender features a sleek design and outstanding performance with 1000 watts of professional power. Ninja Total Crushing Technology is perfect for ice crushing, blending, pureeing, and controlled processing.",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "The Ninja Professional Blender features a sleek design and outstanding performance with 1000 watts of professional power. Ninja Total Crushing Technology is perfect for ice crushing, blending, pureeing, and controlled processing. Crush ice, whole fruits and vegetables in seconds! The XL 72 oz. professional blender jar is perfect for making drinks and smoothies for the whole family. All parts are BPA free and dishwasher safe. Total Crushing Technology delivers unbeatable professional power with blades that pulverize and crush through ice, whole fruits and vegetables in seconds. Blast ice into snow in seconds and blend your favorite ingredients into delicious sauces, dips and smoothies!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Motor Power", Value = "1000W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                    //            new Property { Name = "Speed Settings", Value = "3" },
                    //            new Property { Name = "Body material", Value = "plastic" },
                    //            new Property { Name = "Color", Value = "black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 27,
                    //    UnitPrice = 81.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "ninja blender".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Books
                    //new Product
                    //{
                    //    Title = "Python Crash Course: A Hands-On, Project-Based Introduction to Programming",
                    //    ShortDescription = "Python Crash Course is a fast-paced, thorough introduction to programming with Python that will have you writing programs, solving problems, and making things that work in no time.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "Python Crash Course is a fast-paced, thorough introduction to programming with Python that will have you writing programs, solving problems, and making things that work in no time. In the first half of the book, you'll learn about basic programming concepts, such as lists, dictionaries, classes, and loops, and practice writing clean and readable code with exercises for each topic. You'll also learn how to make your programs interactive and how to test your code safely before adding it to a project. In the second half of the book, you'll put your new knowledge into practice with three substantial projects: a Space Invaders-inspired arcade game, data visualizations with Python's super-handy libraries, and a simple web app you can deploy online. As you work through Python Crash Course, you'll learn how to: use powerful Python libraries and tools, including matplotlib, NumPy, and Pygal; make 2D games that respond to keypresses and mouse clicks, and that grow more difficult as the game progresses; work with data to generate interactive visualizations; create and customize simple web apps and deploy them safely online; deal with mistakes and errors so you can solve your own programming problems. If you've been thinking seriously about digging into programming, Python Crash Course will get you up to speed and have you writing real programs fast. Why wait any longer? Start your engines and code!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Eric Matthes" },
                    //            new Property { Name = "Publisher", Value = "No Starch Press" },
                    //            new Property { Name = "Age range", Value = "10 and up" },
                    //            new Property { Name = "Page count", Value = "560" },
                    //            new Property { Name = "Language", Value = "English", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Paperback", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 16,
                    //    UnitPrice = 22.59M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "python programming".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "crash course".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Eric Matthes".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Misty Copeland",
                    //    ShortDescription = "The first authorized photographic tribute to the prolific and wildly inspiring ballerina,these unique and evocative artful color photographs",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "The first authorized photographic tribute to the prolific and wildly inspiring ballerina,these unique and evocative artful color photographs by the celebrated photographer Gregg Delman, capture Misty's grace and strength, and are much anticipated by the worldwide audience who can't get enough of Misty.This stunning volume of photographs captures the sculpturally exquisite and iconic ballerina. Misty Copeland has single-handedly infused diversity and personality into the insular world of ballet, creating an unexpected resurgence of appreciation within contemporary popular culture. Her story is famously what movies are made of, and in 2015 she became an icon and household name when she became the first African-American female principal dancer in the long and prestigious history the American Ballet Theatre. Copeland’s physique is what sculptures are modeled on, heralding the new physical ideal of strength and athleticism, beauty and grace. Misty Copeland is a collection of gorgeous, artful photographs, taken in many studio visits from 2011 through 2014. Delman’s talent for capturing movement is reflected in these images, which range from formal ballet positions to more athletic poses and candid moments, all together building an intimate portrait of Copeland as an athlete, an artist, and a woman. With striking and vibrant color photographs, this incredibly intimate volume is a visual tribute to the brilliant mystique of Misty Copeland, showcasing both her grace and strength.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Gregg Delman" },
                    //            new Property { Name = "Publisher", Value = "Rizzoli " },
                    //            new Property { Name = "Page count", Value = "144" },
                    //            new Property { Name = "Language", Value = "English", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Hardcover", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 5,
                    //    UnitPrice = 26.89M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "performing arts".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "misty copeland".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "LA MENTALITÉ PRIMITIVE (French Edition)",
                    //    ShortDescription = "Introduction. Différence de la mentalité primitive aux causes secondes. Les puissances mystiques et invisibles. Les rêves. Les présages. Les pratiques divinatoires. Les ordalies. Interprétation mystique des accidents et des malheurs. etc...",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "Introduction. Différence de la mentalité primitive aux causes secondes. Les puissances mystiques et invisibles. Les rêves. Les présages. Les pratiques divinatoires. Les ordalies. Interprétation mystique des accidents et des malheurs. etc... Garantie Format professionnel Kindle. Relu, corrigé et intégré par l’éditeur aux fonctionnalités de navigation du Kindle(table des matières dynamique).",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Lucien Lévy-Bruhl" },
                    //            new Property { Name = "File size", Value = "1358 KB" },
                    //            new Property { Name = "Language", Value = "French", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Kindle", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 9999,
                    //    UnitPrice = 7.19M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Lucien Lévy-Bruhl".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Japanese with Ease, Volume 1 (Assimil with Ease) (v. 1)",
                    //    ShortDescription = "Aims to take users through the basic structures needed for communication and become familiar with the basic words and grammar.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "Aims to take users through the basic structures needed for communication and become familiar with the basic words and grammar. This book employs a method which comprises two phases - passive phase, in which users repeat what they hear and read, and active phase, in which users create sentences and imagine themselves in everyday situations.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Assimil" },
                    //            new Property { Name = "Language", Value = "Japanese", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Audio CD", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 3,
                    //    UnitPrice = 63.57M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Assimil".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "japanese language".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Kann die Sonne schwimmen Ein Bilderbuch mit vielen farbigen Illustrationen ab 2 Jahren. (German Edition)",
                    //    ShortDescription = "Weil der kleine Krake keine Flossen hat, kann er nicht schwimmen. Aber er will es unbedingt lernen, weil er wenigstens ein einziges Mal die Sonne sehen will. Jeden Tag übt er fleißig. Der kleine gelbe Fisch hat ihm nämlich erzählt, die Sonne habe auch keine Flossen.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "Weil der kleine Krake keine Flossen hat, kann er nicht schwimmen. Aber er will es unbedingt lernen, weil er wenigstens ein einziges Mal die Sonne sehen will. Jeden Tag übt er fleißig. Der kleine gelbe Fisch hat ihm nämlich erzählt, die Sonne habe auch keine Flossen. Trotzdem könne sie schwimmen. Abends versinke sie im Meer und am Morgen schwimme sie wieder nach oben. Ob es dem kleinen Kraken wohl gelingt, die Sonne zu sehen? Ein Bilderbuch mit vielen farbigen Illustrationen für Kinder ab 2 Jahren. Mehr Informationen und Leseproben finden Sie auf meiner Homepage http://alexandra-dannenmann.de und auf meiner Facebook-Seite http://www.facebook.com/AlexandraDannenmann.Kinderbuch.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Alexandra Dannenmann" },
                    //            new Property { Name = "Publisher", Value = "CreateSpace Independent Publishing Platform" },
                    //            new Property { Name = "Page count", Value = "24" },
                    //            new Property { Name = "Language", Value = "German", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Large print", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 7,
                    //    UnitPrice = 7.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Alexandra Dannenmann".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "children's books".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Hackers de arcoíris",
                    //    ShortDescription = "Amor, sicarios y telépatas. En un México fragmentado, en guerra y atemporal, bañado de persecuciones y odios étnicos y religiosos, los hijos de Makivar son mejores que nadie para hackear telépatas.",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "Amor, sicarios y telépatas. En un México fragmentado, en guerra y atemporal, bañado de persecuciones y odios étnicos y religiosos, los hijos de Makivar son mejores que nadie para hackear telépatas.Ellos son guerreros dorsai, “casi” indestructibles, y de fuerza y velocidad metahumanas, contratados por gobiernos y corporaciones para eliminar telépatas fuera de control.La muerte de su padre, sin embargo, ha desensamblado a los hijos de Makivar y al quinto miembro del equipo, Starla Komatsu.Todo ha cambiado. Una nueva oferta, sin embargo, los vuelve a reunir: deben asistir en el asesinato de Frank Chibi, un agente secreto en el país de Penn que, mientras duerme, se \"desdobla\" y realiza actos psíquicos “inenarrables”. Los hijos de Makivar aceptan… sin saber que se dirigen a algo con lo que nunca se habían enfrentado.El padre de todos los telépatas.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Ruy Xoconostle Waye" },
                    //            new Property { Name = "Language", Value = "Spanish", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Kindle", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            //new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 9999,
                    //    UnitPrice = 11.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Ruy Xoconostle Waye".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Miseria e Nobiltà (Italian Edition)",
                    //    ShortDescription = "La commedia ha come protagonista Felice Sciosciammocca, celebre maschera di Eduardo Scarpetta, e la trama gira attorno all'amore del giovane nobile Eugenio per Gemma, figlia di Gaetano, un cuoco arricchito...",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "La commedia ha come protagonista Felice Sciosciammocca, celebre maschera di Eduardo Scarpetta, e la trama gira attorno all'amore del giovane nobile Eugenio per Gemma, figlia di Gaetano, un cuoco arricchito. Il ragazzo è però ostacolato dal padre, il marchese Favetti, che è contro il matrimonio del figlio per via del fatto che Gemma è la figlia di un cuoco. Eugenio si rivolge quindi allo scrivano Felice per trovare una soluzione. Felice e Pasquale, un altro spiantato, assieme alle rispettive famiglie, si introdurranno a casa del cuoco fingendosi i parenti nobili di Eugenio. La situazione si ingarbuglia poiché anche il vero Marchese Favetti è innamorato della ragazza, al punto di frequentarne la casa sotto le mentite spoglie di Don Bebè. Il figlio, scopertolo e minacciatolo di rivelare la verità, lo costringerà a dare il suo consenso per le nozze.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Eduardo Scarpetta" },
                    //            new Property { Name = "Page count", Value = "56" },
                    //            new Property { Name = "Publisher", Value = "CreateSpace Independent Publishing Platform" },
                    //            new Property { Name = "Language", Value = "Italian", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Paperback", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 50,
                    //    UnitPrice = 7.05M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Eduardo Scarpetta".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "comedy".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Hidden Figures The American Dream and the Untold Story of the Black Women Mathematicians Who Helped Win the Space Race",
                    //    ShortDescription = "New York Times Bestseller. The phenomenal true story of the black female mathematicians at NASA whose calculations helped fuel some of America’s greatest achievements in space.Soon to be a major motion picture starring Taraji P.Henson, Octavia Spencer, Janelle Monae, Kirsten Dunst, and Kevin Costner.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "The phenomenal true story of the black female mathematicians at NASA whose calculations helped fuel some of America’s greatest achievements in space. Soon to be a major motion picture starring Taraji P. Henson, Octavia Spencer, Janelle Monae, Kirsten Dunst, and Kevin Costner. Before John Glenn orbited the earth, or Neil Armstrong walked on the moon, a group of dedicated female mathematicians known as “human computers” used pencils, slide rules and adding machines to calculate the numbers that would launch rockets, and astronauts, into space. Among these problem - solvers were a group of exceptionally talented African American women, some of the brightest minds of their generation.Originally relegated to teaching math in the South’s segregated public schools, they were called into service during the labor shortages of World War II, when America’s aeronautics industry was in dire need of anyone who had the right stuff.Suddenly, these overlooked math whizzes had a shot at jobs worthy of their skills, and they answered Uncle Sam’s call, moving to Hampton, Virginia and the fascinating, high-energy world of the Langley Memorial Aeronautical Laboratory. Even as Virginia’s Jim Crow laws required them to be segregated from their white counterparts, the women of Langley’s all-black “West Computing” group helped America achieve one of the things it desired most: a decisive victory over the Soviet Union in the Cold War, and complete domination of the heavens. Starting in World War II and moving through to the Cold War, the Civil Rights Movement and the Space Race, Hidden Figures follows the interwoven accounts of Dorothy Vaughan, Mary Jackson, Katherine Johnson and Christine Darden, four African American women who participated in some of NASA’s greatest successes.It chronicles their careers over nearly three decades they faced challenges, forged alliances and used their intellect to change their own lives, and their country’s future.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Margot Lee Shetterly" },
                    //            new Property { Name = "File Size", Value = "1388 KB" },
                    //            new Property { Name = "Publisher", Value = "William Morrow; Reprint edition" },
                    //            new Property { Name = "Page count", Value = "373" },
                    //            new Property { Name = "Language", Value = "English", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Kindle", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //        }
                    //    },
                    //    QuantityInStock = 9999,
                    //    UnitPrice = 10.93M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Margot Lee Shetterly".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Happísland: Le court mais pas trop bref récit d'un espion suisse en Islande (French Edition)",
                    //    ShortDescription = "Ce petit ouvrage, plein de ð et de þ, rend hommage à l'Islande, par l'intermédiaire des comptes rendus du fin limier de la Confédération helvétique, Hans-Ueli Stauffacher. Un espion dont la mission est de comprendre pourquoi les Islandais sont plus heureux que les Suisses!",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "Ce petit ouvrage, plein de ð et de þ, rend hommage à l'Islande, par l'intermédiaire des comptes rendus du fin limier de la Confédération helvétique, Hans-Ueli Stauffacher. Un espion dont la mission est de comprendre pourquoi les Islandais sont plus heureux que les Suisses!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Cédric H. Roserens" },
                    //            new Property { Name = "Publisher", Value = "CreateSpace Independent Publishing Platform" },
                    //            new Property { Name = "Language", Value = "French", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Paperback", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                            
                    //    },
                    //    QuantityInStock = 12,
                    //    UnitPrice = 5.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Cédric H. Roserens".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Homo Deus (Spanish Edition)",
                    //    ShortDescription = "Tras el éxito de Sapiens. De animales a dioses, Yuval Noah Harari vuelve su mirada al futuro para ver hacia dónde nos dirigimos. La guerra es algo obsoleto.Es más probable quitarse la vida que morir en un conflicto bélico. La hambruna está desapareciendo.Es más habitual sufrir obesidad que pasar hambre. La muerte es solo un problema técnico.Adiós igualdad.Hola inmortalidad.",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "Yuval Noah Harari, autor bestseller de Sapiens. De animales a dioses, augura un mundo no tan lejano en el cual nos veremos enfrentados a una nueva serie de retos. Homo Deus explora los proyectos, los sueños y las pesadillas que irán moldeando el siglo XXI -desde superar la muerte hasta la creación de la inteligencia artificial. - Cuando tu Smartphone te conozca mejor de lo que te conoces a ti mismo, ¿seguirás escogiendo tu trabajo, a tu pareja y a tu presidente ? -Cuando la inteligencia artificial nos desmarque del mercado laboral, ¿encontrarán los millones de desempleados algún tipo de significado en las drogas o los juegos virtuales ? -Cuando los cuerpos y cerebros sean productos de diseño, ¿cederá la selección natural el paso al diseño inteligente ? Esto es el futuro de la evolución.Esto es Homo Deus.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Author", Value = "Yuval Harari" },
                    //            new Property { Name = "Publisher", Value = "Debate" },
                    //            new Property { Name = "Page count", Value = "528" },
                    //            new Property { Name = "Language", Value = "Spanish", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Language".ToLower()) },
                    //            new Property { Name = "Format", Value = "Hardcover", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Format".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41jYb1i4QKL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "10/41jYb1i4QKL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71wjSOz1NLL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "10/71wjSOz1NLL.jpg" },
                    //    },
                    //    QuantityInStock = 6,
                    //    UnitPrice = 24.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Yuval Harari".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Cameras
                    //new Product
                    //{
                    //    Title = "Canon EOS 5D Mark III 22.3 MP Full Frame CMOS with 1080p Full-HD Video Mode Digital SLR Camera (Body)",
                    //    ShortDescription = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform. Special optical technologies like 61-Point High Density Reticular AF and extended ISO range of 100-25600 make this it ideal for shooting weddings in the studio, out in the field and great for still photography. Professional-level high definition video capabilities includes a host of industry-standard recording protocols and enhanced performance that make it possible to capture beautiful cinematic movies in EOS HD quality. A 22.3 Megapixel full-frame Canon CMOS sensor, Canon DIGIC 5+ Image Processor, and shooting performance up to 6.0fps provide exceptional clarity and sharpness when capturing rapidly-unfolding scenes. Additional technological advancements include an Intelligent Viewfinder, Canon's advanced iFCL metering system, High Dynamic Range (HDR), and Multiple Exposure.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Color", Value = "Black" },
                    //            new Property { Name = "Optical Sensor Resolution", Value = "22.3 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Image Stabilization", Value = "None" },
                    //            new Property { Name = "Continuous Shooting Speed", Value = "6 fps" },
                    //            new Property { Name = "Battery Average Life", Value = "950 Photos" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "718e4oj9wtL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/718e4oj9wtL._SL1000_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61DyxdToZXL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/61DyxdToZXL._SL1000_.jpg" },
                    //        new Image { OriginalFileName = "71YfECxfLKL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/71YfECxfLKL._SL1000_.jpg" },
                    //        new Image { OriginalFileName = "717TLNCZmkL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/717TLNCZmkL._SL1000_.jpg" },
                    //        new Image { OriginalFileName = "71MFCjnJGXL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/71MFCjnJGXL._SL1000_.jpg" },
                    //        new Image { OriginalFileName = "61kmi8G86QL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "1/61kmi8G86QL._SL1000_.jpg" },
                    //    },
                    //    QuantityInStock = 60,
                    //    UnitPrice = 2499.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cheap camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Canon camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Canon PowerShot SX410 IS (Black)",
                    //    ShortDescription = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility.",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility: you'll capture wide landscapes and zoom in for impressive close-ups you never thought possible – all with bright, clear quality thanks to Canon's Optical Image Stabilizer and Intelligent IS. The 20.0 Megapixel* sensor and Canon DIGIC 4+ Image Processor help create crisp resolution and beautiful, natural images. Your videos will impress too: simply press the Movie button to record lifelike 720p HD video – even zoom in and out while shooting. Images you'll want to keep and share are easy to achieve with Smart AUTO that intelligently selects proper camera settings so your images and video look great in all kinds of situations. You'll get creative with fun Scene Modes like Fisheye Effect, Toy Camera Effect and Monochrome, and see and share it all with the camera's big, clear 3.0\" LCD with a wide viewing angle.For versatility and value, the PowerShot SX410 IS camera is a best bet!\n *Image processing may cause a decrease in the number of pixels.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Color", Value = "Black" },
                    //            new Property { Name = "Maximum Aperture Range", Value = "F3.5 - F5.6" },
                    //            new Property { Name = "ISO Minimum", Value = "100" },
                    //            new Property { Name = "ISO Maximum", Value = "1600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Maximum ISO".ToLower()) },
                    //            new Property { Name = "Optical Zoom", Value = "40x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Battery Average Life", Value = "185 Photos" },
                    //            new Property { Name = "Condition", Value = "Refurbished", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81JvWiIQoML._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/81JvWiIQoML._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81iE5piHPoL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/81iE5piHPoL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71GwubTjP8L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/71GwubTjP8L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81nW9tO9C3L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/81nW9tO9C3L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81t+t3sopxL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/81t+t3sopxL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "710EaWkuE7L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/710EaWkuE7L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "812wga2O3PL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/812wga2O3PL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71565Zim9NL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "2/71565Zim9NL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 314,
                    //    UnitPrice = 179.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cheap camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "large digital photo frame".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "canon camera".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Nikon D3200 24.2 MP CMOS Digital SLR with 18-55mm f/3.5-5.6 Auto Focus-S DX VR NIKKOR Zoom Lens (Black)",
                    //    ShortDescription = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light...",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light, EXPEED 3 image-processing for fast operation and creative in-camera effects, Full HD (1080p) movie recording, in-camera tutorials and much more. What does this mean for you? Simply stunning photos and videos in any setting. And now, with Nikon’s optional Wireless Mobile Adapter, you can share those masterpieces instantly with your Smartphone or tablet easily. Supplied Accessories: EN-EL14 Rechargeable Li-ion Battery, MH-24 Quick Charger, EG-CP14 Audio/Video Cable, UC-E6 USB Cable, DK-20 Rubber Eyecup, AN-DC3 Camera Strap, DK-5 Eyepiece Cap, BF-1B Body Cap, BS-1 Accessory Shoe Cover and Nikon View NX CD-ROM.1-Year Nikon U.S.A. Warranty.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Color", Value = "Black" },
                    //            new Property { Name = "Autofocus Points", Value = "11" },
                    //            new Property { Name = "Continuous Shooting Speed", Value = "4 fps" },
                    //            new Property { Name = "Flash Sync Speed", Value = "1/200 sec" },
                    //            new Property { Name = "Image Stabilization", Value = "None" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "91lIcinq-7L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "3/91lIcinq-7L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "91WSe2OKasL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "3/91WSe2OKasL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "91JTbCw3R-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "3/91JTbCw3R-L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81heldS9LWL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "3/81heldS9LWL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 226,
                    //    UnitPrice = 355.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Nikon D7100 24.1 MP DX-Format CMOS Digital SLR Camera Bundle with 18-140mm and 55-300mm VR NIKKOR Zoom Lens (Black)",
                    //    ShortDescription = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more. Shoot up to 6 fps and instantly share shots with the WU-1a Wireless Adapter. Create dazzling Full HD 1080p videos and ultra-smooth slow-motion or time-lapse sequences.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display", Value = "TFT LCD" },
                    //            new Property { Name = "ISO Minimum", Value = "50" },
                    //            new Property { Name = "ISO Maximum", Value = "25,600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Maximum ISO".ToLower()) },
                    //            new Property { Name = "Lens Type", Value = "Fisheye" },
                    //            new Property { Name = "Model Year", Value = "2014" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "51RoKkox9mL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "4/51RoKkox9mL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "51vkBn8zr3L.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "4/51vkBn8zr3L.jpg" },
                    //        new Image { OriginalFileName = "51DBXCJyCmL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "4/51DBXCJyCmL.jpg" },
                    //        new Image { OriginalFileName = "41dtSdwiKAL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "4/41dtSdwiKAL.jpg" },
                    //        new Image { OriginalFileName = "618PVLb1ikL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "4/618PVLb1ikL.jpg" },
                    //    },
                    //    QuantityInStock = 130,
                    //    UnitPrice = 1346.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "3D tracking".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Sony Alpha a6000 Mirrorless Digital Camera with 16-50mm Power Zoom Lens",
                    //    ShortDescription = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality - via newly developed 24.3-effective-megapixel (approx.) Exmor APS HD CMOS sensor and BIONZ X image processing engine - as well as highly intuitive operation thanks to an OLED Tru-Finder and two operation dials.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Optical Sensor Resolution", Value = "24 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Weather Resistance", Value = "No" },
                    //            new Property { Name = "Model Year", Value = "2014" },
                    //            new Property { Name = "Minimum Shutter Speed", Value = "30 seconds" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61m2uvNozqL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/61m2uvNozqL._SL1200_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61mbbcgOGpL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/61mbbcgOGpL._SL1200_.jpg" },
                    //        new Image { OriginalFileName = "61TNwK1mUFL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/61TNwK1mUFL._SL1200_.jpg" },
                    //        new Image { OriginalFileName = "61Wd2xfzufL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/61Wd2xfzufL._SL1200_.jpg" },
                    //        new Image { OriginalFileName = "61xc4+G71LL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/61xc4+G71LL._SL1200_.jpg" },
                    //        new Image { OriginalFileName = "71WUrWtVcTL._SL1200_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "5/71WUrWtVcTL._SL1200_.jpg" },
                    //    },
                    //    QuantityInStock = 414,
                    //    UnitPrice = 648.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "sony camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Nikon COOLPIX L32 Digital Camera with 5x Wide-Angle NIKKOR Zoom Lens",
                    //    ShortDescription = "The Nikon Coolpix L32 Digital Camera makes taking great photos and videos a breeze! Want better selfies or photos of friends, family and even pets? Use the Smart Portrait System and let the COOLPIX L32 do all the work. Want better videos? Press the dedicated Movie Record button and capture 720p HD videos with sound. You can even zoom in while you're recording - electronic Vibration Reduction will help keep your videos steady. Additional features: Image Effects, large 3.0-inch LCD, Glamour Retouch, runs on AA batteries, plus more!",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "The COOLPIX L32 is all about ease just point, shoot and enjoy your great photos and videos.When you want to record videos, you don't have to turn any dials or flip any switches just press the dedicated Movie Record button. And when you want to add fun effects, the COOLPIX L32's simple menu system makes it a breeze. Smart Portrait System makes it easy to create beautiful photos of the people you care about. Turn it on, and several portrait-optimizing features activate. Face Priority AF finds and focuses on faces. Skin Softening applies an attractive soft focus effect. The camera can even automatically take a photo the instant someone smiles! Your loved ones will always look their best. Every COOLPIX is designed around a genuine NIKKOR glass lens, the legendary optics that have made Nikon famous. The COOLPIX L32's 5x Zoom NIKKOR lens is great for everything from wide-angle group shots to close-up portraits. Plus, Electronic Vibration Reduction helps keep every video steadier, even if your hands are not.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Image Stabilization", Value = "Yes" },
                    //            new Property { Name = "Optical Zoom", Value = "5x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Display", Value = "3.0-inch LCD" },
                    //            new Property { Name = "Optical Sensor Resolution", Value = "20.1 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Color", Value = "red" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "818HwwuQw0L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "6/818HwwuQw0L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81BHIVQ9YXL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "6/81BHIVQ9YXL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81YtApb4T+L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "6/81YtApb4T+L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "51tpjvrtztL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "6/51tpjvrtztL.jpg" },
                    //    },
                    //    QuantityInStock = 160,
                    //    UnitPrice = 119.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "large digital photo frame".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "wide angle lens".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "point-and-shoot camera".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Canon EOS Rebel T5 1200D 18MP EF-S Body Full HD 1080p Video Digital SLR Camera (NO LENS)",
                    //    ShortDescription = "This USA Canon EOS Rebel T5 DSLR Camera is an 18MP APS-C format DSLR camera with a DIGIC 4 image processor. The combination of the T5's CMOS sensor and DIGIC 4 image processor provide high clarity, a wide tonal range, and natural color reproduction.",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "This USA Canon EOS Rebel T5 DSLR Camera is an 18MP APS-C format DSLR camera with a DIGIC 4 image processor. The combination of the T5's CMOS sensor and DIGIC 4 image processor provide high clarity, a wide tonal range, and natural color reproduction. With an ISO range of 100-6400 (expandable to 12800), you can shoot in low-light situations, reducing the need for a tripod or a flash. The nine-point autofocus system includes one center cross-type AF point to deliver accurate focus in both landscape and portrait orientations. You can capture Full HD 1080p video by way of the T5's dedicated Live View/Movie Recording button, at 30, 25, or 24 fps. Additionally, the camera can also record HD video in 720p at 60 and 50 fps, and 480p at 30 and 35 fps. The 3\" rear LCD screen has 460,000 pixels, and a 170° viewing angle, making it ideal for menu navigation, composing in Live View, and reviewing or sharing your photos and videos.The Rebel T5 is compatible with the full line of Canon EF and EF - S lenses.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Optical Sensor Resolution", Value = "18 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Video Resolution", Value = "1080p" },
                    //            new Property { Name = "color", Value = "black" },
                    //            new Property { Name = "Photo Sensor Size", Value = "aps-c" },
                    //            new Property { Name = "ISO Minimum", Value = "100" },
                    //            new Property { Name = "ISO Maximum", Value = "6400", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Maximum ISO".ToLower()) },
                    //            new Property { Name = "Autofocus", Value = "9-point AF system" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61vrJdPWFJL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "7/61vrJdPWFJL._SL1000_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71HWQvqvozL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "7/71HWQvqvozL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "31H8v1suL+L.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "7/31H8v1suL+L.jpg" },
                    //    },
                    //    QuantityInStock = 3,
                    //    UnitPrice = 419.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "canon camera".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Olympus Evolt E410 10MP Digital SLR Camera with 14-42mm f3.5-5.6 and 40-150mm f4.0-5.6 Zuiko Lenses",
                    //    ShortDescription = "Innovative 2.5-inch Live View HyperCrystal LCD. Detailed, bright, and colorful photos with 10-megapixel Live MOS image sensor. TruePic III for image clarity. Included 14-42mm f/3.5-5.6 and 40-150mm f/4.0-5.6 Zuiko lenses.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "The E-410 offers ease-of-shooting and greater flexibility with the Live View LCD. Composing photographs is easier as subjects can be seen on the Live View LCD, which offers a wide 176-degree viewing angle. The E-410 is loaded with an impressive 10 million pixels for high-resolution photos. The 10-megapixel sensor gives users the flexibility to blow-up their prints to the large sizes supported by many of today’s printers, or crop the image to print only a part of the image that is important to them. The high-performance Live MOS image sensor in the E-410 delivers excellent dynamic range for accurate color fidelity, and a new state-of-the-art amplifier circuit to eradicate noise and capture fine image details in the highlight and shadow areas. Olympus’ enhanced TruePic III Image Processor produces crystal clear photos using all the pixel information for each image to provide the best digital images possible for every photo with accurate color, true-to-life flesh tones, brilliant blue skies and precise tonal representation in between. TruePic III also lowers image noise by one step to reduce noise in images shot at higher ISO settings, enabling great results in low-light situations. The E-410 one-lens outfit includes a compact, Zuiko Digital ED 14-42 mm f3.5-f5.6 Lens (equivalent to 28mm-84mm in 35mm photography) that perfectly matches the imager so light strikes the sensor directly to ensure rich, accurate colors and edge-to-edge sharpness. Its 3x ED Glass zoom lens covers the range most frequently used in everyday photography and weighs just 7.5 ounces, offering users an extremely dynamic, portable everyday-use zoom. Close-ups as near as 9.84 inches (0.25 m) are also possible throughout the zoom range. The E - 410 two - lens outfit adds the Zuiko Digital ED 40 - 150mm f4.0 - 5.6(80 - 300mm equivalent) Lens, which provides users with greater telephoto power for far - away shots in a compact size.This telephoto lens is smaller than many standard zoom lenses at 2.6 inch diameter x 2.8 inch length and a weight of 8.8 ounces-- a real benefit for anyone who wants to pack a powerful zoom lens without taking up much space.It also has great close focusing abilities, and is able to capture a subject up - close from a distance of 31.5 inches(.8m).",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Optical Sensor Resolution", Value = "10 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Display", Value = "2.5-inch Live View HyperCrystal LCD" },
                    //            new Property { Name = "Condition", Value = "Used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "613i1CswCSS._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "8/613i1CswCSS._SL1000_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61BPUL31VxS._SL1000_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "8/61BPUL31VxS._SL1000_.jpg" },
                    //        new Image { OriginalFileName = "41w5Aqb4FaL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "8/41w5Aqb4FaL.jpg" },
                    //        new Image { OriginalFileName = "41ymDN1EUvL.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "8/41ymDN1EUvL.jpg" },
                    //    },
                    //    QuantityInStock = 36,
                    //    UnitPrice = 467.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "olympus camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Sony a7 Full-Frame Mirrorless Digital Camera with 28-70mm Lens",
                    //    ShortDescription = "No other full frame, interchangeable-lens camera is this light or this portable. 24.3 MP of rich detail. A true-to-life 2.4 million dot OLED viewfinder. Wi-Fi sharing and an expandable shoe system. It's all the full-frame performance you ever wanted in a compact size that will change your perspective entirely.",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "Sony's Exmor® image sensor takes full advantage of the Full-frame format, but in a camera body less than half the size and weight of a full-frame DSLR. A whole new world of high-quality images are realized through the 24.3 MP effective 35 mm full-frame sensor, a normal sensor range of ISO 100 – 25600, and a sophisticated balance of high resolving power, gradation and low noise. The BIONZ® X image processor enables up to 5 fps high-speed continuous shooting and 14-bit RAW image data recording. The high-speed image processing engine and improved algorithms combine with optimized image sensor read-out speed to achieve ultra high-speed AF despite the use of a full-frame sensor.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Optical Sensor Resolution", Value = "24.3 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "Optical Zoom", Value = "4x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Image Stabilization", Value = "Yes" },
                    //            new Property { Name = "ISO Minimum", Value = "100" },
                    //            new Property { Name = "ISO Maximum", Value = "25600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Maximum ISO".ToLower()) },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "71qT3esgbAL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "9/71qT3esgbAL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81-tIoSABiL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "9/81-tIoSABiL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "716g0raonbL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "9/716g0raonbL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81IkmFjF2jL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "9/81IkmFjF2jL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71Cj7HU1KdL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "9/71Cj7HU1KdL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 43,
                    //    UnitPrice = 1398.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "sony camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Panasonic LUMIX DMC-G85MK 4K Mirrorless Interchangeable Lens Camera Kit, 12-60mm Lens, 16 Megapixel (Black)",
                    //    ShortDescription = "The Panasonic LUMIX G85 offers over 27 LUMIX compact lens options built on the next-generation interchangeable lens camera standard (Micro Four Thirds) pioneered by Panasonic. Its “mirrorless” design enables a lighter, more compact camera body that includes cutting-edge video, audio, creative controls, wireless, intelligent-focusing, gyro sensor control in body image stabilization and exposure technologies not possible with traditional DSLRs.",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "When life's adventures take you places, you need a camera that keeps up. Photographer Mitchell Kanashkevich took the LUMIX G85 on a journey to Romania. Perfect for outdoor shooting, the Dual Image Stabilizer helped him take crisper, clearer images in difficult and fast-moving environments, while the compact, weather-sealed body and kit lens improved flexibility wherever he went. With the LUMIX G85, a new gyro sensor increases the image stability compensation power of the 5-Axis Body image stabilization to correct hand-shake for all lenses, including classic lenses not equipped with optical image stabilization. The LUMIX G85 integrates 5-Axis Dual I.S.2 (Image Stabilizer)*, combining 5-axis body and 2-axis lens stabilization for more effective handshake correction and compensation for shots up to 5 f-stops**. The 5-axis stabilization works in both wide and telephoto photography and motion picture recording, including 4K Video. 4K Photo - Never Miss That Shot LUMIX - pioneered 4K PHOTO lets you capture the perfect moment by selecting single frames from a 4K video sequence shot at a blistering 30fps to save as individual high - res images.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Optical Sensor Resolution", Value = "16 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Resolution".ToLower()) },
                    //            new Property { Name = "color", Value = "black" },
                    //            new Property { Name = "Optical Zoom", Value = "5x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Optical Zoom".ToLower()) },
                    //            new Property { Name = "Image Stabilization", Value = "Yes" },
                    //            new Property { Name = "Maximum Shutter Speed", Value = "1/4000 of a second" },
                    //            new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81Y920s19-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/81Y920s19-L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "91oiwSglwmL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/91oiwSglwmL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81DIncBY+TL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/81DIncBY+TL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81iqSNeCm6L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/81iqSNeCm6L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81RplvOgjIL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/81RplvOgjIL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81VqUsXXm4L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = camerasPath + "10/81VqUsXXm4L._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 1,
                    //    UnitPrice = 997.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "panasonic camera".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Furniture
                    //new Product
                    //{
                    //    Title = "Massaging Black Leather Recliner and Ottoman with Leather Wrapped Base",
                    //    ShortDescription = "Enjoy a relaxing massage in the comfort of your own home or office with this recliner and ottoman set. This set offers maximum massaging power that kneads your back, lumbar area, thighs and legs.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "Enjoy a relaxing massage in the comfort of your own home or office with this recliner and ottoman set. This set offers maximum massaging power that kneads your back, lumbar area, thighs and legs. Whatever your preferred intensity the five pre-programmed settings are sure to suit your needs. Look no further for your perfect massage chair offered at an incredible price!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Item Weight", Value = "54 pounds" },
                    //            new Property { Name = "Product Dimensions", Value = "29.2 x 46 x 42 inches" },
                    //            new Property { Name = "Material", Value = "Leather", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "411mFFyqj1L.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "1/411mFFyqj1L.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "41b-Zwb3zzL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "1/41b-Zwb3zzL.jpg" },
                    //        new Image { OriginalFileName = "41i7O3PuyyL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "1/41i7O3PuyyL.jpg" },
                    //        new Image { OriginalFileName = "41MmuLS2ctL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "1/41MmuLS2ctL.jpg" },
                    //        new Image { OriginalFileName = "41rZHccDDiL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "1/41rZHccDDiL.jpg" },
                    //    },
                    //    QuantityInStock = 17,
                    //    UnitPrice = 202.49M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "comfortable chair".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "leather chair".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "2PC. PADDED ROCKING CHAIR CUSHION SET - BLUE",
                    //    ShortDescription = "Our 2 piece rocking chair cushion set makes your favorite rocker extra comfy! Two piece set includes a back pad (22\" x 17\" x 3\") and seat cushion(19\" x 17\" x 3\"). Both have ties that attach easily to any rocker to keep cushions in place. Made of Poly-cotton blend with poly-fill. Color Blue. Also available in Burgundy and Beige sold on Amazon.",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "Our 2 piece rocking chair cushion set makes your favorite rocker extra comfy! Two piece set includes a back pad (22\" x 17\" x 3\") and seat cushion(19\" x 17\" x 3\"). Both have ties that attach easily to any rocker to keep cushions in place. Made of Poly-cotton blend with poly-fill. Color Blue. Also available in Burgundy and Beige sold on Amazon.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Product Dimensions", Value = "24.5 x 15.5 x 5 inches" },
                    //            new Property { Name = "Manufacturer", Value = "PADDED CUSHIONS" },
                    //            new Property { Name = "Material", Value = "Fabric, Wood", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "blue", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41aiy1QM0AL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "2/41aiy1QM0AL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "51NbRhyv4DL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "2/51NbRhyv4DL.jpg" },
                    //    },
                    //    QuantityInStock = 49,
                    //    UnitPrice = 37.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "living room chair".ToLower()),
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "blue chair".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Sauder Palladia Armoire, Cherry",
                    //    ShortDescription = "This Product Garment rod behind doors .Drawer with easy-glide metal runners. Made in USA. This Product is of high Quality. A must buy Product.",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "This Product Garment rod behind doors .Drawer with easy-glide metal runners. Made in USA. This Product is of high Quality. A must buy Product.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Item Weight", Value = "135 pounds" },
                    //            new Property { Name = "Product Dimensions", Value = "36.3 x 21.4 x 66.6 inches" },
                    //            new Property { Name = "Material", Value = "Wood", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "brown", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "71R91VCsciL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "3/71R91VCsciL._SL1000_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "715FCSK4N7L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "3/715FCSK4N7L._SL1000_.jpg" },
                    //    },
                    //    QuantityInStock = 10,
                    //    UnitPrice = 246.38M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cherry wood".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Rolando Grey & Black Rolling Wardrobe Trunk",
                    //    ShortDescription = "The Rolando Rolling wardrobe offers a sleek and sophisticated solution to all of your clothing storage needs. Built with antique chrome hinges, this trunk open to reveal spacious interior to hang or put away clothes.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "The Rolando Rolling wardrobe offers a sleek and sophisticated solution to all of your clothing storage needs. Built with antique chrome hinges, this trunk open to reveal spacious interior to hang or put away clothes. The entire piece is built on black rubber wheels for easy mobility. Inspired by vintage design, this trunk exudes a traditionally trendy feel with attention to detailing, making for an impressive and functional piece. Dimensions: 24.70\" L x 22\" W x 59.30\" H",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Item Weight", Value = "187 pounds" },
                    //            new Property { Name = "Product Dimensions", Value = "24.7 x 22 x 59.3 inches" },
                    //            new Property { Name = "Material", Value = "wood, leather, chrome", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "black, grey", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "51jRMxHbWnL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/51jRMxHbWnL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "51KW1wmGEXL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/51KW1wmGEXL.jpg" },
                    //        new Image { OriginalFileName = "91SjtLUIRkL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/91SjtLUIRkL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "916ruWi4aZL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/916ruWi4aZL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "A1CRuoS1hQL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/A1CRuoS1hQL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "A1d2xwYfAtL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "4/A1d2xwYfAtL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 4,
                    //    UnitPrice = 1394.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Rolando wardrobe".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Altra The Works L-Shaped Desk, CherrySlate Gray",
                    //    ShortDescription = "Furnish your home office with the Altra The Works L-Shaped Desk to work more efficiently and get more done. Perfect for a corner, this attractive desk makes a stylish addition to virtually any office space...",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "Furnish your home office with the Altra The Works L-Shaped Desk to work more efficiently and get more done. Perfect for a corner, this attractive desk makes a stylish addition to virtually any office space. The L-shape design creates a large workspace with plenty of room for a laptop, monitor, keyboard, papers, office supplies and more. Crafted in a two-tone finish of Cherry and Slate Gray, this contemporary styled desk look great with any décor. A convenient grommet hole at the back corner lets you conceal wires and electrical cords for your computer accessories and other devices. Finish off your desk with the matching The Works Hutch – sold separately. Altra The Works L-Shaped Desk requires assembly upon delivery.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Manufacturer", Value = "Dorel Home Furnishings" },
                    //            new Property { Name = "Product Dimensions", Value = "52 x 52 x 29.1 inches" },
                    //            new Property { Name = "Material", Value = "Wood", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "cherry, slate grey", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81mXwiALh3L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "5/81mXwiALh3L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81o0QUxNo1L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "5/81o0QUxNo1L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "61WRGgZjgxL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "5/61WRGgZjgxL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71-4wd+qLgL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "5/71-4wd+qLgL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81Ie6s27iYL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "5/81Ie6s27iYL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 11,
                    //    UnitPrice = 94.49M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "L-Shaped Desk".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Z-Line Nero Desk and Bookcase",
                    //    ShortDescription = "Nero desk and bookcase.",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "Contemporary black glossy powder coat frame; 8mm / 5mm clear tempered safety glass; Pullout keyboard tray with room for a mouse; Spacious desktop workspace ideal for any home or office; Attached 3 - tier bookcase for added workspace",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Item Weight", Value = "62.8 pounds" },
                    //            new Property { Name = "Product Dimensions", Value = "24 x 57.5 x 40 inches" },
                    //            new Property { Name = "Material", Value = "metal", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "611Z1dUWArL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "6/611Z1dUWArL._SL1000_.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 31,
                    //    UnitPrice = 104.43M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Z-Line Nero Desk".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Orlando White and Orange Dining Table - Creative Furniture",
                    //    ShortDescription = "This tiny Orlando White and Orange Dining Table will easily seat a large family gathering around its stunning borders. Sturdy supports hold up the orange high gloss top. Its folding design and space saving function is so convenient in small rooms where space is limited.",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "This tiny Orlando White and Orange Dining Table - Creative Furniture will easily seat a large family gathering around its stunning borders. Sturdy supports hold up the orange high gloss top. Its folding design and space saving function is so convenient in small rooms where space is limited. This dining table may well be the perfect choice if you have a small and modern dining room, and enjoy entertaining. Features: Category: Dining table Orlando Collection Folding design Contemporary style White and Orange finish Wood and wood products Square/ rectangular shape Dimensions: Table: 30\"W x 34\"H x 44\"L",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Product Dimensions", Value = "44 x 30 x 34 inches" },
                    //            new Property { Name = "Material", Value = "wood, glass", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "white, orange", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "71HSrS-KhrL._SL1461_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "7/71HSrS-KhrL._SL1461_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71xiLhkYDcL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "7/71xiLhkYDcL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "715Obl5olKL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "7/715Obl5olKL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 2,
                    //    UnitPrice = 575.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Orlando Dining Table".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Handi-Craft 3 Piece Compact Dining Set wTable and Matching Chairs",
                    //    ShortDescription = "Handi-craft brings you a 3-piece compact dining set at an affordable price. treat yourself to a new compact dining set with two chairs included for immediate use.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "Handi-craft brings you a 3-piece compact dining set at an affordable price. treat yourself to a new compact dining set with two chairs included for immediate use. Add a new table at your home, office, apartment, dorm, or party, our 3-piece compact dining set is designed for quick assembly with all instructions included. The table top and chairs are made of MDF wood with an elegant dark walnut PVC veneer for a real wood look and feel. It is easy to wipe clean with normal household wipes. The frame is built with powder coated dark brown metal tubes. This dining set will look great in any environment. Handi-craft offers a line of affordable DIY household products and furniture. - Dining set at an affordable price. - Easy care. - Contemporary look. - Fade-resistant and stain-resistant. - Immediate use.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Product Dimensions", Value = "32.5 x 5.5 x 21.5 inches" },
                    //            new Property { Name = "Item Weight", Value = "30 pounds" },
                    //            new Property { Name = "Material", Value = "wood, pvc", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "brown", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41Y4VCdzX6L.jpg", FileExtension = ".jpg", UrlPath = "8/41Y4VCdzX6L.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81q67HE8T0L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "8/81q67HE8T0L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81ZuoeobWWL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "8/81ZuoeobWWL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "91GKHKLCQPL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "8/91GKHKLCQPL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "A1UKg7ndUFL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "8/A1UKg7ndUFL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 32,
                    //    UnitPrice = 67.93M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Handi-craft".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Modern Linen Fabric Small Space Sectional Sofa with Reversible Chaise (Green)",
                    //    ShortDescription = "Divano Roma Furniture Presents this small space configurable reversible chaise lounge. Soft linen fabric upholstery on hardwood frame with overstuffed back cushions and memory foam seat cushion.",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "Modern linen fabric sectional sofa with reversible chaise lounge in a variant of colors; Features soft fabrics and fun colors on hardwood frame, overstuffed cushions and two decorative pillows in the same fabric; Small space configurable sectional, allowing to position chaise on either end; Dimensions: Overall - 76\"W x 50\"D x 28\"H, Seat- 66\"W x 22\"D, Back Rest- 17\"H; Minor Assembly Required. All hardware and instructions included.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Product Dimensions", Value = "28 x 76 x 50 inches" },
                    //            new Property { Name = "Material", Value = "fabric", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "green", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "91ViLre38KL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "9/91ViLre38KL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "A1Z0+fOjpgL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "9/A1Z0+fOjpgL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71a+NV-gAPL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "9/71a+NV-gAPL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81hgT+iOklL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "9/81hgT+iOklL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81wXvn6ajzL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "9/81wXvn6ajzL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 4,
                    //    UnitPrice = 175.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Sectional Sofa".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Merax 55-74\" Multicolor Adjustable Loveseat Home Furniture Sofa with 2 Free Pillows, Colorful",
                    //    ShortDescription = "With its minimalist looks and clean lines, the sofa is designed to be seen and shared with friends. Comfort too is a top priority and the adjustable angled back guarantees it will feel great all day, every day.",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "With its minimalist looks and clean lines, the sofa is designed to be seen and shared with friends. Comfort too is a top priority and the adjustable angled back guarantees it will feel great all day, every day. The fold-down seat back and armrests also brings convenience to the couch.We advise to mat something below the back to increase the stability. Use this lovely sofa as the focal point of your contemporary living room. This piece's urban design makes it a great sofa for a young person's first apartment. This versatile piece has simple lines that work well with modern decor, and its warm tonal and unique styling have an organic feel that blends well in home. Its deceptively compact with proportions that won’t overwhelm your room. Weight limit: 500lbs. We advise to mat something below the back to increase the stability when the sofa is folded down.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Product Dimensions", Value = "58.2 x 36.2 x 23.6 inches" },
                    //            new Property { Name = "Item Weight", Value = "54 pounds" },
                    //            new Property { Name = "Material", Value = "Cotton", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Material".ToLower()) },
                    //            new Property { Name = "Color", Value = "multiple", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "51xpnc-M2cL.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "10/51xpnc-M2cL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71kuQUV6omL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "10/71kuQUV6omL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71wth789j-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "10/71wth789j-L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71yqAAZ4qpL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "10/71yqAAZ4qpL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "715+OI4CAeL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = furniturePath + "10/715+OI4CAeL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 4,
                    //    UnitPrice = 239.90M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Adjustable sofa".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Heath and Beauty
                    //new Product
                    //{
                    //    Title = "Marvelous Moxie Lipgloss - Rebel",
                    //    ShortDescription = "Contains natural ingredients, exotic oils, botanical extracts & antioxidants. With a refreshing formula that nourishes lips for exceptional comfort. Provides sheer, vibrant color. Creates a full, satiny, shimmering & mirror-like pout that lasts for hours.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "Contains natural ingredients, exotic oils, botanical extracts & antioxidants. With a refreshing formula that nourishes lips for exceptional comfort. Provides sheer, vibrant color. Creates a full, satiny, shimmering & mirror-like pout that lasts for hours. Ingredients: Hydrogenated Polyisobutene, Tridecyl Trimellitate, Polyglyceryl - 2 Triisostearate, Octyldodecanol, Bis - Diglyceryl Polyacyladipate - 2, Microcrystalline Wax(Cera Microcristallina), Trimethylolpropane Triisostearate, Stearalkonium Hectorite, Menthone Glycerin Acetal, Cetearyl Ethylhexanoate, Vp / Hexadecene Copolymer, Butyrospermum Parkii(Shea) Butter, Persea Gratissima(Avocado) Butter, Silica Dimethyl Silylate, Mentha Piperita(Peppermint) Oil, Euphorbia Cerifera(Candelilla) Wax, Vanillin, Astrocaryum Murumuru Seed Butter, Lavandula Angustifolia(Lavender) Flower Wax, Cinnamomum Cassia Leaf Oil, Sorbitan Isostearate, Retinyl Palmitate, Tocopherol, Ascorbyl Palmitate, Eugenol, Portulaca Pilosa Extract, Linalool, Sucrose Cocoate, Copper Gluconate, Sodium Chloride, Zinc Gluconate, Benzyl Benzoate, Palmitoyl Tripeptide - 38, Mica(Ci 77019).May Contain(+/ -): Titanium Dioxide(Ci 77891), Red 28(Ci 45410), Red 27(Ci 45410), Yellow 5(Ci 19140), Blue 1(Ci 42090), Iron Oxides(Ci 77491, 77492, 77499), Red 6(Ci 15850).",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Specific properties", Value = "Cruelty Free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Color", Value = "Pink Mauve", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "31wVYvB764L.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "1/31wVYvB764L.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 216,
                    //    UnitPrice = 24.95M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cruelty free".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "W3LL PEOPLE - Expressionist Mascara (PRO BROWN)",
                    //    ShortDescription = "The trailblazing W3LL PEOPLE Beauty Dream Team is taking pretty to a more positive place with their unique integrated approach to healthy beauty.",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "The trailblazing W3LL PEOPLE Beauty Dream Team is taking pretty to a more positive place with their unique integrated approach to healthy beauty. This new cult favorite is known for their pure, positive, performance makeup that’s winning every award under the sun. Now they’ve curated their best seller into an exclusive duo set at a radical value for you. Revel in their toxinfree makeup, powered by organic botanicals, that delivers extraordinary performance that’s better than conventional cosmetics!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Specific properties", Value = "Hypoallergenic", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Color", Value = "brown", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61NiG49nEsL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "2/61NiG49nEsL._SL1500_.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 60,
                    //    UnitPrice = 27.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "brown mascara".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Bel Ami By Hermes Eau De Toilette Spray 3.4 Oz For Men",
                    //    ShortDescription = "Beautiful and distinctive, TrendToGo brings you another fine fragrance from Hermes ALL Fragrances are 100% Guaranteed Authentic. Add it to your cart now: BEL AMI by Hermes EDT SPRAY 3.3 OZ for MEN Gender: Men's Brand: Hermes",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "Beautiful and distinctive, TrendToGo brings you another fine fragrance from Hermes ALL Fragrances are 100% Guaranteed Authentic. Add it to your cart now: BEL AMI by Hermes EDT SPRAY 3.3 OZ for MEN Gender: Men's Brand: Hermes",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Specific properties", Value = "Cruelty Free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "314EWbNdV1L.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "3/314EWbNdV1L.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 30,
                    //    UnitPrice = 125.71M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Cruelty Free Eau De Toilette".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "1st Femme Beauty Fragrance",
                    //    ShortDescription = "This 1st class scent, is an exotic and powdery sweet floral fragrance...Perfect for the smart, sophisticated ingénue on the go. With a soft effervescent mix of freesia and jasmine blossom, creamy silk vanilla and fresh zesty touches of green, this unique and intoxicating scent is meant to...",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "This 1st class scent, is an exotic and powdery sweet floral fragrance...Perfect for the smart, sophisticated ingénue on the go. With a soft effervescent mix of freesia and jasmine blossom, creamy silk vanilla and fresh zesty touches of green, this unique and intoxicating scent is meant to evoke your most touching memories and is also the perfect accompaniment to take you from the business room to an impromptu getaway in the Maldives. Our base notes include silky musk and creamed vanilla. We put silky musk in our fragrance, as it evokes an element of status and is the most expensive natural product in the world, even more expensive than gold. Vanilla, is the second most expensive spice in the world, next to saffron, which again inspires an element of luxury in the woman wearing it. Our mid notes are jasmine blossom accord and freesia. Jasmine, is one of the most commonly used oils in meditation, engendering feelings of harmony and optimism. When you smell jasmine you know that you are getting a delicate yet rich product. Freesia, embodies freshness at the right intensity, it was added to the mix, to ensure that the fragrance radiates from a long distance. And lastly, the top notes are fresh green nuances, which provide variance and adaptability. The 1st femme fragrance, can take you from the business room to your honeymoon and still be able to sensually communicate the various moods you may encounter in the course of your day.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Specific properties", Value = "Natural", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "814xfBs7qGL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "4/814xfBs7qGL._SL1500_.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 46,
                    //    UnitPrice = 88.25M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Cruelty Free Fragrance".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "St James of London Cedarwood & Clarysage Shaving Cream",
                    //    ShortDescription = "Formerly known as founder's reserve, our all natural luxurious shave cream blends the exquisite top-notes of cedar wood & clarysage and essential oils.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "Formerly known as founder's reserve, our all natural luxurious shave cream blends the exquisite top-notes of cedar wood & clarysage and essential oils. Our creams offer an amazingly silky smooth glide shave making this daily ritual something to look forward to. Top-shelf, rich and creamy, explodes with lather. Made in England. Elegantly packaged and presented in a beautiful heavy glass bowl with built-in lid seal to maintain freshness.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Specific properties", Value = "Natural, Alcohol Free, sulfate free, paraben free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "For Him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "619ZKJo1wWL._SX522_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "5/619ZKJo1wWL._SX522_.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 52,
                    //    UnitPrice = 25.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Alcohol Free Shaving Cream".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Hypoallergenic Hair Removing Strips for Face with Beeswax and Cotton Seed Oil Elea - 16 pcs + Calming Balm 15 g. / 0.5 oz.",
                    //    ShortDescription = "Hypoallergenic Hair Removing Strips for Face with Beeswax and Cotton Seed Oil Elea - 16 pcs + Calming Balm 15 g. / 0.5 oz. Body Hair Removing Strips with Cotton Seed Oil and Beeswax from Elea...",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "Hypoallergenic Hair Removing Strips for Face with Beeswax and Cotton Seed Oil Elea - 16 pcs + Calming Balm 15 g. / 0.5 oz. Body Hair Removing Strips with Cotton Seed Oil and Beeswax from Elea - offer a fast, easy and efficient way for removing unwanted body hair. It works even on shorter hairs, provides a long-lasting effect (up to 4 weeks) and leaves the skin soft and smooth with a pleasant fresh fragrance.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Specific properties", Value = "Hypoallergenic", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41dj1tIOxmL.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "6/41dj1tIOxmL.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 26,
                    //    UnitPrice = 19.90M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //},
                    //new Product
                    //{
                    //    Title = "Rene Furterer Lissea Smoothing Shampoo, 6.76 fl. oz.",
                    //    ShortDescription = "Smoothing shampoo for unruly and frizzy hair. Shampoo with natural Alkekenge extract to smoothe hair while controlling frizz and volume.",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "René Furterer pioneered the art of creating a healthy environment for hair and scalp using plant extracts and essential oils. From styling products to scalp treatments, the renowned line is a go-to for industry professionals. 6.76 oz shampoo. Lessee smoothing shampoo was launched by the design house of rene furthered. It is recommended for casual wear. Lessee smoothing shampoo by rene furthered for unisex - 676 oz shampoo.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Hair Type", Value = "Curly" },
                    //            new Property { Name = "Specific properties", Value = "Sulfate Free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "unisex", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "716P7dnrZ8L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "7/716P7dnrZ8L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "91H3rEPmlzL._SY679_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "7/91H3rEPmlzL._SY679_.jpg" },
                    //        new Image { OriginalFileName = "81nQwf1k0vL._SX522_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "7/81nQwf1k0vL._SX522_.jpg" },
                    //    },
                    //    QuantityInStock = 78,
                    //    UnitPrice = 28.50M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Rene Furterer shampoo".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Ducray Kelual DS Shampoo, 3.3 fl. oz.",
                    //    ShortDescription = "Soothing and calming shampoo for scalp prone to seborrheic dermatitis.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "Ducray offers hair care solutions adapted to every need that restore and preserve the health and beauty of the hair. A shampoo specifically formulated to exfoliate and soothe scalp prone to seborrheic dermatitis. Calms itching from the very first use.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Hair Type", Value = "Dandruff" },
                    //            new Property { Name = "Specific properties", Value = "Paraben Free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "unisex", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "71aENZwXrgL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "8/71aENZwXrgL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71RJb7d1RML._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "8/71RJb7d1RML._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71AfaWAlaDL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "8/71AfaWAlaDL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 106,
                    //    UnitPrice = 28.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Ducray Shampoo".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "AmLactin Alpha-Hydroxy Therapy Moisturizing Body Lotion for Dry Skin, Fragrance-Free, 15.8oz Twin Pack (7.9oz per bottle)",
                    //    ShortDescription = "AmLactin Moisturizing Body Lotion has a special formula with clinically proven 12% lactic acid that’s pH balanced for the skin. Don’t let the word 'acid' concern you - especially since lactic acid is a naturally occurring humectant for the skin with a certain affinity for water molecules to help keep skin hydrated.",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "AmLactin Moisturizing Body Lotion has a special formula with clinically proven 12% lactic acid that’s pH balanced for the skin. Don’t let the word 'acid' concern you - especially since lactic acid is a naturally occurring humectant for the skin with a certain affinity for water molecules to help keep skin hydrated. And the more moisture that can be retained deep within the skin, the softer and smoother your skin feels. By encouraging natural skin cell renewal through exfoliation and delivering intense hydration deep within the skin, this lotion creates a soft, smooth texture you’ll love.If you suffer from dry skin, you know there’s a big difference between short-term relief and long-term therapy. While traditional moisturizers provide superficial results, AmLactin Skin Care is different. Our unique alpha-hydroxy therapy exfoliates, retains the skin's natural moisture, and draws water to the skin to hydrate so it looks and feels soft and smooth.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Skin Type", Value = "Dry" },
                    //            new Property { Name = "Specific properties", Value = "Unscented, Oil Free", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "unisex", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61ykcNPgHWL._SL1204_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "9/61ykcNPgHWL._SL1204_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71gJO6hfjnL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "9/71gJO6hfjnL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81Irv9aikBL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "9/81Irv9aikBL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "91q3LrMf3FL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "9/91q3LrMf3FL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 21,
                    //    UnitPrice = 16.62M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "AmLactin Body Lotion".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Tummy Butter for Stretch Marks ~ Safe for Pregnancy - 4 oz.",
                    //    ShortDescription = "Looking to prevent or treat stretch marks? See why The Spoiled Mama's Tummy Butter is trusted by mamas around the globe...",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "Looking to prevent or treat stretch marks? See why The Spoiled Mama's Tummy Butter is trusted by mamas around the globe. Tummy Butter stretch marks lotion penetrates into the deepest skin layers ; while creating a protective shield that locks in moisture and nourishes your growing belly. Our Special stretch marks Butter blend will help fade scars and stretch marks and prevent new ones from appearing. Works wonders on C-Section scars too!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Specific properties", Value = "Organic", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Specific properties".ToLower()) },
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61uoY7OboiL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "10/61uoY7OboiL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "610Z8FEBDZL._SL1326_.jpg", FileExtension = ".jpg", UrlPath = healthBeautyPath + "10/610Z8FEBDZL._SL1326_.jpg" },
                    //    },
                    //    QuantityInStock = 62,
                    //    UnitPrice = 39.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Heath and Beauty").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "maternity cosmetics".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Notebooks
                    //new Product
                    //{
                    //    Title = "Apple MMGG2LL/A MacBook Air 13.3-Inch Laptop (Intel Core i5, 8GB, 256GB,Mac OS X), Silver",
                    //    ShortDescription = "1.6GHz dual-core Intel Core i5 processor, Turbo Boost up to 2.7GHz; Intel HD Graphics 6000; 8GB memory; 256GB PCIe-based flash storage; 12hr Battery life",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "1.6GHz dual-core Intel Core i5 processor, Turbo Boost up to 2.7GHz; Intel HD Graphics 6000; 8GB memory; 256GB PCIe-based flash storage; 12hr Battery life",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "13.3 inches", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "8 GB LPDDR3", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "1.6 GHz Intel Core i5", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41AerRC5u6L.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "1/41AerRC5u6L.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 4,
                    //    UnitPrice = 1179.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "MacBook Air".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Apple MacBook Air MJVP2LLA 11.6-Inch Laptop (256 GB)",
                    //    ShortDescription = "1.6GHz dual-core Intel Core i5 processor; Turbo Boost up to 2.7GHz; Intel HD Graphics 6000; 4GB memory; 256GB PCIe-based flash storage",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "Fifth-generation Intel processors: designed to reduce power consumption while maintaining high performance. Next-generation graphics: Intel HD Graphics 6000 is the next generation of advanced integrated graphics and delivers fast graphics performance for immersive mainstream gaming and smooth scrolling through large music or photo libraries. Multi-Touch trackpad: supports all the Multi-Touch gestures Mac users love including tap, scroll, pinch, and swipe",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "11.6 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "4 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "1.6GHz dual-core Intel Core i5 (Broadwell), Turbo Boost up to 2.7GHz", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "915UBjNfA4L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "2/915UBjNfA4L._SL1500_.jpg", IsMainImage = true },
                    //    },
                    //    QuantityInStock = 2,
                    //    UnitPrice = 799.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "MacBook Air".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Samsung Tab Pro S 12\" 128 GB Wifi Tablet(Black) SM - W700NZKAXAR",
                    //    ShortDescription = "Samsung Galaxy TabPro S, quick start guide, customer service insert, travel adapter, data cable, and keyboard.",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "Built for both work and play, the thin, lightweight Samsung Galaxy TabPro S lets you take your digital content with you wherever you go. This tablet features a Super AMOLED touchscreen, powerful Intel Core M3 processor, long-lasting battery, and a full-size keyboard, so you can get business done from anywhere. Plus, Samsung Flow lets you easily connect and share content between your Samsung devices. The ultrathin Galaxy TabPro S is light and comfortable to hold, so you can take it with you anywhere you go. Slip it into a backpack, briefcase, or handbag for travel. The Galaxy TabPro S runs the Windows 10 operating system to deliver performance similar to a PC. It combines a powerful Intel Core M3 processor with 4GB RAM and a 128GB solid state drive to give you the speed and capacity you need for any task. Navigate quickly between multiple windows when writing reports or quickly switch between your favorite apps. Thanks to its 2160x1440 pixel Super AMOLED display, the Galaxy TabPro S brings movies, presentations, and games to life with deep contrast, rich colors, and crisp detail.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "12 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "4GB RAM", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "Intel Core m3 Processor", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61omrmSNBwL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "3/61omrmSNBwL._SL1000_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81FE56saZ8L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "3/81FE56saZ8L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81iraCUK6VL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "3/81iraCUK6VL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 2,
                    //    UnitPrice = 749.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Samsung Tab Pro tablet".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "ASUS Transformer Book T100HA-C4-GR 10.1-Inch 2 in 1 Touchscreen Laptop (Cherry Trail Quad-Core Z8500 Processor, 4GB RAM, 64GB Storage, Windows 10), Gray",
                    //    ShortDescription = "Thinner. Faster. And an Amazing 12-hour Battery Life. As the proud successor of the top-selling ASUS Transformer Book T100TA, the new T100HA gets a 20% performance boost with the new quad-core Intel ‘Cherry Trail’ processor and 4GB RAM installed. Measuring only 0.72-inch thin and 2.28 pounds light, the T100HA is 20% thinner than its predecessor, making it even more portable and easier to carry around wherever you go.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "As the proud successor of the top-selling ASUS Transformer Book T100TA, the new T100HA gets a 20% performance boost with the new quad-core Intel ‘Cherry Trail’ processor and 4GB RAM installed. Measuring only 0.72-inch thin and 2.28 pounds light, the T100HA is 20% thinner than its predecessor, making it even more portable and easier to carry around wherever you go. And with up to an incredible 12 hours of battery life, it’s always ready — for work and play! Reengineered with technology from the flagship Transformer Book Chi series, the T100HA integrates four neodymium magnets to ensure the tablet and keyboard connect with ease, precision and strength. The self-aligning magnetic hinge design allows for lightning-fast transformation and ensures a strong connection when docked. At just 2.28 pounds and 0.72-inch, the lightweight and compact T100HA is the perfect grab-and-go device that fits in your purse, backpack or messenger bag to accompany you anywhere your day takes you.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "10.1 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "4 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "Intel Atom Quad-Core Cherry Trail x5-Z8500 1.44GHz (Turbo up to 2.24GHz)", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81Gz+fLD1ML._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "4/81Gz+fLD1ML._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81HgjsZ5lFL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "4/81HgjsZ5lFL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81xlC4V9akL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "4/81xlC4V9akL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 12,
                    //    UnitPrice = 299.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "ASUS Transformer Book".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Samsung Chromebook Plus Touch-Screen Laptop XE513C24-K01US",
                    //    ShortDescription = "Chromebook plus is the next generation of premium Chromebook with the flexibility of a tablet. Chromebook Pro is designed and optimized for Andriod apps, and is the first Chromebook designed with an integrated pen.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "Whatever your day brings, the Samsung Chromebook Plus is up for it. With the power of a Chromebook and the versatility of a tablet, the 360° rotating screen helps you get things done or just kick back. Add a personal touch to your notes with the built-in pen. Access your favorite apps right on your Samsung Chromebook Plus. And keep watching, gaming, working, and creating anywhere, even when you’re offline. The Samsung Chromebook Plus adapts to whatever you’re doing. Use it like a laptop to reply to emails or to work on a paper. When you need a break, flip the screen so you can play games or catch up on your latest book.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "12.3 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "4 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "OP1, Made for Chromebooks, Hexa-core", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81-85Cr6tEL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "5/81-85Cr6tEL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81HlS3UAMNL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "5/81HlS3UAMNL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81qUGJSCqzL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "5/81qUGJSCqzL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "91inJd-RDKL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "5/91inJd-RDKL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 33,
                    //    UnitPrice = 449.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Samsung Chromebook".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Lenovo Yoga 710 15 - 15.6\" FHD Touch - Core i5 - 6200U up to 2.8Ghz - 8GB - 256GB SSD",
                    //    ShortDescription = "Enjoy a strong, stable Wi-Fi connection with the unique antenna on this Lenovo Yoga laptop. It has a 15-inch IPS touch screen with 1920 x 1080 resolution that looks crisp and clear, even when viewed from an angle. This Lenovo Yoga laptop has 8GB of onboard RAM for efficient multitasking and a 360-degree hinge for tablet or laptop use.",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "15.6\" Full HD 10 - point multitouch screen for hands - on control: The 1920 x 1080 resolution boasts impressive color and clarity.Touch, tap, glide and make the most of Windows 10.IPS technology.LED backlight. 6th Gen Intel® Core™ i5-6200U mobile processor: Ultra-low-voltage platform. Dual-core, four-way processing performance. Intel Turbo Boost Technology delivers dynamic extra power when you need it. 8GB system memory for advanced multitasking: Substantial high-bandwidth RAM to smoothly run your games and photo- and video-editing applications, as well as multiple programs and browser tabs all at once. 360° flip-and-fold design: Offers four versatile modes — laptop, tablet, tent and stand. Lenovo Transition automatically switches specific applications to full screen when changing from PC to tablet, tent or stand position.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "15.6 inches", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "8 GB DDR4 SDRAM", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "2.3 GHz Intel Core i5 6200U", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61C9+HN8yDL._SL1318_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "6/61C9+HN8yDL._SL1318_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61GozNFPfkL._SL1070_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "6/61GozNFPfkL._SL1070_.jpg" },
                    //        new Image { OriginalFileName = "61RLS2JCHUL._SL1154_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "6/61RLS2JCHUL._SL1154_.jpg" },
                    //        new Image { OriginalFileName = "612ShD9DIeL._SL1328_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "6/612ShD9DIeL._SL1328_.jpg" },
                    //    },
                    //    QuantityInStock = 5,
                    //    UnitPrice = 687.22M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Lenovo Yoga laptop".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "ASUS ZenBook Pro UX501VW 15.6-Inch 4K Touchscreen Laptop (Core i7-6700HQ CPU, 16 GB DDR4, 512 GB NVMe SSD, GTX960M GPU, Thunderbolt III, Windows 10 Home)",
                    //    ShortDescription = "Asus Silver Touch Screen NB Zenbook, Windows 10 Professional (64bit), 15.6\" UHD, glossy, Intel Quad - Core i7 - 6700HQ, 8GB DDR4, Nvidia GTX960M 2G GDDR5, 512GB M.2 SSD, 802.11AC, HD Camera, Illuminated Chiclet Keyboard, Bluetooth 4.0",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "Imagine what you can do with the power and mobility to not only create visual masterpieces but to do so wherever you want. The stunning ASUS ZenBook Pro UX501 redefines its class combining high standard craftsmanship and high-performance components. Featuring a 15.6-inch IPS touchscreen display with 4K resolution and a host of technologies that all add up to breathtaking clarity and definition. Inspired by Zen inside and out, do what you thought was only possible before on a home PC on the move. With an astonishing resolution of 3840 by 2160 pixels - that’s four times more than Full HD - the state-of-the-art 4K/UHD VisualMaster display on ZenBook Pro is something you’ll never get tired of looking at. The touchscreen has 10 points of touch control for smart gesture navigation through webpages and documents. ZenBook Pro UX501 flourishes for photoshopping, video editing and any of your design-related tasks. With ASUS VisualMaster display on the ZenBook Pro features a wide color gamut of 72% NTSC, 100% sRGB and 74% Adobe RGB.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "15.6 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "16 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "2.6 GHz Intel Core i7", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "41Wn6Eth-BL.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "7/41Wn6Eth-BL.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "41BNALx21rL.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "7/41BNALx21rL.jpg" },
                    //        new Image { OriginalFileName = "41uc5o6Si8L.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "7/41uc5o6Si8L.jpg" },
                    //        new Image { OriginalFileName = "51IW5gSSc5L.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "7/51IW5gSSc5L.jpg" },
                    //        new Image { OriginalFileName = "91XNpxjYxhL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "7/91XNpxjYxhL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 42,
                    //    UnitPrice = 1499.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "ASUS ZenBook".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Dell Inspiron i5765-1317GRY 17.3\" FHD Laptop(7th Generation AMDA9 - 9400, 8GB RAM, 1TB HDD)",
                    //    ShortDescription = "The Inspiron 17 5000 is a great desktop Replacement. It reduces the clutter of a conventional stationary setup, allows you to take your projects on the go occasionally (to the living room or to the beach) and provides an expansive 17.3\" screen ideal for working on big projects or watching a movie with friends.",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "Dynamic display: Enjoy a vivid new view with the Inspiron 17’s superior display — the expansive 17.3 inch screen creates an immersive experience that you won’t want to put down. HD+ offers impressive clarity with 37% more pixels than regular HD screens. Studio-quality sound: Whether you’re mixing, streaming or chatting, Waves MaxxAudio delivers lower lows, higher highs and outstanding audio performance. We want you to love your new PC for years to come. That's why we test Inspiron laptops for reliability not just in the places where you expect it, but for the open road ahead.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "17.3 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "8 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "2.4 GHz AMD A-Series", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "71eQZ7JkUaL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "8/71eQZ7JkUaL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71j8PxE2mpL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "8/71j8PxE2mpL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71zAtITFIyL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "8/71zAtITFIyL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81TpdXQNx-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "8/81TpdXQNx-L._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 73,
                    //    UnitPrice = 519.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Dell Inspiron".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "HP 14-an010nr 14-Inch Laptop (AMD E2, 4GB RAM, 32GB Hard Drive)",
                    //    ShortDescription = "Accomplish more with your day. Tackle all your daily tasks with an affordable laptop that comes packed with the features you need.",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "With the latest AMD processors and plenty of storage space, you can work, play, multitask, and store more of what matters to you. Get reliable power and storage you can trust. The crisp HD screen lets you enjoy your photos, videos, and web pages in detail. Life can be unpredictable, but your HP laptop shouldn’t be. So whether it’s last minute projects or spontaneous movie nights, enjoy outstanding performance backed by over 200 tests.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "14.0-inch", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "4GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "AMD E-Series Quad-Core E2-7110 APU (1.8 GHz, 2MB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81xhFn7E-JL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "9/81xhFn7E-JL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81Kvh1BZnaL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "9/81Kvh1BZnaL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81QfWX9UK9L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "9/81QfWX9UK9L._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81BgAq1i7hL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "9/81BgAq1i7hL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 80,
                    //    UnitPrice = 209.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "hp laptop".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "HP ProBook 450 G4 15.6\" Business Ultrabook: Intel 7th Core i7 - 7500U | 256GB SSD | 8GB DDR4 | (1920x1080) FHD | DVD | Back - lit | FingerPrint - Windows 10 Pro",
                    //    ShortDescription = "Built for productivity, the newest HP ProBook 450 delivers the performance and security features essential for today’s workforce. The sleek and tough design provides professionals a flexible platform to stay productive in or out of the office. Ideal for professionals in corporate settings or small to medium businesses, wanting an affordable combination of innovation, essential security and multimedia capabilities",
                    //    //DescriptionId = 10,
                    //    Description = new Description
                    //    {
                    //        Content = "Built for productivity, the newest HP ProBook 450 delivers the performance and security features essential for today’s workforce. The sleek and tough design provides professionals a flexible platform to stay productive in or out of the office. Ideal for professionals in corporate settings or small to medium businesses, wanting an affordable combination of innovation, essential security and multimedia capabilities. Mail Features: Intel Core i7 - 7500U Processor 2.7Ghz(4M Cache, up to 3.50 GHz), 256 GB SSD; 8GB DDR4 2133 Memory installed, 15.6\" HD anti-glare Full HD (1920x1080) with Intel HD Graphics 620, 802.11ac Wireless, Bluetooth, VGA Ports, HDMI, Type C USB, Back - lit Keyboard, Windows 10 Professional 64",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Display Size", Value = "15.6 in", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Display size".ToLower()) },
                    //            new Property { Name = "RAM", Value = "8 GB", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "RAM".ToLower()) },
                    //            new Property { Name = "Processor", Value = "Intel Core i7-7500U Processor 2.7Ghz (4M Cache, up to 3.50 GHz )", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Processor".ToLower()) },
                    //            new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61Zq9JlEBjL._SL1280_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "10/61Zq9JlEBjL._SL1280_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61GI7jvuKYL._SL1280_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "10/61GI7jvuKYL._SL1280_.jpg" },
                    //        new Image { OriginalFileName = "51l4RGL2u+L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = notebooksPath + "10/51l4RGL2u+L._SL1000_.jpg" },
                    //    },
                    //    QuantityInStock = 22,
                    //    UnitPrice = 779.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "HP ProBook".ToLower()),
                    //    //}
                    //},
                #endregion
                #region Sports Equipment
                    //new Product
                    //{
                    //    Title = "adidas Performance Women's Supernova Storm Half-Zip Jacket",
                    //    ShortDescription = "The elements have nothing on you. Especially with climaproof storm protection on your side. Our Supernova Storm women's half-zip lets you tackle wind and rain like a pro. It's the running jacket that keeps you fast and focused no matter what the weather report says.",
                    //    //DescriptionId = 1,
                    //    Description = new Description
                    //    {
                    //        Content = "The elements have nothing on you. Especially with climaproof storm protection on your side. Our Supernova Storm women's half-zip lets you tackle wind and rain like a pro. It's the running jacket that keeps you fast and focused no matter what the weather report says.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "L", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "Black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "817fCn7tMoL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "1/817fCn7tMoL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81WbEEjm1VL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "1/81WbEEjm1VL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 1,
                    //    UnitPrice = 65.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "adidas Jacket".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Adriana Arango Women's Gym Outfit Includes All 3 Pieces",
                    //    ShortDescription = "High quality activewear set, Includes 3 Pieces! Ideal for all types of exercise: Workout, Running, Yoga, or outdoor activities, Made in Colombia",
                    //    //DescriptionId = 2,
                    //    Description = new Description
                    //    {
                    //        Content = "Polyester 86.5%/Elastane 13.5%. High quality 3 Piece activewear set. Get ready to exercise with this Multi-use sportswear set. Includes all 3 pieces! Ideal for all types of exercise: Workout, Running, Yoga, and more. Imported quality fabrics. Made in Colombia.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "M", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "Grey", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61kwCcCakCL._UL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "2/61kwCcCakCL._UL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "71Bm9d03izL._UL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "2/71Bm9d03izL._UL1500_.jpg" },
                    //        new Image { OriginalFileName = "71X6nQdYOUL._UL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "2/71X6nQdYOUL._UL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 46,
                    //    UnitPrice = 39.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Adriana Arango Gym Outfit".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "TrailHeads Women’s Ponytail Headband",
                    //    ShortDescription = "The TrailHeads Women’s Ponytail Headband has a unique integrated loop construction that provides a comfortable and secure fit. Fashion meets function by combining eye-catching style with ear warmer performance. Keep your hair in place while benefiting from the warmth and softness of a top quality fleece headband.",
                    //    //DescriptionId = 3,
                    //    Description = new Description
                    //    {
                    //        Content = "The TrailHeads Women’s Ponytail Headband has a unique integrated loop construction that provides a comfortable and secure fit. Fashion meets function by combining eye-catching style with ear warmer performance. Keep your hair in place while benefiting from the warmth and softness of a top quality fleece headband. The mid - weight polyester fleece wicks moisture from your skin to keep you comfortable when running, skating, or playing in the snow with your kids.The polyester spandex trim in contrasting colors provides just enough stretch for a fit that conforms to a variety of head sizes.Winter athletes will appreciate the full ear coverage, further ensuring warmth where it is needed most. We designed this ponytail headband with aerobic activities in mind, but it also performs well in less strenuous activities such as walking your dog or watching an early winter football game.Available in a wide variety of colors, this winter headband is bound to be a favorite in your cold weather wardrobe. Trail Tested Guarantee: We’ve been designing hats, gloves, headbands and accessories since 2002 and we take great pride in our relentless commitment to quality, fit and comfort.We’re passionate about delivering exceptional customer service – your complete satisfaction is our goal.If you feel we’ve come up short, then just let us know and we’ll provide a replacement or refund – no fine print, no strings, no time limits, no shipping charges.Nothing but our commitment to do what it takes to satisfy our customers.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Color", Value = "purple/black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81QpOG8kfrL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "3/81QpOG8kfrL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81Cwb489UpL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "3/81Cwb489UpL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "81GXk6lcarL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "3/81GXk6lcarL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71a-jRtrx8L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "3/71a-jRtrx8L._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 28,
                    //    UnitPrice = 20.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "TrailHeads Headband".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "ASICS Women's Tank Top",
                    //    ShortDescription = "Our core tank features soft, breathable stretch fabric and wrap-around seams. A scooped neckline and ASICS logo at hem delivers classic style.",
                    //    //DescriptionId = 4,
                    //    Description = new Description
                    //    {
                    //        Content = "100% Polyester. Imported. Moisture management performance fabric. Seams wrapped to the back for improved comfort. Flatlock seams for chafe - free comfort. Reflective elements. Reflective prints offer added visibility. Seams wrapped to the back for improved comfort. Flatlock stitching reduces chaffing and promotes freedom of movement",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Her", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "S", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "Sulphur Green", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81f8qnIIATL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "4/81f8qnIIATL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81b5SCw0AML._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "4/81b5SCw0AML._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 3,
                    //    UnitPrice = 10.39M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "ASICS Tank Top".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "MÜV365 Ultimate Comfort Sports Running Armband for iPhone 7/6/6s Plus, Galaxy S6/S7 and All Other Phone Models With Case Up To 7”",
                    //    ShortDescription = "Looking for an armband that stands out over the rest?? Look no more! Our armbands, unlike all the others, take a totally different approach by giving you ultimate comfort in a lightweight fabric that will hold up to your active lifestyle.",
                    //    //DescriptionId = 5,
                    //    Description = new Description
                    //    {
                    //        Content = "Looking for an armband that stands out over the rest?? Look no more! Our armbands, unlike all the others, take a totally different approach by giving you ultimate comfort in a lightweight fabric that will hold up to your active lifestyle. Why is it so comfy ? Well….while other armbands are bulky and can have an uncomfortable fit due to fabric, plastic, or velcro.Ours are simple but effective!Made of 84 % Nylon and 16 % Lycra, it feels soft on your arm and has just enough stretch and compression so you can easily slide it on but be confident that it will stay in place. Another reason our MUV armbands are the best...they can accommodate ANY SIZE PHONE and slim-line case (up to 7 inches). That’s right!Whether you are running, hiking, walking the dog, doing yoga, traveling, visiting an amusement park, you name it - our armband will be the perfect solution to safely and securely store your phone, keys, passport, money and/ or cards.Simply pull back the tab, tuck your valuables inside and secure them in the pocket. Finally, slide it on your arm and go!You’ll be hands free of any clutter weighing you down or in your pockets!You will love the ease of use, comfortable fit(so comfy you won’t even know it’s there) and you’ll never have to worry again about chaffing, itchy velcro, no more pinched skin and no sliding or banging around. It’s the armband you’ve been waiting on!",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "unisex", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "M", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81qNYwPp+8L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "5/81qNYwPp+8L._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81Ghn33mQUL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "5/81Ghn33mQUL._SL1500_.jpg" },
                    //        new Image { OriginalFileName = "71ByDGvfF0L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "5/71ByDGvfF0L._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 73,
                    //    UnitPrice = 16.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "MÜV365 Armband".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Under Armour Men's Fast Logo T-Shirt",
                    //    ShortDescription = "Charged Cotton has the comfort of cotton, but dries much faster. 4-way stretch fabrication allows greater mobility in any direction. Moisture Transport System wicks sweat & dries fast.",
                    //    //DescriptionId = 6,
                    //    Description = new Description
                    //    {
                    //        Content = "57% Cotton/38% Polyester/5% Elastane; Imported; Charged Cotton has the comfort of cotton, but dries much faster; 4 - way stretch fabrication allows greater mobility in any direction; Moisture Transport System wicks sweat & dries fast",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "XL", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "Brilliant Blue/Black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "818qIVWS6cL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "6/818qIVWS6cL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81ad9svHmFL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "6/81ad9svHmFL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 5,
                    //    UnitPrice = 24.99M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Under Armour T-Shirt".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "New Balance Men's Accelerate Long-Sleeve Shirt",
                    //    ShortDescription = "Break a sweat in the New Balance accelerate long sleeve, a 100% polyester performance top that helps you stay well ventilated, silver reflective details to help keep you visible...",
                    //    //DescriptionId = 7,
                    //    Description = new Description
                    //    {
                    //        Content = "Break a sweat in the New Balance accelerate long sleeve, a 100% polyester performance top that helps you stay well ventilated, silver reflective details to help keep you visible, and a clean design to keep you motivated during your workout. Nb dry helps wick away sweat fast, leaving you comfortable and at ease during core routines. Color contrast inset panels complete the design for a bold look with maximum impact.",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "L", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "Chrome Red/Crater", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "818kHJmwjHL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "7/818kHJmwjHL._SL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81Mw2nN6LyL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "7/81Mw2nN6LyL._SL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 9,
                    //    UnitPrice = 35.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "New Balance Long-Sleeve Shirt".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "PUMA Men's Training Pant",
                    //    ShortDescription = "Conceptualized to fit with puma team training lines, covering all your essential needs. Puma cat branding, heat transfer application, polyester microfiber double knit shell fabric, mesh gusset inserts, elasticated waistband with draw cords, side pockets, zipped leg openings, articulated knee for comfort, engineered fit. Lifecycle: 4 years",
                    //    //DescriptionId = 8,
                    //    Description = new Description
                    //    {
                    //        Content = "Conceptualized to fit with puma team training lines, covering all your essential needs. Puma cat branding, heat transfer application, polyester microfiber double knit shell fabric, mesh gusset inserts, elasticated waistband with draw cords, side pockets, zipped leg openings, articulated knee for comfort, engineered fit. Lifecycle: 4 years",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "2XL", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "New Navy Blue/White", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "81abg7dp+7L._UL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "8/81abg7dp+7L._UL1500_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "81twaZKcNCL._UL1500_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "8/81twaZKcNCL._UL1500_.jpg" },
                    //    },
                    //    QuantityInStock = 3,
                    //    UnitPrice = 36.00M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "PUMA Training Pant".ToLower()),
                    //    //}
                    //},
                    //new Product
                    //{
                    //    Title = "Lemu Men's Casual Outdoor Sportswear Lightweight Bomber Jacket",
                    //    ShortDescription = "40% cotton and 60% polyester; zipper closure; Hoodie Windbreaker Jacket; Front two pockets of windbreaker jackets; Machine wash or hand wash, wash dark colors separately, do not bleach",
                    //    //DescriptionId = 9,
                    //    Description = new Description
                    //    {
                    //        Content = "40% cotton and 60% polyester; zipper closure; Hoodie Windbreaker Jacket; Front two pockets of windbreaker jackets; Machine wash or hand wash, wash dark colors separately, do not bleach",
                    //        Properties = new HashSet<Property>()
                    //        {
                    //            new Property { Name = "Sex/Gender", Value = "For Him", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                    //            new Property { Name = "Size", Value = "3XL", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                    //            new Property { Name = "Color", Value = "green", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                    //        }
                    //    },
                    //    Images = new HashSet<Image>()
                    //    {
                    //        new Image { OriginalFileName = "61qwo149C7L._UL1264_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "9/61qwo149C7L._UL1264_.jpg", IsMainImage = true },
                    //        new Image { OriginalFileName = "61vlqv68lpL._UL1226_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "9/61vlqv68lpL._UL1226_.jpg" },
                    //        new Image { OriginalFileName = "81TkDTOJtBL._UL1469_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "9/81TkDTOJtBL._UL1469_.jpg" },
                    //    },
                    //    QuantityInStock = 25,
                    //    UnitPrice = 36.76M,
                    //    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                    //    SellerId = sellerIds[random.Next(sellerIds.Count)],
                    //    //Tags = new HashSet<Tag>
                    //    //{
                    //    //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Lemu Jacket".ToLower()),
                    //    //}
                    //},
                    new Product
                    {
                        Title = "2 Tone Thai Fisherman Pants Yoga Trousers Free Size Cotton Blue and Maroon",
                        ShortDescription = "We DO NOT support sweatshops! You might be able to find lower quality cheaper pants from large factories with low wages and poor working conditions. Our pants however are sewn by a group of skilled women in the quiet home of our friend in suburban Chiang Mai Thailand.",
                        //DescriptionId = 10,
                        Description = new Description
                        {
                            Content = "cotton; Imported; Thai Cotton Drill Fisherman Yoga Pants FREE SIZE; Top quality authentic 100 % Cotton Drill \"Gangaeng Chaolay\" Thai Fisherman pants for men and women!Super - comfortable and versatile - wear them for any occasion; Thai Fisherman Pants have a very wide waist with a belt that ties from the rear.Simply step into the pants, pull the waist out to one side wrap the extra fabric around to the front and tie the belt.Length can be adjusted by folding over the top of the pants; These versatile Freesize pants measure about 56\" around the waist and total length is about 42\".One size fits most!For your reference - I am 5'10\" tall with a 33\" waist and 32\" inseam. These versatile Freesize Fisherman Pants will comfortably fit XS-XL sizes. Our high quality fisherman pants are hand made in Thailand of 100 % strong and durable heavy weight cotton drill.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Sex/Gender", Value = "unisex", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Sex/Gender".ToLower()) },
                                new Property { Name = "Size", Value = "XS/S/M/L/XL/2XL/3XL/4XL/5XL", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Size".ToLower()) },
                                new Property { Name = "Color", Value = "purple", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                            }
                        },
                        Images = new HashSet<Image>()
                        {
                            new Image { OriginalFileName = "61YBu79IRUL._SL1005_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "10/61YBu79IRUL._SL1005_.jpg", IsMainImage = true },
                            new Image { OriginalFileName = "615njbEGWVL._SL1005_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "10/615njbEGWVL._SL1005_.jpg" },
                            new Image { OriginalFileName = "61CtUdE0a8L._SL1005_.jpg", FileExtension = ".jpg", UrlPath = sportsPath + "10/61CtUdE0a8L._SL1005_.jpg" },
                        },
                        QuantityInStock = 46,
                        UnitPrice = 9.35M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports Equipment").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        //Tags = new HashSet<Tag>
                        //{
                        //    context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Thai Fisherman Yoga Pants".ToLower()),
                        //}
                    }
                #endregion
                    );

                context.SaveChanges();

                //this.SetMainImageIds(context);

                this.ProcessProductImages(context);
            }
            #endregion

            #region Images
            //if (!context.Images.Any())
            //{
                

            //    context.Images.AddOrUpdate(
            //        i => i.Id,

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel").Id, OriginalFileName = "41KLCZojhyL.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "1/41KLCZojhyL.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel").Id, OriginalFileName = "41RqSZ2LN3L.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "1/41RqSZ2LN3L.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel").Id, OriginalFileName = "61EPxCNK-9L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "1/61EPxCNK-9L._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel").Id, OriginalFileName = "71eIUJoOFJL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "1/71eIUJoOFJL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel").Id, OriginalFileName = "81HvkJgXcgL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "1/81HvkJgXcgL._SL1500_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "SPT SD-9252SS Energy Star 18\" Built - In Dishwasher, Stainless Steel").Id, OriginalFileName = "81K820M6oTL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "2/81K820M6oTL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "SPT SD-9252SS Energy Star 18\" Built - In Dishwasher, Stainless Steel").Id, OriginalFileName = "91bkQ3Oid-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "2/91bkQ3Oid-L._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "SPT SD-9252SS Energy Star 18\" Built - In Dishwasher, Stainless Steel").Id, OriginalFileName = "912wST6G8HL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "2/912wST6G8HL._SL1500_.jpg", IsMainImage = true },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "71rH1tH6pOL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "3/71rH1tH6pOL._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "81l0eW5grOL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "3/81l0eW5grOL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "71uFRVmKasL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "3/71uFRVmKasL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "81EJUhnDmmL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "3/81EJUhnDmmL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "71G-SaQzcXL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "3/71G-SaQzcXL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BFP800XL Sous Chef Food Processor, Stainless Steel").Id, OriginalFileName = "41vsyGjbk6L.jpg", FileExtension = ".jpg", UrlPath = "3/41vsyGjbk6L.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red").Id, OriginalFileName = "61Vygt70LoL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "4/61Vygt70LoL._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red").Id, OriginalFileName = "617GMI9ak3L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "4/617GMI9ak3L._SL1000_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red").Id, OriginalFileName = "61dJEp8OO0L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "4/61dJEp8OO0L._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red").Id, OriginalFileName = "61nt8UUpyzL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "4/61nt8UUpyzL._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red").Id, OriginalFileName = "61-7zp+dx8L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "4/61-7zp+dx8L._SL1000_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple").Id, OriginalFileName = "41dRCxTHQ7L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "5/41dRCxTHQ7L._SL1000_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple").Id, OriginalFileName = "51JUEoBK3nL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "5/51JUEoBK3nL._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple").Id, OriginalFileName = "51quPoAJd8L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "5/51quPoAJd8L._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple").Id, OriginalFileName = "61-ScPHiImL._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "5/61-ScPHiImL._SL1000_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple").Id, OriginalFileName = "61xr5a-ht9L._SL1000_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "5/61xr5a-ht9L._SL1000_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver").Id, OriginalFileName = "71ZOjf2YelL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "6/71ZOjf2YelL._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver").Id, OriginalFileName = "71dTMxypLVL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "6/71dTMxypLVL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver").Id, OriginalFileName = "81YLcs3SAML._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "6/81YLcs3SAML._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver").Id, OriginalFileName = "81XCc7rQDFL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "6/81XCc7rQDFL._SL1500_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "81yPz5oTwQL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/81yPz5oTwQL._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "617hYSNvgFL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/617hYSNvgFL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "81Xdd6spx1L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/81Xdd6spx1L._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "81sDBrQqkaL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/81sDBrQqkaL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "81rfOthjb-L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/81rfOthjb-L._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "81R0LZ3BlAL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/81R0LZ3BlAL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "41ycOPFHlaL.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/41ycOPFHlaL.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "41nCIWsgBGL.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/41nCIWsgBGL.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering").Id, OriginalFileName = "41Af+MLhWbL.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "7/41Af+MLhWbL.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "71ekW3x5Q4L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/71ekW3x5Q4L._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "81BXZ4jB3nL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/81BXZ4jB3nL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "81hEU4cw-kL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/81hEU4cw-kL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "811IlCEr7EL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/811IlCEr7EL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "71v8CDuZ9fL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/71v8CDuZ9fL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Vitamix 5200 Series Blender, Black").Id, OriginalFileName = "41NoqeaXK6L.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "8/41NoqeaXK6L.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BBL605XL Hemisphere Control Blender").Id, OriginalFileName = "71KtILD2oXL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "9/71KtILD2oXL._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BBL605XL Hemisphere Control Blender").Id, OriginalFileName = "61+pCoKWraL._SL1397_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "9/61+pCoKWraL._SL1397_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Breville BBL605XL Hemisphere Control Blender").Id, OriginalFileName = "81exLIGPasL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "9/81exLIGPasL._SL1500_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Ninja Professional Blender (NJ600)").Id, OriginalFileName = "71YALzAhflL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "10/71YALzAhflL._SL1500_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Ninja Professional Blender (NJ600)").Id, OriginalFileName = "81fxCRxRFGL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "10/81fxCRxRFGL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Ninja Professional Blender (NJ600)").Id, OriginalFileName = "81RWt6YF6aL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "10/81RWt6YF6aL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Ninja Professional Blender (NJ600)").Id, OriginalFileName = "81T-rhXPlQL._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "10/81T-rhXPlQL._SL1500_.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Ninja Professional Blender (NJ600)").Id, OriginalFileName = "817oxKW4T3L._SL1500_.jpg", FileExtension = ".jpg", UrlPath = appliancesPath + "10/817oxKW4T3L._SL1500_.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Python Crash Course: A Hands-On, Project-Based Introduction to Programming").Id, OriginalFileName = "51-u3J3mtTL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "1/51-u3J3mtTL.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Python Crash Course: A Hands-On, Project-Based Introduction to Programming").Id, OriginalFileName = "51LM0-8ettL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "1/51LM0-8ettL.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Python Crash Course: A Hands-On, Project-Based Introduction to Programming").Id, OriginalFileName = "41VQcpNMdwL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "1/41VQcpNMdwL.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Misty Copeland").Id, OriginalFileName = "51FllqaDNGL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "2/51FllqaDNGL.jpg", IsMainImage = true },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "LA MENTALITÉ PRIMITIVE (French Edition)").Id, OriginalFileName = "41IOuBxV-uL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "3/41IOuBxV-uL.jpg", IsMainImage = true },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Japanese with Ease, Volume 1 (Assimil with Ease) (v. 1)").Id, OriginalFileName = "51EYDrfFa7L._SX343_BO1,204,203,200_.jpg", FileExtension = ".jpg", UrlPath = booksPath + "4/51EYDrfFa7L._SX343_BO1,204,203,200_.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Japanese with Ease, Volume 1 (Assimil with Ease) (v. 1)").Id, OriginalFileName = "assimil-japanese-with-ease-volume-1-cd.png", FileExtension = ".png", UrlPath = booksPath + "4/assimil-japanese-with-ease-volume-1-cd.png" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Kann die Sonne schwimmen Ein Bilderbuch mit vielen farbigen Illustrationen ab 2 Jahren. (German Edition)").Id, OriginalFileName = "510mBd7W-RL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "5/510mBd7W-RL.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Kann die Sonne schwimmen Ein Bilderbuch mit vielen farbigen Illustrationen ab 2 Jahren. (German Edition)").Id, OriginalFileName = "51pAaq-sqdL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "5/51pAaq-sqdL.jpg" },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Kann die Sonne schwimmen Ein Bilderbuch mit vielen farbigen Illustrationen ab 2 Jahren. (German Edition)").Id, OriginalFileName = "31Vrx9NL7QL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "5/31Vrx9NL7QL.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Hackers de arcoíris").Id, OriginalFileName = "51JKFBsKz0L.jpg", FileExtension = ".jpg", UrlPath = booksPath + "6/51JKFBsKz0L.jpg", IsMainImage = true },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Miseria e Nobiltà (Italian Edition)").Id, OriginalFileName = "51Wm-i6bhYL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "7/51Wm-i6bhYL.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Miseria e Nobiltà (Italian Edition)").Id, OriginalFileName = "31Mmtij+gyL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "7/31Mmtij+gyL.jpg" },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Hidden Figures The American Dream and the Untold Story of the Black Women Mathematicians Who Helped Win the Space Race").Id, OriginalFileName = "51O1sI8z4SL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "8/51O1sI8z4SL.jpg", IsMainImage = true },

            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Happísland: Le court mais pas trop bref récit d'un espion suisse en Islande (French Edition)").Id, OriginalFileName = "71+wsDV09DL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "9/71+wsDV09DL.jpg", IsMainImage = true },
            //        new Image { ProductId = context.Products.FirstOrDefault(p => p.Title == "Happísland: Le court mais pas trop bref récit d'un espion suisse en Islande (French Edition)").Id, OriginalFileName = "71WGl+k6YQL.jpg", FileExtension = ".jpg", UrlPath = booksPath + "9/71WGl+k6YQL.jpg" },
            //        );

            //    context.SaveChanges();
            //}
            #endregion

            #region Comments
            //if (!context.Comments.Any())
            //{
            //    var customerIds = new Dictionary<int, string>();
            //    var customerIndex = 0;
            //    var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleName == IdentityRoles.Customer)).OrderBy(u => u.UserName);
            //    foreach (var customer in customers)
            //    {
            //        customerIds.Add(customerIndex, customer.Id);
            //        customerIndex++;
            //    }

            //    var random = new Random();
            //    var productsIds = new Dictionary<int, int>();
            //    int productIndex = 0;
            //    foreach (var product in context.Products)
            //    {
            //        productsIds.Add(productIndex, product.Id);
            //        productIndex++;
            //    }

            //    for (int i = 0; i < 1000; i++)
            //    {
            //        context.Comments.AddOrUpdate(
            //            c => c.Id,
            //            new Comment
            //            {
            //                Content = RandomStringsGenerator.LoremIpsum(3, 10, 1, 10, ValidationConstants.CommentContentMinLength, ValidationConstants.CommentContentMaxLength),
            //                ProductId = productsIds[random.Next(productsIds.Count())],
            //                UserId = customerIds[random.Next(customerIds.Count)]
            //            });
            //    }

            //    context.SaveChanges();
            //}
            #endregion
        }

        #region Helpers
        private void ProcessProductImages(JustOrderItDbContext context)
        {
            if (context.Products.Any())
            {
                foreach (var product in context.Products)
                {
                    product.MainImageId = product.Images.FirstOrDefault(i => i.IsMainImage)?.Id;

                    if (product.Images.Any())
                    {
                        foreach (var image in product.Images)
                        {
                            //var content = this.sampleDataGenerator.
                            //var processedImage = this
                            image.UrlPath = this.sampleDataGenerator.GenerateImageFile(image.Id, image.UrlPath, image.OriginalFileName, image.FileExtension);

                            //image.UrlPath = this.sampleDataGenerator.GetFilePath(image.Id);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        //private void SetMainImageIds(JustOrderItDbContext context)
        //{
        //    if (context.Products.Any())
        //    {
        //        foreach (var product in context.Products)
        //        {
        //            product.MainImageId = product.Images.FirstOrDefault(i => i.IsMainImage)?.Id;
        //        }

        //        context.SaveChanges();
        //    }
        //}
        #endregion
    }
}
