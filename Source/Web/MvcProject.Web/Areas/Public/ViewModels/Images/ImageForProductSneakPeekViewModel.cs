namespace MvcProject.Web.Areas.Public.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ImageForProductSneakPeekViewModel : IMapFrom<Image>
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }
    }
}