namespace JustOrderIt.Web.Areas.Public.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Media;
    using Infrastructure.Mapping;

    public class ImageForThumbnailProductViewModel : BasePublicViewModel<int>, IMapFrom<Image>
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }
    }
}