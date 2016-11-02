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
    using MvcProject.Common.GlobalConstants;

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
                        CreatedOn = DateTime.Now
                    },
                    new ApplicationRole
                    {
                        Name = IdentityRoles.Customer,
                        CreatedOn = DateTime.Now
                    });

                context.SaveChanges();
            }
            #endregion

            #region Users
            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher();
                context.Users.AddOrUpdate(
                    u => u.Email,
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
                    },
                    new ApplicationUser
                    {
                        Email = "sample1@mail.com",
                        UserName = "sample1",
                        PasswordHash = hasher.HashPassword("111111"),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                    },
                    new ApplicationUser
                    {
                        Email = "sample2@mail.com",
                        UserName = "sample2",
                        PasswordHash = hasher.HashPassword("222222"),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                    });

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
                    },
                    new ApplicationUserRole
                    {
                        UserId = context.Users.FirstOrDefault(u => u.UserName == "sample1").Id,
                        UserName = "sample1",
                        RoleId = context.Roles.FirstOrDefault(r => r.Name == IdentityRoles.Customer).Id,
                        RoleName = IdentityRoles.Customer
                    },
                    new ApplicationUserRole
                    {
                        UserId = context.Users.FirstOrDefault(u => u.UserName == "sample2").Id,
                        UserName = "sample2",
                        RoleId = context.Roles.FirstOrDefault(r => r.Name == IdentityRoles.Customer).Id,
                        RoleName = IdentityRoles.Customer
                    });

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

            #region SearchFilters
            if (!context.SearchFilters.Any())
            {
                context.SearchFilters.AddOrUpdate(
                    c => c.Id,
                #region Appliances
                    new SearchFilter
                    {
                        Name = "Motor Power",
                        CategoryId = 1,
                        Options = "200, 300, 400, 600, 800, 1000",
                        MeasureUnit = "Watts",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 1,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 1,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        CategoryId = 1,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Books
                    new SearchFilter
                    {
                        Name = "Format",
                        CategoryId = 2,
                        Options = "Paperback, Handcover, Kindle Edition, Large Print, Audible Audio Edition, Audio CD",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Language",
                        CategoryId = 2,
                        Options = "English, German, French, Spanish, Italian",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 2,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        CategoryId = 2,
                        Options = "New, Used, Collectible",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Cameras
                    new SearchFilter
                    {
                        Name = "Resolution",
                        CategoryId = 3,
                        Options = "12, 24, 36",
                        MeasureUnit = "MP",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Optical Zoom",
                        CategoryId = 3,
                        Options = "4, 10, 20, 50",
                        MeasureUnit = "x",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Maximum ISO",
                        CategoryId = 3,
                        Options = "800, 1600, 3200, 6400, 12800, 25600, 51200, 102400, 204800",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 3,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 3,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        CategoryId = 3,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Furniture
                    new SearchFilter
                    {
                        Name = "Material",
                        CategoryId = 4,
                        Options = "Wood, Fabric, Leather, Metal, Rattan, Glass, Plastic",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Color",
                        CategoryId = 4,
                        Options = "black, grey, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 4,
                        Options = "25, 50, 100, 200",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 4,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        CategoryId = 4,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Health & Beauty
                    new SearchFilter
                    {
                        Name = "Sex/Gender",
                        CategoryId = 5,
                        Options = "For Her, For Him",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 5,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 5,
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
                        CategoryId = 6,
                        Options = "11, 12, 13, 14, 15, 16, 17",
                        MeasureUnit = "inches",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "RAM",
                        CategoryId = 6,
                        Options = "2, 3, 4, 6, 8, 12, 16, 24, 32, 64",
                        MeasureUnit = "GB",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Processor",
                        CategoryId = 6,
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
                        Name = "Average Customer Review",
                        CategoryId = 6,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 6,
                        Options = "500, 600, 700, 800, 1000",
                        MeasureUnit = "$",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ValueRange,
                    },
                    new SearchFilter
                    {
                        Name = "Condition",
                        CategoryId = 6,
                        Options = "New, Used, Refurbished",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                #endregion
                #region Sports Equipment
                    new SearchFilter
                    {
                        Name = "Size",
                        CategoryId = 7,
                        Options = "XS, S, M, L, XL, 2XL, 3XL, 4XL, 5XL",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Color",
                        CategoryId = 7,
                        Options = "black, grey, white, brown, red, pink, orange, yellow, green, blue, purple, multi",
                        SelectionType = SearchFilterSelectionType.Multiple,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Average Customer Review",
                        CategoryId = 7,
                        Options = "5, 4, 3, 2, 1",
                        SelectionType = SearchFilterSelectionType.Single,
                        OptionsType = SearchFilterOptionsType.ConcreteValue,
                    },
                    new SearchFilter
                    {
                        Name = "Price",
                        CategoryId = 7,
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
                    new Tag { Name = "light" },
                    new Tag { Name = "cheap" },
                    new Tag { Name = "great" },
                    new Tag { Name = "amazing" },
                    new Tag { Name = "hot" },
                    new Tag { Name = "luxury" },
                    new Tag { Name = "functional" },
                    new Tag { Name = "trendy" },
                    new Tag { Name = "new" });

                context.SaveChanges();
            }
            #endregion

            #region Descriptions
            if (!context.Descriptions.Any())
            {
                context.Descriptions.AddOrUpdate(
                    d => d.Id,
                    new Description { Content = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform. Special optical technologies like 61-Point High Density Reticular AF and extended ISO range of 100-25600 make this it ideal for shooting weddings in the studio, out in the field and great for still photography. Professional-level high definition video capabilities includes a host of industry-standard recording protocols and enhanced performance that make it possible to capture beautiful cinematic movies in EOS HD quality. A 22.3 Megapixel full-frame Canon CMOS sensor, Canon DIGIC 5+ Image Processor, and shooting performance up to 6.0fps provide exceptional clarity and sharpness when capturing rapidly-unfolding scenes. Additional technological advancements include an Intelligent Viewfinder, Canon's advanced iFCL metering system, High Dynamic Range (HDR), and Multiple Exposure." },
                    new Description { Content = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility: you'll capture wide landscapes and zoom in for impressive close-ups you never thought possible – all with bright, clear quality thanks to Canon's Optical Image Stabilizer and Intelligent IS. The 20.0 Megapixel* sensor and Canon DIGIC 4+ Image Processor help create crisp resolution and beautiful, natural images. Your videos will impress too: simply press the Movie button to record lifelike 720p HD video – even zoom in and out while shooting. Images you'll want to keep and share are easy to achieve with Smart AUTO that intelligently selects proper camera settings so your images and video look great in all kinds of situations. You'll get creative with fun Scene Modes like Fisheye Effect, Toy Camera Effect and Monochrome, and see and share it all with the camera's big, clear 3.0\" LCD with a wide viewing angle.For versatility and value, the PowerShot SX410 IS camera is a best bet!\n *Image processing may cause a decrease in the number of pixels." },
                    new Description { Content = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light, EXPEED 3 image-processing for fast operation and creative in-camera effects, Full HD (1080p) movie recording, in-camera tutorials and much more. What does this mean for you? Simply stunning photos and videos in any setting. And now, with Nikon’s optional Wireless Mobile Adapter, you can share those masterpieces instantly with your Smartphone or tablet easily. Supplied Accessories: EN-EL14 Rechargeable Li-ion Battery, MH-24 Quick Charger, EG-CP14 Audio/Video Cable, UC-E6 USB Cable, DK-20 Rubber Eyecup, AN-DC3 Camera Strap, DK-5 Eyepiece Cap, BF-1B Body Cap, BS-1 Accessory Shoe Cover and Nikon View NX CD-ROM.1-Year Nikon U.S.A. Warranty." },
                    new Description { Content = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more. Shoot up to 6 fps and instantly share shots with the WU-1a Wireless Adapter. Create dazzling Full HD 1080p videos and ultra-smooth slow-motion or time-lapse sequences." },
                    new Description { Content = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality - via newly developed 24.3-effective-megapixel (approx.) Exmor APS HD CMOS sensor and BIONZ X image processing engine - as well as highly intuitive operation thanks to an OLED Tru-Finder and two operation dials." });

                context.SaveChanges();
            }
            #endregion

            #region Products
            if (!context.Products.Any())
            {
                context.Products.AddOrUpdate(
                    p => p.Id,
                    new Product
                    {
                        Title = "Canon EOS 5D Mark III 22.3 MP Full Frame CMOS with 1080p Full-HD Video Mode Digital SLR Camera (Body)",
                        ShortDescription = "The Canon 5260B002 EOS 5D Mark III 22.3MP Digital SLR Camera Body (lens required and sold separately) with supercharged EOS performance and full frame, high-resolution image capture is designed to perform.",
                        DescriptionId = 1,
                        QuantityInStock = 60,
                        UnitPrice = 2499.00M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(0).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "Canon PowerShot SX410 IS (Black)",
                        ShortDescription = "The PowerShot SX410 IS camera is packed with advanced Canon technologies that make it easy to capture your best images ever. The camera's powerful 40x Optical Zoom (24–960mm) and 24mm Wide-Angle lens gives you amazing versatility.",
                        DescriptionId = 2,
                        QuantityInStock = 314,
                        UnitPrice = 179.00M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(1).FirstOrDefault(),
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault(),
                            context.Tags.OrderBy(t => t.Id).Skip(2).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "Nikon D3200 24.2 MP CMOS Digital SLR with 18-55mm f/3.5-5.6 Auto Focus-S DX VR NIKKOR Zoom Lens (Black)",
                        ShortDescription = "Don’t let the D3200’s compact size and price fool you—packed inside this easy to use HD-SLR is serious Nikon.power: a 24.2 MP DX-format CMOS sensor that excels in any light...",
                        DescriptionId = 3,
                        QuantityInStock = 226,
                        UnitPrice = 355.95M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(3).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "Nikon D7100 24.1 MP DX-Format CMOS Digital SLR Camera Bundle with 18-140mm and 55-300mm VR NIKKOR Zoom Lens (Black)",
                        ShortDescription = "The Nikon D7100 Digital SLR Camera brings a specially designed 24.1-megapixel DX-format image sensor, superior low-light performance, ultra-precise autofocus and metering, advanced video recording features, built-in HDR, mobile connectivity and more.",
                        DescriptionId = 4,
                        QuantityInStock = 130,
                        UnitPrice = 1346.95M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(2).FirstOrDefault(),
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "Sony Alpha a6000 Mirrorless Digital Camera with 16-50mm Power Zoom Lens",
                        ShortDescription = "79-point focal plane phase-detection AF sensor. The compact, lightweight camera delivers superb image quality.",
                        DescriptionId = 5,
                        QuantityInStock = 414,
                        UnitPrice = 648.00M,
                        CategoryId = 3,
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Title = "huba buba",
                        ShortDescription = "Gums massage your gums",
                        QuantityInStock = 0,
                        UnitPrice = 32.453M,
                        CategoryId = 4,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(6).FirstOrDefault(),
                            context.Tags.OrderBy(t => t.Id).Skip(3).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "vafla chudo",
                        ShortDescription = "Waffles you can't get enough of",
                        QuantityInStock = 14,
                        UnitPrice = 3662717.0002M,
                        CategoryId = 7,
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 2,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    },
                    new Product
                    {
                        Title = "MnM",
                        ShortDescription = "Eating the rainbow",
                        QuantityInStock = 26,
                        UnitPrice = 7.88773M,
                        CategoryId = 3,
                        Tags = new List<Tag>
                        {
                            context.Tags.OrderBy(t => t.Id).Skip(5).FirstOrDefault()
                        }
                    }
                    );

                context.SaveChanges();
            }
            #endregion

            #region Comments
            if (!context.Comments.Any())
            {
                var userIds = new string[]
                {
                    context.Users.FirstOrDefault(u => u.UserName == "admin").Id,
                    context.Users.FirstOrDefault(u => u.UserName == "sample1").Id,
                    context.Users.FirstOrDefault(u => u.UserName == "sample2").Id,
                };
                context.Comments.AddOrUpdate(
                    c => c.Id,
                    new Comment { Content = "Oh, it's amazing! I'll buy it again tomorrow..", ProductId = 4, UserId = userIds[0] },
                    new Comment { Content = "blaaaaaahahahahhahahahahaha", ProductId = 1, UserId = userIds[1] },
                    new Comment { Content = "Dont waste your money for that crap!", ProductId = 2, UserId = userIds[2] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 2, UserId = userIds[0] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 2, UserId = userIds[1] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 7, UserId = userIds[2] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 3, UserId = userIds[0] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 3, UserId = userIds[1] },
                    new Comment { Content = "bokluk e.. naistina, mnogo sym dovolen.", ProductId = 5, UserId = userIds[0] });

                context.SaveChanges();
            }
            #endregion

            #region Properties
            if (!context.Properties.Any())
            {
                context.Properties.AddOrUpdate(
                    p => p.Id,
                    new Property { Name = "Color", Value = "Black", DescriptionId = 1 },
                    new Property { Name = "Optical Sensor Resolution", Value = "22.3 MP", DescriptionId = 1 },
                    new Property { Name = "Image Stabilization", Value = "None", DescriptionId = 1 },
                    new Property { Name = "Continuous Shooting Speed", Value = "6 fps", DescriptionId = 1 },
                    new Property { Name = "Battery Average Life", Value = "950 Photos", DescriptionId = 1 },
                    new Property { Name = "Color", Value = "Black", DescriptionId = 2 },
                    new Property { Name = "Maximum Aperture Range", Value = "F3.5 - F5.6", DescriptionId = 2 },
                    new Property { Name = "ISO Minimum", Value = "100", DescriptionId = 2 },
                    new Property { Name = "ISO Maximum", Value = "1600", DescriptionId = 2 },
                    new Property { Name = "Digital Zoom", Value = "4x", DescriptionId = 2 },
                    new Property { Name = "Battery Average Life", Value = "185 Photos", DescriptionId = 2 },
                    new Property { Name = "Color", Value = "Black", DescriptionId = 3 },
                    new Property { Name = "Autofocus Points", Value = "11", DescriptionId = 3 },
                    new Property { Name = "Continuous Shooting Speed", Value = "4 fps", DescriptionId = 3 },
                    new Property { Name = "Flash Sync Speed", Value = "1/200 sec", DescriptionId = 3 },
                    new Property { Name = "Image Stabilization", Value = "None", DescriptionId = 3 },
                    new Property { Name = "Display", Value = "TFT LCD", DescriptionId = 4 },
                    new Property { Name = "Expanded ISO Maximum", Value = "25,600", DescriptionId = 4 },
                    new Property { Name = "ISO Minimum", Value = "50", DescriptionId = 4 },
                    new Property { Name = "ISO Maximum", Value = "25,600", DescriptionId = 4 },
                    new Property { Name = "Lens Type", Value = "Fisheye", DescriptionId = 4 },
                    new Property { Name = "Model Year", Value = "2014", DescriptionId = 4 },
                    new Property { Name = "Optical Sensor Resolution", Value = "24 MP", DescriptionId = 5 },
                    new Property { Name = "Optical Zoom", Value = "3x", DescriptionId = 5 },
                    new Property { Name = "Weather Resistance", Value = "No", DescriptionId = 5 },
                    new Property { Name = "Model Year", Value = "2014", DescriptionId = 5 },
                    new Property { Name = "Minimum Shutter Speed", Value = "30 seconds", DescriptionId = 5 });

                context.SaveChanges();
            }
            #endregion
        }
    }
}
