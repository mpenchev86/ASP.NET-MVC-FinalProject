namespace MvcProject.Data.DbAccessConfig.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Contexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Catalog;
    using Models.Identity;
    using Models.Orders;
    using Models.Search;
    using MvcProject.Common.GlobalConstants;
    using Web.Infrastructure.StringHelpers;

    public sealed class Configuration : DbMigrationsConfiguration<MvcProjectDbContext>
    {
        public Configuration()
        {
            // TODO: Remove in production
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            this.ContextKey = DbAccess.DbMigrationsConfigurationContextKey;
        }

        protected override void Seed(MvcProjectDbContext context)
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
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
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
                        Options = "Paperback, Handcover, Kindle Edition, Large Print, Audible Audio Edition, Audio CD",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Language",
                        DisplayName = "Language",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Options = "English, German, French, Spanish, Italian",
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
                        Options = "black, grey, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
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
                        Options = "For Her, For Him",
                        SelectionType = SearchFilterSelectionType.Single,
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
                            "Intel Core 2, " +
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
                    new Tag { Name = "green" },
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


                    new Tag { Name = "Yada Yada" }
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
                context.Products.AddOrUpdate(
                    p => p.Id,
                #region Appliances
                    new Product
                    {
                        Title = "Cuisinart DLC-2ABC Mini Prep Plus Food Processor Brushed Chrome and Nickel",
                        ShortDescription = "Why Is This The Perfect Mini Processor For You? The Cuisinart Mini-Prep Plus Processor handles a variety of food preparation tasks including chopping, grinding, puréeing, emulsifying and blending. The patented auto-reversing SmartPower blade provides a super-sharp edge for the delicate chopping of herbs and for blending and puréeing other soft foods.",
                        //DescriptionId = 1,
                        Description = new Description
                        {
                            Content = "Why Is This The Perfect Mini Processor For You? The Cuisinart Mini-Prep Plus Processor handles a variety of food preparation tasks including chopping, grinding, puréeing, emulsifying and blending. The patented auto-reversing SmartPower blade provides a super-sharp edge for the delicate chopping of herbs and for blending and puréeing other soft foods. The blunt edge offers a powerful cutting surface to grind through spices and other hard foods. Pulse activation gives maximum control for precision processing, whether chopping or grinding. Spatula, product manual and recipe booklet included. Using Your Cuisinart Mini-Prep Plus Processor The powerful high-speed 250-Watt motor works hard and fast to accomplish any small job with ease. Chop herbs, onions, garlic; grind spices, hard cheese, purée baby foods; blend mayonnaise and flavored butters, all with the same compact appliance. The Mini-Prep Plus Processor takes up minimum counter space and stores neatly on the countertop or in a cabinet.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor Power", Value = "250W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "chrome", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Size", Value = "3 cup" },
                                new Property { Name = "Body material", Value = "plastic" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cuisinart food processor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "SPT SD-9252SS Energy Star 18\" Built - In Dishwasher, Stainless Steel",
                        ShortDescription = "18\" wide, but spacious cavity.Up to 8 standard place settings.It is a great addition to any home.It takes up minimal space and is a great replacement for older appliances. Meets federal guidelines for energy efficiency for year-round energy and money savings. Bring more cleaning power into a smaller space.",
                        //DescriptionId = 2,
                        Description = new Description
                        {
                            Content = "This built-in 8 place setting dishwasher is a great addition to any home. At 18 inches wide, this unit takes up minimal space and is a great replacement for older appliances. Features 2 pull out dish racks, 6 wash programs and time delay feature. Time Delay Feature: allows you to program operation at a later start time (1-24 hours). Error Alarm: displays fault codes. Rinse Aid Warning Indicator: refill reminder on rinse aid. Stainless Steel Interior. Quite Operation: at 55 dBA. Capacity: up to 8 standard place settings. 6 Wash Programs: All-in-1, Heavy, Normal, Light , Rinse and Speed. Two Racks: with adjustable upper rack to accommodate larger plates/pots. 2 Spray Arms: for complete cleaning coverage. Silverware Basket: holds silverware and utensils for easy cleaning. Automatic Dispensers: detergent and rinse agent. Energy Star: meets or exceeds federal guidelines for energy efficiency for year-round energy and money savings.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Power Consumption", Value = "1104W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Installation type", Value = "built-in" },
                                new Property { Name = "Color", Value = "steel", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "color".ToLower()) },
                                new Property { Name = "Voltage", Value = "120V" },
                                new Property { Name = "Material type", Value = "stainless steel" },
                                new Property { Name = "Noise level", Value = "55db" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 37,
                        UnitPrice = 329.99M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "steel".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "energy star dishwasher".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Breville BFP800XL Sous Chef Food Processor, Stainless Steel",
                        ShortDescription = "Every Breville product begins with a simple moment of brilliance. The Breville Sous Chef began with the observation that the food comes in many different shapes and sizes, making it difficult for one machine to consistently cut all ingredients into the optimal size pieces. So how do you make sure that you get the perfect size for what you’re cooking?...",
                        //DescriptionId = 3,
                        Description = new Description
                        {
                            Content = "Every Breville product begins with a simple moment of brilliance. The Breville Sous Chef began with the observation that the food comes in many different shapes and sizes, making it difficult for one machine to consistently cut all ingredients into the optimal size pieces. So how do you make sure that you get the perfect size for what you’re cooking? The Breville Sous Chef solves this problem with its unique design.Its wide feed chute makes it possible to slice vegetables of all shapes and sizes, while numerous disc and blade options makes it easy to get perfect results, any way you slice it. Food processors are supposed to make food prep easier, not more frustrating. The Breville Sous Chef has a 5.5” Super Wide Feed Chute that reduces the need to pre-cut most fruits & veggies, saving you time. The Breville Sous Chef comes with a set of 8 discs and blades for numerous prep options, all housed in a convenient accessory storage.The discs include a variable slicing disc that can be set to 24 different slicing settings so you can customize the thickness of your slices from a paper thin 0.3mm all the way up to a thick 8.0mm.Other discs in the set include a julienne disc, a French fry cutting disc, a whisking disc, and a reversible shredding disc, while the blades include a micro-serrated universal S blade, a dough blade for kneading and combining ingredients, and a mini blade for use with mini - bowl.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor Power", Value = "1200W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "steel", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Food pusher", Value = "yes" },
                                new Property { Name = "Display", Value = "LCD" },
                                new Property { Name = "Body material", Value = "metal" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 6,
                        UnitPrice = 373.44M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "red".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville food processor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "KitchenAid KFP0933ER 9-Cup Food Processor with Exact Slice System - Empire Red",
                        ShortDescription = "This model has a 9-cup work bowl with 2-in-1 Feed Tube and pusher for continuous processing. The 9-cup capacity is ideal for many home cooking needs, allowing you to chop, mix, slice and shred with ease, offering multiple tools in one appliance.",
                        //DescriptionId = 4,
                        Description = new Description
                        {
                            Content = "The first ever externally adjustable slicing, KitchenAid ExactSlice System gives you precise slicing and accuracy for all kinds of food—hard or soft, large or small. And it does it all using less energy than previous model. Accommodates tomatoes, cucumbers, and potatoes with minimal prep work required. The UltraTight Seal Features a specially designed locking system with leak-resistant ring that allows you to fill the work bowl to capacity with ingredients without worrying about making a mess. High, Low & Pulse speed options allow you to precisely and properly handle soft or hard ingredients with the touch of a button.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor power", Value = "1000W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "red", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Body material", Value = "Polycarbonate" },
                                new Property { Name = "Size", Value = "9 cup" },
                                new Property { Name = "Condition", Value = "refurbished", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 27,
                        UnitPrice = 199.99M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "red".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "KitchenAid food processor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "KitchenAid KHB1231GA 2-Speed Hand Blender, Green Apple",
                        ShortDescription = "The 2-Speed Hand Blender let's you blend, puree, and crush with ease. Two speeds provide control for food, such as, smoothies, soups, or baby food. The blending arm twists off for quick and easy cleanup.",
                        //DescriptionId = 5,
                        Description = new Description
                        {
                            Content = "The 2-Speed Hand Blender let's you blend, puree, and crush with ease. Two speeds provide control for food, such as, smoothies, soups, or baby food. The blending arm twists off for quick and easy cleanup. The Removable 8-inch Blending Arm locks into the motor body for easy operation when blending in deeper pots. The soft grip handle offers a non-slip and comfortable grip when continuously blending ingredients. The 3-Cup BPA-Free Blending Jar with Lid is convenient for individual blending jobs, to serve or store for later. Top-rack dishwasher safe. Lid not included with Model KHB2352. Model KHB2571 comes with 4-Cup Pitcher.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor power", Value = "250W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "green", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Speed Settings", Value = "2" },
                                new Property { Name = "Material type", Value = "plastic" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 76,
                        UnitPrice = 39.99M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "green".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "KitchenAid hand blender".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Breville BHM800SIL Handy Mix Scraper Hand Mixer, Silver",
                        ShortDescription = "The Breville BHM800SIL Handy Mix Scraper with intuitive ergonomic control and Beater IQ automatically adjusts power to suit what you're mixing. Features an intuitive 9 speed selector...",
                        //DescriptionId = 6,
                        Description = new Description
                        {
                            Content = "How do you maximize speed for whisking and power for kneading from the same handy mixer? The Breville BHM800SIL Handy Mix Scraper with intuitive ergonomic control and Beater IQ automatically adjusts power to suit what you're mixing. Features an intuitive 9 speed selector, plus boost, with an easy to use scroll wheel is electronically controlled to spin at a precise speed no matter what the load. Accessories include 2 scraper beaters, 2 dough hooks, and 2 balloon whisks which are housed in a storage case which clips under the unit so nothing gets lost. Pause function holds your setting while you prepare or add ingredients. Quick release trigger and swivel cord also featured.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor power", Value = "240W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "silver", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Speed Settings", Value = "9" },
                                new Property { Name = "Material type", Value = "plastic" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 37,
                        UnitPrice = 129.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "silver".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville hand mixer".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Braun FP3020 12 Cup Food Processor Ultra Quiet Powerful motor, includes 7 Attachment Blades + Chopper and Citrus Juicer , Made in Europe with German Engineering",
                        ShortDescription = "Every cook – professional or not – knows the frequent slicing, dicing, shredding and mixing that happens in the kitchen. By purchasing a food processor you can save your time doing all these much quicker than do it with a knife. The well - known Braun brand designed the most functional food processor with a wide range of tools for your convenience.",
                        //DescriptionId = 7,
                        Description = new Description
                        {
                            Content = "Every cook – professional or not – knows the frequent slicing, dicing, shredding and mixing that happens in the kitchen. By purchasing a food processor you can save your time doing all these much quicker than do it with a knife. The well - known Braun brand designed the most functional food processor with a wide range of tools for your convenience. Here’s what they have for you. Made in Europe with German Engineering for the best performance. Rated 600W and can Deliver Up to 900W peak Power. 12 cup for dry ingredients, 9 cups for liquid(wet ingredients). Quick to put together. Silent strength Ultra - Quiet. Energy efficient with low power consumption. Pre - set speed function. Compact design Easy to store. Easy to clean: Every part(except the base with the motor) is dishwasher safe.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor power", Value = "600W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Color", Value = "white", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Body material", Value = "plastic" },
                                new Property { Name = "Speed Settings", Value = "11" },
                                new Property { Name = "Size", Value = "12 cup" },
                                new Property { Name = "Voltage", Value = "110V" },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 18,
                        UnitPrice = 159.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "white".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "braun food processor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Vitamix 5200 Series Blender, Black",
                        ShortDescription = "Create every course of your home-cooked meal-from frozen drinks to creamy desserts-in minutes. The Vitamix 5200 is the universal tool for family meals and entertaining.",
                        //DescriptionId = 8,
                        Description = new Description
                        {
                            Content = "Create every course of your home-cooked meal-from frozen drinks to creamy desserts-in minutes. The Vitamix 5200 is the universal tool for family meals and entertaining. The size and shape of the 64-ounce container is ideal for blending medium to large batches. Easily adjust speed to achieve a variety of textures. The dial can be rotated at any point during the blend, so you're in complete control. The power and precision of our patented designs are able to pulverize every recipe ingredient, including the tiniest seeds. The blades in the Vitamix container reach speeds fast enough to create friction heat, bringing cold ingredients to steaming hot in about six minutes.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor Power", Value = "1380W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Speed Settings", Value = "10" },
                                new Property { Name = "Body material", Value = "Tritan Copolyester" },
                                new Property { Name = "Color", Value = "Black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 112,
                        UnitPrice = 429.98M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "vitamix blender".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Breville BBL605XL Hemisphere Control Blender",
                        ShortDescription = "The Hemisphere Control combines the functionality of a powerful blender with some food processing tasks. It crushes and chops to turn ice into snow for velvety cocktails and also folds and aerates for creamy smoothies and soups. In addition, thanks to its innovative blade design...",
                        //DescriptionId = 9,
                        Description = new Description
                        {
                            Content = "Declared one of the best blenders of 2012 by a leading rater of kitchen products, the Breville Hemisphere Control uses an innovative design: extra wide stainless steel blades are contoured to sweep along the base of the jug and push ingredients up while central blades pull ingredients down. The Hemisphere Control combines the functionality of a powerful blender with some food processing tasks. It crushes and chops to turn ice into snow for velvety cocktails and also folds and aerates for creamy smoothies and soups. In addition, thanks to its innovative blade design and high torque motor, the Hemisphere Control can handle blending tasks more efficiently and with less noise. The Hemisphere Control blender is both easy to use and easy to clean. An LCD timer, 5 different speeds and three additional task functions make creating the perfect blend as easy as pushing one button. The permanent blade system is a snap to clean: just put in a drop of detergent and blend with water to remove food remnants, and then put the entire jug in the dishwasher.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor Power", Value = "750W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Speed Settings", Value = "5" },
                                new Property { Name = "Body material", Value = "Tritan" },
                                new Property { Name = "Color", Value = "silver", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 64,
                        UnitPrice = 180.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "silver".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "breville blender".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Ninja Professional Blender (NJ600)",
                        ShortDescription = "The Ninja Professional Blender features a sleek design and outstanding performance with 1000 watts of professional power. Ninja Total Crushing Technology is perfect for ice crushing, blending, pureeing, and controlled processing.",
                        //DescriptionId = 10,
                        Description = new Description
                        {
                            Content = "The Ninja Professional Blender features a sleek design and outstanding performance with 1000 watts of professional power. Ninja Total Crushing Technology is perfect for ice crushing, blending, pureeing, and controlled processing. Crush ice, whole fruits and vegetables in seconds! The XL 72 oz. professional blender jar is perfect for making drinks and smoothies for the whole family. All parts are BPA free and dishwasher safe. Total Crushing Technology delivers unbeatable professional power with blades that pulverize and crush through ice, whole fruits and vegetables in seconds. Blast ice into snow in seconds and blend your favorite ingredients into delicious sauces, dips and smoothies!",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Motor Power", Value = "1000W", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Motor Power".ToLower()) },
                                new Property { Name = "Speed Settings", Value = "3" },
                                new Property { Name = "Body material", Value = "plastic" },
                                new Property { Name = "Color", Value = "black", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Color".ToLower()) },
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 27,
                        UnitPrice = 81.99M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Appliances").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "ninja blender".ToLower()),
                        }
                    },
                #endregion
                #region Books
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 1,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 2,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 3,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 4,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 5,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 6,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 7,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 8,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 9,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "",
                        ShortDescription = "",
                        //DescriptionId = 10,
                        Description = new Description
                        {
                            Content = "",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Condition", Value = "new", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.ToLower() == "Condition".ToLower()) },
                            }
                        },
                        QuantityInStock = 50,
                        UnitPrice = 37.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "chrome".ToLower()),
                        }
                    },
                #endregion
                #region Cameras
                    new Product
                    {
                        Title = "Canon EOS 5D Mark III 22.3 MP Full Frame CMOS with 1080p Full-HD Video Mode Digital SLR Camera (Body)",
                        ShortDescription = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform.",
                        //DescriptionId = 1,
                        Description = new Description
                        {
                            Content = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform. Special optical technologies like 61-Point High Density Reticular AF and extended ISO range of 100-25600 make this it ideal for shooting weddings in the studio, out in the field and great for still photography. Professional-level high definition video capabilities includes a host of industry-standard recording protocols and enhanced performance that make it possible to capture beautiful cinematic movies in EOS HD quality. A 22.3 Megapixel full-frame Canon CMOS sensor, Canon DIGIC 5+ Image Processor, and shooting performance up to 6.0fps provide exceptional clarity and sharpness when capturing rapidly-unfolding scenes. Additional technological advancements include an Intelligent Viewfinder, Canon's advanced iFCL metering system, High Dynamic Range (HDR), and Multiple Exposure.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Color", Value = "Black" },
                                new Property { Name = "Optical Sensor Resolution", Value = "22.3 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Image Stabilization", Value = "None" },
                                new Property { Name = "Continuous Shooting Speed", Value = "6 fps" },
                                new Property { Name = "Battery Average Life", Value = "950 Photos" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 60,
                        UnitPrice = 2499.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cheap camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Canon camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Canon PowerShot SX410 IS (Black)",
                        ShortDescription = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility.",
                        //DescriptionId = 2,
                        Description = new Description
                        {
                            Content = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility: you'll capture wide landscapes and zoom in for impressive close-ups you never thought possible – all with bright, clear quality thanks to Canon's Optical Image Stabilizer and Intelligent IS. The 20.0 Megapixel* sensor and Canon DIGIC 4+ Image Processor help create crisp resolution and beautiful, natural images. Your videos will impress too: simply press the Movie button to record lifelike 720p HD video – even zoom in and out while shooting. Images you'll want to keep and share are easy to achieve with Smart AUTO that intelligently selects proper camera settings so your images and video look great in all kinds of situations. You'll get creative with fun Scene Modes like Fisheye Effect, Toy Camera Effect and Monochrome, and see and share it all with the camera's big, clear 3.0\" LCD with a wide viewing angle.For versatility and value, the PowerShot SX410 IS camera is a best bet!\n *Image processing may cause a decrease in the number of pixels.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Color", Value = "Black" },
                                new Property { Name = "Maximum Aperture Range", Value = "F3.5 - F5.6" },
                                new Property { Name = "ISO Minimum", Value = "100" },
                                new Property { Name = "ISO Maximum", Value = "1600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Maximum ISO")) },
                                new Property { Name = "Optical Zoom", Value = "40x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Battery Average Life", Value = "185 Photos" },
                                new Property { Name = "Condition", Value = "Refurbished", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 314,
                        UnitPrice = 179.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "black".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "cheap camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "large digital photo frame".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "canon camera".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Nikon D3200 24.2 MP CMOS Digital SLR with 18-55mm f/3.5-5.6 Auto Focus-S DX VR NIKKOR Zoom Lens (Black)",
                        ShortDescription = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light...",
                        //DescriptionId = 3,
                        Description = new Description
                        {
                            Content = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light, EXPEED 3 image-processing for fast operation and creative in-camera effects, Full HD (1080p) movie recording, in-camera tutorials and much more. What does this mean for you? Simply stunning photos and videos in any setting. And now, with Nikon’s optional Wireless Mobile Adapter, you can share those masterpieces instantly with your Smartphone or tablet easily. Supplied Accessories: EN-EL14 Rechargeable Li-ion Battery, MH-24 Quick Charger, EG-CP14 Audio/Video Cable, UC-E6 USB Cable, DK-20 Rubber Eyecup, AN-DC3 Camera Strap, DK-5 Eyepiece Cap, BF-1B Body Cap, BS-1 Accessory Shoe Cover and Nikon View NX CD-ROM.1-Year Nikon U.S.A. Warranty.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Color", Value = "Black" },
                                new Property { Name = "Autofocus Points", Value = "11" },
                                new Property { Name = "Continuous Shooting Speed", Value = "4 fps" },
                                new Property { Name = "Flash Sync Speed", Value = "1/200 sec" },
                                new Property { Name = "Image Stabilization", Value = "None" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 226,
                        UnitPrice = 355.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Nikon D7100 24.1 MP DX-Format CMOS Digital SLR Camera Bundle with 18-140mm and 55-300mm VR NIKKOR Zoom Lens (Black)",
                        ShortDescription = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more.",
                        //DescriptionId = 4,
                        Description = new Description
                        {
                            Content = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more. Shoot up to 6 fps and instantly share shots with the WU-1a Wireless Adapter. Create dazzling Full HD 1080p videos and ultra-smooth slow-motion or time-lapse sequences.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Display", Value = "TFT LCD" },
                                new Property { Name = "ISO Minimum", Value = "50" },
                                new Property { Name = "ISO Maximum", Value = "25,600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Maximum ISO")) },
                                new Property { Name = "Lens Type", Value = "Fisheye" },
                                new Property { Name = "Model Year", Value = "2014" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 130,
                        UnitPrice = 1346.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "3D tracking".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Sony Alpha a6000 Mirrorless Digital Camera with 16-50mm Power Zoom Lens",
                        ShortDescription = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality.",
                        //DescriptionId = 5,
                        Description = new Description
                        {
                            Content = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality - via newly developed 24.3-effective-megapixel (approx.) Exmor APS HD CMOS sensor and BIONZ X image processing engine - as well as highly intuitive operation thanks to an OLED Tru-Finder and two operation dials.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Optical Sensor Resolution", Value = "24 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Weather Resistance", Value = "No" },
                                new Property { Name = "Model Year", Value = "2014" },
                                new Property { Name = "Minimum Shutter Speed", Value = "30 seconds" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 414,
                        UnitPrice = 648.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "sony camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Nikon COOLPIX L32 Digital Camera with 5x Wide-Angle NIKKOR Zoom Lens",
                        ShortDescription = "The Nikon Coolpix L32 Digital Camera makes taking great photos and videos a breeze! Want better selfies or photos of friends, family and even pets? Use the Smart Portrait System and let the COOLPIX L32 do all the work. Want better videos? Press the dedicated Movie Record button and capture 720p HD videos with sound. You can even zoom in while you're recording - electronic Vibration Reduction will help keep your videos steady. Additional features: Image Effects, large 3.0-inch LCD, Glamour Retouch, runs on AA batteries, plus more!",
                        //DescriptionId = 6,
                        Description = new Description
                        {
                            Content = "The COOLPIX L32 is all about ease just point, shoot and enjoy your great photos and videos.When you want to record videos, you don't have to turn any dials or flip any switches just press the dedicated Movie Record button. And when you want to add fun effects, the COOLPIX L32's simple menu system makes it a breeze. Smart Portrait System makes it easy to create beautiful photos of the people you care about. Turn it on, and several portrait-optimizing features activate. Face Priority AF finds and focuses on faces. Skin Softening applies an attractive soft focus effect. The camera can even automatically take a photo the instant someone smiles! Your loved ones will always look their best. Every COOLPIX is designed around a genuine NIKKOR glass lens, the legendary optics that have made Nikon famous. The COOLPIX L32's 5x Zoom NIKKOR lens is great for everything from wide-angle group shots to close-up portraits. Plus, Electronic Vibration Reduction helps keep every video steadier, even if your hands are not.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Image Stabilization", Value = "Yes" },
                                new Property { Name = "Optical Zoom", Value = "5x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Display", Value = "3.0-inch LCD" },
                                new Property { Name = "Optical Sensor Resolution", Value = "20.1 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Color", Value = "red" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 160,
                        UnitPrice = 119.95M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "large digital photo frame".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Nikon camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "wide angle lens".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "point-and-shoot camera".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Canon EOS Rebel T5 1200D 18MP EF-S Body Full HD 1080p Video Digital SLR Camera (NO LENS)",
                        ShortDescription = "This USA Canon EOS Rebel T5 DSLR Camera is an 18MP APS-C format DSLR camera with a DIGIC 4 image processor. The combination of the T5's CMOS sensor and DIGIC 4 image processor provide high clarity, a wide tonal range, and natural color reproduction.",
                        //DescriptionId = 7,
                        Description = new Description
                        {
                            Content = "This USA Canon EOS Rebel T5 DSLR Camera is an 18MP APS-C format DSLR camera with a DIGIC 4 image processor. The combination of the T5's CMOS sensor and DIGIC 4 image processor provide high clarity, a wide tonal range, and natural color reproduction. With an ISO range of 100-6400 (expandable to 12800), you can shoot in low-light situations, reducing the need for a tripod or a flash. The nine-point autofocus system includes one center cross-type AF point to deliver accurate focus in both landscape and portrait orientations. You can capture Full HD 1080p video by way of the T5's dedicated Live View/Movie Recording button, at 30, 25, or 24 fps. Additionally, the camera can also record HD video in 720p at 60 and 50 fps, and 480p at 30 and 35 fps. The 3\" rear LCD screen has 460,000 pixels, and a 170° viewing angle, making it ideal for menu navigation, composing in Live View, and reviewing or sharing your photos and videos.The Rebel T5 is compatible with the full line of Canon EF and EF - S lenses.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Optical Sensor Resolution", Value = "18 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Video Resolution", Value = "1080p" },
                                new Property { Name = "color", Value = "black" },
                                new Property { Name = "Photo Sensor Size", Value = "aps-c" },
                                new Property { Name = "ISO Minimum", Value = "100" },
                                new Property { Name = "ISO Maximum", Value = "6400", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Maximum ISO")) },
                                new Property { Name = "Autofocus", Value = "9-point AF system" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 3,
                        UnitPrice = 419.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "DSLR Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "canon camera".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Olympus Evolt E410 10MP Digital SLR Camera with 14-42mm f3.5-5.6 and 40-150mm f4.0-5.6 Zuiko Lenses",
                        ShortDescription = "Innovative 2.5-inch Live View HyperCrystal LCD. Detailed, bright, and colorful photos with 10-megapixel Live MOS image sensor. TruePic III for image clarity. Included 14-42mm f/3.5-5.6 and 40-150mm f/4.0-5.6 Zuiko lenses.",
                        //DescriptionId = 8,
                        Description = new Description
                        {
                            Content = "The E-410 offers ease-of-shooting and greater flexibility with the Live View LCD. Composing photographs is easier as subjects can be seen on the Live View LCD, which offers a wide 176-degree viewing angle. The E-410 is loaded with an impressive 10 million pixels for high-resolution photos. The 10-megapixel sensor gives users the flexibility to blow-up their prints to the large sizes supported by many of today’s printers, or crop the image to print only a part of the image that is important to them. The high-performance Live MOS image sensor in the E-410 delivers excellent dynamic range for accurate color fidelity, and a new state-of-the-art amplifier circuit to eradicate noise and capture fine image details in the highlight and shadow areas. Olympus’ enhanced TruePic III Image Processor produces crystal clear photos using all the pixel information for each image to provide the best digital images possible for every photo with accurate color, true-to-life flesh tones, brilliant blue skies and precise tonal representation in between. TruePic III also lowers image noise by one step to reduce noise in images shot at higher ISO settings, enabling great results in low-light situations. The E-410 one-lens outfit includes a compact, Zuiko Digital ED 14-42 mm f3.5-f5.6 Lens (equivalent to 28mm-84mm in 35mm photography) that perfectly matches the imager so light strikes the sensor directly to ensure rich, accurate colors and edge-to-edge sharpness. Its 3x ED Glass zoom lens covers the range most frequently used in everyday photography and weighs just 7.5 ounces, offering users an extremely dynamic, portable everyday-use zoom. Close-ups as near as 9.84 inches (0.25 m) are also possible throughout the zoom range. The E - 410 two - lens outfit adds the Zuiko Digital ED 40 - 150mm f4.0 - 5.6(80 - 300mm equivalent) Lens, which provides users with greater telephoto power for far - away shots in a compact size.This telephoto lens is smaller than many standard zoom lenses at 2.6 inch diameter x 2.8 inch length and a weight of 8.8 ounces-- a real benefit for anyone who wants to pack a powerful zoom lens without taking up much space.It also has great close focusing abilities, and is able to capture a subject up - close from a distance of 31.5 inches(.8m).",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Optical Sensor Resolution", Value = "10 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Optical Zoom", Value = "3x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Display", Value = "2.5-inch Live View HyperCrystal LCD" },
                                new Property { Name = "Condition", Value = "Used", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 36,
                        UnitPrice = 467.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "olympus camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Sony a7 Full-Frame Mirrorless Digital Camera with 28-70mm Lens",
                        ShortDescription = "No other full frame, interchangeable-lens camera is this light or this portable. 24.3 MP of rich detail. A true-to-life 2.4 million dot OLED viewfinder. Wi-Fi sharing and an expandable shoe system. It's all the full-frame performance you ever wanted in a compact size that will change your perspective entirely.",
                        //DescriptionId = 9,
                        Description = new Description
                        {
                            Content = "Sony's Exmor® image sensor takes full advantage of the Full-frame format, but in a camera body less than half the size and weight of a full-frame DSLR. A whole new world of high-quality images are realized through the 24.3 MP effective 35 mm full-frame sensor, a normal sensor range of ISO 100 – 25600, and a sophisticated balance of high resolving power, gradation and low noise. The BIONZ® X image processor enables up to 5 fps high-speed continuous shooting and 14-bit RAW image data recording. The high-speed image processing engine and improved algorithms combine with optimized image sensor read-out speed to achieve ultra high-speed AF despite the use of a full-frame sensor.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Optical Sensor Resolution", Value = "24.3 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "Optical Zoom", Value = "4x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Image Stabilization", Value = "Yes" },
                                new Property { Name = "ISO Minimum", Value = "100" },
                                new Property { Name = "ISO Maximum", Value = "25600", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Maximum ISO")) },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 43,
                        UnitPrice = 1398.00M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "sony camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "CMOS sensor".ToLower()),
                        }
                    },
                    new Product
                    {
                        Title = "Panasonic LUMIX DMC-G85MK 4K Mirrorless Interchangeable Lens Camera Kit, 12-60mm Lens, 16 Megapixel (Black)",
                        ShortDescription = "The Panasonic LUMIX G85 offers over 27 LUMIX compact lens options built on the next-generation interchangeable lens camera standard (Micro Four Thirds) pioneered by Panasonic. Its “mirrorless” design enables a lighter, more compact camera body that includes cutting-edge video, audio, creative controls, wireless, intelligent-focusing, gyro sensor control in body image stabilization and exposure technologies not possible with traditional DSLRs.",
                        //DescriptionId = 10,
                        Description = new Description
                        {
                            Content = "When life's adventures take you places, you need a camera that keeps up. Photographer Mitchell Kanashkevich took the LUMIX G85 on a journey to Romania. Perfect for outdoor shooting, the Dual Image Stabilizer helped him take crisper, clearer images in difficult and fast-moving environments, while the compact, weather-sealed body and kit lens improved flexibility wherever he went. With the LUMIX G85, a new gyro sensor increases the image stability compensation power of the 5-Axis Body image stabilization to correct hand-shake for all lenses, including classic lenses not equipped with optical image stabilization. The LUMIX G85 integrates 5-Axis Dual I.S.2 (Image Stabilizer)*, combining 5-axis body and 2-axis lens stabilization for more effective handshake correction and compensation for shots up to 5 f-stops**. The 5-axis stabilization works in both wide and telephoto photography and motion picture recording, including 4K Video. 4K Photo - Never Miss That Shot LUMIX - pioneered 4K PHOTO lets you capture the perfect moment by selecting single frames from a 4K video sequence shot at a blistering 30fps to save as individual high - res images.",
                            Properties = new HashSet<Property>()
                            {
                                new Property { Name = "Optical Sensor Resolution", Value = "16 MP", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Resolution")) },
                                new Property { Name = "color", Value = "black" },
                                new Property { Name = "Optical Zoom", Value = "5x", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name.Contains("Optical Zoom")) },
                                new Property { Name = "Image Stabilization", Value = "Yes" },
                                new Property { Name = "Maximum Shutter Speed", Value = "1/4000 of a second" },
                                new Property { Name = "Condition", Value = "New", SearchFilter = context.SearchFilters.FirstOrDefault(sf => sf.Name == "Condition") },
                            }
                        },
                        QuantityInStock = 1,
                        UnitPrice = 997.99M,
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        SellerId = sellerIds[random.Next(sellerIds.Count)],
                        Tags = new HashSet<Tag>
                        {
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "Mirrorless Digital Camera".ToLower()),
                            context.Tags.FirstOrDefault(t => t.Name.ToLower() == "panasonic camera".ToLower()),
                        }
                    }
                    #endregion
                #region Furniture
                #endregion
                #region Heath and Beauty
                #endregion
                #region Notebooks
                #endregion
                #region Sports Equipment
                #endregion
                    );

                context.SaveChanges();
            }
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
    }
}
