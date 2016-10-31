﻿namespace MvcProject.Web.Areas.Public.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageForProductSneakPeekViewModel : BasePublicViewModel<int>, IMapFrom<Image>
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public bool IsMainImage { get; set; }
    }
}