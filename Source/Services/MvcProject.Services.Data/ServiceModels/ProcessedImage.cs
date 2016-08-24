namespace MvcProject.Services.Data.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ProcessedImage : IMapTo<Image>, IMapFrom<Image>, IHaveCustomMappings
    {
        public const int ThumbnailImageWidth = 260;
        public const string ThumbnailImage = "tmbl";

        public const int HighResolutionWidth = 1360;
        public const string HighResolutionImage = "hi-res";

        public int Id { get; set; }

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
        }
    }
}
