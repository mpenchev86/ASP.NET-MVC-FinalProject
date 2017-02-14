# JustOrderIt - an e-commerce ASP.NET MVC Project

[![Build status](https://ci.appveyor.com/api/projects/status/3pebesusknx35m7n/branch/master?svg=true)](https://ci.appveyor.com/project/mpenchev86/JustOrderIt/branch/master)
[![codecov](https://codecov.io/gh/mpenchev86/JustOrderIt/branch/master/graph/badge.svg)](https://codecov.io/gh/mpenchev86/JustOrderIt)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](LICENSE)

JustOrderIt is a proof-of-concept e-commerce application based on ASP.NET MVC and Entity Framework with a MS SQL backend database. It was initially intended to be the Final project for Telerik Academy, season 2015-2016. The application solution is divided in several major components placed in their respective folders. Use the Contents shortcuts for more information.

**Contents:**

1. [User guide](#user-guide)
2. [Source code structure. Solution folders](#source-code-structure-solution-folders)
 * [Common](#common-folder)
 * [Data](#data-folder)
 * [Services](#services-folder)
 * [Tests](#tests-folder)
 * [Web](#web-folder)
3. [Technologies and Frameworks used](#technologies-and-frameworks)
 * [DI container](#di-container)
 * [Object Mapping](#object-mapping)
 * [UI frameworks](#ui-frameworks)
 * [Task Scheduler](#task-scheduler)
 * [Image processing](#image-processing)
 * [Testing](#testing)
 * [Others](#others)
 
[//]: # (It contains presentation layers with frontend (public area) and backend(administration area) functionality.)

##User guide

When you run the application for the first time, it is configured to populate the database with some sample data, including products, images, users, categories, comments, etc.

The sample image files are located at *Solution\Web\JustOrderIt.Web\App_Data\SampleProductImages* folder. If they're moved or renamed before running the application for the first time, they won't be associated with their corresponding sample product and won't be displayed in the browser.

If you want to start with a clear database, you can open the Configuration.cs file (located in *Solution\Data\JustOrderIt.Data.DbAccessConfig* project, *Migrations* folder) and place in comments the lines of the Seed method associated with generating sample entries via a sample data generator.

The SampleDataGenerator creates an admin user (in admin role) with name **"admin"** and password **"123456"**. Initially, only the admin user is in administration role and thus only admin has access to the Administration area. Through the admin backend, any admin can assign another user to administration role.

SampleDataGenerator also creates regular application users with names and passwords following a pattern: name - "user\*\*", password - "0000\*\*", where \*\* represents a number between 01 and 99, which is the same for the name and password of a user. You can log on with one of these users or register a new one. 

[//]: # (The starting point of the application is the *JustOrderIt.Web* project located in the *Source\Web* folder.)

##Source code structure. Solution folders

###Common Folder

The common folder contains the *GlobalConstants* and *Resources* projects.

#### JustOrderIt.Common.GlobalConstants project

This project is a class library project contains classes which define constants commonly used throughout most or all projects. It helps keep the code clear of 'magic' strings that would be harder to maintain. Not all uses of magic strings are abstracted yet. The Html- and UrlHelpers, as well as the ActionResult generating methods still employ strings. In the future they'll be implemented in a fluent-api way through expression building frameworks, such as the **ASP.NET MVC Lambda Expression Helpers** library.(https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers)

#### JustOrderIt.Common.Resources project

It is intended for resources for internationalization. It's under development.

###Data Folder

In the data folder can be found projects implementing the data layer of the application - the *JustOrderIt.Data.Models* and *JustOrderIt.Data.DbAccessConfig* projects.

#### JustOrderIt.Data.Models project

The *JustOrderIt.Data.Models* project contains the application's business object types.

#### JustOrderIt.Data.DbAccessConfig project

This project, as the name suggests, is dedicated to managing the application's interaction with the database. It serves as a separation layer between the business objects and logic and the underlying data access operations. Entity Framework Code-First approach and Repository pattern are used to implement this task. The objects from the *JustOrderIt.Data.Models* project serve as models for the entities declared in the DbContext.
Also, this project contains classes related to migrations configuration, extending some default Entity Framework service types, and generation of sample initial database entries.

###Services Folder

Services folder accomodates class libraries providing various services. Depending on the type of functionality they implement, the services are placed in separate projects.

#### JustOrderIt.Services.Data project

This project is mainly concerned with decoupling the MVC layer of the application from a particular data access layer implementation. There's a data service interface for each data model type. A repostiry is injected by the DI container in the constructor of the class implementing a data service interface, and is used in the service's methods to read and write data to the database. The data service itself is injected, where necessary, in the Controller classes of the MVC tier so that CRUD database operations are available to the controller's actions.

#### JustOrderIt.Services.Identity project

This project contains custom implementations of the default managers for ASP.NET Identity objects (role store, user store, etc.)

#### JustOrderIt.Services.Logic project

The *JustOrderIt.Services.Logic* project contains worker classes concerned with various types of business logic and calculations, sometimes employing external libraries, that are more or less heavy enough to be extracted into a separate assembly, with the option for different implementations of a desired functionality. Such tasks include file system interaction, image processing, product search functionality, etc.

#### JustOrderIt.Services.Web project

This project defines types implementing logic that takes part in the processing of web requests (i.e., messaging services, memory cache, query string processing).

###Tests Folder

*Tests* Folder contains the assemblies with unit tests.

###Web Folder

This folder contains the MVC web application projects and the *JustOrderIt.Web.Infrastructure* project.

#### JustOrderIt.Web project

This project is the startup MVC project of the application. Since an e-commerce application is usually complex and contains multiple modules, most of the presentation is organized in separate areas - *Administration* and *Public*. What remains in the JustOrderIt.Web project are the *AccountController* and *ManageController*, commomly used css files, script files and views, and the initialization of some core services (DI container, object mapping, background worker, etc.).

#### JustOrderIt.Web.Administration project

The *JustOrderIt.Web.Administration* project implements the Administration presentation layer of the application. It is an MVC project which cannot be run by itself. It is located in a subfolder to the Areas folder of the main MVC project. It is accessible only to users with administrative rights (users assigned to the role "Admin"). There's a controller, processing CRUD requests for every business object which inherits the IAdministerable interface. The views makes extensive use **Telerik's Kendo UI** widgets, mainly the Grid widget.

#### JustOrderIt.Web.Public project

The *JustOrderIt.Web.Public* project is the presentation layer for the publicly accessible store. It is an MVC project which cannot be run by itself. It is located in a subfolder to the *Areas* folder of the main MVC project. The functionality of the project is incomplete and some pages load as a "under construction" page or are just missing. All users can interact with the public store, but some things are allowed to specific groups of users only (commenting, product rating, shopping cart actions, etc.). Some views here also use Kendo UI widgets, especially the ListView.

#### JustOrderIt.Web.Infrastructure project

*JustOrderIt.Web.Infrastructure* project is a class library that contains various helpers that bind the application together. These classes are essential or commonly used throughout the application. They're organized into folders named according to their function (Caching, Extensions, Filters, HtmlHelpers, Mapping, ModelBinders, etc.).

##Technologies and Frameworks

###DI container

Initially Ninject was used but then it was replaced by Autofac v3.5.2.

###Object Mapping

Automapper v5.2 is the technology used for object-to-object mapping. Mapping configuration Profiles are employed.

###UI frameworks

The UI of the application employs the Bootstrap, jQuery UI and **Telerik's Kendo UI** frontend frameworks extensively. Kendo UI Grid is used in the admin area and ListView is used on the Home and Search pages. Also Infragistics' **Ignite UI** Rating widget is used for displaying and changing product rating.

###Task Scheduler

The background tasks that repopulate the in-memory cache objects are run by the **Hangfire v1.6.8** - an open-source framework for background jobs. The jobs are persisted to SQL database (default) to ensure that tasks are not canceled or interrupted while in progress if the server drops or is restarted. Therefore, a *separate database* for Hangfire's data is created when the application runs for the first time. The connection string is placed in the main MVC project's web.config, alongside the JustOrderIt connection string. The database is named "HangFire-JustOrderIt" to specify in case many Hangfire databases exist on the server.

###Image processing

The product images are processed (resized and formatted) by the ImageProcessor v2.5.2 framework.

###Testing

The NUnit v3.6.0 and FluentMVCTesting v3.0.0 testing frameworks and Moq v4.5.30 mocking framework are used.

###Others
Castle.Core, Elmah.Mvc, Glimpse, Newtonsoft.Json, OpenCover, StyleCop.Analyzers, 