# JustOrderIt - an e-commerce ASP.NET MVC Project

[![Build status](https://ci.appveyor.com/api/projects/status/3pebesusknx35m7n/branch/master?svg=true)](https://ci.appveyor.com/project/mpenchev86/JustOrderIt/branch/master)
[![codecov](https://codecov.io/gh/mpenchev86/JustOrderIt/branch/master/graph/badge.svg)](https://codecov.io/gh/mpenchev86/JustOrderIt)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](LICENSE)

JustOrderIt is a proof-of-concept e-commerce application based on ASP.NET MVC and Entity Framework with a MS SQL backend database. It was initially intended to be the Final project for Telerik Academy, season 2015-2016. The application solution is divided in several major components placed in their respective folders. Use the Contents shortcuts for more information.

Contents: 

1. [Solution folders:](#solution-folders)
 *[Common](#common-folder)
 *[Data](#data-folder)
 *[Services](#services-folder)
 *[Tests](#tests-folder)
 *[Web](#web-folder)
2. [Technologies and Frameworks used:](#technologies-and-frameworks)

[//]: # (It contains presentation layers with frontend (public area) and backend(administration area) functionality.)

##Solution Folders

###Common Folder

The common folder contains the GlobalConstants and Resources projects.

#### GlobalConstants project

This project is a class library project contains classes which define constants commonly used throughout most or all projects. It helps keep the code clear of 'magic' strings that would be harder to maintain. Not all uses of magic strings are abstracted yet. The Html- and UrlHelpers, as well as the ActionResult generating methods still employ strings. In the future they'll be implemented in a fluent-api way through expression building frameworks, such as the ASP.NET MVC Lambda Expression Helpers library.(https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers)

###Data Folder

###Services Folder

###Tests Folder

###Web Folder

##Technologies and Frameworks
