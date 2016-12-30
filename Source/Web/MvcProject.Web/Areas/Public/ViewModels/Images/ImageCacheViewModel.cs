namespace MvcProject.Web.Areas.Public.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Models;
    using Data.Models.Media;
    using Infrastructure.Mapping;

    public class ImageCacheViewModel : BasePublicViewModel<int>, IMapFrom<Image>
    {
        public int? ProductId { get; set; }

        public bool IsMainImage { get; set; }

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public byte[] Content { get; set; }
    }
}