# JustOrderIt - an e-commerce ASP.NET MVC Project

[![Build status](https://ci.appveyor.com/api/projects/status/3pebesusknx35m7n/branch/master?svg=true)](https://ci.appveyor.com/project/mpenchev86/JustOrderIt/branch/master)
[![codecov](https://codecov.io/gh/mpenchev86/JustOrderIt/branch/master/graph/badge.svg)](https://codecov.io/gh/mpenchev86/JustOrderIt)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](LICENSE)

JustOrderIt is a proof-of-concept e-commerce application based on ASP.NET MVC and Entity Framework with a MS SQL backend database. It was initially intended to be the Final project for Telerik Academy, season 2015-2016. The application solution is divided in several major components placed in their respective folders. Use the Contents shortcuts for more information.

**Contents:**

1. [Source code structure. Solution folders](#solution-folders)
 * [Common](#common-folder)
 * [Data](#data-folder)
 * [Services](#services-folder)
 * [Tests](#tests-folder)
 * [Web](#web-folder)
2. [Technologies and Frameworks used](#technologies-and-frameworks)
 * [DI container](#di-container)
 * [Object Mapping](#object-mapping)
 * [UI frameworks](#ui-frameworks)
 * [Task Scheduler](#task-scheduler)
 * [Image processing](#image-processing)
 * [Others](#others)
 

[//]: # (It contains presentation layers with frontend (public area) and backend(administration area) functionality.)

##Source code structure. Solution folders

###Common Folder

The common folder contains the GlobalConstants and Resources projects.

#### JustOrderIt.Common.GlobalConstants project

This project is a class library project contains classes which define constants commonly used throughout most or all projects. It helps keep the code clear of 'magic' strings that would be harder to maintain. Not all uses of magic strings are abstracted yet. The Html- and UrlHelpers, as well as the ActionResult generating methods still employ strings. In the future they'll be implemented in a fluent-api way through expression building frameworks, such as the ASP.NET MVC Lambda Expression Helpers library.(https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers)

#### JustOrderIt.Common.Resources project

It is intended for resources for internationalization. It's under development.

###Data Folder

In the data folder can be found projects implementing the data layer of the application - the JustOrderIt.Data.Models and JustOrderIt.Data.DbAccessConfig projects.

#### JustOrderIt.Data.Models project

The JustOrderIt.Data.Models project contains the application's business object types.

#### JustOrderIt.Data.DbAccessConfig project

This project, as the name suggests, is dedicated to managing the application's interaction with the database. It serves as a separation layer between the business objects and logic and the underlying data access operations. Entity Framework Code-First approach and Repository pattern are used to implement this task. The objects from the JustOrderIt.Data.Models project serve as models for the entities declared in the DbContext.
Also, this project contains classes related to migrations configuration, extending some default Entity Framework service types, and generation of sample initial database entries.

###Services Folder

Services folder accomodates class libraries providing various services. Depending on the type of functionality they implement, the services are placed in separate projects.

#### JustOrderIt.Services.Data project

This project is mainly concerned with decoupling the MVC layer of the application from a particular data access layer implementation. There's a data service interface for each data model type. A repostiry is injected by the DI container in the constructor of the class implementing a data service interface, and is used in the service's methods to read and write data to the database. The data service itself is injected, where necessary, in the Controller classes of the MVC tier so that CRUD database operations are available to the controller's actions.

#### JustOrderIt.Services.Identity project

This project contains custom implementations of the default managers for ASP.NET Identity objects (role store, user store, etc.)

#### JustOrderIt.Services.Logic project

The JustOrderIt.Services.Logic project contains worker classes concerned with various types of business logic and calculations, sometimes employing external libraries, that are more or less heavy enough to be extracted into a separate assembly, with the option for different implementations of a desired functionality. Such tasks include file system interaction, image processing, product search functionality, etc.

#### JustOrderIt.Services.Web project

This project defines types implementing logic that takes part in the processing of web requests (i.e., messaging services, memory cache, query string processing).

###Tests Folder

###Web Folder

##Technologies and Frameworks

###DI container

###Object Mapping

###UI frameworks

###Task Scheduler

###Image processing

###Others
